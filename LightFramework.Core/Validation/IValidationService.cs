namespace LightFramework.Core
{
    public interface IValidationService
    {
        ValidationState Validate<T>(T model);
    }
}
