using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;
using System.Collections;
using System.Reflection;

namespace LightFramework.Data
{
    /// <summary>
    /// 实体映射器类
    /// </summary>
    public static class EntityMapper
    {
        /// <summary>
        /// 获取实体对象。
        /// </summary>
        /// <typeparam name="TEntity">实体对象类型</typeparam>
        /// <param name="dr">DbDataReader</param>
        /// <param name="entity">实体对象</param>
        /// <returns>实体对象</returns>
        public static TEntity GetEntity<TEntity>(DbDataReader dr, TEntity entity)
        {
            return GetEntity<TEntity>(dr, entity, string.Empty);
        }

        /// <summary>
        /// 获取实体对象。
        /// </summary>
        /// <typeparam name="TEntity">实体对象类型</typeparam>
        /// <param name="dr">DbDataReader</param>
        /// <param name="entity">实体对象</param>
        /// <param name="entityName">实体名称</param>
        /// <returns>实体对象</returns>
        public static TEntity GetEntity<TEntity>(DbDataReader dr, TEntity entity, string entityName)
        {
            var metaTable = new MetaDataTable(typeof(TEntity), entityName);
            return GetEntity<TEntity>(dr, entity, metaTable);
        }

        /// <summary>
        /// 获取实体对象。
        /// </summary>
        /// <typeparam name="TEntity">实体对象类型</typeparam>
        /// <param name="dr">DbDataReader</param>
        /// <param name="entity">实体对象</param>
        /// <param name="metaDataTable">MetaDataTable对象</param>
        /// <returns>实体对象</returns>
        public static TEntity GetEntity<TEntity>(DbDataReader dr, TEntity entity, MetaDataTable metaDataTable)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                string colName = dr.GetName(i).ToLower();
                if (!metaDataTable.Columns.ContainsKey(colName)) continue;

                MetaDataColumn metaColumn = metaDataTable.Columns[colName];
                if (!dr.IsDBNull(i))
                {
                    object value = Convert.ChangeType(dr.GetValue(i),
                        IsNullableType(metaColumn.DataType) ?
                        metaColumn.DataType.GetGenericArguments()[0] : metaColumn.DataType);
                    metaColumn.Member.SetValue(entity, value, null);
                }
            }

            return entity;
        }

        /// <summary>
        /// 获取实体属性与值的对应表集合。
        /// </summary>
        /// <typeparam name="TEntity">实体对象类型</typeparam>
        /// <param name="entity">实体对象</param>
        /// <param name="columnNames">属性列名称</param>
        /// <returns>实体属性与值的对应表集合</returns>
        public static DataFieldMapTable GetMapTable<TEntity>(TEntity entity, params string[] columnNames)
        {
            return GetMapTable<TEntity>(entity,string.Empty,columnNames);
        }

        /// <summary>
        /// 获取实体属性与值的对应表集合。
        /// </summary>
        /// <typeparam name="TEntity">实体对象类型</typeparam>
        /// <param name="entity">实体对象</param>
        /// <param name="entityName">实体名称</param>
        /// <param name="columnNames">属性列名称</param>
        /// <returns>实体属性与值的对应表集合</returns>
        public static DataFieldMapTable GetMapTable<TEntity>(TEntity entity, string entityName, params string[] columnNames)
        {
            var metaDataTable = new MetaDataTable(typeof(TEntity), entityName);

            if (columnNames != null && columnNames.Length > 0)
                return GetMapTableByColumnNames<TEntity>(entity, metaDataTable, columnNames);

            DataFieldMapTable mapTable = new DataFieldMapTable(metaDataTable.Columns.Count);
            foreach (string key in metaDataTable.Columns.Keys)
            {
                var metaColumn = metaDataTable.Columns[key];
                if (metaColumn.Attribute.IsIgnored) continue;

                mapTable.Add(metaColumn.Name, GetColumnValue(metaColumn, entity));
            }

            return mapTable;
        }

        private static DataFieldMapTable GetMapTableByColumnNames<TEntity>(TEntity entity, MetaDataTable metaDataTable,
            params string[] columnNames)
        {
            DataFieldMapTable mapTable = new DataFieldMapTable(columnNames.Length);
            foreach (string columnName in columnNames)
            {
                string colName = columnName.Trim().ToLower();
                if (!metaDataTable.Columns.ContainsKey(colName) ||
                    metaDataTable.Columns[colName].Attribute.IsIdentity)
                {
                    continue;
                }

                var metaColumn = metaDataTable.Columns[colName];
                mapTable.Add(metaColumn.Name, GetColumnValue(metaColumn, entity));
            }

            return mapTable;
        }

        /// <summary>
        /// 判断当前类型为可为空类型
        /// </summary>
        /// <param name="type">Type对象</param>
        /// <returns>true|false</returns>
        private static bool IsNullableType(Type type)
        {
            return (((type != null) && type.IsGenericType) && (type.GetGenericTypeDefinition() == typeof(Nullable<>)));
        }

        /// <summary>
        /// 获取实体对象中对应的数据库列的值。
        /// </summary>
        /// <typeparam name="TEntity">实体对象类型</typeparam>
        /// <param name="metaColumn">列的元数据</param>
        /// <param name="entity">实体对象</param>
        /// <returns>实体对象中对应的数据库列的值</returns>
        private static object GetColumnValue<TEntity>(MetaDataColumn metaColumn,TEntity entity)
        {
            object value = metaColumn.Member.GetValue(entity, null);

            //如果当前列的数据类型可设置为空类型且值为null
            return (IsNullableType(metaColumn.DataType) && value == null) ? DBNull.Value : value;
        }
    }
}
