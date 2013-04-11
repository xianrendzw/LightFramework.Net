using System.Collections.Generic;

namespace LightFramework.Data
{
    /// <summary>
    /// IBaseInsert提供对数据库一些基本增加操作的接口。
    /// </summary>
    /// <typeparam name="T">通用类型</typeparam>
    public interface IBaseInsert<T>
    {
        /// <summary>
        /// 向数据库中添加一条记录。
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="columnNames">目标表列名集合</param>
        /// <returns>返回影响记录的行数,-1表示操作失败,大于-1表示成功</returns>
        int Insert(T entity, params string[] columnNames);

        /// <summary>
        /// 向数据库中添加一条记录，并返回插入记录的ID值。
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="columnNames">目标表列名集合</param>
        /// <returns>插入记录的数据库自增标识</returns>
        int InsertWithId(T entity, params string[] columnNames);

        /// <summary>
        /// 使用多条sql语句用分号连接的方式向数据库中批量添加数据。
        /// </summary>
        /// <param name="batchEntities">记录集合</param>
        /// <returns>影响的行数</returns>
        int Insert(List<BatchEntity<T>> batchEntities);

        /// <summary>
        /// 向数据库中批量添加记录
        /// </summary>
        /// <param name="entities">记录集合</param>
        /// <param name="method">批量添加方式</param>
        /// <param name="columnNames">目标表列名集合</param>
        /// <returns>影响的行数</returns>
        int Insert(List<T> entities, SqlInsertMethod method, params string[] columnNames);

        /// <summary>
        /// 使用insert into ... select ... from 语句从指定表中批量向当前表中添加数据。
        /// </summary>
        /// <param name="fromTableName">源数据表名</param>
        /// <param name="columnNames">目标表列名集合</param>
        /// <returns>影响的行数</returns>
        int Insert(string fromTableName, params string[] columnNames);
    }
}
