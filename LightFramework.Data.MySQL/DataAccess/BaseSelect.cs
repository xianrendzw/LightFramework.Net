using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;

namespace LightFramework.Data.MySQL
{
    /// <summary>
    /// BaseSelect类提供对MySQL数据库一些基本的查询操作的抽象基类。
    /// </summary>
    /// <typeparam name="T">通用类型</typeparam>
    public abstract class BaseSelect<T> : IBaseSelect<T>
    {
        #region 构造函数

        /// <summary>
        /// 指定表名的构造函数,数据库连接字符串的构造函数。
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="connectionString">当前表的数据库连接字符串</param>
        protected BaseSelect(string tableName, string connectionString)
        {
            this._tableName = tableName;
            this._connectionString = connectionString;
        }

        #endregion

        #region 公共属性

        /// <summary>
        /// 需要初始化的对象表名。
        /// </summary>
        protected string _tableName = string.Empty;

        /// <summary>
        /// 获取或设置初始化的对象表名
        /// </summary>
        public string TableName
        {
            get
            {
                return this._tableName;
            }
            set
            {
                this._tableName = value;
            }
        }

        /// <summary>
        /// 数据库连接字符串。
        /// </summary>
        protected string _connectionString = string.Empty;

        /// <summary>
        /// 获取或设置当前的数据库连接字符串。
        /// </summary>
        public string ConnectionString
        {
            get { return this._connectionString; }
            set { this._connectionString = value; }
        }

        #endregion

        #region 子类必须实现的函数(用于读取数据)

        /// <summary>
        /// 将DataReader的属性值转化为实体对象的属性值，返回实体对象
        /// </summary>
        /// <param name="dr">有效的DataReader对象</param>
        /// <param name="metaDataTable">MetaDataTable对象</param>
        /// <param name="columnNames">该实体对象中对应的数据库表的列名</param>
        /// <returns>实体对象实例</returns>
        protected abstract T DataReaderToEntity(MySqlDataReader dr, MetaDataTable metaDataTable, params string[] columnNames);

        #endregion

        #region IBaseSelect<T> 成员

        /// <summary>
        /// 查询数据库,判断指定条件的记录是否存在。
        /// </summary>
        /// <param name="condition">指定的条件,不需要带SQL语句的Where关键字,如果条件字符串中含有SQL参数标记(@),且必须写成如下格式:(@p0,@p1...)
        /// <example>e.g.:[UserName]=@p0 AND [Password] = @p1</example></param>
        /// <param name="parameterValues">SQL参数对应值的集合,如果条件字符串中含有参数标记,则必须设置该数组的值</param>
        /// <returns>存在则返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool IsExistWithCondition(string condition, params object[] parameterValues)
        {
            if (this.ContainWhere(condition))
                throw new ArgumentException("指定的条件,不要求带SQL语句Where关键字的条件", "condition");

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(0) FROM {0} ");
            strSql.Append("WHERE {1} ");
            string sqlCmd = string.Format(strSql.ToString(), this._tableName, condition);
            MySqlParameter[] parameters = this.FillParameters(parameterValues);

            object result = MySqlHelper.ExecuteScalar(this._connectionString, sqlCmd, parameters);
            return (Convert.ToInt32(result) > 0 ? true : false);
        }

        /// <summary>
        /// 从数据库中获取表中所有的实体对象集合。
        /// </summary>
        /// <returns>表中所有的实体对象集合</returns>
        public virtual List<T> Select()
        {
            return this.SelectWithCondition(string.Empty, null);
        }

        /// <summary>
        /// 从数据库中获取所有的实体对象集合(返回值不需判断是否为null)。
        /// </summary>
        /// <param name="columnNames">该实体对象中对应的数据库表的列名</param>
        /// <returns>实体对象集合</returns>
        public virtual List<T> Select(params string[] columnNames)
        {
            return this.SelectWithCondition(string.Empty, null, columnNames);
        }

        /// <summary>
        /// 从数据库中获取满足指定条件的实体对象集合(返回值不需判断是否为null)。
        /// </summary>
        /// <param name="condition">指定的条件,要求带SQL语句Where关键字的条件,如果条件字符串中含有SQL参数标记(@),且必须写成如下格式:(@p0,@p1...)
        /// <example>e.g.:[UserName]=@p0 AND [Password] = @p1</example></param>
        /// <param name="columnNames">该实体对象中对应的数据库表的列名</param>
        /// <returns>指定条件的表中的实体对象集合</returns>
        public virtual List<T> SelectWithCondition(string condition, params string[] columnNames)
        {
            return this.SelectWithCondition(condition, null, columnNames);
        }

        /// <summary>
        /// 从数据库中获取所有的实体对象集合(返回值不需判断是否为null)。
        /// </summary>
        /// <param name="orderByColumnName">排序字段名称，不要求带ORDER BY关键字,只要指定排序字段名称即可</param>
        /// <param name="sortType">SQL语句排序类型</param>
        /// <param name="columnNames">该实体对象中对应的数据库表的列名</param>
        /// <returns>实体对象集合</returns>
        public virtual List<T> Select(string orderByColumnName, SortTypeEnum sortType, params string[] columnNames)
        {
            return this.SelectWithCondition(string.Empty, orderByColumnName, sortType, null, columnNames);
        }

        /// <summary>
        /// 从数据库中获取满足指定条件的实体对象集合(返回值不需判断是否为null)。
        /// </summary>
        /// <param name="condition">指定的条件,要求带SQL语句Where关键字的条件,如果条件字符串中含有SQL参数标记(@),且必须写成如下格式:(@p0,@p1...)
        /// <example>e.g.:[UserName]=@p0 AND [Password] = @p1</example></param>
        /// <param name="parameterValues">SQL参数对应值的集合,如果条件字符串中含有参数标记,则必须设置该数组的值</param>
        /// <param name="columnNames">该实体对象中对应的数据库表的列名</param>
        /// <returns>实体对象集合</returns>
        public virtual List<T> SelectWithCondition(string condition, object[] parameterValues, params string[] columnNames)
        {
            return this.SelectWithCondition(condition, string.Empty, SortTypeEnum.ASC, parameterValues, columnNames);
        }

        /// <summary>
        /// 从数据库中获取满足指定条件的实体对象集合(返回值不需判断是否为null)。
        /// </summary>
        /// <param name="condition">指定的条件,要求带SQL语句Where关键字的条件,如果条件字符串中含有SQL参数标记(@),且必须写成如下格式:(@p0,@p1...)
        /// <param name="orderByColumnName">排序字段名称，不要求带ORDER BY关键字,只要指定排序字段名称即可</param>
        /// <param name="sortType">SQL语句排序类型</param>
        /// <example>e.g.:[UserName]=@p0 AND [Password] = @p1</example></param>
        /// <param name="parameterValues">SQL参数对应值的集合,如果条件字符串中含有参数标记,则必须设置该数组的值</param>
        /// <param name="columnNames">该实体对象中对应的数据库表的列名</param>
        /// <returns>实体对象集合</returns>
        public virtual List<T> SelectWithCondition(string condition, string orderByColumnName, SortTypeEnum sortType, object[] parameterValues, params string[] columnNames)
        {
            if (!this.ContainWhere(condition))
                throw new ArgumentException("指定的条件,要求带SQL语句Where关键字的条件", "condition");

            //获取筛选列
            string columns = this.GetColumns(columnNames);
            string sqlCmd = string.Format("SELECT {0} FROM {1} {2} {3}", columns, this._tableName, condition,
                string.IsNullOrEmpty(orderByColumnName) ? string.Empty : string.Format("ORDER BY {0} {1}", orderByColumnName, sortType.ToString()));

            return this.GetEntities(sqlCmd, parameterValues, columnNames);
        }

        /// <summary>
        /// 从数据库中获取满足指定条件的前N项实体对象集合(返回值不需判断是否为null)。
        /// </summary>
        /// <param name="topN">表中的前N条记录</param>
        /// <param name="condition">指定的条件,要求带SQL语句Where关键字的条件,如果条件字符串中含有SQL参数标记(@),且必须写成如下格式:(@p0,@p1...)
        /// <example>e.g.:[UserName]=@p0 AND [Password] = @p1</example></param>
        /// <param name="parameterValues">SQL参数对应值的集合,如果条件字符串中含有参数标记,则必须设置该数组的值</param>
        /// <param name="columnNames">该实体对象中对应的数据库表的列名</param>
        /// <returns>实体对象集合</returns>
        public virtual List<T> SelectTopN(int topN, string condition, object[] parameterValues, params string[] columnNames)
        {
            return this.SelectTopN(topN, condition, string.Empty, SortTypeEnum.ASC, parameterValues, columnNames);
        }

        /// <summary>
        /// 从数据库中获取满足指定条件的前N项实体对象集合(返回值不需判断是否为null)。
        /// </summary>
        /// <param name="topN">表中的前N条记录</param>
        /// <param name="condition">指定的条件,要求带SQL语句Where关键字的条件,如果条件字符串中含有SQL参数标记(@),且必须写成如下格式:(@p0,@p1...)
        /// <param name="orderByColumnName">排序字段名称，不要求带ORDER BY关键字,只要指定排序字段名称即可</param>
        /// <param name="sortType">SQL语句排序类型</param>
        /// <example>e.g.:[UserName]=@p0 AND [Password] = @p1</example></param>
        /// <param name="parameterValues">SQL参数对应值的集合,如果条件字符串中含有参数标记,则必须设置该数组的值</param>
        /// <param name="columnNames">该实体对象中对应的数据库表的列名</param>
        /// <returns>实体对象集合</returns>
        public virtual List<T> SelectTopN(int topN, string condition, string orderByColumnName, SortTypeEnum sortType, object[] parameterValues, params string[] columnNames)
        {
            //如果topN的值不合法.
            if (topN < 1)
            {
                return this.SelectWithCondition(condition, columnNames);
            }

            if (!this.ContainWhere(condition))
                throw new ArgumentException("指定的条件,要求带SQL语句Where关键字的条件", "condition");

            //获取筛选列
            string columns = this.GetColumns(columnNames);
            string sqlCmd = string.Format("SELECT {0} FROM {1} {2} {3} LIMIT {4}", columns, this._tableName, condition,
                string.IsNullOrEmpty(orderByColumnName) ? string.Empty : string.Format("ORDER BY {0} {1} ", orderByColumnName, sortType.ToString()),
                topN);

            return this.GetEntities(sqlCmd, parameterValues, columnNames);
        }

        /// <summary>
        /// 从数据库中获取一条满足指定条件的实体对象集合(返回值需要判断是否为null)。
        /// </summary>
        /// <param name="condition">指定的条件,要求带SQL语句Where关键字的条件,如果条件字符串中含有SQL参数标记(@),且必须写成如下格式:(@p0,@p1...)
        /// <example>e.g.:[UserName]=@p0 AND [Password] = @p1</example></param>
        /// <param name="parameterValues">SQL参数对应值的集合,如果条件字符串中含有参数标记,则必须设置该数组的值</param>
        /// <param name="columnNames">该实体对象中对应的数据库表的列名</param>
        /// <returns>实体对象或null</returns>
        public T SelectOne(string condition, object[] parameterValues, params string[] columnNames)
        {
            List<T> list = this.SelectWithCondition(condition, parameterValues, columnNames);
            return (list == null || list.Count == 0) ? default(T) : list[0];
        }

        /// <summary>
        /// 从数据库中按分页大小进行获取实体对象集合(返回值不需判断是否为null),默认返回按表中的字段降序排序的集合,
        /// notinColumnName(指定的筛选列名称),该参数为必须指定，且为当前表中合法的列名称。如果未指定列名称，该方法将返回null。
        /// </summary>
        /// <param name="pageSize">分页大小，即分页显示多少条记录</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="condition">指定的条件,要求带SQL语句Where关键字的条件,如果不带Where关键字则条件无效,如果条件字符串中含有SQL参数标记(@),且必须写成如下格式:(@p0,@p1...)
        /// <example>e.g.:[UserName]=@p0 AND [Password] = @p1</example></param>
        /// <param name="notinColumnName">指定的筛选列名称,该参数为必须指定，且为当前表中合法的列名称。如果未指定列名称，该方法将返回null</param>
        /// <param name="orderByColumnName">排序字段名称，不要求带ORDER BY关键字,只要指定排序字段名称即可</param>
        /// <param name="sortType">SQL语句排序类型</param>
        /// <param name="parameterValues">SQL参数对应值的集合,如果条件字符串中含有参数标记,则必须设置该数组的值</param>
        /// <param name="columnNames">该实体对象中对应的数据库表的列名</param>
        /// <returns>当前分页的所有取实体对象集合</returns>
        public virtual PageData<T> SelectWithPageSizeByNotIn(int pageSize, int pageIndex, string condition, string notinColumnName, string orderByColumnName, SortTypeEnum sortType, object[] parameterValues, params string[] columnNames)
        {
            if (string.IsNullOrEmpty(notinColumnName))
                throw new ArgumentException("请指定的筛选列名称,该参数为必须指定，且为当前表中合法的列名称", "notinColumnName");

            if (!this.ContainWhere(condition))
                throw new ArgumentException("指定的条件,要求带SQL语句Where关键字的条件", "condition");

            //设置默认分页参数
            if (pageSize < 1) pageSize = 10;
            if (pageIndex < 0) pageIndex = 0;

            //临时表大小
            int tempTableSize = pageIndex * pageSize;

            //获取筛选列
            string columns = this.GetColumns(columnNames);
            //设置第五个格式化参数为去掉Where字符串
            string param5 = string.IsNullOrEmpty(condition) ?
                string.Empty :
                string.Format("AND {0}", condition.Trim().Substring(5));
            string orderBy = string.IsNullOrEmpty(orderByColumnName) ?
                string.Format("{0} {1}", notinColumnName, sortType.ToString()) :
                string.Format("{0} {1}", orderByColumnName, sortType.ToString());

            //分页获取数据库记录集的SQL语句
            StringBuilder sqlFormat = new StringBuilder();
            sqlFormat.Append("SELECT {0} ");
            sqlFormat.Append("FROM {1} ");
            sqlFormat.Append("WHERE {2} NOT IN ");
            sqlFormat.Append("(SELECT {2} FROM {1} {4} ORDER BY {6} LIMIT {3}) ");
            sqlFormat.Append("{5} ORDER BY {6} LIMIT {7}");

            string sqlCmd = string.Format(sqlFormat.ToString(), columns, this._tableName,
                notinColumnName, tempTableSize, condition, param5, orderBy, pageSize);
            return this.GetPageData(condition, parameterValues, columnNames, sqlCmd);
        }

        /// <summary>
        /// 利用数据库表的RowId属性对数据进行分页查询的方法(返回值不需判断是否为null)。
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
        public virtual PageData<T> SelectWithPageSizeByRowId(int pageSize, int pageIndex, string condition, string orderByColumnName, SortTypeEnum sortType, object[] parameterValues, params string[] columnNames)
        {
            if (string.IsNullOrEmpty(orderByColumnName))
                throw new ArgumentException("排序字段名称，不要求带ORDER BY关键字,只要指定排序字段名称即可", "orderByColumnName");

            if (!this.ContainWhere(condition))
                throw new ArgumentException("指定的条件,要求带SQL语句Where关键字的条件", "condition");

            //设置默认分页参数
            if (pageSize < 1) pageSize = 10;
            if (pageIndex < 0) pageIndex = 0;

            //设置分页起点与终点
            int startRowId = (pageIndex - 1) * pageSize;
            //获取筛选列
            string columns = this.GetColumns(columnNames);

            //分页获取数据库记录集的SQL语句
            StringBuilder sqlFormat = new StringBuilder();
            sqlFormat.Append("SELECT {0} ");
            sqlFormat.Append("FROM {1} ");
            sqlFormat.Append("{2} ORDER BY {3} {4} LIMIT {5},{6} ");

            string sqlCmd = string.Format(sqlFormat.ToString(), columns, this._tableName, condition, orderByColumnName, sortType.ToString(), startRowId, pageSize);
            return this.GetPageData(condition, parameterValues, columnNames, sqlCmd);
        }

        /// <summary>
        /// 利用存储过程对表中数据进行分页查询的方法(返回值不需判断是否为null)。
        /// </summary>
        /// <param name="pageSize">分页大小，即分页显示多少条记录</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="condition">指定的条件,要求不带SQL语句Where关键字的条件,空值为无条件</param>
        /// <param name="orderby">排序字段名称，不要求带ORDER BY关键字,只要指定排序字段名称即可</param>
        /// <param name="sortType">SQL语句排序类型</param>
        /// <param name="parameterValues">SQL参数对应值的集合,如果条件字符串中含有参数标记,则必须设置该数组的值</param>
        /// <param name="columnNames">该实体对象中对应的数据库表的列名</param>
        /// <returns>当前分页的所有取实体对象集合</returns>
        public virtual PageData<T> SelectWithPageSizeByStoredProcedure(int pageSize, int pageIndex, string condition, string orderby, SortTypeEnum sortType, params string[] columnNames)
        {
            if (this.ContainWhere(condition))
                throw new ArgumentException("指定的条件,要求不带SQL语句Where关键字的条件", "condition");

            MySqlParameter[] parameters = {
                                            new MySqlParameter("@tblName",MySqlDbType.VarChar, 255),
                                            new MySqlParameter("@fldName", MySqlDbType.VarChar, 255),
                                            new MySqlParameter("@OrderfldName", MySqlDbType.VarChar, 255),
                                            new MySqlParameter("@StatfldName", MySqlDbType.VarChar, 255),
                                            new MySqlParameter("@PageSize", MySqlDbType.Int32),
                                            new MySqlParameter("@PageIndex", MySqlDbType.Int32),
                                            new MySqlParameter("@IsReCount", MySqlDbType.Int32),
                                            new MySqlParameter("@OrderType", MySqlDbType.Int32),
                                            new MySqlParameter("@strWhere", MySqlDbType.VarChar,1000)
                                        };
            parameters[0].Value = this._tableName;
            parameters[1].Value = "*";
            parameters[2].Value = orderby;
            parameters[3].Value = "";
            parameters[4].Value = pageSize;
            parameters[5].Value = pageIndex;
            parameters[6].Value = 0;
            parameters[7].Value = (int)sortType;
            parameters[8].Value = condition;

            PageData<T> pageData = new PageData<T>();
            pageData.RecordSet = this.GetEntities("sp_SelectWithPageSize", parameters, CommandType.StoredProcedure, columnNames);
            pageData.Count = this.Count(condition);
            return pageData;

            //存储过程代码
            //CREATE PROCEDURE [dbo].[sp_SelectWithPageSize]
            //    @tblName      varchar(255),       -- 表名
            //    @fldName      varchar(255),       -- 字段名
            //    @PageSize     int = 10,           -- 页尺寸
            //    @PageIndex    int = 1,            -- 页码
            //    @IsReCount    bit = 0,            -- 返回记录总数, 非 0 值则返回
            //    @OrderType    bit = 0,            -- 设置排序类型, 非 0 值则降序
            //    @strWhere     varchar(1000) = ''  -- 查询条件 (注意: 不要加 where)
            //AS

            //declare @strSQL   varchar(6000)       -- 主语句
            //declare @strTmp   varchar(100)        -- 临时变量
            //declare @strOrder varchar(400)        -- 排序类型

            //if @OrderType != 0
            //begin
            //    set @strTmp = '<(select min'
            //    set @strOrder = ' order by [' + @fldName +'] desc'
            //end
            //else
            //begin
            //    set @strTmp = '>(select max'
            //    set @strOrder = ' order by [' + @fldName +'] asc'
            //end

            //set @strSQL = 'select top ' + str(@PageSize) + ' * from ['
            //    + @tblName + '] where [' + @fldName + ']' + @strTmp + '(['
            //    + @fldName + ']) from (select top ' + str((@PageIndex-1)*@PageSize) + ' ['
            //    + @fldName + '] from [' + @tblName + ']' + @strOrder + ') as tblTmp)'
            //    + @strOrder

            //if @strWhere != ''
            //    set @strSQL = 'select top ' + str(@PageSize) + ' * from ['
            //        + @tblName + '] where [' + @fldName + ']' + @strTmp + '(['
            //        + @fldName + ']) from (select top ' + str((@PageIndex-1)*@PageSize) + ' ['
            //        + @fldName + '] from [' + @tblName + '] where ' + @strWhere + ' '
            //        + @strOrder + ') as tblTmp) and ' + @strWhere + ' ' + @strOrder

            //if @PageIndex = 1
            //begin
            //    set @strTmp =''
            //    if @strWhere != ''
            //        set @strTmp = ' where ' + @strWhere

            //    set @strSQL = 'select top ' + str(@PageSize) + ' * from ['
            //        + @tblName + ']' + @strTmp + ' ' + @strOrder
            //end

            //if @IsReCount != 0
            //    set @strSQL = 'select count(*) as Total from [' + @tblName + ']'+' where ' + @strWhere

            //exec (@strSQL)
        }

        /// <summary>
        /// 获取数据库中表的记录总数。
        /// </summary>
        /// <returns>表的记录总数</returns>
        public virtual int Count()
        {
            return this.Count(string.Empty);
        }

        /// <summary>
        /// 获取数据库表中指定条件的记录总数。
        /// </summary>
        /// <param name="condition">要求带SQL语句Where关键字的条件，如果不带Where关键字该方法将对表中所有记录执行操作,如果条件字符串中含有SQL参数标记(@),且必须写成如下格式:(@p0,@p1...)
        /// <example>e.g.:[UserName]=@p0 AND [Password] = @p1</example></param>
        /// <param name="parameterValues">SQL参数对应值的集合,如果条件字符串中含有参数标记,则必须设置该数组的值</param>
        /// <returns>指定条件的记录总数</returns>
        public virtual int Count(string condition, params object[] parameterValues)
        {
            if (!this.ContainWhere(condition))
                throw new ArgumentException("指定的条件,要求带SQL语句Where关键字的条件", "condition");

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(0) AS TotalCount FROM {0} {1}");

            string sqlCmd = sqlCmd = string.Format(strSql.ToString(), this._tableName, condition);
            MySqlParameter[] parameters = this.FillParameters(parameterValues);
            object obj = MySqlHelper.ExecuteScalar(this._connectionString, sqlCmd, parameters);
            return Convert.IsDBNull(obj) ? 0 : Convert.ToInt32(obj);
        }

        #endregion

        #region 类方法成员

        /// <summary>
        /// 条件中是否有Where关键字
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>true|false</returns>
        protected bool ContainWhere(string condition)
        {
            return (string.IsNullOrEmpty(condition) ||
                (!string.IsNullOrEmpty(condition) &&
                condition.IndexOf("where", StringComparison.CurrentCultureIgnoreCase) != -1));
        }

        /// <summary>
        /// 执行一条查询语句，并以实体对象集合形式返回查询结果集。
        /// </summary>
        /// <param name="sqlCmd">SQL命令</param>
        /// <param name="columnNames">该实体对象中对应的数据库表的列名</param>
        /// <returns>实体对象集合</returns>
        protected List<T> GetEntities(string sqlCmd, params string[] columnNames)
        {
            return this.GetEntities(sqlCmd, null, columnNames);
        }

        /// <summary>
        /// 执行一条查询语句，并以实体对象集合形式返回查询结果集。
        /// </summary>
        /// <param name="sqlCmd">SQL命令</param>
        /// <param name="parameterValues">SQL参数对应值的集合,如果SQL语句字符串中含有参数标记,则必须设置该数组的值</param>
        /// <param name="columnNames">该实体对象中对应的数据库表的列名</param>
        /// <returns>实体对象集合</returns>
        protected List<T> GetEntities(string sqlCmd, object[] parameterValues, params string[] columnNames)
        {
            return this.GetEntities(sqlCmd, parameterValues, CommandType.Text, columnNames);
        }

        /// <summary>
        /// 执行一条查询语句，并以实体对象集合形式返回查询结果集。
        /// </summary>
        /// <param name="sqlCmd">SQL命令</param>
        /// <param name="parameterValues">SQL参数对应值的集合,如果SQL语句字符串中含有参数标记,则必须设置该数组的值</param>
        /// <param name="commandType">CommandType</param>
        /// <param name="columnNames">该实体对象中对应的数据库表的列名</param>
        /// <returns>实体对象集合</returns>
        protected List<T> GetEntities(string sqlCmd, object[] parameterValues, CommandType commandType, params string[] columnNames)
        {
            return this.GetEntities(sqlCmd, this.FillParameters(parameterValues), commandType, columnNames);
        }

        /// <summary>
        /// 执行一条查询语句，并以实体对象集合形式返回查询结果集。
        /// </summary>
        /// <param name="sqlCmd">SQL命令</param>
        /// <param name="parameters">SQL参数对应值的集合,如果SQL语句字符串中含有参数标记,则必须设置该数组的值</param>
        /// <param name="commandType">CommandType</param>
        /// <param name="columnNames">该实体对象中对应的数据库表的列名</param>
        /// <returns>实体对象集合</returns>
        protected virtual List<T> GetEntities(string sqlCmd, MySqlParameter[] parameters, CommandType commandType, params string[] columnNames)
        {
            return this.GetEntities<T>(sqlCmd, parameters, commandType, this.DataReaderToEntity, columnNames);
        }

        /// <summary>
        /// 执行一条查询语句，并以实体对象集合形式返回查询结果集。
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="sqlCmd">SQL命令</param>
        /// <param name="parameters">SQL参数对应值的集合,如果SQL语句字符串中含有参数标记,则必须设置该数组的值</param>
        /// <param name="commandType">CommandType</param>
        /// <param name="drToEntityAction">读出DataReader数据到DTO</param>
        /// <param name="columnNames">该实体对象中对应的数据库表的列名</param>
        /// <returns>实体对象集合</returns>
        protected virtual List<TEntity> GetEntities<TEntity>(string sqlCmd, MySqlParameter[] parameters, CommandType commandType,
            Func<MySqlDataReader, MetaDataTable, string[], TEntity> drToEntityAction, params string[] columnNames)
        {
            List<TEntity> entities = new List<TEntity>();
            var metaDataTable = new MetaDataTable(typeof(TEntity), this._tableName);

            using (MySqlDataReader dr = MySqlHelper.ExecuteReader(this._connectionString, sqlCmd, parameters))
            {
                try
                {
                    while (dr.Read())
                    {
                        entities.Add(drToEntityAction(dr, metaDataTable, columnNames));
                    }
                }
                catch (MySqlException ex)
                {
                    string message = string.Format("[SQL]:{0},[Exception]:{1}", sqlCmd, ex.ToString());
                    System.Diagnostics.EventLog.WriteEntry("LightFramework.Data.MySQL", message);
                }
            }

            return entities;
        }

        /// <summary>
        /// 获取分页数据集合。
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="parameterValues">SQL参数对应值的集合,如果SQL语句字符串中含有参数标记,则必须设置该数组的值</param>
        /// <param name="columnNames">该实体对象中对应的数据库表的列名</param>
        /// <param name="sqlCmd">SQL命令</param>
        /// <returns>分页数据集合</returns>
        protected PageData<T> GetPageData(string condition, object[] parameterValues, string[] columnNames, string sqlCmd)
        {
            return this.GetPageData(condition, parameterValues, columnNames, sqlCmd, CommandType.Text);
        }

        /// <summary>
        /// 获取分页数据集合。
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="parameterValues">SQL参数对应值的集合,如果SQL语句字符串中含有参数标记,则必须设置该数组的值</param>
        /// <param name="columnNames">该实体对象中对应的数据库表的列名</param>
        /// <param name="sqlCmd">SQL命令</param>
        /// <param name="commandType">CommandType</param>
        /// <returns>分页数据集合</returns>
        protected virtual PageData<T> GetPageData(string condition, object[] parameterValues, string[] columnNames, string sqlCmd, CommandType commandType)
        {
            PageData<T> pageData = new PageData<T>();
            pageData.RecordSet = this.GetEntities(sqlCmd, parameterValues, commandType, columnNames);
            pageData.Count = this.Count(condition, parameterValues);
            return pageData;
        }

        ///<summary>
        ///生成参数对象集合。
        /// </summary>
        /// <param name="parameterValues">SQL参数值的集合</param>
        /// <returns>参数对象集合</returns>
        protected virtual MySqlParameter[] FillParameters(object[] parameterValues)
        {
            if (parameterValues == null ||
                parameterValues.Length == 0)
            {
                return null;
            }

            MySqlParameter[] parameters = new MySqlParameter[parameterValues.Length];
            for (int i = 0; i < parameterValues.Length; i++)
            {
                parameters[i] = new MySqlParameter();
                parameters[i].ParameterName = "@p" + i;
                parameters[i].Value = parameterValues[i] != null ? parameterValues[i] : DBNull.Value;
            }

            return parameters;
        }

        /// <summary>
        /// 获取筛选的列名组成的字符串。
        /// </summary>
        /// <param name="columnNames">筛选的列名集合</param>
        /// <returns>列名组成的字符串</returns>
        protected virtual string GetColumns(params string[] columnNames)
        {
            if (columnNames == null ||
                columnNames.Length == 0) return "*";

            string[] cols = new string[columnNames.Length];
            for (int i = 0; i < columnNames.Length; i++)
            {
                cols[i] = string.Format("{0}", columnNames[i]);
            }

            return string.Join(",", cols);
        }

        /// <summary>
        /// 获取SQL参数对应的值的集合。
        /// </summary>
        /// <param name="values">值</param>
        /// <returns>SQL参数对应的值的集合</returns>
        protected virtual object[] GetParamerterValues(params object[] values)
        {
            return values;
        }

        /// <summary>
        /// 把当前Sql语句以事务方式执行,并以ReadCommitted隔离级别。
        /// </summary>
        /// <param name="sqlExpression">SqlExpression对象</param>
        protected void ExecuteByTransaction(SqlExpression sqlExpression)
        {
            this.ExecuteByTransaction(sqlExpression, IsolationLevel.ReadCommitted);
        }

        /// <summary>
        /// 把当前Sql语句以事务方式执行。
        /// </summary>
        /// <param name="sqlExpressions">SqlExpression对象</param>
        /// <param name="isolationLevel">事务隔离级别</param>
        protected void ExecuteByTransaction(SqlExpression sqlExpression, IsolationLevel isolationLevel)
        {
            this.ExecuteByTransaction(new List<SqlExpression>(1) { sqlExpression }, isolationLevel);
        }

        /// <summary>
        /// 把当前Sql语句以事务方式执行。
        /// </summary>
        /// <param name="sqlExpressions">SqlExpression对象集合</param>
        protected void ExecuteByTransaction(List<SqlExpression> sqlExpressions)
        {
            this.ExecuteByTransaction(sqlExpressions, IsolationLevel.ReadCommitted);
        }

        /// <summary>
        /// 把当前Sql语句以事务方式执行。
        /// </summary>
        /// <param name="sqlExpressions">SqlExpression对象集合</param>
        /// <param name="isolationLevel">事务隔离级别</param>
        protected virtual void ExecuteByTransaction(List<SqlExpression> sqlExpressions, IsolationLevel isolationLevel)
        {
            if (sqlExpressions == null ||
                sqlExpressions.Count == 0) throw new ArgumentNullException("sqlExpressions");

            using (MySqlConnection mySqlConnection = new MySqlConnection(this._connectionString))
            {
                mySqlConnection.Open();
                using (MySqlTransaction mySqlTransaction = mySqlConnection.BeginTransaction(isolationLevel))
                {
                    try
                    {
                        sqlExpressions.ForEach(expr =>
                            MySqlHelper.ExecuteNonQuery(mySqlConnection, expr.CommandText, expr.Parameters));
                        mySqlTransaction.Commit();
                    }
                    catch (MySqlException ex)
                    {
                        System.Diagnostics.EventLog.WriteEntry("LightFramework.Data.MySQL", ex.ToString());
                    }
                }
            }
        }

        #endregion
    }
}
