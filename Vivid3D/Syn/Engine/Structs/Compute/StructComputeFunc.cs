﻿using System.Collections.Generic;

namespace Vivid.Structs.Compute
{
    public class StructComputeFunc
    {
        public ComputeVarType ReturnType = ComputeVarType.Void;
        public List<ComputeVar> InVars = new List<ComputeVar>();
        public string FuncName = "";
        public StructComputeCode Code = null;
    }
}