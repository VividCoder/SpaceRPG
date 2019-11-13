using System.Collections.Generic;

namespace Vivid.Structs
{
    public class StructDefineVars : Struct
    {
        public VarType Type = VarType.Bool;
        public List<Var> Vars = new List<Var>();
    }
}