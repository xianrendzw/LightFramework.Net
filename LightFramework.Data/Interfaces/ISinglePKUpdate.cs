using System.Collections.Generic;

namespace LightFramework.Data
{
    /// <summary>
    /// ISinglePKUpdate提供对数据库中单主键表一些基本修改操作的接口。
    /// </summary>
    /// <typeparam name="T">通用类型</typeparam>
    public interface ISinglePKUpdate<T>
    {
        /// <summary>
        /// 更新数据库表中指定主键值的记录。
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="id">表中主键的值</param>
        /// <param name="columnNames">该实体对象中对应的数据库表的列名</param>
        /// <returns>返回影响记录的行数,-1表示操作失败,大于-1表示成功</returns>
        int Update(T entity, int id, params string[] columnNames);

        /// <summary>
        /// 更新数据库表中指定主键值的记录。
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="id">表中主键的值</param>
        /// <param name="columnNames">该实体对象中对应的数据库表的列名</param>
        /// <returns>返回影响记录的行数,-1表示操作失败,大于-1表示成功</returns>
        int Update(T entity, string id, params string[] columnNames);

        /// <summary>
        /// 更新一条数据 条件为符合指定列的值 使用 IN 匹配
        /// 提示：In的条件仅用于主键列
        /// </summary>
        /// <param name="entity">更新的实体数据对象</param>
        /// <param name="keyValues">主键ID值 以,号分割</param>
        /// <param name="columnNames">更新的数据列名称(对应实体对象的属性)</param>
        /// <returns>返回影响记录的行数,-1表示操作失败,大于-1表示成功</returns>
        int UpdateIn(T entity, string keyValues, params string[] columnNames);

    }
}
