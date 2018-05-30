using System;
using System.Collections.Generic;
using Castle.Core;

namespace DynamicCMF.DatabaseInfo {
    public class DatabaseSchemaModel {
        private readonly IDictionary<Type, TableInfo> tableByType = new Dictionary<Type, TableInfo>();
        private readonly IDictionary<Pair<string, string>, Type> typeByTable = new Dictionary<Pair<string, string>, Type>();
        private readonly IDictionary<Pair<string, string>, TableInfo> tableByName = new Dictionary<Pair<string, string>, TableInfo>();
    }
}