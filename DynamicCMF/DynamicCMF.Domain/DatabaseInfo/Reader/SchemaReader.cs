using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Castle.Core;
using Dapper;

namespace DynamicCMF.DatabaseInfo.Reader {
    [Singleton]
    internal class SchemaReader : ISchemaReader {
        private readonly string connectionString;
        private volatile IDictionary<(string, string), TableInfo> storedSchemas;

        public SchemaReader(string connectionString) {
            this.connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public TableInfo TryReadTableSchema(string schemaName, string tableName) {
            var key = (schemaName, tableName);
            TableInfo res;
            if (storedSchemas != null) {
                return storedSchemas.TryGetValue(key, out res)
                    ? res
                    : null;
            }

            storedSchemas = ReadSchemas();
            return storedSchemas.TryGetValue(key, out res)
                ? res
                : null;
        }

        private static T NullOr<T>(object dbValue) where T : class {
            return (dbValue is DBNull)
                ? null
                : (T) dbValue;
        }

        private static bool ParseBool(string data) {
            if (data == null) {
                throw new ArgumentNullException(nameof(data));
            }

            var trueValues = new[] { "TRUE", "YES", "1" };
            return (trueValues.Contains(data.Trim()));
        }

        private Dictionary<(string, string), TableInfo> ReadSchemas() {
            var schemas = new Dictionary<(string, string), TableInfo>();

            using (IDbConnection db = new SqlConnection(connectionString)) {
                var foreignKeys = db.Query<(string FromSchema, string FromTable, string FromColumn, string ToSchema, string ToTable, string ToColumn)>(
                                        @"
    select cu_from.TABLE_SCHEMA, cu_from.TABLE_NAME, cu_from.COLUMN_NAME, cu_to.TABLE_SCHEMA, cu_to.TABLE_NAME, cu_to.COLUMN_NAME
    from INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS rc 
    join INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE cu_from ON cu_from.CONSTRAINT_NAME = rc.CONSTRAINT_NAME AND cu_from.CONSTRAINT_SCHEMA=rc.CONSTRAINT_SCHEMA 
    join INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE cu_to ON cu_to.CONSTRAINT_NAME=rc.UNIQUE_CONSTRAINT_NAME AND cu_to.CONSTRAINT_SCHEMA=rc.UNIQUE_CONSTRAINT_SCHEMA")
                                    .Select(x => (new ColumnFullName(x.FromSchema, x.FromTable, x.FromColumn),
                                                new ForeignKeyInfo(x.ToSchema, x.ToTable, x.ToColumn)))
                                    .ToDictionary(el => el.Item1, el => el.Item2);

                var columns = db.Query<(string schemaName, string tableName, string columnName, string dataType, string isNullable, string defaultValue)>(@"
    select TABLE_SCHEMA, TABLE_NAME, COLUMN_NAME, DATA_TYPE, IS_NULLABLE, COLUMN_DEFAULT
    from INFORMATION_SCHEMA.COLUMNS").Select(r => {
                                                 var key = (r.schemaName, r.tableName);

                                                 foreignKeys.TryGetValue(new ColumnFullName(r.schemaName, r.tableName, r.columnName), out var fkInfo);
                                                 var columnInfo = new ColumnInfo(
                                                     r.columnName,
                                                     r.dataType,
                                                     ParseBool(r.isNullable),
                                                     NullOr<string>(r.defaultValue) ?? string.Empty,
                                                     fkInfo);

                                                 return new { First = key, Second = columnInfo };
                                             });

                foreach (var column in columns) {
                    var key = column.First;
                    var columnInfo = column.Second;

                    if (!schemas.TryGetValue(key, out var tableInfo)) {
                        tableInfo = new TableInfo(key.Item1, key.Item2);
                        schemas.Add(key, tableInfo);
                    }

                    tableInfo.AddColumn(columnInfo);
                }

                foreach (var fk in foreignKeys.Where(k => "Id".Equals(k.Value.ReferencedColumnName))) {
                    var key = (fk.Value.ReferencedSchemaName, fk.Value.ReferencedTableName);
                    if (schemas.TryGetValue(key, out var tableInfo)) {
                        tableInfo.AddReferencingTable(fk.Key.ToReferencingTableInfo());
                    }
                }
            }

            return schemas;
        }

        private class ColumnFullName {
            public ColumnFullName(string schemaName, string tableName, string columnName) {
                SchemaName = schemaName ?? throw new ArgumentNullException(nameof(schemaName));
                TableName = tableName ?? throw new ArgumentNullException(nameof(tableName));
                ColumnName = columnName ?? throw new ArgumentNullException(nameof(columnName));
            }

            private string SchemaName { get; }

            private string TableName { get; }

            private string ColumnName { get; }

            public ReferenceTableInfo ToReferencingTableInfo() {
                return new ReferenceTableInfo(SchemaName, TableName, ColumnName);
            }

            private bool Equals(ColumnFullName other) {
                return string.Equals(SchemaName, other.SchemaName) && string.Equals(TableName, other.TableName) && string.Equals(ColumnName, other.ColumnName);
            }

            public override bool Equals(object obj) {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != GetType()) return false;
                return Equals((ColumnFullName) obj);
            }

            public override int GetHashCode() {
                unchecked {
                    var hashCode = (SchemaName?.GetHashCode() ?? 0);
                    hashCode = (hashCode * 397) ^ (TableName?.GetHashCode() ?? 0);
                    hashCode = (hashCode * 397) ^ (ColumnName?.GetHashCode() ?? 0);
                    return hashCode;
                }
            }
        }
    }
}