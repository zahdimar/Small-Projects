namespace DynamicCMF.DatabaseInfo {
    public class ReferenceTableInfo {
        public ReferenceTableInfo(string schema, string table, string column) {
            Schema = schema;
            Table = table;
            Column = column;
        }

        public string Schema { get; }

        public string Table { get; }

        public string Column { get; }

        protected bool Equals(ReferenceTableInfo other) => string.Equals(Schema, other.Schema) && string.Equals(Table, other.Table) && string.Equals(Column, other.Column);

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ReferenceTableInfo) obj);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = Schema?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (Table?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Column?.GetHashCode() ?? 0);
                return hashCode;
            }
        }
    }
}