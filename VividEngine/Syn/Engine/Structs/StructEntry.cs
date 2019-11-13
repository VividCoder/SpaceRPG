using System.Collections.Generic;

namespace Vivid.Structs
{
    public class StructEntry : Struct
    {
        public List<StructModule> Modules = new List<StructModule>();
        public List<StructFunc> SystemFuncs = new List<StructFunc>();
        public List<Compute.StructComputeStruct> ComStructs = new List<Compute.StructComputeStruct>();
        public List<Compute.StructCompute> Coms = new List<Compute.StructCompute>();
        public Structs.Game.StructGame Game = null;
        public Structs.Game.StructGameState InitState = null;

        public override string DebugString()
        {
            return "EntryPoint: Modules:" + Modules.Count + " SysFuncs:" + SystemFuncs.Count;
        }

        public StructFunc FindSystemFunc(string name)
        {
            foreach (StructFunc func in SystemFuncs)
            {
                if (func.FuncName == name)
                {
                    return func;
                }
            }
            return null;
        }
    }
}