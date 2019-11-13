using Vivid.Structs;

namespace Vivid
{
    public class Module
    {
        public StructEntry Mod;
        public ManagedHost.CModule CMod;

        public Module(StructEntry entry)
        {
            Mod = entry;
        }

        public Module()
        {
        }
    }
}