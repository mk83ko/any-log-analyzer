using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Mkko.Configuration
{
    public class FileSettings : ConfigurationElement
    {
        [ConfigurationProperty("file", IsRequired = true)]
        public string File
        {
            get { return (string) this["file"]; }
            set { this["file"] = value; }
        }

        [ConfigurationProperty("type", IsRequired = true)]
        public string FileType
        {
            get { return (string) this["type"]; }
            set { this["type"] = value; }
        }
    }
}
