using System;
using System.Configuration;

namespace Whalin.Caching.Configuration
{
	internal class ConfigurationElementException : ConfigurationErrorsException
	{
        public ConfigurationElementException(string message, ConfigurationElement element)
            : this(message, null, element)
        {
        }

        public ConfigurationElementException(string message, Exception inner, ConfigurationElement element)
            : base(message, inner, element.ElementInformation.Source, element.ElementInformation.LineNumber)
        {
        }
	}
}

