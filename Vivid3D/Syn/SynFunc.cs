using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid;
using Vivid.Structs;

namespace Vivid.SynWave
{
    public class SynFunc
    {

        public string Name
        { get; set; }
        public StructFunc Link = null;

        public CFunc RealCode = null;

        public FuncType Type = FuncType.CCode;

        public SynFunc(StructFunc link)
        {

            Link = link;
            Name = link.FuncName;
            Type = FuncType.SCode;

        }

        public SynFunc(string name)
        {
            Name = name;
            Type = FuncType.CCode;
        }


    }
    public enum FuncType
    {
        CCode,SCode
    }
    public delegate dynamic CFunc(params dynamic[] var);
}
