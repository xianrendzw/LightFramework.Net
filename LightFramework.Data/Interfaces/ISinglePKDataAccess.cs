namespace LightFramework.Data
{
    /// <summary>
    /// ISinglePKDataAccess提供对数据库中单主键表进行基本CRUD操作的接口。
    /// </summary>
    /// <typeparam name="T">通用类型</typeparam>
    public interface ISinglePKDataAccess<T>
        : IBaseDataAccess<T>, ISinglePKInsert<T>, ISinglePKDelete<T>, ISinglePKUpdate<T>, ISinglePKSelect<T>
    {
    }
}
