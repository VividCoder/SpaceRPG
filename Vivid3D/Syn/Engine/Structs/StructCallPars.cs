using System.Collections.Generic;

namespace Vivid.Structs
{
    public class StructCallPars : Struct
    {
        public List<StructExpr> Pars = new List<StructExpr>();

        public override dynamic Exec()
        {
            dynamic[] pars = new dynamic[Pars.Count];

            int pi = 0;
            foreach(var pp in Pars)
            {
                pars[pi] = pp.Exec();
                pi++;
            }

            return pars;

            //return base.Exec();
        }

    }
}