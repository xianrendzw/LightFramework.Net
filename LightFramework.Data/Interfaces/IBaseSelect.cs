using System.Collections.Generic;

namespace LightFramework.Data
{
    /// <summary>
    /// IBaseSelect提供对数据库一些基本查询操作的接口。
    /// </summary>
    /// <typeparam name="T">通用类型</typeparam>
    public interface IBaseSelect<T>
    {
        /// <summary>
        /// 获取或设置初始化的对象表名
        /// </summary>
        string TableName { get; set; }

        /// <summary>
        /// 获取或设置当前的数据库连接字符串。
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// 查询数据库,判断指定条件的记录是否存在。
        /// </summary>
        /// <param name="condition">指定的条件,不需要带SQL语句的Where关键字,如果条件字符串中含有SQL参数标记(@),且必须写成如下格式:(@p0,@p1...)
        /// <example>e.g.:[UserName]=@p0 AND [Password] = @p1</example></param>
        /// <param name="parameterValues">SQL参数对应值的集合,如果条件字符串中含有参数标记,则必须设置该数组的值</param>
        /// <returns>存在则返回<c>true</c>，否则为<c>false</c>。</returns>
        bool IsExistWithCondition(string condition, params object[] parameterValues);

        /// <summary>
        /// 从数据库中获取所有的实体对象集合(返回值不需判断是否为null)。
        /// </summary>
        /// <returns>实体对象集合</returns>
        List<T> Select();

        /// <summary>
        /// 从数据库中获取所有的实体对象集合(返回值不需判断是否为null)。
        /// </summary>
        /// <param name="columnNames">该实体对象中对应的数据库表的列名</param>
        /// <returns>实体对象集合</returns>
        List<T> Select(params string[] columnNames);

        /// <summary>
        /// 从数据库中获取满足指定条件的实体对象集合(返回值不需判断是否为null)。
        /// </summary>
        /// <param name="condition">指定的条件,要求带SQL语句Where关键字的条件,如果条件字符串中含有SQL参数标记(@),且必须写成如下格式:(@p0,@p1...)
        /// <example>e.g.:[UserName]=@p0 AND [Password] = @p1</example></param>
        /// <param name="columnNames">该实体对象中对应的数据库表的列名</param>
        /// <returns>指定条件的表中的实体对象集合</returns>
        List<T> SelectWithCondition(string condition, params string[] columnNames);

        /// <summary>
        /// 从数据库中获取满足指定条件的实体对象集合(返回值不需判断是否为null)。
        /// </summary>
        /// <param name="condition">指定的条件,要求带SQL语句Where关键字的条件,如果条件字符串中含有SQL参数标记(@),且必须写成如下格式:(@p0,@p1...)
        /// <example>e.g.:[UserName]=@p0 AND [Password] = @p1</example></param>
        /// <param name="parameterValues">SQL参数对应值的集合,如果条件字符串中含有参数标记,则必须设置该数组的值</param>
        /// <param name="columnNames">该实体对象中对应的数据库表的列名</param>
        /// <returns>实体对象集合</returns>
        List<T> SelectWithCondition(string condition, object[] parameterValues, params string[] columnNames);


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
        List<T> SelectWithCondition(string condition, string orderByColumnName, SortTypeEnum sortType, object[] parameterValues, params string[] columnNames);
        

        /// <summary>
        /// 从数据库中获取满足指定条件的前N项实体对象集合(返回值不需判断是否为null)。
        /// </summary>
        /// <param name="topN">表中的前N条记录</param>
        /// <param name="condition">指定的条件,要求带SQL语句Where关键字的条件,如果条件字符串中含有SQL参数标记(@),且必须写成如下格式:(@p0,@p1...)
        /// <example>e.g.:[UserName]=@p0 AND [Password] = @p1</example></param>
        /// <param name="parameterValues">SQL参数对应值的集合,如果条件字符串中含有参数标记,则必须设置该数组的值</param>
        /// <param name="columnNames">该实体对象中对应的数据库表的列名</param>
        /// <returns>实体对象集合</returns>
        List<T> SelectTopN(int topN, string condition, object[] parameterValues, params string[] columnNames);
        
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
        List<T> SelectTopN(int topN, string condition, string orderByColumnName, SortTypeEnum sortType, object[] parameterValues, params string[] columnNames);

        /// <summary>
        /// 从数据库中获取一条满足指定条件的实体对象集合(返回值需要判断是否为null)。
        /// </summary>
        /// <param name="condition">指定的条件,要求带SQL语句Where关键字的条件,如果条件字符串中含有SQL参数标记(@),且必须写成如下格式:(@p0,@p1...)
        /// <example>e.g.:[UserName]=@p0 AND [Password] = @p1</example></param>
        /// <param name="parameterValues">SQL参数对应值的集合,如果条件字符串中含有参数标记,则必须设置该数组的值</param>
        /// <param name="columnNames">该实体对象中对应的数据库表的列名</param>
        /// <returns>实体对象或null</returns>
        T SelectOne(string condition, object[] parameterValues, params string[] columnNames);

        /// <summary>
        /// 从数据库中按分页大小进行获取实体对象集合(返回值不需判断是否为null),默认返回按表中的字段降序排序的集合,
        /// notinColumnName(指定的筛选列名称),该参数为必须指定，且为当前表中合法的列名称。如果未指定列名称，该方法将返回null。
        /// </summary>
        /// <param name="pageSize">分页大小，即分页显示多少条记录</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="condition">指定的条件,要求带SQL语句Where关键字的条件,如果不带Where关键字则条件无效,如果条件字符串中含有SQL参数标记(@),且必须写成如下格式:(@p0,@p1...)
        /// <example>e.g.:[UserName]=@p0 AND [Password] = @p1</example></param>
        /// <param name="notinColumnName">指定的筛选列名称,该参数为必须指定，且为当前表中合法的列名称。如果未指定列名称，该方法将返回null</param>
        /// <param name="orderby">排序字段名称，不要求带ORDER BY关键字,只要指定排序字段名称即可</param>
        /// <param name="sortType">SQL语句排序类型</param>
        /// <param name="parameterValues">SQL参数对应值的集合,如果条件字符串中含有参数标记,则必须设置该数组的值</param>
        /// <param name="columnNames">该实体对象中对应的数据库表的列名</param>
        /// <returns>当前分页的所有取实体对象集合</returns>
        PageData<T> SelectWithPageSizeByNotIn(int pageSize, int pageIndex, string condition, string notinColumnName, string orderby, SortTypeEnum sortType, object[] parameterValues, params string[] columnNames);

        /// <summary>
        /// 利用数据库表的RowId属性对数据进行分页查询的方法(返回值不需判断是否为null),该方法只能支持SqlServer2005以上版的数据库
        /// </summary>
        /// <param name="pageSize">分页大小，即分页显示多少条记录</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="condition">指定的条件,要求带SQL语句Where关键字的条件,如果不带Where关键字则条件无效,如果条件字符串中含有SQL参数标记(@),且必须写成如下格式:(@p0,@p1...)
        /// <example>e.g.:[UserName]=@p0 AND [Password] = @p1</example></param>
        /// <param name="orderby">排序字段名称，不要求带ORDER BY关键字,只要指定排序字段名称即可</param>
        /// <param name="sortType">SQL语句排序类型</param>
        /// <param name="parameterValues">SQL参数对应值的集合,如果条件字符串中含有参数标记,则必须设置该数组的值</param>
        /// <param name="columnNames">该实体对象中对应的数据库表的列名</param>
        /// <returns>当前分页的所有取实体对象集合</returns>
        PageData<T> SelectWithPageSizeByRowId(int pageSize, int pageIndex, string condition, string orderby, SortTypeEnum sortType, object[] parameterValues, params string[] columnNames);

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
        PageData<T> SelectWithPageSizeByStoredProcedure(int pageSize, int pageIndex, string condition, string orderby, SortTypeEnum sortType, params string[] columnNames);

        /// <summary>
        /// 获取数据库中表的记录总数。
        /// </summary>
        /// <returns>表的记录总数</returns>
        int Count();

        /// <summary>
        /// 获取数据库表中指定条件的记录总数。
        /// </summary>
        /// <param name="condition">要求带SQL语句Where关键字的条件，如果不带Where关键字该方法将对表中所有记录执行操作,如果条件字符串中含有SQL参数标记(@),且必须写成如下格式:(@p0,@p1...)
        /// <example>e.g.:[UserName]=@p0 AND [Password] = @p1</example></param>
        /// <param name="parameterValues">SQL参数对应值的集合,如果条件字符串中含有参数标记,则必须设置该数组的值</param>
        /// <returns>指定条件的记录总数</returns>
        int Count(string condition, params object[] parameterValues);
    }
}
