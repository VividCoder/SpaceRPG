namespace Vivid.Structs.Compute.ComputeCodeTypes
{
    public class ComputeCodeFor : ComputeCodeBase
    {
        public ComputeCodeAssign InitAssign;
        public ComputeCodeExpr Condition;
        public ComputeCodeAssign Inc;
        public StructComputeCode Code;
    }
}