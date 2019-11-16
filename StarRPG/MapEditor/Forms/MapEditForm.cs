﻿using System;
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
    public enum EditMode
    {
        Paste,Fill,SmartFill,Rect,Oval,Erase,Clear
    }
    public class MapEditForm : WindowForm
    {
        public EditMode Mode = EditMode.Paste;
        EditMode lMode = EditMode.Paste;
        public TabForm Tab = null;
        public MapViewForm View;
        public SpaceEngine.Map.Map CurMap;
        public List<MapLayer> Layers = new List<MapLayer>();
        public MapLayer CurLayer;
        public Vivid.Scene.GraphNode ONode;
        public Tile CurTile;

        public int LayerNum = 0;
        bool MouseIn = false;
        int lx, ly;
        bool MoveCam = false;
        bool zoomCam = false;
        bool rotCam = false;
        public int EditZ = 0;
        public MapEditForm()
        {

            var layDown = new ButtonForm().Set(140, -20, 30, 20, "\\/");
            var layUp = new ButtonForm().Set(240, -20,30, 20, "/\\");
            var lay = new TextBoxForm().Set(175, -20, 70, 20, "0");
                body.Add(layDown);
            body.Add(layUp);
            body.Add(lay);

            layDown.Click = (b) =>
              {
                  EditZ--;
                  if (EditZ < 0) EditZ = 0;
                  lay.Text = EditZ.ToString();
              };
            layUp.Click = (b) =>
            {
                EditZ++;
                if (EditZ >= View.Map.Layers.Count)
                {
                    EditZ = View.Map.Layers.Count - 1;
                };
                lay.Text = EditZ.ToString();
            };
            Tab = new TabForm();

            CurMap = new Map();
            for(int i = 0; i < 4; i++)
            {

                Layers.Add(new MapLayer(6, 6));
                CurMap.AddLayer(Layers[i]);
            }
            //CurMap.AddLayer(layer[0]);

            CurLayer = Layers[0];

            CurTile = null;


            View = new MapViewForm(CurMap);

            var l1 = new Vivid.Scene.GraphLight();

            View.Map.AddLight(l1);

            LabelForm cLab = null;
            ; var TView = View;

            float r = 0;
            Update = () =>

            {

                if (lMode != Mode)
                {
                    updateModeLabel();
                    lMode = Mode;
                }
                TView.Map.Lights[0].SetPos(64,0);
                TView.Map.Lights[0].Range = 250;

              
            };

            void updateModeLabel()
            {

                if (cLab != null)
                {
                    View.Forms.Remove(cLab);
                }
                cLab = new LabelForm().Set(2, -23, 30, 20, "Mode:Paste") as LabelForm;

                View.Add(cLab);
            }

            updateModeLabel();

            body.Add(View);

          

            TView.MouseDown = (b) =>
            {
                if (b == 2)
                {
                    rotCam = true;
                    return;
                }
                if (b == 1)
                {
                    MoveCam = true;
                    return;
                }
                MouseIn = true;
                var hz = ONode; // TView.Graph.Pick(lx, ly);

                if (hz != null)
                {
                   
                }

               

            };
            TView.MouseWheelMoved = (z) =>
            {

                TView.Graph.Z += ((z * 0.1f)*TView.Graph.Z);

            };
            TView.MouseUp = (b) =>
            {
                if (b == 2)
                {
                    rotCam = false;
                    return;
                }
                if (b == 1)
                {
                    MoveCam = false;
                    return;
                }
                MouseIn = false;
            };

            TView.MouseMove = (x, y, dx, dy) =>
            {
                if (rotCam)
                {
                    TView.Graph.Rot += dx;
                }
                if (MoveCam)
                {
                    TView.Graph.Move(-dx,-dy);
                    //TView.Graph.X -= dx;
                   // TView.Graph.Y -= dy;

                }

                lx = x;
                ly = y;
                if (TView.Graph != null)
                {
                    var node = TView.Graph.Pick(x, y);

                    if (node != null)
                    {
                        ONode = node;

                        if (MouseIn)
                        {
                            switch (Mode)
                            {
                                case EditMode.Paste:
                                    TView.Map.Layers[EditZ].SetTile(node.TileX, node.TileY, TileBrowser.ActiveTile);
                                    //TView.UpdateGraph();
                                    break;
                                case EditMode.Fill:
                                    TView.Map.Layers[EditZ].Fill(TileBrowser.ActiveTile);
                                    break;
                            }
                        }

                        var tView = TView;
                        TView.Map.HL.Clear();
                        TView.Map.HighlightTile(node.TileX, node.TileY);
                        TView.UpdateGraph();

                      //  TView.Graph.X = -32 + TView.W / 2;
                       // TView.Graph.Y = -32 + TView.H / 2;
                        // Console.WriteLine("MX:" + x + " MY:" + y);
                    }
                    else
                    {

                        ClearHL(TView);

                    }
                }
            };


            AfterSet = () =>
            {

                Tab.W = W;
                Tab.H = body.H;
                View.Set(0, 0, body.W, body.H);
                View.UpdateGraph();

            };

          

        }

        private void ClearHL(MapViewForm tileSet_View)
        {
            //var tView = Map;

            if (View.Graph != null)
            {

                if (View.Map.HL.Count > 0)
                {

                    View.Map.HL.Clear();
                    tileSet_View.UpdateGraph();
                    //tileSet_View.Graph.X = -32 + tileSet_View.W / 2;
                    //tileSet_View.Graph.Y = -32 + tileSet_View.H / 2;


                }

            }
        }

    }

}
