namespace LightFramework.Data
{
    /// <summary>
    /// SQL语句中的排序类型枚举
    /// </summary>
    public enum SortTypeEnum
    {
        /// <summary>
        /// 升序
        /// </summary>
        ASC,

        /// <summary>
        /// 降序
        /// </summary>
        DESC
    }

    /// <summary>
    /// SQL语句条件从句类型
    /// </summary>
    public enum SqlClause : int
    {
        /// <summary>
        /// 无从句
        /// </summary>
        None = 0,

        /// <summary>
        /// WHERE从句
        /// </summary>
        Where = 1,

        /// <summary>
        /// Having从句
        /// </summary>
        Having = 2
    }

    /// <summary>
    /// 括号类型
    /// </summary>
    public enum Bracket : int
    {
        /// <summary>
        /// '('
        /// </summary>
        Left = 0,

        /// <summary>
        /// ')'
        /// </summary>
        Rgiht = 1,
    }

    /// <summary>
    /// Sql数据插入方法。
    /// </summary>
    public enum SqlInsertMethod
    {
        /// <summary>
        /// 使用BulkCopy
        /// </summary>
        BulkCopy,

        /// <summary>
        /// 使用SQLServer2008及以上版本表值参数,
        /// 需要先在数据库创建与目标表结构相同的表。且表名必须为tvps。
        /// </summary>
        TableValue
    }
}
