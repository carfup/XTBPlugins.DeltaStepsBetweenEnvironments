using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfup.XTBPlugins
{
    public class Differences
    {
        public List<string> Assemblies { get; private set; }

        public Differences()
        {
            Assemblies = new List<string>();
        }
    }
}
