namespace LightFramework.Data.SQLServer
{
    /// <summary>
    /// Sql数据插入方法。
    /// </summary>
    public enum SqlInsertMethod
    {
        /// <summary>
        /// 使用SqlBulkCopy
        /// </summary>
        SqlBulkCopy,

        /// <summary>
        /// 使用SQLServer2008及以上版本表值参数,
        /// 需要先在数据库创建与目标表结构相同的表。且表名必须为tvps。
        /// </summary>
        TableValue
    }
}
