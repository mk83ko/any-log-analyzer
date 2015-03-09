using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Mkko.Configuration
{
    public class AnyLogAnalyzerElement : ConfigurationSection
    {
        [ConfigurationProperty("executionEnvironments", IsRequired = true)]
        public ExecutionEnvironmentCollection ExecutionEnvironments
        {
            get { return (ExecutionEnvironmentCollection) this["executionEnvironments"]; }
            set { this["executionEnvironments"] = value; }
        }
    }
}
