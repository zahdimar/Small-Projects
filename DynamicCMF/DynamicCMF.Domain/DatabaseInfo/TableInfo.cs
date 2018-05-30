using System;
using System.Collections.Generic;

namespace DynamicCMF.DatabaseInfo {
    public class TableInfo {
        private readonly List<ColumnInfo> columns = new List<ColumnInfo>();
        private readonly List<ReferenceTableInfo> referencingTables = new List<ReferenceTableInfo>();

        public string TableName { get; }

        public string SchemaName { get; }

        public ColumnInfo[] Columns => columns.ToArray();

        public ReferenceTableInfo[] ReferencingTables => referencingTables.ToArray();

        internal void AddColumn(ColumnInfo column) {
            if (column == null) {
                throw new ArgumentNullException(nameof(column));
            }
            columns.Add(column);
        }

        internal void AddReferencingTable(ReferenceTableInfo referencedTableInfo) {
            if (referencedTableInfo == null) {
                throw new ArgumentNullException(nameof(referencedTableInfo));
            }
            referencingTables.Add(referencedTableInfo);
        }

        public TableInfo(string tableName, string schemaName) {
            TableName = tableName ?? throw new ArgumentNullException(nameof(tableName));
            SchemaName = schemaName ?? throw new ArgumentNullException(nameof(schemaName));
        }

        protected bool Equals(TableInfo other) {
            return string.Equals(TableName, other.TableName) && string.Equals(SchemaName, other.SchemaName);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TableInfo) obj);
        }

        public override int GetHashCode() {
            unchecked {
                return ((TableName?.GetHashCode() ?? 0) * 397) ^ (SchemaName?.GetHashCode() ?? 0);
            }
        }
    }
}