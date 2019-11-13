using System.Collections.Generic;

namespace Vivid.Structs
{
    public class StructIf : Struct
    {
        public StructExpr Condition;
        public StructCode TrueCode;
        public StructCode ElseCode;
        public List<StructExpr> ElseIf = new List<StructExpr>();
        public List<StructCode> ElseIfCode = new List<StructCode>();

        public bool hasReturn = false;

        public dynamic Run(StructCode code)
        {

            dynamic rv = code.Exec();
            return rv;

        }

        public override dynamic Exec()
        {
            var cond = Condition.Exec();

            if(cond is int)
            {
                if (cond==1)
                {

                    return Run(TrueCode);

                }
            }
            else
            {

                if((cond == null))
                {

                    return Run(TrueCode);

                }
                else
                {

                    return Run(ElseCode);

                }

            }

            return null;
        }

        public dynamic Exec2()
        {
            var cond = Condition.Exec();
            hasReturn = false;
          
            if (!(cond is StructModule))
            {
            

                var rl = TrueCode.Exec();
                if (rl is StructBreak)
                {
                    return rl;
                }
                if (rl != null)
                {
                    hasReturn = true;
                }
                return rl;

            }
            else
            {
                int ii = 0;
                bool done = false;
                foreach (var else_if in ElseIf)
                {
                    if (else_if.Exec() == 1)
                    {
                        var rc = ElseIfCode[ii].Exec();

                        done = true;
                        if (rc is StructBreak)
                        {
                            return rc;
                        }
                        if (rc != null)
                        {
                            
                        }
                        break;
                    }
                    ii++;
                }

                if (!done)
                {
                    if (ElseCode != null)
                    {
                        var rc = ElseCode.Exec();
                        if (rc is StructBreak)
                        {
                            return rc;
                        }
                        return null;
                    }
                }
            }
            return null;
        }
    }
}