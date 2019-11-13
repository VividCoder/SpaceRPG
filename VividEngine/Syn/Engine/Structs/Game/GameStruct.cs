using System.Collections.Generic;

namespace Vivid.Structs.Game
{
    public class StructGame : Struct
    {
        public List<StructGameState> States = new List<StructGameState>();

        public StructGameState InitState = null;

        public List<Var> StaticVars = new List<Var>();
        public List<Var> Vars = new List<Var>();
        public List<StructFunc> StaticFuncs = new List<StructFunc>();
        public List<StructFunc> Methods = new List<StructFunc>();
       // public CodeScope StaticScope = new CodeScope("ModuleStatic");
        //public CodeScope InstanceScope = new CodeScope("ModuleInstance");



        public void Begin()
        {
            foreach (var v in Vars)
            {
                //var nv = new Var();
                //nv.Name = v.Name;
                try
                {
                    v.Value = v.Init.Exec();
                }
                catch
                {
                    v.Value = 0;
                }
                //Vars.Add(nv);

                SynWave.SynHost.Active.RegVar(v);
                
            }

        }

        public string Author
        {
            get;
            set;
        }

        public string Copyright
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string Version
        {
            get;
            set;
        }
    }
}