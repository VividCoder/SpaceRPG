using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceEngine.Map.Layer;
using Vivid.Scene;
using Vivid.Tex;
namespace SpaceEngine.Map
{
    public class Map
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
        public List<MapLayer> Layers
        {
            get;
            set;
        }

        public List<GraphLight> Lights
        {
            get;
            set;
        }

        public Map()
        {

            Layers = new List<MapLayer>();
            TileWidth = TileHeight = 64;
            Lights = new List<GraphLight>();
        }

        public void AddLight(GraphLight l)
        {

            Lights.Add(l);
        }

        public int TileWidth

        {
            get;
            set;
        }

        public int TileHeight
        {
            get;
            set;
        }

        public MapLayer AddLayer(MapLayer layer)
        {
            Layers.Add(layer);
            return layer;
        }

        public Map(int numLayers)
        {

            Layers = new List<MapLayer>();

            for (int i = 0; i < numLayers; i++)
            {
               // Layers.Add(new MapLayer());
            }

        }

        MapLayer GetLayer(int index)
        {

            return Layers[index];

        }
    
        void SetLayer(MapLayer layer,int index)
        {

            Layers[index] = layer;                
            
        }

        public List<HighLightTile> HL = new List<HighLightTile>();

        public void HighlightTile(int x,int y)
        {

            HL.Add(new HighLightTile() { X = x, Y = y });

        }

        public Vivid.Scene.SceneGraph2D UpdateGraph()
        {

            Vivid.Scene.SceneGraph2D Graph = new Vivid.Scene.SceneGraph2D();

            foreach (var layer in Layers)
            {

                for (int y = 0; y < layer.Height; y++)
                {
                    for (int x = 0; x < layer.Width; x++)
                    {

                        var tile = layer.GetTile(x, y);

                        if (tile == null) continue;

                        var tileSpr = new GraphSprite(tile.Image,TileWidth,TileHeight);

                        tileSpr.TileX = x;
                        tileSpr.TileY = y;

                        int mx = x * TileWidth;
                        int my = y * TileHeight;

                        tileSpr.SetPos(mx, my);

                        Graph.Add(tileSpr);
                                               
                    }
                }

            }

            foreach(var hl in HL)
            {


                int mx = hl.X * TileWidth;
                int my = hl.Y * TileHeight;


                var hs = new GraphSprite(new Tex2D("content/edit/highlight1.png",true));

                hs.SetPos(mx, my);
                // tileSpr.SetPos(mx, my);


                Graph.Add(hs); ;

            }
          
               
            

            Graph.Add(Lights.ToArray());
           // Graph.X = -32+ Vivid.App.AppInfo.RW/2;// (TileWidth * Layers[0].Width) / 2;
            //Graph.Y = -32+ Vivid.App.AppInfo.RH / 2;// (TileHeight * Layers[0].Height) / 2;


            //foreach(var l in Lights)
            //{
             //   Graph.Add(l);
            //}
            return Graph;

        }
    }
}
