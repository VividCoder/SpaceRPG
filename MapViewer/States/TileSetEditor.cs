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
using SpaceEngine;
using SpaceEngine.Forms;
using SpaceEngine.Map;
namespace MapViewer.States
{

    public class TileSetEditor : VividState
    {

        public string ContentPath = "C:/Projects/GameInfo/";
        public SpaceEngine.Map.TileSet.TileSet CurSet = new SpaceEngine.Map.TileSet.TileSet("new set");
        public SpaceEngine.Map.Map CurSetMap;
        public SpaceEngine.Map.Layer.MapLayer CurSetLayer;
        public int setWidth = 16;
        public int setHeight = 16;
        public int setX, setY;

        public WindowForm CrTileSetEditor()
        {
            setX = 0;
            setY = 0;
            CurSetMap = new Map();

            var setLight = new Vivid.Scene.GraphLight();

            setLight.SetPos(120, 120);
            setLight.SetZ(40);
            setLight.Diffuse = new OpenTK.Vector3(1, 1, 1);
            setLight.Specular = new OpenTK.Vector3(1, 1, 1);
            setLight.Range = 1200;

            CurSetMap.AddLight(setLight);

            WindowForm set_Editor = new WindowForm().Set(200, 200, 700, 600, "TileSet") as WindowForm;

            ToolBarForm tools = new ToolBarForm().Set(0, 0, 700, 25) as ToolBarForm;

            

            set_Editor.body.Add(tools);

            MapViewForm tileSet_View = new MapViewForm(CurSetMap).Set(0,25,set_Editor.body.W,set_Editor.body.H-25) as MapViewForm;

            //CurSetMap = new Map(1);

            CurSetLayer = CurSetMap.AddLayer(new SpaceEngine.Map.Layer.MapLayer(setWidth,setHeight));



            set_Editor.body.Add(tileSet_View);

            tools.AddItem("Clear");
            var tile_Add = tools.AddItem("Add Tile");

            void click_AddTile()
            {

                var addReq = new RequestFileForm("Add Tile to set...",ContentPath);
                SUI.Top = addReq;

                addReq.Selected = (path) =>
                {

                    Console.WriteLine("Loading Tile:" + path);

                    var nTile = new SpaceEngine.Map.Tile.Tile(path);



                    //CurSetLayer.SetTile(setX, setY, nTile);
                    CurSetLayer.Fill(nTile);
                    setX++;
                    tileSet_View.UpdateGraph();

                    SUI.Top = null;

                };


            }

            tile_Add.Click = click_AddTile;


            return set_Editor;

        }

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

            ui_Root.Add(CrTileSetEditor());



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
