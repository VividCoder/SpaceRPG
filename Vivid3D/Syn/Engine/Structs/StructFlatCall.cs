using Vivid.SynWave;
namespace Vivid.Structs
{
    public class StructFlatCall : Struct
    {
        public StructCallPars CallPars = null;
        public string FuncName = "";

        public override string DebugString()
        {
            return "FlatCall:" + FuncName + " Pars:" + CallPars.Pars.Count;
        }
        public dynamic[] pars = null;
        public override dynamic Exec()
        {
            Vivid.SynWave.SynFunc funcLink = Vivid.SynWave.SynHost.Active.FindFunc(FuncName);


     
          
                dynamic[] par = new dynamic[CallPars.Pars.Count];
                int i = 0;
                foreach (StructExpr exp in CallPars.Pars)
                {
                    par[i] = exp.Exec();
                    i++;
                }




            if (funcLink.Type == SynWave.FuncType.SCode)
            {
                return funcLink.Link.Exec();
            }
            else
            {
                return SynHost.Active.RunFunc(funcLink, par);
            }
            return null;
        }
    }
}