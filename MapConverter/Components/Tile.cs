using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapConverter.Components
{
    public class Tile
    {
        public int Elevation { get; set; }

        public TileType Type { get; set; }

        public TileStyle Style { get; set; }

        public int tileMapRef { get; set; }

    }
}
