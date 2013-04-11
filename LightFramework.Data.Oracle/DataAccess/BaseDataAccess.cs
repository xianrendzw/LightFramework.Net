using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;

namespace LightFramework.Data.Oracle
{
    /// <summary>
    /// BaseDataAccess类提供对Oracle数据库公共数据访问的抽象基类。
    /// </summary>
    /// <typeparam name="T">通用类型</typeparam>
    public abstract class BaseDataAccess<T> : BaseSelect<T>, IBaseDataAccess<T>
    {
        #region 构造函数

        /// <summary>
        /// 指定表名的构造函数,数据库连接字符串的构造函数。
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="connectionString">当前表的数据库连接字符串</param>
        protected BaseDataAccess(string tableName, string connectionString)
            : base(tableName, connectionString)
        {
        }

        #endregion

        #region 通用的数据增加与更新操作方法

        /// <summary>
        /// 生成一条INSERT SQLExpression
        /// </summary>
        /// <param name="mapTable">mapTable:键[key]为字段名;值[value]为字段对应的值</param>
        /// <param name="targetTable">需要操作的目标表名称</param>
        /// <returns>返回生成的INSERT SQL语句</returns>
        protected SqlExpression GenerateInsertSqlExpression(DataFieldMapTable mapTable, string targetTable)
        {
            if (mapTable == null ||
                mapTable.Count == 0)
            {
                throw new ArgumentException("数据映射对象表为空或没有值", "mapTable");
            }

            StringBuilder fields = new StringBuilder();
            StringBuilder values = new StringBuilder();
            List<OracleParameter> sqlParameters = new List<OracleParameter>(mapTable.Count);
            foreach (string key in mapTable.Keys)
            {
                fields.AppendFormat("{0},", key);
                values.AppendFormat(":{0},", key);
                sqlParameters.Add(new OracleParameter(":" + key, mapTable[key]));
            }

            string commandText = string.Format("INSERT INTO {0} ({1}) VALUES ({2})",
                targetTable, fields.ToString().TrimEnd(','), values.ToString().TrimEnd(','));
            return new SqlExpression(commandText, sqlParameters.ToArray());
        }

        /// <summary>
        /// 生成一条INSERT SQL语句
        /// </summary>
        /// <param name="mapTable">mapTable:键[key]为字段名;值[value]为字段对应的值</param>
        /// <param name="targetTable">需要操作的目标表名称</param>
        /// <returns>返回生成的INSERT SQL语句</returns>
        protected string GenerateInsertSql(DataFieldMapTable mapTable, string targetTable)
        {
            if (mapTable == null ||
                mapTable.Count == 0)
            {
                throw new ArgumentException("数据映射对象表为空或没有值", "mapTable");
            }

            StringBuilder fields = new StringBuilder();
            StringBuilder values = new StringBuilder();
            foreach (string key in mapTable.Keys)
            {
                fields.AppendFormat("{0},", key);
                values.AppendFormat("'{0}',", mapTable[key]);
            }

            string commandText = string.Format("INSERT INTO {0} ({1}) VALUES ({2})",
                targetTable, fields.ToString().TrimEnd(','), values.ToString().TrimEnd(','));
            return commandText;
        }

        /// <summary>
        ///  生成一条Update SQLExpression
        /// </summary>
        /// <param name="condition">不带Where关键字的条件,如果条件字符串中含有参数标记,则必须设置parameterValues数组的值</param>
        /// <param name="mapTable">DataFieldMap:键[key]为字段名;值[value]为字段对应的值</param>
        /// <param name="targetTable">需要操作的目标表名称</param>
        /// <param name="parameterValues">SQL参数对应值的集合,如果条件字符串中含有参数标记,则必须设置该数组的值</param>
        /// <returns>更新记录的条数</returns>
        protected virtual SqlExpression GenerateUpdateSqlExpression(string condition, DataFieldMapTable mapTable, string targetTable, params object[] parameterValues)
        {
            if (mapTable == null ||
                mapTable.Count == 0)
            {
                throw new ArgumentException("数据映射对象表为空或没有值", "mapTable");
            }

            StringBuilder setValues = new StringBuilder();
            List<OracleParameter> sqlParameters = new List<OracleParameter>(mapTable.Count);
            foreach (string key in mapTable.Keys)
            {
                setValues.AppendFormat(string.Format("{0} = :{0},", key));
                sqlParameters.Add(new OracleParameter(":" + key, mapTable[key]));
            }

            if (parameterValues != null &&
                parameterValues.Length > 0)
            {
                foreach (OracleParameter parameter in this.FillParameters(parameterValues))
                {
                    sqlParameters.Add(parameter);
                }
            }

            string commandText = string.Format("UPDATE {0} SET {1} WHERE {2} ", targetTable,
                setValues.ToString().TrimEnd(','), condition);
            return new SqlExpression(commandText, sqlParameters.ToArray());
        }

        /// <summary>
        /// 生成一条Update SQL语句
        /// </summary>
        /// <param name="condition">不带Where关键字的条件,如果条件字符串中含有参数标记,则必须设置parameterValues数组的值</param>
        /// <param name="mapTable">DataFieldMap:键[key]为字段名;值[value]为字段对应的值</param>
        /// <param name="targetTable">需要操作的目标表名称</param>
        /// <param name="parameterValues">SQL参数对应值的集合,如果条件字符串中含有参数标记,则必须设置该数组的值</param>
        /// <returns>更新记录的条数</returns>
        protected virtual string GenerateUpdateSql(string condition, DataFieldMapTable mapTable, string targetTable, params object[] parameterValues)
        {
            if (mapTable == null ||
                mapTable.Count == 0)
            {
                throw new ArgumentException("数据映射对象表为空或没有值", "mapTable");
            }

            StringBuilder setValues = new StringBuilder();
            List<OracleParameter> sqlParameters = null;

            foreach (string key in mapTable.Keys)
            {
                setValues.AppendFormat(string.Format("{0} = '{1}',",key, mapTable[key]));
            }

            if (parameterValues != null &&
                parameterValues.Length > 0)
            {
                sqlParameters = new List<OracleParameter>(parameterValues.Length);
                foreach (OracleParameter parameter in this.FillParameters(parameterValues))
                {
                    sqlParameters.Add(parameter);
                }
            }

            string sqlCmd = string.Format("UPDATE {0} SET {1} WHERE {2} ", targetTable,
                setValues.ToString().TrimEnd(','), condition);
            return sqlCmd;
        }

        #endregion

        #region 子类必须实现的函数(用于更新或者插入)

        /// <summary>
        /// 将实体对象的属性值转化为DataFieldMapTable对应的键值(用于插入或者更新操作)
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="columnNames">该实体对象中对应的数据库表的列名</param>
        /// <returns>包含键值映射的DataFieldMapTable</returns>
        protected abstract DataFieldMapTable GetDataFieldMapTable(T entity, params string[] columnNames);

        #endregion

        #region IBaseInsert<T> 成员

        /// <summary>
        /// 向数据库中添加一条记录。
        /// </summary>
        /// <param name="entity">实体对象数据</param>
        /// <param name="columnNames">目标表列名集合</param>
        /// <returns>返回影响记录的行数,-1表示操作失败,大于-1表示成功</returns>
        public virtual int Insert(T entity, params string[] columnNames)
        {
            SqlExpression sqlExpr = this.GenerateInsertSqlExpression(this.GetDataFieldMapTable(entity, columnNames), this._tableName);
            return OracleHelper.ExecuteNonQuery(this._connectionString, CommandType.Text, sqlExpr.CommandText, sqlExpr.Parameters);
        }

        /// <summary>
        /// 向数据库中添加一条记录，并返回插入记录的ID值。
        /// </summary>
        /// <param name="entity">实体对象数据</param>
        /// <param name="columnNames">目标表列名集合</param>
        /// <returns>插入记录的数据库自增标识</returns>
        public virtual int InsertWithId(T entity, params string[] columnNames)
        {
            SqlExpression sqlExpr = this.GenerateInsertSqlExpression(this.GetDataFieldMapTable(entity, columnNames), this._tableName);
            string sqlCmd = sqlExpr.CommandText + ";SELECT SCOPE_IDEntity() AS ID";
            return Convert.ToInt32(OracleHelper.ExecuteScalar(this._connectionString, CommandType.Text, sqlCmd, sqlExpr.Parameters));
        }

        #endregion

        #region IBaseDelete<T> 成员

        /// <summary>
        /// 根据指定条件,从数据库中删除指定对象。
        /// </summary>
        /// <param name="condition">删除记录的条件语句,不需要带SQL语句的Where关键字,如果条件字符串中含有SQL参数标记(:),且必须写成如下格式:(:p0,:p1...)
        /// <example>e.g.:[UserName]=:p0 AND [Password] = :p1</example></param>
        /// <param name="parameterValues">SQL参数对应值的集合,如果条件字符串中含有参数标记,则必须设置该数组的值</param>
        /// <returns>返回影响记录的行数,-1表示操作失败,大于-1表示成功</returns>
        public virtual int DeleteWithCondition(string condition, params object[] parameterValues)
        {
            if (this.ContainWhere(condition))
                throw new ArgumentException("指定的条件,不要求带SQL语句Where关键字的条件", "condition");

            if (string.IsNullOrEmpty(condition))
                return -1;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM {0} WHERE {1}");
            string sqlCmd = string.Format(strSql.ToString(), this._tableName, condition);
            OracleParameter[] parameters = this.FillParameters(parameterValues);
            return OracleHelper.ExecuteNonQuery(this._connectionString, CommandType.Text, sqlCmd, parameters);
        }

        /// <summary>
        /// 清空表中的所有数据。
        /// </summary>
        public virtual int DeleteAll()
        {
            string sqlCmd = string.Format("DELETE FROM {0}", this._tableName);
            return OracleHelper.ExecuteNonQuery(this._connectionString, CommandType.Text, sqlCmd);
        }

        /// <summary>
        /// 清空表中的所有数据,且不记录数据库日志。
        /// </summary>
        public virtual void Truncate()
        {
            string sqlCmd = string.Format("TRUNCATE TABLE {0}", this._tableName);
            OracleHelper.ExecuteNonQuery(this._connectionString, CommandType.Text, sqlCmd);
        }

        #endregion

        #region IBaseUpdate<T> 成员

        /// <summary>
        /// 更新数据库表中的记录。
        /// </summary>
        /// <param name="entity">实体对象集合</param>
        /// <param name="condition">不带Where的更新条件,如果条件字符串中含有SQL参数标记(:),且必须写成如下格式:(:p0,:p1...)
        /// <example>e.g.:[UserName]=:p0 AND [Password] = :p1</example></param>
        /// <param name="parameterValues">SQL参数对应值的集合,如果条件字符串中含有参数标记,则必须设置该数组的值</param>
        /// <param name="columnNames">该实体对象中对应的数据库表的列名</param>
        /// <returns>返回影响记录的行数,-1表示操作失败,大于-1表示成功</returns>
        public virtual int UpdateWithCondition(T entity, string condition, object[] parameterValues, params string[] columnNames)
        {
            if (entity == null)
                throw new ArgumentNullException("dto", "未将对象引用到实例");

            if (this.ContainWhere(condition))
                throw new ArgumentException("指定的条件,不要求带SQL语句Where关键字的条件", "condition");

            SqlExpression sqlExpr = this.GenerateUpdateSqlExpression(condition,
                this.GetDataFieldMapTable(entity, columnNames), this._tableName, parameterValues);
            return OracleHelper.ExecuteNonQuery(this._connectionString, CommandType.Text, sqlExpr.CommandText, sqlExpr.Parameters);
        }

        #endregion

        #region 批量导入数据方法

        /// <summary>
        /// 使用多条sql语句用分号连接的方式向数据库中批量添加数据。
        /// </summary>
        /// <param name="batchEntities">记录集合</param>
        /// <returns>影响的行数</returns>
        public virtual int Insert(List<BatchEntity<T>> batchEntities)
        {
            if (batchEntities == null || batchEntities.Count == 0)
                throw new ArgumentNullException("batchEntities");

            StringBuilder batchSqlText = new StringBuilder();
            foreach (BatchEntity<T> batchEntity in batchEntities)
            {
                var dataFieldMapTable = this.GetDataFieldMapTable(batchEntity.Entity, batchEntity.ColumnNames);
                batchSqlText.AppendFormat("{0};", this.GenerateInsertSql(dataFieldMapTable, batchEntity.DestTableName));
            }

            return OracleHelper.ExecuteNonQuery(this._connectionString, CommandType.Text, batchSqlText.ToString());
        }

        /// <summary>
        /// 向数据库中批量添加记录
        /// </summary>
        /// <param name="entities">记录集合</param>
        /// <param name="method">批量添加方式</param>
        /// <param name="columnNames">目标表列名集合</param>
        /// <returns>影响的行数</returns>
        public virtual int Insert(List<T> entities, SqlInsertMethod method, params string[] columnNames)
        {
            if (method == SqlInsertMethod.BulkCopy)
                return this.InsertByBulkCopy(entities, columnNames);
            throw new NotImplementedException();
        }

        /// <summary>
        /// 使用insert into ... select ... from 语句从指定表中批量向当前表中添加数据。
        /// </summary>
        /// <param name="fromTableName">源数据表名</param>
        /// <param name="columnNames">目标表列名集合</param>
        /// <returns>影响的行数</returns>
        public virtual int Insert(string fromTableName, params string[] columnNames)
        {
            if (string.IsNullOrEmpty(fromTableName))
                throw new ArgumentNullException("fromTableName");

            string columns = this.GetColumns(columnNames);
            string insertColumns = columnNames.Equals("*") ? string.Empty : string.Format("({0})", columns);
            string sqlCmd = string.Format("insert into {0} {1} select {2} from {3}",
                this._tableName, insertColumns, columns, fromTableName);

            return OracleHelper.ExecuteNonQuery(this._connectionString, CommandType.Text, sqlCmd);
        }

        #endregion

        #region protected 成员

        /// <summary>
        /// 使用OracleBulkCopy方式向表中批量添加数据。
        /// </summary>
        /// <param name="entities">记录集合</param>
        /// <param name="columnNames">目标表列名集合</param>
        /// <returns>0表示成功，其他失败</returns>
        protected virtual int InsertByBulkCopy(List<T> entities, params string[] columnNames)
        {
            if (entities == null || entities.Count == 0)
                throw new ArgumentNullException("entites");

            MetaDataTable metaDataTable = new MetaDataTable(typeof(T), this._tableName);
            DataTable dataTable = this.GetDataTable(entities, metaDataTable, columnNames);

            using (OracleBulkCopy bulkCopy = new OracleBulkCopy(this._connectionString))
            {
                bulkCopy.DestinationTableName = this._tableName;
                bulkCopy.BatchSize = dataTable.Rows.Count;
                this.SetColumnMappings(bulkCopy.ColumnMappings, metaDataTable, columnNames);

                try
                {
                    bulkCopy.WriteToServer(dataTable);
                }
                catch (Exception ex)
                {
                    string message = string.Format("[SQL]:{0},[Exception]:{1}", "BulkCopy", ex.ToString());
                    throw new ApplicationException(message);
                }
            }

            return 0;
        }

        #endregion

        #region private 成员

        /// <summary>
        /// 创建实体集合对应的DataTable对象
        /// </summary>
        /// <param name="metaDataTable">MetaDataTable对象</param>
        /// <param name="entities">实体记录集合</param>
        /// <param name="columnNames">目标表列名集合</param>
        /// <returns>DataTable对象</returns>
        private DataTable GetDataTable(List<T> entities, MetaDataTable metaDataTable, params string[] columnNames)
        {
            DataTable dataTable = new DataTable(this._tableName);
            this.SetDataColumns(dataTable, metaDataTable, columnNames);

            foreach (T entity in entities)
            {
                DataRow dr = dataTable.NewRow();
                foreach (DataColumn dataColumn in dataTable.Columns)
                {
                    var metaDataColumn = metaDataTable.Columns[dataColumn.ColumnName];
                    dr[dataColumn.ColumnName] = metaDataColumn.Member.GetValue(entity, null);
                }
                dataTable.Rows.Add(dr);
            }
            return dataTable;
        }

        private void SetDataColumns(DataTable dataTable, MetaDataTable metaDataTable, params string[] columnNames)
        {
            if (columnNames == null || columnNames.Length == 0)
            {
                foreach (var kp in metaDataTable.Columns)
                    this.SetDataColumn(dataTable, kp.Value, kp.Key);
                return;
            }

            foreach (var columnName in columnNames)
            {
                string colName = columnName.Trim().ToLower();
                if (!metaDataTable.Columns.ContainsKey(colName)) continue;
                this.SetDataColumn(dataTable, metaDataTable.Columns[colName], colName);
            }
        }

        private void SetDataColumn(DataTable dataTable, MetaDataColumn metaDataColumn, string colName)
        {
            var dataColumn = new DataColumn(colName, metaDataColumn.DataType);
            dataTable.Columns.Add(dataColumn);
            if (metaDataColumn.Attribute.IsPrimaryKey)
                dataTable.PrimaryKey = new DataColumn[] { dataColumn };
        }

        /// <summary>
        /// 设置OracleBulkCopy中的ColumnMappings
        /// </summary>
        /// <param name="columnMappings">OracleBulkCopyColumnMappingCollection对象</param>
        /// <param name="metaDataTable">MetaDataTable对象</param>
        /// <param name="columnNames">目标表列名集合</param>
        private void SetColumnMappings(OracleBulkCopyColumnMappingCollection columnMappings, MetaDataTable metaDataTable, params string[] columnNames)
        {
            if (columnNames == null || columnNames.Length == 0)
            {
                foreach (var kp in metaDataTable.Columns)
                    columnMappings.Add(kp.Key, kp.Value.Name);
                return;
            }

            foreach (var columnName in columnNames)
            {
                string colName = columnName.Trim().ToLower();
                if (!metaDataTable.Columns.ContainsKey(colName)) continue;
                columnMappings.Add(colName, metaDataTable.Columns[colName].Name);
            }
        }

        #endregion
    }
}
