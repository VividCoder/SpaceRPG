namespace Vivid.Structs
{
    public class StructWhile : Struct
    {
        public StructExpr Condition = null;
        public StructCode Code = null;

        public override string DebugString()
        {
            return "Conditon:" + Condition.DebugString();
        }

        public override dynamic Exec()
        {
            //System.Console.WriteLine("ExecWhile");
            while (Condition.Exec() == 1)
            {
                var rv = Code.Exec();
                    
                if(rv is StructBreak)
                {
                    break;
                }
                if (rv != null)
                {
                    return rv;
                    int bb = 4;
                }

            }
            //Code.Exec ( );

            return null;
        }
    }
}