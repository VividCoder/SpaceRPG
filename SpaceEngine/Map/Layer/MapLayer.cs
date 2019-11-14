using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceEngine.Map.Layer
{
    public class MapLayer
    {

        public Tile.Tile[,] Tiles
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

        public MapLayer(int width,int height)
        {

            Tiles = new Tile.Tile[width, height];
            Width = width;
            Height = height;
            for(int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    Tiles[x, y] = null;
                }
            }
        }

        public void SetTile(int x,int y,Tile.Tile tile)
        {

            Tiles[x, y] = tile;

        }

        public Tile.Tile GetTile(int x,int y)
        {

            return Tiles[x, y];

        }

    }
}
