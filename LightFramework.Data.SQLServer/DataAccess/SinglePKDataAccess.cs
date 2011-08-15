using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace LightFramework.Data.SQLServer
{
    /// <summary>
    /// SinglePKDataAccess类实现对SQLServer数据库中单主键表数据访问的基本操作，
    /// 并实现ISinglePKDataAccess接口。
    /// </summary>
    /// <typeparam name="T">通用类型</typeparam>
    public abstract class SinglePKDataAccess<T> : BaseDataAccess<T>, ISinglePKDataAccess<T>
    {
        #region 字段与属性

        /// <summary>
        /// 数据库的主键字段名。
        /// </summary>
        protected string _primaryKey;

        /// <summary>
        /// 数据库的主键字段名
        /// </summary>
        public string PrimaryKey
        {
            get
            {
                return this._primaryKey;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 指定表名以及单主键名称,数据库连接字符串对基类进行构造,此方法只针对单主键的表。
        /// </summary>
        /// <param name="tableName">表名</param>  
        /// <param name="primaryKey">表的单主键名称</param>
        /// <param name="connectionString">当前表的数据库连接字符串</param>
        protected SinglePKDataAccess(string tableName, string primaryKey, string connectionString)
            : base(tableName, connectionString)
        {
            this._primaryKey = primaryKey;
        }

        #endregion

        #region ISinglePKDelete<T> 成员

        /// <summary>
        /// 根据指定对象的ID,从数据库中删除指定对象(用于整型主键)
        /// </summary>
        /// <param name="keyValue">指定对象的ID值</param>
        /// <returns>返回影响记录的行数,-1表示操作失败,大于-1表示成功</returns>
        public virtual int Delete(int keyValue)
        {
            string condition = string.Format("[{0}] = {1}", this._primaryKey, keyValue);
            return this.DeleteWithCondition(condition);
        }

        /// <summary>
        /// 根据指定对象的ID,从数据库中删除指定对象(用于整型主键)
        /// </summary>
        /// <param name="keyValue">指定对象的ID值</param>
        /// <returns>返回影响记录的行数,-1表示操作失败,大于-1表示成功</returns>
        public virtual int Delete(string keyValue)
        {
            string condition = string.Format("[{0}] = @p0", this._primaryKey);
            object[] parameterValues = new object[] { keyValue };

            return this.DeleteWithCondition(condition, parameterValues);
        }

        /// <summary>
        /// 从数据库中删除一个或多个指定标识的记录，并以事务方式提交。
        /// </summary>
        /// <param name="keyValues">记录标识数组</param>
        /// <returns>返回影响记录的行数,-1表示操作失败,大于-1表示成功</returns>
        public virtual int Delete(params int[] keyValues)
        {
            string[] keys = new string[keyValues.Length];
            for (int i = 0; i < keyValues.Length; i++)
            {
                keys[i] = keyValues[i].ToString();
            }

            return this.Delete(keys);
        }

        /// <summary>
        /// 从数据库中删除一个或多个指定标识的记录，并以事务方式提交。
        /// </summary>
        /// <param name="keyValues">记录标识数组</param>
        /// <returns>返回影响记录的行数,-1表示操作失败,大于-1表示成功</returns>
        public virtual int Delete(params string[] keyValues)
        {
            return this.DeleteIn(string.Join(",", keyValues).TrimEnd(','));
        }

        /// <summary>
        /// 删除多条数据 条件为符合多个指定列的值 使用 IN 匹配
        /// </summary>
        /// <param name="keyValues">主键ID值 以,号分割</param>
        /// <returns>返回影响记录的行数,-1表示操作失败,大于-1表示成功</returns>
        public virtual int DeleteIn(string keyValues)
        {
            string condition = string.Format("{0} IN({1})", this._primaryKey, keyValues.TrimEnd(','));
            return this.DeleteWithCondition(condition);
        }

        #endregion

        #region ISinglePKUpdate<T> 成员

        /// <summary>
        /// 更新数据库表中指定主键值的记录。
        /// </summary>
        /// <param name="entity">实体对象集合</param>
        /// <param name="id">表中主键的值</param>
        /// <param name="columnNames">该实体对象中对应的数据库表的列名</param>
        /// <returns>返回影响记录的行数,-1表示操作失败,大于-1表示成功</returns>
        public virtual int Update(T entity, int id, params string[] columnNames)
        {
            string condition = String.Format("{0} = {1}", this._primaryKey, id);
            return this.UpdateWithCondition(entity, condition, null, columnNames);
        }

        /// <summary>
        /// 更新数据库表中指定主键值的记录。
        /// </summary>
        /// <param name="entity">实体对象集合</param>
        /// <param name="id">表中主键的值</param>
        /// <param name="columnNames">该实体对象中对应的数据库表的列名</param>
        /// <returns>返回影响记录的行数,-1表示操作失败,大于-1表示成功</returns>
        public virtual int Update(T entity, string id, params string[] columnNames)
        {
            string condition = string.Format("[{0}] = @p0", this._primaryKey);
            object[] parameterValues = new object[] { id };

            return this.UpdateWithCondition(entity, condition, parameterValues, columnNames);
        }

        /// <summary>
        /// 更新一条数据 条件为符合指定列的值 使用 IN 匹配
        /// 提示：In的条件仅用于主键列
        /// </summary>
        /// <param name="entity">更新的数据对象</param>
        /// <param name="keyValues">主键ID值 以,号分割</param>
        /// <param name="columnNames">更新的列名称(对应数据对象实体)</param>
        /// <returns>返回影响记录的行数,-1表示操作失败,大于-1表示成功</returns>
        public virtual int UpdateIn(T entity, string keyValues, params string[] columnNames)
        {
            string condition = String.Format("{0} IN({1})", this._primaryKey, keyValues.TrimEnd(','));
            return this.UpdateWithCondition(entity, condition, null, columnNames);
        }

        #endregion

        #region ISinglePKSelect<T> 成员

        /// <summary>
        /// 查询数据库,判断指定标识的记录是否存在。
        /// </summary>
        /// <param name="keyValues">指定的记录的唯一标识</param>
        /// <returns>存在则返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool IsExistKey(int keyValue)
        {
            string condition = string.Format("[{0}] = {1}", this._primaryKey, keyValue);
            return this.IsExistWithCondition(condition);
        }

        /// <summary>
        /// 查询数据库,判断指定标识的记录是否存在。
        /// </summary>
        /// <param name="keyValues">指定的记录的唯一标识</param>
        /// <returns>存在则返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool IsExistKey(params int[] keyValues)
        {
            string[] keys = new string[keyValues.Length];
            for (int i = 0; i < keyValues.Length; i++)
            {
                keys[i] = keyValues[i].ToString();
            }

            return this.IsExistKey(keys);
        }

        /// <summary>
        /// 查询数据库,判断指定标识的记录是否存在。
        /// </summary>
        /// <param name="keyValues">指定的记录的唯一标识</param>
        /// <returns>存在则返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool IsExistKey(string keyValue)
        {
            string condition = string.Format("[{0}] = @p0", this._primaryKey);
            object[] parameterValues = new object[] { keyValue };

            return this.IsExistWithCondition(condition, parameterValues);
        }

        /// <summary>
        /// 查询数据库,判断指定标识的记录是否存在。
        /// </summary>
        /// <param name="keyValues">指定的记录的唯一标识</param>
        /// <returns>存在则返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool IsExistKey(params string[] keyValues)
        {
            return this.IsExistKey(string.Join(",", keyValues));
        }

        /// <summary>
        /// 查询数据库,判断指定标识的记录是否存在，使用 IN 匹配
        /// </summary>
        /// <param name="keyValues">主键ID值 以,号分割</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool IsExistKeyIn(string keyValues)
        {
            string condition = string.Format("{0} IN({1})", this._primaryKey, keyValues.Trim(','));
            return this.IsExistWithCondition(condition);
        }

        /// <summary>
        /// 从数据库中获取表中指定标识的实体对象集合(返回值需要判断是否为null)。
        /// </summary>
        /// <param name="keyValue">标识值</param>
        /// <param name="columnNames">该实体对象中对应的数据库表的列名</param>
        /// <returns>返回查询结果数据</returns>
        public virtual T Select(int keyValue, params string[] columnNames)
        {
            string condition = string.Format("WHERE [{0}] = {1}", this._primaryKey,keyValue);
            return this.SelectOne(condition, null, columnNames);
        }

        /// <summary>
        /// 从数据库中获取指定标识的实体对象集合仅支持主键ID查询(返回值需要判断是否为null)。
        /// </summary>
        /// <param name="keyValue">标识值 (仅支持主键ID查询)</param>
        /// <param name="columnNames">该实体对象中对应的数据库表的列名</param>
        /// <returns>返回查询结果数据</returns>
        public virtual T Select(string keyValue, params string[] columnNames)
        {
            string condition = string.Format("WHERE [{0}] = @p0", this._primaryKey);
            return this.SelectOne(condition, this.GetParamerterValues(keyValue), columnNames);
        }

        /// <summary>
        /// 从数据库中按表的ID列值分页大小进行获取实体对象集合(返回值不需要判断是否为null),默认返回按表中的标识字段降序排序的集合。
        /// </summary>
        /// <param name="pageSize">分页大小，即分页显示多少条记录</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="condition">指定的条件,要求带SQL语句Where关键字的条件,如果不带Where关键字则条件无效,如果条件字符串中含有SQL参数标记(@),且必须写成如下格式:(@p0,@p1...)
        /// <example>e.g.:[UserName]=@p0 AND [Password] = @p1</example></param>
        /// <param name="orderByColumnName">排序字段名称，不要求带ORDER BY关键字,只要指定排序字段名称即可</param>
        /// <param name="sortType">SQL语句排序类型</param>
        /// <param name="parameterValues">SQL参数对应值的集合,如果条件字符串中含有参数标记,则必须设置该数组的值</param>
        /// <param name="columnNames">该实体对象中对应的数据库表的列名</param>
        /// <returns>当前分页的所有取实体对象集合</returns>
        public virtual PageData<T> SelectWithPageSizeByIdentity(int pageSize, int pageIndex, string condition, string orderByColumnName, SortTypeEnum sortType, object[] parameterValues, params string[] columnNames)
        {
            if (!this.ContainWhere(condition))
                throw new ArgumentException("指定的条件,要求带SQL语句Where关键字的条件", "condition");

            if (pageSize < 1) pageSize = 10;
            if (pageIndex < 0) pageIndex = 0;
            //临时表大小
            int tempTableSize = pageIndex * pageSize;
            //设置SQL语句的MAX函数或MIN函数返回空值时的替换值
            int isNullValue = 0;

            //设置第五个格式化参数为去掉Where字符串
            string param5 = string.IsNullOrEmpty(condition) ?
                 string.Empty :
                 string.Format("AND {0}", condition.Trim().Substring(5));

            string orderBy = string.IsNullOrEmpty(orderByColumnName) ?
               string.Format("[{0}] {1}", this._primaryKey, sortType.ToString()) :
               string.Format("[{0}] {1}", orderByColumnName, sortType.ToString());

            //获取筛选列
            string columns = this.GetColumns(columnNames);
            columns = string.Format("TOP {0} {1}", pageSize, columns);

            //分页获取数据库记录集的SQL语句
            StringBuilder sqlFormat = new StringBuilder();
            if (sortType == SortTypeEnum.ASC)
            {
                sqlFormat.Append("SELECT {0} ");
                sqlFormat.Append("FROM [{1}] ");
                sqlFormat.Append("WHERE ([{2}] > ");
                sqlFormat.Append("(SELECT ISNULL(MAX([{2}]),{7}) ");
                sqlFormat.Append("FROM (SELECT TOP {3} [{2}] ");
                sqlFormat.Append("FROM [{1}] {4} ");
                sqlFormat.Append("ORDER BY {6}) AS TempTable) {5} ) ");
                sqlFormat.Append("ORDER BY {6}");
            }
            else
            {
                sqlFormat.Append("SELECT {0} ");
                sqlFormat.Append("FROM [{1}] ");
                sqlFormat.Append("WHERE ([{2}] < ");
                sqlFormat.Append("(SELECT ISNULL(MIN([{2}]),{7}) ");
                sqlFormat.Append("FROM (SELECT TOP {3} [{2}] ");
                sqlFormat.Append("FROM [{1}] {4} ");
                sqlFormat.Append("ORDER BY {6}) AS TempTable) {5} ) ");
                sqlFormat.Append("ORDER BY {6}");

                //设置为最大整数值
                isNullValue = int.MaxValue;
            }

            string sqlCmd = string.Format(sqlFormat.ToString(), columns, this._tableName,
                this._primaryKey, tempTableSize, condition, param5, orderBy, isNullValue);
            return this.GetPageData(condition, parameterValues, columnNames, sqlCmd, CommandType.Text);
        }

        /// <summary>
        /// 获取数据库中该对象的最大ID值
        /// </summary>
        /// <returns>最大ID值</returns>
        public virtual int GetMaxID()
        {
            return this.GetMaxValue(this._primaryKey, 10, string.Empty);
        }

        /// <summary>
        /// 获取数据库中该对象指定属性的最大值(没有记录的时候返回0)。
        /// </summary>
        /// <param name="fieldName">表中的字段(列)名称,字段的值必需是整型数据</param>
        /// <param name="fromBase">从(2,8,10,16)进制的整型转换成10进制</param>
        /// <param name="condition">要求带SQL语句Where关键字的条件,如果条件字符串中含有SQL参数标记(@),且必须写成如下格式:(@p0,@p1...)
        /// <example>e.g.:[UserName]=@p0 AND [Password] = @p1</example></param>
        /// <param name="parameterValues">SQL参数对应值的集合,如果条件字符串中含有参数标记,则必须设置该数组的值</param>
        /// <returns>指定属性的最大值,没有记录的时候为0</returns>
        public virtual int GetMaxValue(string fieldName, int fromBase, string condition, params object[] parameterValues)
        {
            if (!this.ContainWhere(condition))
                throw new ArgumentException("指定的条件,要求带SQL语句Where关键字的条件", "condition");

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT MAX({0}) AS MaxValue FROM [{1}] {2}");

            string sqlCmd = string.Format(strSql.ToString(), fieldName, this._tableName, condition);
            SqlParameter[] parameters = this.FillParameters(parameterValues);
            object obj = SqlHelper.ExecuteScalar(this._connectionString, CommandType.Text, sqlCmd, parameters);
            return Convert.IsDBNull(obj) ? 0 : Convert.ToInt32(obj.ToString(), fromBase);
        }

        #endregion
    }
}