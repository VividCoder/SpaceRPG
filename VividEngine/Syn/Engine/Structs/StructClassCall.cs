using System;
using System.Collections.Generic;

namespace Vivid.Structs
{
    public class StructClassCall : Struct
    {
        public List<string> call = new List<string>();
        public StructCallPars Pars;// = new VSCallPars();

        public override dynamic Exec()
        {
            dynamic bc = null;
            for (int i = 0; i < call.Count; i++)
            {
                if (call[i] == ".")
                {
                    continue;
                }
                if (bc != null && i == call.Count - 1)
                {
                    var meth = call[i];
                    dynamic[] pars = new dynamic[Pars.Pars.Count];
                    for (int p = 0; p < Pars.Pars.Count; p++)
                    {
                        pars[p] = Pars.Pars[p].Exec();
                    }
                    // ManagedHost.Main.ExecuteMethod(bc, meth, pars);
                    Vivid.SynWave.SynHost.Active.RunMeth(bc, meth, pars);
                    //Console.WriteLine("Class:" + bc.ModuleName + " Meth:" + call[i]);
                }
                else
                {
                    if (bc == null)
                    {
                        bc =Vivid.SynWave.SynHost.Active.FindVar(call[i]).Value;

                    }
                    else
                    {
                        bc = bc.FindVar(call[i]).Value;
                    }
                }
            }
          
            return null;
        }
    }
}