using System;

namespace DynamicCMF.DatabaseInfo.Reader {
    public interface ISchemaReader {
        TableInfo TryReadTableSchema(string schemaName, string tableName);
    }

    public interface IDbSchemaReaderFactory {
        ISchemaReader Create();

        void Release(ISchemaReader schemaReader);
    }

    public class ReleasableSchemaReaderDecorator : ISchemaReader, IDisposable {
        private readonly IDbSchemaReaderFactory factory;
        private readonly ISchemaReader wrappedReader;

        public ReleasableSchemaReaderDecorator(IDbSchemaReaderFactory factory) {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
            wrappedReader = factory.Create();
        }

        public TableInfo TryReadTableSchema(string schemaName, string tableName) {
            return wrappedReader.TryReadTableSchema(schemaName, tableName);
        }

        public void Dispose() {
            factory.Release(wrappedReader);
        }
    }
}