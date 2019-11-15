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
        public TileBrowser()
        {

            Tab = new TabForm();

            Tab.AddPage(new TabPageForm("Test Page"));
            Tab.AddPage(new TabPageForm("Other Page"));

            body.Add(Tab);

            AfterSet = () =>
            {
                Tab.X = 0;
                Tab.Y = -15;
                Tab.W = W;
                Tab.H = body.H;

            };

        }

    }
}
