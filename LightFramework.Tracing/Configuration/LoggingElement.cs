using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace LightFramework.Tracing
{
    public class LoggingElement : ConfigurationElement
    {
        [ConfigurationProperty("appender", IsRequired = true)]
        public string Appender
        {
            get { return base["appender"].ToString(); }
            set { base["appender"] = value; }
        }

        [ConfigurationProperty("isAsync", IsRequired = true, DefaultValue = true)]
        public bool IsAsync
        {
            get { return bool.Parse(base["isAsync"].ToString()); }
            set { base["isAsync"] = value; }
        }

        [ConfigurationProperty("level", IsRequired = true, DefaultValue = "Info")]
        public string Level
        {
            get { return base["level"].ToString(); }
            set { base["level"] = value; }
        }

        [ConfigurationProperty("dbDialect", IsRequired = false, DefaultValue = "MySql")]
        public string DbDialect
        {
            get { return base["dbDialect"].ToString(); }
            set { base["dbDialect"] = value; }
        }
    }
}
