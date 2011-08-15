namespace LightFramework.Core
{
    public interface IDependencyResolverFactory
    {
        IDependencyResolver CreateInstance();
    }
}