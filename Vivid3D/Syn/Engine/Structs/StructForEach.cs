using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Vivid.Structs
{
    public class StructForEach : Struct
    {

        public StructCode Code = null;
        public StructExpr Enumer = null;
        public override dynamic Exec()
        {

            var list = Enumer.Exec();

            var first = list.FindVar("first").Value;

            var ns = new SynWave.SynScope();

            SynWave.SynHost.Active.PushScope(ns);

            var tmp_v = new Var();

            tmp_v.Name = "item";
            tmp_v.Value = first;

            SynWave.SynHost.Active.RegVar(tmp_v);
            
            while (true)
            {

                Code.Exec();

                if(first.FindVar("Next").Value is int || first.FindVar("Next").Value.FindVar("Next").Value is int)
                {
                    break;
                }
                first = first.FindVar("Next").Value;
                tmp_v.Value = first;
                if(first == null)
                {
                    break;
                }


            }

            SynWave.SynHost.Active.PopScope();

            return null;
            

            //return base.Exec();
        }

    }
}
