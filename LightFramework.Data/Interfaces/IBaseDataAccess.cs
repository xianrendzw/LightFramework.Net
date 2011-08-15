namespace LightFramework.Data
{
    /// <summary>
    /// IBaseDataAccess提供对数据库进行基本CRUD操作的接口。
    /// </summary>
    /// <typeparam name="T">通用类型</typeparam>
    public interface IBaseDataAccess<T> : IBaseInsert<T>, IBaseDelete<T>, IBaseUpdate<T>, IBaseSelect<T>
    {
    }
}
