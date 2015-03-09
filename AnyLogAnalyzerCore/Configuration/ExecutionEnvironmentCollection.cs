using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Mkko.Configuration
{
    [ConfigurationCollection(typeof(ExecutionEnvironmentSettings), AddItemName = "executionEnvironment", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class ExecutionEnvironmentCollection : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override string ElementName
        {
            get { return "executionEnvironment"; }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ExecutionEnvironmentSettings();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as ExecutionEnvironmentSettings).Id;
        }
    }
}
