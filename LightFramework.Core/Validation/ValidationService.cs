using System;
//using Microsoft.Practices.Unity;

namespace LightFramework.Core
{
    public class ValidationService : IValidationService
    {
        //private readonly IUnityContainer container;

        public ValidationService()//IUnityContainer container)
        {
            //this.container = container;
        }

        public IValidator<T> GetValidatorFor<T>(T entity)
        {
            //return container.Resolve<IValidator<T>>();
            return null;
        }

        public ValidationState Validate<T>(T entity)
        {
            IValidator<T> validator = GetValidatorFor(entity);
            
            if (validator == null) // or just return null?
                throw new Exception(string.Format("No validator found for type ({0})", entity.GetType()));

            return validator.Validate(entity);
        }
    }
}
