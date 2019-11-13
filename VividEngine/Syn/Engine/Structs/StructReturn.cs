namespace Vivid.Structs
{
    public class StructReturn : Struct
    {
        public StructExpr ReturnExp = null;

        public override dynamic Exec()
        {
            if (ReturnExp != null)
            {
                return ReturnExp.Exec();
            }

            return null;
        }
    }
}