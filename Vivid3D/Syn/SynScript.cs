using Vivid;

namespace Vivid.SynWave
{
    public class GameScript
    {
        public ManagedHost Host;
        public static Vivid.Structs.Game.StructGame ActiveGame = null;
        public static Vivid.Structs.Game.StructGameState ActiveGameState = null;

        public GameScript(string path)
        {
            if (Host == null) Host = new ManagedHost();

            var src = new ScriptSource(path);
            var compiler = new Compiler();
            var com_src = compiler.Compile(src);

            Host.SetEntry(com_src.EntryPoint);
        }

        public void MakeGameActive()
        {
  ;

            //ManagedHost.PushScope(ActiveGame.InstanceScope);
        }

        public void BeginInitState()
        {
            //ActiveGameState = ActiveGame.InitState;
            //Host.Entry.InitState.Begin();

        }

        public static void Update()
        {
            if (ActiveGameState != null)
            {
         
            }
        }

        public void BeginState(string name)
        {
        }
    }
}