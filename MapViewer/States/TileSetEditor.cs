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
namespace MapViewer.States
{
    public class TileSetEditor : VividState
    {

        public override void InitState()
        {
            base.InitState();
            SUI = new Vivid.Resonance.UI();

            var main_menu = new MenuForm().Set(0, 0, AppInfo.W, 25, "") as MenuForm;

            UIForm ui_Root = new UIForm();

            ui_Root.Add(main_menu);

            SUI.Root = ui_Root;

            var menu_TileSet = main_menu.AddItem("TileSet");
            main_menu.AddItem("Edit");

            var win_NewSet = new WindowForm().Set(50, 100, 500, 300, "New Tileset") as WindowForm;
            var newSet_Name = new TextBoxForm().Set(85, 5, 120, 25, "") as TextBoxForm;
            win_NewSet.body.Add(newSet_Name);
            var newSet_Label = new LabelForm().Set(5, 5, 35, 25, "SetName");

            var newSet_Create = new ButtonForm().Set(5, 245, 60, 25, "Create");

            win_NewSet.body.Add(newSet_Create);


            win_NewSet.body.Add(newSet_Label);


            void click_NewSet(int b)
            {

                SUI.Top = win_NewSet;

            }

            void click_Exit(int b)
            {

                Environment.Exit(1);

            }

            menu_TileSet.Menu.AddItem("New Set",click_NewSet);
            menu_TileSet.Menu.AddItem("Load Set");
            menu_TileSet.Menu.AddItem("Save Set");
            menu_TileSet.Menu.AddItem("Exit",click_Exit);

            



        }

        public override void UpdateState()
        {
            base.UpdateState();
            SUI.Update();
            Vivid.Texture.Texture2D.UpdateLoading();
        }

        public override void DrawState()
        {
            base.DrawState();
            SUI.Render();
        }

    }
}
