using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid;
using Vivid.App;
using Vivid.State;
using Vivid.Resonance;
using Vivid.Resonance.Forms;
using Vivid.Tex;
using Vivid.Texture;
using SpaceEngine;
using SpaceEngine.Map.Layer;
using SpaceEngine.Map;
using SpaceEngine.Forms;
using SpaceEngine.Map.Tile;
using SpaceEngine.Map.TileSet;
namespace MapEditor.Forms
{
    public class TileBrowser : WindowForm
    {
        public TabForm Tab = null;

        public MapViewForm Map = null;

        public TabPageForm GetActivePage()
        {

            return Tab.Shown;
            
        }

        public Map TileMap;
        public MapLayer TileLayer;
        int tx, ty;
        public void AddTileSet(TileSet ts)
        {

            foreach (var t in ts.Tiles)
            {
                TileLayer.SetTile(tx,ty,t);
                tx++;
                if (tx > 63)
                {
                    tx = 0;
                    ty++;
                }
            }
            Map.UpdateGraph();

        }

        public TileBrowser()
        {

            Tab = new TabForm();
            TileMap = new Map();
            TileLayer = new MapLayer(64, 64);
            TileMap.AddLayer(TileLayer);

            var l1 = new Vivid.Scene.GraphLight();



            Map = new MapViewForm(TileMap);

            TileMap.AddLight(l1);

            Tab.AddPage(new TabPageForm("TileSet 1"));
        

            body.Add(Tab);
            body.Add(Map);
            
            AfterSet = () =>
            {
                Tab.X = 0;
                Tab.Y = -15;
                Tab.W = W;
                Tab.H = body.H;
                Map.Set(0, 0, body.W, body.H);
            };

        }

    }
}
