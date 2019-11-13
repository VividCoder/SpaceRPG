using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceEngine.Map.TileSet
{
    public class TileSet
    {

        public List<Tile.Tile> Tiles
        {
            get;
                  
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public TileSet(string name)
        {

            Tiles = new List<Tile.Tile>();
            Name = name;

        }

        void AddTile(Tile.Tile tile)
        {

            Tiles.Add(tile);

        }

    }
}
