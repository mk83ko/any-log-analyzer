using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mkko.Configuration.HtmlReportConfiguration;

namespace Mkko.Configuration
{
    public class ExecutionEnvironmentSettings : ConfigurationSection
    {
        [ConfigurationProperty("id", IsRequired = true)]
        public string Id
        {
            get { return (string) this["id"]; }
            set { this["id"] = value; }
        }

        [ConfigurationProperty("returnNumberOfEvents", IsRequired = false, DefaultValue = false)]
        public bool ReturnNumberOfEvents
        {
            get { return (bool)this["returnNumberOfEvents"]; }
            set { this["returnNumberOfEvents"] = value; }
        }

        [ConfigurationProperty("definitionsFile", IsRequired = false)]
        public FileSettings EventDefinitions
        {
            get { return (FileSettings)this["definitionsFile"]; }
            set { this["definitionsFile"] = value; }
        }

        [ConfigurationProperty("logfile", IsRequired = false)]
        public FileSettings LogFileSettings
        {
            get { return (FileSettings) this["logfile"]; }
            set { this["logfile"] = value; }
        }

        [ConfigurationProperty("htmlReportSettings", IsRequired = false)]
        public HtmlReportSettings HtmlReportSettings
        {
            get { return (HtmlReportSettings) this["htmlReportSettings"];  }
            set { this["htmlReportSettings"] = value; }
        }
    }
}
