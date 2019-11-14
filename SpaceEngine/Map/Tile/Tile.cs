using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.Tex;
namespace SpaceEngine.Map.Tile
{
    public class Tile
    {

        public int X
        {
            get;
            set;
        }

        public int Y
        {
            get;
            set;
        }

        public int Width
        {
            get;
            set;
        }

        public int Height
        {
            get;
            set;
        }

        public string ImagePath
        {
            get;
            set;
        }

        public Tex2D Image
        {
            get;
            set;
        }
            
        public Tile(string imagePath)
        {
            ImagePath = imagePath;
            Image = new Tex2D(imagePath, true);
        }

        public Tile(Tex2D tex)
        {

            Image = tex;

        }

    }
}
