using Vivid.Structs;

namespace Vivid
{
    public class Compiler
    {
        public CompiledSource Compile(string path)
        {
            return Compile(new ScriptSource(path));
        }

        public CompiledSource Compile(ScriptSource s, LogInfo logger = null)
        {
            CompiledSource cs = new CompiledSource();

            var parser = new Parser(s.Tokens, logger);
            //parser.LogOut = logger;

            StructEntry entry = parser.Entry;

            cs.EntryPoint = entry;

            return cs;
        }
    }
}