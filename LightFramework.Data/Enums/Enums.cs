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
    /// SQL语句条件类型
    /// </summary>
    public enum Condition : int
    {
        /// <summary>
        /// 不需要WHERE关键字
        /// </summary>
        Empty = 0,

        /// <summary>
        /// 需要WHERE关键字
        /// </summary>
        Where = 1
    }
}
