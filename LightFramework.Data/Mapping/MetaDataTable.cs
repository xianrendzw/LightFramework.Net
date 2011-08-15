using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LightFramework.Data
{
    /// <summary>
    /// 获取实体与其对应的数据库表或视图的相关元数据映射信息。
    /// </summary>
    public sealed class MetaDataTable
    {
        private readonly string _tableName;
        private readonly Type _entityType;
        private Dictionary<string, MetaDataColumn> _columns;

        public MetaDataTable(Type entityType, string tableName)
        {
            _entityType = entityType;
            _tableName = tableName;
            _columns = ExtractColumns(entityType);
        }

        private Dictionary<string, MetaDataColumn> ExtractColumns(Type entityType)
        {
            PropertyInfo[] properties = entityType.GetProperties();
            if (properties == null)
                return this._columns;

            this._columns = new Dictionary<string, MetaDataColumn>(properties.Length);
            foreach (PropertyInfo property in properties)
            {
                var attr = (ColumnAttribute)Attribute.GetCustomAttribute(property, typeof(ColumnAttribute));
                if (attr == null) continue;
                var metacolumn = new MetaDataColumn(property, attr);
                this._columns.Add(metacolumn.Name.ToLower(), metacolumn);
            }

            return this._columns;
        }

        /// <summary>
        /// Table name
        /// </summary>
        public string TableName
        {
            get { return this._tableName; }
        }

        /// <summary>
        /// Get entity type.
        /// </summary>
        public Type EntityType
        {
            get { return this._entityType; }
        }

        /// <summary>
        /// All columns of a table
        /// </summary>
        public Dictionary<string, MetaDataColumn> Columns
        {
            get { return _columns; }
        }
    }
}
