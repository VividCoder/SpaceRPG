using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Vivid;
using Vivid.SynC;
namespace Vivid.SynWave
{
    public class SynGame
    {

        public SynGameState CurState = null;
        public Structs.Game.StructGame Game;
        public List<SynModule> Mods = new List<SynModule>();
        public SynGame(List<SynModule> modules, string path)
        {

            Console.WriteLine("Compiling app:" + path);
            Source = new ScriptSource(path);
            CompiledSrc = SynHost.Comp.Compile(Source);

            Mods = modules;

            
            //Console.WriteLine("Compiled sucesfully.");
            Game = CompiledSrc.EntryPoint.Game;
            

        }

        public void Run()
        {

            var game_s = new SynScope();

            SynHost.Active.PushScope(game_s);

            CompiledSrc.EntryPoint.Game.Begin();

            CurState = new SynGameState(CompiledSrc.EntryPoint.InitState);

      

            SynGameHost.GameLink = this;

         

            SynGameHost.InitState = new SynGameHostState();

            var host = new SynGameHost();

            host.Run();

           
        }

        public string Name
        { get; set; }

        public bool Compiled = false;
        public string Path = "";
        public ScriptSource Source = null;
        public CompiledSource CompiledSrc = null;
    }


    public class SynGameHost : Vivid.App.VividApp
    {

        public static SynGame GameLink = null;

        public SynGameHost() : base(GameLink.Game.Title,1368,800,false)
        {

        }


    }


    public class SynGameHostState : Vivid.State.VividState
    {

        public static SynCState State = null;

        public override void InitState()
        {
            State.Init();
        }

        public override void UpdateState()
        {
            Vivid.Texture.Texture2D.UpdateLoading();
            State.Update();

            base.UpdateState();
        }

        public override void DrawState()
        {
            //StateLink.Draw();
            State.Draw();
            base.DrawState();
        }

    }

    public class SynGameApp : Vivid.App.VividApp
    {


        public SynGameApp(string app) : base(app, 1368, 800, false)
        {

        }


    }

}
