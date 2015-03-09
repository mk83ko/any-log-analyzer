using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Mkko.Configuration.HtmlReportConfiguration
{

    public class HtmlColumnElement : ConfigurationElement
    {
        [ConfigurationProperty("id", IsRequired = true)]
        public string Id
        {
            get { return (string)this["id"]; }
            set { this["id"] = value; }
        }

        [ConfigurationProperty("sortable", DefaultValue = false, IsRequired = false)]
        public bool Sortable
        {
            get { return (bool)this["sortable"]; }
            set { this["sortable"] = value; }
        }

        [ConfigurationProperty("filterable", DefaultValue = false, IsRequired = false)]
        public bool Filterable
        {
            get { return (bool)this["filterable"]; }
            set { this["filterable"] = value; }
        }
    }
}
