namespace LightFramework.Data
{
    /// <summary>
    /// ISinglePKInsert提供对数据库中单主键表一些基本增加操作的接口。
    /// </summary>
    /// <typeparam name="T">通用类型</typeparam>
    public interface ISinglePKInsert<T> : IBaseInsert<T>
    {
    }
}
