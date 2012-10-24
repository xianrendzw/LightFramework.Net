using System;
using System.Configuration;

namespace LightFramework.Tracing.Configuration
{
    public class LoggingConfigHelper
    {
        private static LoggingSection configSection;

        static LoggingConfigHelper()
        {
            LoadConfigSection();
        }

        public static LoggingSection ConfigSection
        {
            get { return configSection; }
        }

        private static void LoadConfigSection()
        {
            try
            {
                configSection = (LoggingSection)ConfigurationManager.GetSection("loggingSection");
            }
            catch (Exception ex)
            {
                throw new ConfigurationErrorsException("load logging configsection failure.", ex);
            }
        }
    }
}
