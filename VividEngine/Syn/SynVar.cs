using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid;
using Vivid.Structs;

namespace Vivid.SynWave
{
    public class SynVar
    {

        public Var Link = null;
        public string Name = "";

        public SynVar(Var link)
        {
            Link = link;
            Name = link.Name;

        }

    }
}
