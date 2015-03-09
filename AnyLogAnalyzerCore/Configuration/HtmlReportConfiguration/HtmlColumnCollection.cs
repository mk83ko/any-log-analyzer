using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Mkko.Configuration.HtmlReportConfiguration
{
    [ConfigurationCollection(typeof(HtmlColumnElement), AddItemName = "column", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class HtmlColumnCollection : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override string ElementName
        {
            get { return "column"; }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new HtmlColumnElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as HtmlColumnElement).Id;
        }
    }
}
