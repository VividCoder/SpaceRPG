using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.Resonance.Forms;
using Vivid.Resonance;
using OpenTK;
using Vivid;
using Vivid.App;
using Vivid.Scene;
using Vivid.Tex;
namespace SpaceEngine.Forms
{

    

    public class MapViewForm : UIForm
    {

        public Map.Map Map
        {
            get;
            set;
        }

        public Vivid.Scene.SceneGraph2D Graph
        {
            get;
            set;
        }

        //public List<HighLightTile> Highlights = new List<HighLightTile>();

        public Vivid.FrameBuffer.FrameBufferColor MapFrame;


  //      public void HighlightTile(int x,int y,int z)
//        {

        public void UpdateGraphHL()
        {

            //Map.AddHL();
        }
        float gr, gz;
        public void UpdateGraph()
        {
            float gx, gy;
            if (Graph != null)
            {
                gr = Graph.Rot;
                gx = Graph.X;
                gy = Graph.Y;
                gz = Graph.Z;
                Graph = Map.UpdateGraph();
                Graph.X = gx;
                Graph.Y = gy;
                Graph.Rot = gr;
                Graph.Z = gz;
               // Graph.X = -32 + W / 2;
               // Graph.Y = -32 + H / 2;
            }
            else
            {
                Graph = Map.UpdateGraph();
                //Graph.X = -32 + W / 2;
                //Graph.Y = -32 + H / 2;
            }
            Changed = true;
            Console.WriteLine("Changed..");
            //Graph.X -= 370;
            //Graph.Y -= 170;
        }
        public bool Changed = true;
        public MapViewForm(Map.Map map)
        {

            Map = map;

            if (MapFrame == null)
            {

                //MapFrame = new Vivid.FrameBuffer.FrameBufferColor()


            }

            AfterSet = () =>
            {

                MapFrame = new Vivid.FrameBuffer.FrameBufferColor (W, H);
                Changed = true;
            };

            PreDraw = () =>
            {

                
                if (Graph != null && Changed)
                {
                    MapFrame.Bind();
                    Changed = false;
                    //Console.WriteLine("Rendering map");
                   // AppInfo.RW = AppInfo.RW;
                    //AppInfo.RH = AppInfo.RH;
                    Graph.Draw();

                    //Graph.X += 1;
                    //Graph.Y += 1;
                    //Graph.;

                    //Graph.Z = 0.2f;
                    //Graph.Rot += 1.0f;
                    MapFrame.Release();

                }
              



            };
            float r = 1.0f;
            Draw = () =>
            {

                DrawFormSolid(new Vector4(1, 0.8f, 0.8f, 1.0f));
                Col = new Vector4(1, 1, 1, 1);
                DrawForm(MapFrame.BB,0,0,-1,-1,true);
                Graph.Rot = r;
                r = r + 1;
                Changed = true;



            };

            

        }

        public void Update()
        {



        }
      

    }
}
