using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MapConverter.Components
{
    public class AsciiMapReader
    {
        public Map ReadMap(string filepath, int x, int y)
        {
            StreamReader reader = File.OpenText(filepath);

            var ascii = ParseMap(reader);

            var map = AsciiMapToMap(ascii, x, y);

            return map;
        }

        private AsciiMap ParseMap(StreamReader reader)
        {
            string line;

            var map = new AsciiMap();

            line = reader.ReadLine();
            map.Columns = Convert.ToInt32(line.Substring(13, line.Length - 13));

            line = reader.ReadLine();
            map.Rows = Convert.ToInt32(line.Substring(13, line.Length - 13));

            line = reader.ReadLine();
            map.xllcorner = Convert.ToDecimal(line.Substring(13, line.Length - 13));

            line = reader.ReadLine();
            map.yllcorner = Convert.ToDecimal(line.Substring(13, line.Length - 13));

            line = reader.ReadLine();
            map.CellSize = Convert.ToDecimal(line.Substring(13, line.Length - 13));

            line = reader.ReadLine();
            map.NoDataValue = Convert.ToDecimal(line.Substring(13, line.Length - 13));

            map.Data = new decimal[map.Rows, map.Columns];

            var i = 0;
            while(!reader.EndOfStream)
            {
                line = reader.ReadLine();

                line = line.TrimStart(' '); // removing leading space.

                var heights = line.Split(' ');
                for (int j = 0; j < heights.Length; j++)
                {
                    map.Data[i, j] = Convert.ToDecimal(heights[j]);
                }

                i++;
            }

            return map;
        }

        private Map AsciiMapToMap(AsciiMap ascii, int x, int y)
        {
            var scaleFactor = 100;

            var map = new Map();
            map.NullHeight = Convert.ToInt32(Math.Floor(ascii.NoDataValue / scaleFactor));
            map.Height = y;
            map.Width = x;

            map.Tiles = new Tile[y, x];

            decimal xScaleFactor = ascii.Columns / x;
            decimal yScaleFactor = ascii.Rows / y;

            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    // for each item grab the interpolated value.

                    // x = the column
                    // y = the row

                    int itemX = Convert.ToInt32(Math.Floor(j * xScaleFactor));
                    int itemY = Convert.ToInt32(Math.Floor(i * yScaleFactor));

                    map.Tiles[i, j] = new Tile();

                    map.Tiles[i, j].Elevation = Convert.ToInt32(ascii.Data[itemY, itemX] / scaleFactor);
                }
            }

            return map;

        }
    }
}
