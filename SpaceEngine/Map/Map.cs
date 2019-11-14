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

        public List<MapLayer> Layers
        {
            get;
            set;
        }

        public Map()
        {

            Layers = new List<MapLayer>();
            TileWidth = TileHeight = 64;
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

                        int mx = x * TileWidth;
                        int my = y * TileHeight;

                        tileSpr.SetPos(mx, my);

                        Graph.Add(tileSpr);
                                               
                    }
                }

            }
            return Graph;

        }
    }
}
