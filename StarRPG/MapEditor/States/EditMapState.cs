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

namespace MapEditor.States
{
    public class EditMapState : VividState
    {

        public override void InitState()
        {
            base.InitState();

            var split1 = new HorizontalSplitterForm().Set(0, 0, AppInfo.W, AppInfo.H) as HorizontalSplitterForm;

            split1.SetSplit(AppInfo.H - AppInfo.H / 4);

            var split2 = new HorizontalSplitterForm().Set(0, 0, split1.TopDock.W, split1.TopDock.H) as HorizontalSplitterForm;

            var menu = new MenuForm().Set(0, 0, AppInfo.W, 20) as MenuForm;
            UI.Menu = menu;

            var menu_map = menu.AddItem("Map");

            menu_map.Menu.AddItem("Load Map");
            menu_map.Menu.AddItem("Save Map");
            menu_map.Menu.AddItem("New Map");
            menu_map.Menu.AddItem("Exit", (b) => { Environment.Exit(1); });

            menu.AddItem("Edit");
            //menu.AddItem("")
            split1.SetTop(split2);

            UIForm topForm = new UIForm().Set(0, 0, split2.TopDock.W, split2.TopDock.H);

            //topForm.Add(menu);
            //split2.SetTop(topForm);

            //split1.SetTop(split2);

            split2.SetSplit(50);

            SUI = new UI();
            SUI.Root = split1;

         

        }

        public override void UpdateState()
        {
            base.UpdateState();
            Texture2D.UpdateLoading();
            SUI.Update();

        }

        public override void DrawState()
        {
            base.DrawState();
            SUI.Render();
        }



    }
}
