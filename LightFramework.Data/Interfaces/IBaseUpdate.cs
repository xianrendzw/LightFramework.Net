using System.Collections.Generic;

namespace LightFramework.Data
{
    /// <summary>
    /// IBaseUpdate提供对数据库一些基本修改操作的接口。
    /// </summary>
    /// <typeparam name="T">通用类型</typeparam>
    public interface IBaseUpdate<T>
    {
        /// <summary>
        /// 更新数据库表中的记录。
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="condition">不带Where的更新条件,如果条件字符串中含有SQL参数标记(@),且必须写成如下格式:(@p0,@p1...)
        /// <example>e.g.:[UserName]=@p0 AND [Password] = @p1</example></param>
        /// <param name="parameterValues">SQL参数对应值的集合,如果条件字符串中含有参数标记,则必须设置该数组的值</param>
        /// <param name="columnNames">该实体对象中对应的数据库表的列名</param>
        /// <returns>返回影响记录的行数,-1表示操作失败,大于-1表示成功</returns>
        int UpdateWithCondition(T entity, string condition, object[] parameterValues, params string[] columnNames);
    }
}
