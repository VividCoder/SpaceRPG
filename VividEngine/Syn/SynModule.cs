using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Vivid;
namespace Vivid.SynWave
{

    public class SynModule
    {

        public List<SynModule> Depends = new List<SynModule>();
        public List<string> DependsRef = new List<string>();
        public string Name
        { get; set; }

        public bool Compiled = false;
        public string Path = "";
        public ScriptSource Source = null;
        public CompiledSource CompiledSrc = null;

        public List<SynModule> SubModules = new List<SynModule>();

         public Vivid.Structs.StructModule DirectModule = null;

        public ModuleType Type = ModuleType.Module;

        public List<SynFunc> LocalFuncs = new List<SynFunc>();

        public bool Instance = false;


        public Vivid.Structs.StructModule CreateInstance()
        {

            var new_mod = new SynModule(DirectModule.CreateInstance());
            return new_mod.DirectModule;

        }

        public SynModule(Vivid.Structs.StructModule mod)
        {

            DirectModule = mod;
            Type = ModuleType.Module;
            Name = DirectModule.ModuleName;
        }

        public SynModule(string path, string name)
        {

            Path = path;
            Name = name;
            Type = ModuleType.List;

        }

        public SynModule GetMod(string name)
        {

            if(Type == ModuleType.Module)
            {
                if (this.Name == name) return this;
                return null;
            }

            foreach(var mod in SubModules)
            {
                var m = mod.GetMod(name);
                if (m != null)
                {
                    return m;
                }
            }
            return null;

        }

        public bool Compile()
        {

            //Console.WriteLine("Mod:" + Path);
            // Console.WriteLine("Name:" + Name);


            Source = new ScriptSource(Path + "/" + Name + ".syn");

            Console.WriteLine("Compiling:" + Name + ".Module");
            CompiledSrc = SynHost.Comp.Compile(Source);
            Console.WriteLine("Compiled succesfully.");

            foreach(var mod in CompiledSrc.EntryPoint.Modules)
            {

                SubModules.Add(new SynModule(mod));

            }

            return true;
            //var src = new SynScript.ScriptSource(Path)


        }

    }

    public enum ModuleType
    {
        Module,List
    }
}


