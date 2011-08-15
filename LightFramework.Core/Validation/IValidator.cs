namespace LightFramework.Core
{
    public interface IValidator<T>
    {
        ValidationState Validate(T entity);
    }
}
