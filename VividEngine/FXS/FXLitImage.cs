﻿using Vivid.FX;
using Vivid.Scene;
using Vivid.Util;

namespace Vivid.FXS
{
    public class FXLitImage : VEffect
    {
        public GraphLight Light
        {
            get;
            set;
        }

        public SceneGraph2D Graph
        {
            get;
            set;
        }

        public FXLitImage() : base("", "data/Shader/LitImageVS.glsl", "data/Shader/LitImageFS1.glsl")
        {
        }

        public override void SetPars()
        {
            float sw, sh;
            sw = Vivid.App.AppInfo.RW;
            sh = Vivid.App.AppInfo.RH;
            float px, py;

            // px = Light.X + Graph.X; py = Light.Y + Graph.Y;
            px = Light.X * Graph.Z;
            py = Light.Y * Graph.Z;

            //px = (sw / 2) + px;
            //py = (sh / 2) + py;

            px = px - Graph.X * Graph.Z;
            py = py - Graph.Y * Graph.Z;

            OpenTK.Vector2 res = Maths.Rotate(px, py, Graph.Rot, 1.0f);

            res = Maths.Push(res, sw / 2, sh / 2);

            SetTex("tR", 0);
            
            SetVec3("lPos", new OpenTK.Vector3(res.X, res.Y, 0));
            SetVec3("lDif", Light.Diffuse);
            SetVec3("lSpec", Light.Specular);
            SetFloat("lShiny", Light.Shiny);
            SetFloat("lRange", Light.Range * Graph.Z);
            SetFloat("sWidth", Vivid.App.AppInfo.RW);
            SetFloat("sHeight", Vivid.App.AppInfo.RH);
                     
   
    SetMat("proj", OpenTK.Matrix4.CreateOrthographicOffCenter(0, Vivid.App.AppInfo.RW, Vivid.App.AppInfo.RH, 0, -1, 1000));


        }
    }
}