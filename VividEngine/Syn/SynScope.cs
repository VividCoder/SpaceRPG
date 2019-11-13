using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Vivid;
using Vivid.Structs;

namespace Vivid.SynWave
{
    public class SynScope
    {

        public List<Var> Vars = new List<Var>();
        public List<SynFunc> Funcs = new List<SynFunc>();
        public List<SynModule> Mods = new List<SynModule>();
        public SynScope Root = null;

        public SynScope(StructEntry entry)
        {

            foreach(var mod in entry.Modules)
            {

                var s_mod = new SynModule(mod);

            }

        }

        public void RegFunc(SynFunc func)
        {

            Funcs.Add(func);

        }

        public bool HasSysFunc(string name)
        {

            foreach (var f in Funcs)
            {
                if (f.RealCode != null)
                {
                    if (name == f.Name)
                    {
                        return true;
                    }
                }

            }
            return false;
        }

        public void RegFunc(CFunc func,string name)
        {

            var nfunc = new SynFunc(name);
            nfunc.RealCode = func;
            RegFunc(nfunc);

        }

        public void RegVar(Var v)
        {

            Vars.Add(v);

        }

        public Var FindVar(string var)
        {

            foreach(var v in Vars)
            {
                if(v.Name == var)
                {
                    return v;
                }
            }
            if (Root != null)
            {
                return Root.FindVar(var);
            }
            return null;
        }

        public SynFunc FindFunc(string name)
        {

            foreach(var func in Funcs)
            {
                if(func.Name == name)
                {
                    return func;
                }
            }
            if (Root != null)
            {
                return Root.FindFunc(name);
            }
            Console.WriteLine("Could not find function:" + name);
            return null;
        }

        public SynScope()
        {

        }

    }
}
