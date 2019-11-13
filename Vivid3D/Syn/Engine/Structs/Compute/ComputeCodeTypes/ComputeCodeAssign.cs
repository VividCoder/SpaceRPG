namespace Vivid.Structs.Compute.ComputeCodeTypes
{
    public class ComputeCodeAssign : ComputeCodeBase
    {
        public bool Init = false;
        public string VarName = "";
        public ComputeCodeExpr Value = null;
        public ComputeVarType Type = ComputeVarType.Void;
    }
}