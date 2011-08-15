using System.Collections.Generic;

namespace LightFramework.Data
{
    /// <summary>
    /// ISinglePKSelect提供对数据库中单主键表一些基本查询操作的接口。
    /// </summary>
    /// <typeparam name="T">通用类型</typeparam>
    public interface ISinglePKSelect<T>
    {
        /// <summary>
        /// 查询数据库,判断指定标识的记录是否存在。
        /// </summary>
        /// <param name="keyValues">指定的记录的唯一标识</param>
        /// <returns>存在则返回<c>true</c>，否则为<c>false</c>。</returns>
        bool IsExistKey(int keyValue);

        /// <summary>
        /// 查询数据库,判断指定标识的记录是否存在。
        /// </summary>
        /// <param name="keyValues">指定的记录的唯一标识</param>
        /// <returns>存在则返回<c>true</c>，否则为<c>false</c>。</returns>
        bool IsExistKey(params int[] keyValues);

        /// <summary>
        /// 查询数据库,判断指定标识的记录是否存在。
        /// </summary>
        /// <param name="keyValues">指定的记录的唯一标识</param>
        /// <returns>存在则返回<c>true</c>，否则为<c>false</c>。</returns>
        bool IsExistKey(string keyValue);

        /// <summary>
        /// 查询数据库,判断指定标识的记录是否存在。
        /// </summary>
        /// <param name="keyValues">指定的记录的唯一标识</param>
        /// <returns>存在则返回<c>true</c>，否则为<c>false</c>。</returns>
        bool IsExistKey(params string[] keyValues);

        /// <summary>
        /// 查询数据库,判断指定标识的记录是否存在，使用 IN 匹配
        /// </summary>
        /// <param name="keyValues">主键ID值 以,号分割</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        bool IsExistKeyIn(string keyValues);

        /// <summary>
        /// 从数据库中获取指定标识的实体对象(返回值需要判断是否为null)。
        /// </summary>
        /// <param name="keyValue">标识值</param>
        /// <param name="columnNames">该实体对象中对应的数据库表的列名</param>
        /// <returns>具体的实体对象</returns>
        T Select(string keyValue, params string[] columnNames);

        /// <summary>
        /// 从数据库中获取指定标识的实体对象(返回值需要判断是否为null)。
        /// </summary>
        /// <param name="keyValue">标识值</param>
        /// <param name="columnNames">该实体对象中对应的数据库表的列名</param>
        /// <returns>具体的实体对象</returns>
        T Select(int keyValue, params string[] columnNames);
        
        /// <summary>
        /// 从数据库中按表的ID列值分页大小进行获取实体对象集合(返回值不需要判断是否为null),默认返回按表中的标识字段降序排序的集合。
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
        PageData<T> SelectWithPageSizeByIdentity(int pageSize, int pageIndex, string condition, string orderby, SortTypeEnum sortType, object[] parameterValues, params string[] columnNames);

        /// <summary>
        /// 获取数据库中表的最大ID值(没有记录的时候返回0)。
        /// </summary>
        /// <returns>最大ID值</returns>
        int GetMaxID();

        /// <summary>
        /// 获取数据库中该对象指定属性的最大值(没有记录的时候返回0)。
        /// </summary>
        /// <param name="fieldName">表中的字段(列)名称,字段的值必需是整型数据</param>
        /// <param name="fromBase">从(2,8,10,16)进制的整型转换成10进制</param>
        /// <param name="condition">要求带SQL语句Where关键字的条件,如果条件字符串中含有SQL参数标记(@),且必须写成如下格式:(@p0,@p1...)
        /// <example>e.g.:[UserName]=@p0 AND [Password] = @p1</example></param>
        /// <param name="parameterValues">SQL参数对应值的集合,如果条件字符串中含有参数标记,则必须设置该数组的值</param>
        /// <returns>指定属性的最大值</returns>
        int GetMaxValue(string fieldName, int fromBase, string condition, params object[] parameterValues);
    }
}
