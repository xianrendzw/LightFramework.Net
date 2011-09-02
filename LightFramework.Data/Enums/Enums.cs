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
}
