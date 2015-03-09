using System;
using System.Configuration;

namespace Mkko.Configuration.HtmlReportConfiguration
{
    public class HtmlReportSettings : ConfigurationElement
    {
        [ConfigurationProperty("type", IsRequired = false, DefaultValue = "plain")]
        public string Type
        {
            get { return (string)this["type"]; }
        }

        [ConfigurationProperty("output", IsRequired = true)]
        public string Output
        {
            get { return (string)this["output"]; }
        }

        [ConfigurationProperty("columns", IsRequired = true)]
        public HtmlColumnCollection Columns
        {
            get { return (HtmlColumnCollection)this["columns"]; }
        }
    }
}
