using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapConverter.Components.MapProcessors
{
    public class DetermineTileTypes
    {

        public Map Process(Map map)
        {
            for (int row = 0; row < map.Height; row++)
            {
                for (int column = 0; column < map.Width; column++)
                {
                    var tile = map.Tiles[row, column];

                    var tester = new TypeTester(map, row, column);
                    tile.Type = tester.GetTileType();
                }
            }

            return map;
        }
    }
}
