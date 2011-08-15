using System;
using System.Configuration;

namespace Whalin.Caching.Configuration
{
	public class InterfaceValidator : ConfigurationValidatorBase
	{
		private Type interfaceType;

		public InterfaceValidator(Type type)
		{
			if (!type.IsInterface)
				throw new ArgumentException(type + " must be an interface");

			this.interfaceType = type;
		}

		public override bool CanValidate(Type type)
		{
			return (type == typeof(Type)) || base.CanValidate(type);
		}

		public override void Validate(object value)
		{
			if (value != null)
				ConfigurationHelper.CheckForInterface((Type)value, this.interfaceType);
		}
	}

	public sealed class InterfaceValidatorAttribute : ConfigurationValidatorAttribute
	{
		private Type interfaceType;

		public InterfaceValidatorAttribute(Type type)
		{
			if (!type.IsInterface)
				throw new ArgumentException(type + " must be an interface");

			this.interfaceType = type;
		}

		public override ConfigurationValidatorBase ValidatorInstance
		{
			get { return new InterfaceValidator(this.interfaceType); }
		}
	}
}
