using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Vivid;
namespace Vivid.SynWave
{
    public class SynApp
    {

        public SynApp(List<SynModule> modules,string path)
        {

            Console.WriteLine("Compiling app:" + path);
            Source = new ScriptSource(path);
            CompiledSrc = SynHost.Comp.Compile(Source);
            Console.WriteLine("Compiled sucesfully.");

        }

        public dynamic Run()
        {

            var func = CompiledSrc.EntryPoint.FindSystemFunc("entry");

            func.Exec();

            return null;

        }

        public string Name
        { get; set; }

        public bool Compiled = false;
        public string Path = "";
        public ScriptSource Source = null;
        public CompiledSource CompiledSrc = null;

    }
}
