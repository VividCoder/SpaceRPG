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

    public class HighLightTile
    {

        public int X
        {
            get;
            set;
        }

        public int Y
        {
            get;
            set;
        }

    }

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

        public List<HighLightTile> Highlights = new List<HighLightTile>();

        public void UpdateGraph()
        {

            Graph = Map.UpdateGraph();

        }

        public MapViewForm(Map.Map map)
        {

            Map = map;

            Draw = () =>
            {

                DrawFormSolid(new Vector4(0, 0.8f, 0.8f, 1.0f));

                if (Graph != null) {

                    Console.WriteLine("Rendering map");

                    Graph.Draw();

                }



            };

            

        }

        public void Update()
        {



        }
      

    }
}
