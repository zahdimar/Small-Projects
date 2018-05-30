using System;

namespace DynamicCMF.DatabaseInfo {
    public class ColumnInfo {
        public ColumnInfo(string name, string dataType, bool isNullable, string defaultValue)
            : this(name, dataType, isNullable, defaultValue, null) { }

        public ColumnInfo(string name, string dataType, bool isNullable, string defaultValue, ForeignKeyInfo foreignKeyInfo) {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            DataType = dataType ?? throw new ArgumentNullException(nameof(dataType));
            IsNullable = isNullable;
            DefaultValue = defaultValue ?? throw new ArgumentNullException(nameof(defaultValue));

            ForeignKeyInfo = foreignKeyInfo;
        }

        public string Name { get; }

        public string DataType { get; }

        public bool IsNullable { get; }

        public string DefaultValue { get; }

        public ForeignKeyInfo ForeignKeyInfo { get; }

        public bool HasForeignKey => ForeignKeyInfo != null;
    }
}