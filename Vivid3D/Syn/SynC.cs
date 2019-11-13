using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.Texture;
using Vivid.Draw;
using Vivid.SynWave;
namespace Vivid.SynC
{

    public class SynGameHostC : SynCObject
    {
        public SynCState CurState = null;
        public SynCState InitState = null;
        public SynGameApp GameApp = null;
        public SynGameHostState HostState = null;
        public void Run()
        {

            GameApp = new SynGameApp("Syn Game");
            SynGameApp.InitState = new SynGameHostState();
            HostState = SynGameApp.InitState as SynGameHostState;
            SynGameHostState.State = InitState;
            SetState(InitState, true);

            GameApp.Run();

        }
        public void SetState(SynCState state,bool init = false)
        {
            CurState = state;

            //if (init) CurState.Init();
        }

    }
    public class SynCState : SynCObject
    {
        public virtual void Init()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void Draw()
        {

        }

    }
    public class SynCObject 
    {

   
        
        public dynamic rnd(dynamic v1,dynamic v2)
        {

            float rv = (float)rndg.NextDouble();

            float r2 = v2- v1;

            r2 = r2 * rv + v1;

            return r2;

        }

        public dynamic rndInt(dynamic v1,dynamic v2)
        {


            //return
            return rndg.Next(v1, v2);

        }

        public static Random rndg = new Random(Environment.TickCount);


        public void playSong(dynamic val)
        {
            Vivid.Audio.Songs.PlaySong(val);
        }

        public dynamic loadImg(dynamic img)
        {
            return new Texture2D(img, LoadMethod.Single, true);
        }
        public dynamic drawImg(dynamic img,dynamic x,dynamic y,dynamic w,dynamic h)
        {
            IntelliDraw.DrawImg((int)x, (int)y, (int)w, (int)h, img, new OpenTK.Vector4(1, 1, 1, 1));
            return null;
        }
        public void beginDraw()
        {
            IntelliDraw.BeginDraw();
        }

        public void endDraw()
        {
            IntelliDraw.EndDraw();
        }

        public void drawLine(dynamic x,dynamic y,dynamic x2,dynamic y2,dynamic r,dynamic g,dynamic b)
        {

            if(whiteTex == null)
            {
                whiteTex = new Texture2D("syn/data/whiteTex.png", LoadMethod.Single, false);
            }

            dynamic xd = x2 - x;
            dynamic yd = y2 - y;
            dynamic steps = 0;
            if (Math.Abs(xd) > Math.Abs(yd))
            {
                steps = Math.Abs(xd);
            }
            else
            {
                steps = Math.Abs(yd);
            }
            dynamic xi = xd / steps;
            dynamic yi = yd / steps;
            for(int i = 0; i < steps; i++)
            {
                IntelliDraw.DrawImg((int)x, (int)y, 2, 2, whiteTex, new OpenTK.Vector4(r, g, b, 1.0f));
                x = x + xi;
                y = y + yi;

            }




        }

        public dynamic appWidth()
        {
            return (float)Vivid.App.AppInfo.RW;
        }

        public dynamic appHeight()
        {
            return (float)Vivid.App.AppInfo.RH;
        }

        public void drawRect(dynamic x,dynamic y,dynamic w,dynamic h,dynamic r,dynamic g,dynamic b)
        {
            if (whiteTex == null)
            {
                whiteTex = new Texture2D("syn/data/whiteTex.png", LoadMethod.Single, false);
            }
            IntelliDraw.DrawImg((int)x, (int)y, (int)w, (int)h, whiteTex, new OpenTK.Vector4(r,g,b, 1));
        }
        public void drawRect(dynamic x,dynamic y,dynamic w,dynamic h)
        {
            drawRect(x, y, w, h, 1, 1, 1);
        }

        public void drawImgCentered(dynamic img,dynamic w,dynamic h,dynamic alpha)
        {

            int cx = App.AppInfo.RW / 2;
            int cy = App.AppInfo.RH / 2;
            IntelliDraw.DrawImg(cx - w / 2, cy - h / 2, w, h, img, new OpenTK.Vector4(1, 1, 1, alpha));
        }
        public static Texture2D whiteTex = null;

        public void printf(dynamic val)
        {
            Console.WriteLine(val);
            //System.Diagnostics.Debug.


            //System.Diagnostics.Debug.WriteLine(val);

        }
        public dynamic getTicks()
        {
            return Environment.TickCount;
        }
    }

}
