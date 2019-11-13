using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceEngine.Map.Layer;
using Vivid.Scene;
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
                Layers.Add(new MapLayer());
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

        SceneGraph2D ToSceneGraph(bool optimize = false)
        {

            SceneGraph2D graph = new SceneGraph2D();

            return graph;

        }

    }
}
