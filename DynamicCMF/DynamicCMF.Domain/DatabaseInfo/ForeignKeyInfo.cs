using System;

namespace DynamicCMF.DatabaseInfo {
    public class ForeignKeyInfo {
        public ForeignKeyInfo(string referencedSchemaName, string referencedTableName, string referencedColumnName) {
            ReferencedSchemaName = referencedSchemaName ?? throw new ArgumentNullException(nameof(referencedSchemaName));
            ReferencedTableName = referencedTableName ?? throw new ArgumentNullException(nameof(referencedTableName));
            ReferencedColumnName = referencedColumnName ?? throw new ArgumentNullException(nameof(referencedColumnName));
        }

        public string ReferencedSchemaName { get; }

        public string ReferencedTableName { get; }

        public string ReferencedColumnName { get; }
    }
}