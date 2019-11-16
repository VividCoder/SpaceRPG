﻿namespace Vivid.Scene
{
    public class GraphSprite : GraphNode
    {
     
        public GraphSprite(Tex.Tex2D img, int w = -1, int h = -1)
        {
            CastShadow = false;
            ImgFrame = img;
            if (w == -1)
            {
                W = ImgFrame.Width;
            }
            else
            {
                W = w;
            }
            if (h == -1)
            {
                H = ImgFrame.Height;
            }
            else
            {
                H = h;
            }
        }

        public GraphSprite(string path, int w = -1, int h = -1)
        {
            CastShadow = false;
            ImgFrame = new Tex.Tex2D(path, true);
            if (w == -1)
            {
                W = ImgFrame.Width;
            }
            else
            {
                W = w;
            }
            if (h == -1)
            {
                H = ImgFrame.Height;
            }
            else
            {
                H = h;
            }
        }
    }
}