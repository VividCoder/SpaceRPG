using System;

namespace Vivid.Structs
{
    public class StructAssign : Struct
    {
        public System.Collections.Generic.List<Var> Vars = new System.Collections.Generic.List<Var>();
        public System.Collections.Generic.List<StructExpr> Expr = new System.Collections.Generic.List<StructExpr>();

        public bool CreatedVars = false;

        public StructFlatCall Call = null;

        public override dynamic Exec()
        {
          
                Var qv = Vivid.SynWave.SynHost.Active.FindVar(Vars[0].Name);
                if (qv != null)
                {
                    Vars[0] = qv;
                }
                else
                {
                    Vivid.SynWave.SynHost.Active.RegVar(Vars[0]);
                }
           
            if (Vars.Count == 1)
            {
                if (Call != null)
                {
                    Vars[0].Value = Call.Exec();
                    if(Vars[0].Value is object && !(Vars[0].Value is int))
                    {
                        Vars[0].Type = VarType.Class;
                    }
                    return null;
                }
              
                Vars[0].Value = Expr[0].Exec();
                if (Vars[0].Value is float)
                {
                    Vars[0].Type = VarType.Float;
                }
                else if (Vars[0].Value is int)
                {
                    Vars[0].Type = VarType.Int;
                }
                else if (Vars[0].Value is string)
                {
                    Vars[0].Type = VarType.String;
                }
            }
            else
            {
                dynamic basev = null;
                foreach (var var in Vars)
                {
                    if (basev == null)
                    {
                        basev = SynWave.SynHost.Active.FindVar(var.Name);
                    }
                    else
                    {
                        basev = basev.Value.FindVar(var.Name);
                    }
                }

                basev.Value = Expr[0].Exec();

              
            }
   
            return null;
        }
    }
}