using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Vivid;
namespace Vivid.SynWave
{

       public class SynHost
    {
        public static Compiler Comp = new Compiler();

        public static SynHost Active = null;

        public List<SynModule> BaseModules = new List<SynModule>();

        public Stack<SynScope> Scopes = new Stack<SynScope>();

        public SynScope GlobalScope = new SynScope();

    

        public SynGameState GameState = null;

        public SynScope TopScope
        {

            get
            {
                if (Scopes.Count == 0) return null;
                return Scopes.Peek();
            }
        }

        public SynHost()
        {
            Scopes.Push(GlobalScope);
            RegisterFuncs();
            Active = this;
        }

        public void PushScope(SynScope scope)
        {

            var top = TopScope;
            if (top != null)
            {
                scope.Root = top;
            }
            Scopes.Push(scope);


        }
        public void PopScope()
        {
            if (Scopes.Count == 0) return;
            Scopes.Pop();
        }
        public void RegisterFuncs()
        {

            CFunc cf_printf = (pars) =>
            {
                foreach (var p in pars)
                {
                    Console.WriteLine(p);
                }
                return null;
            };

            GlobalScope.RegFunc(cf_printf, "printf");


            CFunc cf_playsong = (pars) =>
            {

                Console.WriteLine("Playing song:" + pars[0]);

                Vivid.Audio.Songs.PlaySong(pars[0]);

                return null;
            };

            GlobalScope.RegFunc(cf_playsong, "playsong");

            CFunc cf_loadImg = (pars) =>
            {

                var img = new Texture.Texture2D(pars[0], Texture.LoadMethod.Single, true);

                return img;
            };

            GlobalScope.RegFunc(cf_loadImg, "loadImg");

            CFunc cf_drawImg = (pars) =>
            {

                Draw.IntelliDraw.BeginDraw();
                Draw.IntelliDraw.DrawImg(pars[1], pars[2], pars[3], pars[4], pars[0], new OpenTK.Vector4(1, 1, 1, 1));
                Draw.IntelliDraw.EndDraw();
                return null;
            };

            GlobalScope.RegFunc(cf_drawImg, "drawImg");

            CFunc cf_drawImgCentered = (pars) =>
            {

                int cx = App.AppInfo.RW / 2;
                int cy = App.AppInfo.RH / 2;

                Draw.IntelliDraw.BeginDraw();
                Draw.IntelliDraw.DrawImg(cx-pars[1]/2, cy-pars[2]/2, pars[1],pars[2],pars[0], new OpenTK.Vector4(1, 1, 1,pars[3]));
                Draw.IntelliDraw.EndDraw();


                return null;

            };

            GlobalScope.RegFunc(cf_drawImgCentered, "drawImgCentered");


            CFunc cf_getTicks = (pars) =>
            {

                int tick = Environment.TickCount;
                return tick;
            };

            
            GlobalScope.RegFunc(cf_getTicks, "getTicks");

            CFunc cf_rndInt = (pars) =>
            {

                return rnd.Next(pars[0], pars[1]);

            };

            GlobalScope.RegFunc(cf_rndInt,"rndInt");

            CFunc cf_rnd = (pars) =>
            {

                float rv =  (float)rnd.NextDouble();

                float r2 = pars[1] - pars[0];

                r2 = r2 * rv + pars[0];

                return r2;

            };

            GlobalScope.RegFunc(cf_rnd, "rnd");

            CFunc cf_beginDraw = (pars) =>
            {

                Draw.IntelliDraw.BeginDraw();
                return null;
            };

            GlobalScope.RegFunc(cf_beginDraw, "beginDraw");

            CFunc cf_endDraw = (pars) =>
            {
                Draw.IntelliDraw.EndDraw();
                return null;
            };

            GlobalScope.RegFunc(cf_endDraw, "endDraw");

            CFunc cf_drawRect = (pars) =>
            {
                if (whiteTex == null)
                {
                    whiteTex = new Texture.Texture2D("syn/data/whiteTex.png", Texture.LoadMethod.Single, false);
                }


            
                Draw.IntelliDraw.DrawImg((int)pars[0], (int)pars[1], (int)pars[2], (int)pars[3], whiteTex, new OpenTK.Vector4(1, 1, 1, 1));
                

                return null;
            };

            GlobalScope.RegFunc(cf_drawRect, "drawRect");

        }
        public Texture.Texture2D whiteTex = null;
        public Random rnd = new Random(Environment.TickCount);
        public bool HasSysFunc(string name)
        {
            return GlobalScope.HasSysFunc(name);
        }

        public dynamic RunSysFunc(string name,dynamic[] pars)
        {

            var func = GlobalScope.FindFunc(name);

            return RunFunc(func, pars);
        }

        public void RegVar(Vivid.Structs.Var v)
        {

            var cc = TopScope;

            cc.RegVar(v);

        }

        public dynamic FindVar(string name)
        {
            return TopScope.FindVar(name);
        }

        public SynFunc FindFunc(string name)
        {

            var func = TopScope.FindFunc(name);

            return func;

        }

        public dynamic RunFunc(SynFunc func,params dynamic[] var)
        {

            var new_scope = new SynScope();

            PushScope(new_scope);
            if (func.Type == FuncType.CCode)
            {

                var rv= func.RealCode.Invoke(var);
                PopScope();
                return rv;
            }
            else
            {

                var rv= func.Link.Exec();
                PopScope();
                return rv;

            }


            PopScope();

            Console.WriteLine("Running:" + func.Name);


            return null;
        }

        public SynModule FindMod(string name)
        {

            foreach(var mods in BaseModules)
            {

                var mod = mods.GetMod(name);
                if (mod != null)
                {
                    return mod;
                }
            }

            return null;

        }

        public dynamic RunMeth(SynModule mod,string name,params dynamic[] pars)
        {

            var m_scope = new SynScope();

            PushScope(m_scope);

            Console.WriteLine("Running method:" + name);

            PopScope();

            return null;

        }

        public void PushClassScope(Structs.StructModule mod)
        {

            var c_scope = new SynScope();
            PushScope(c_scope);

            foreach(var vv in mod.Vars)
            {

                RegVar(vv);

            }

        }

        public void DebugVars()
        {

            DebugScope(TopScope);

        }

        public void DebugMod(Structs.StructModule m)
        {

            foreach(var v in m.Vars)
            {

                Console.WriteLine("Mod:" + m.ModuleName + " Var:" + v.Name + " Value:" + v.Value);
                if(v.Value is Structs.StructModule)
                {
                    if(v.Value == m)
                    {
                        Console.WriteLine("Endless cycle.");
                        return;

                    }
                    DebugMod(v.Value); ;
                }

            }

        }

        public void DebugScope(SynScope s)
        {

            Console.WriteLine("Scope:");

            foreach(var v in s.Vars)
            {

                Console.WriteLine("Var:" + v.Name + " Value:" + v.Value);
                if(v.Value is Structs.StructModule)
                {

                    DebugMod(v.Value);

                }
            }

            

            if (s.Root != null)
            {

                DebugScope(s.Root);

            }

        }

        public dynamic RunMeth(Structs.StructModule mod, string name, params dynamic[] pars)
        {

            //Console.WriteLine("Running method:" + name);

            var m_scope = new SynScope();

            PushScope(m_scope);

            foreach(var vv in mod.Vars)
            {

                RegVar(vv);

            }

            var meth = mod.FindMethod(name);

            int pc = 0;

            if (meth.Pars != null && meth.Pars.Pars != null)
            {
                foreach (var p in meth.Pars.Pars)
                {

                    RegVar(p);

                    p.Value = pars[pc];

                }
            }

            //var func = meth;

            //RunFunc(new SynFunc(meth), pars);

            var func = new SynFunc(meth);

            

            var val = func.Link.Exec();

            var nv = new Vivid.Structs.Var();

            nv.Value = val;
      

            PopScope();

            return nv;

            return null;

        }

        public void MakeActive()
        {
            Active = this;
        }

        public void CompileGame(string path)
        {

            LoadModules("syn/Modules/");
            var app = new SynGame(BaseModules, path);

            SynToC sync = new SynToC(app, "sync/game.cs");

            return;

        }

        public void RunGame(string path)
        {

            var app = new SynGame(BaseModules, path);

            app.Run();

        }

        public dynamic RunCode(string path)
        {

            var app = new SynApp(BaseModules,path);

            app.Run();

            return null;

        }

        public void LoadModules(string path)
        {

            var dirInfo = new DirectoryInfo(path);

            while (true)
            {

                bool done = true;

                foreach (var mod_folder in dirInfo.GetDirectories())
                {

                    Console.WriteLine("Module:" + mod_folder.Name);

                    var mod = new SynModule(mod_folder.FullName, mod_folder.Name);

                    mod.Compile();

                    BaseModules.Add(mod);



                }
                if (done) return;
            }

        }

    }




}
