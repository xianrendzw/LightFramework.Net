namespace LightFramework.Data
{
    /// <summary>
    /// IBaseDelete提供对数据库一些基本删除操作的接口。
    /// </summary>
    /// <typeparam name="T">通用类型</typeparam>
    public interface IBaseDelete<T>
    {
        /// <summary>
        /// 根据指定条件,从数据库中删除指定对象。
        /// </summary>
        /// <param name="condition">删除记录的条件语句,不需要带SQL语句的Where关键字,如果条件字符串中含有SQL参数标记(@),且必须写成如下格式:(@p0,@p1...)
        /// <example>e.g.:[UserName]=@p0 AND [Password] = @p1</example></param>
        /// <param name="parameterValues">SQL参数对应值的集合,如果条件字符串中含有参数标记,则必须设置该数组的值</param>
        /// <returns>返回影响记录的行数,-1表示操作失败,大于-1表示成功</returns>
        int DeleteWithCondition(string condition, params object[] parameterValues);

        /// <summary>
        /// 清空表中的所有数据。
        /// </summary>
        /// <returns>返回影响记录的行数,-1表示操作失败,大于-1表示成功</returns>
        int DeleteAll();

        /// <summary>
        /// 清空表中的所有数据,且不记录数据库日志。
        /// </summary>
        void Truncate();
    }
}
