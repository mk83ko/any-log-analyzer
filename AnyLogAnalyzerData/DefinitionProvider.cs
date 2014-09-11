using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mkko.AnyLogAnalyzerData
{
    public class DefinitionProvider
    {
        public Timestamp Timestamp { get; set; }
        public List<DefinitionElement> Definitions { get; set; }
    }
}
