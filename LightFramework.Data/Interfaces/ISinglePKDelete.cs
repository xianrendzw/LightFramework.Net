using System.Collections.Generic;

namespace LightFramework.Data
{
    /// <summary>
    /// ISinglePKDelete提供对数据库中单主键表一些基本删除操作的接口。
    /// </summary>
    /// <typeparam name="T">通用类型</typeparam>
    public interface ISinglePKDelete<T> : IBaseDelete<T>
    {
        /// <summary>
        /// 根据指定对象的ID,从数据库中删除指定对象(用于整型主键)。
        /// </summary>
        /// <param name="keyValue">指定对象的ID值</param>
        /// <returns>返回影响记录的行数,-1表示操作失败,大于-1表示成功</returns>
        int Delete(int keyValue);

        /// <summary>
        /// 根据指定对象的ID,从数据库中删除指定对象(用于整型主键)。
        /// </summary>
        /// <param name="keyValue">指定对象的ID值</param>
        /// <returns>返回影响记录的行数,-1表示操作失败,大于-1表示成功</returns>
        int Delete(string keyValue);

        /// <summary>
        /// 从数据库中删除一个或多个指定标识的记录。
        /// </summary>
        /// <param name="keyValues">记录标识数组</param>
        /// <returns>返回影响记录的行数,-1表示操作失败,大于-1表示成功</returns>
        int Delete(params int[] keyValues);

        /// <summary>
        /// 从数据库中删除一个或多个指定标识的记录。
        /// </summary>
        /// <param name="keyValues">记录标识数组</param>
        /// <returns>返回影响记录的行数,-1表示操作失败,大于-1表示成功</returns>
        int Delete(params string[] keyValues);

        /// <summary>
        /// 删除多条数据 条件为符合多个指定列的值 使用 IN 匹配
        /// </summary>
        /// <param name="keyValues">主键ID值 以,号分割</param>
        /// <returns>返回影响记录的行数,-1表示操作失败,大于-1表示成功</returns>
        int DeleteIn(string keyValues);
    }


}
