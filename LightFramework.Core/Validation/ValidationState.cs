namespace LightFramework.Core
{
    public class ValidationState
    {
        private readonly ValidationErrorCollection errors;

        public ValidationState()
        {
            errors = new ValidationErrorCollection();
        }

        public ValidationErrorCollection Errors
        {
            get { return errors; }
        }

        public bool IsValid
        {
            get
            {
                return Errors.Count == 0;
            }
        }
    }
}
