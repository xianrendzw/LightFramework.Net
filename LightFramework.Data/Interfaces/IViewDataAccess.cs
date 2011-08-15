namespace LightFramework.Data
{
    /// <summary>
    /// IViewDataAccess提供对数据库视图进行数据访问的一些基本操作接口。
    /// </summary>
    /// <typeparam name="T">通用类型</typeparam>
    public interface IViewDataAccess<T> : IBaseSelect<T>
    {
    }
}
