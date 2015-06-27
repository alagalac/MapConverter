using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MapConverter.Components
{
    public class MapExporter
    {

        public void Export(Map map, string filename)
        {
            ExportToCsv(map, filename);
        }

        private void ExportToCsv(Map map, string filename)
        {
            StringBuilder sb = new StringBuilder();

            for (int row = 0; row < map.Height; row++)
            {
                for (int column = 0; column < map.Width; column++)
                {
                    sb.Append((int)map.Tiles[row, column].Type);

                    if (column != map.Width - 1 || row != map.Height - 1)
                    {
                        sb.Append(",");
                    }
                    
                }
                sb.AppendLine();
            }

            File.WriteAllText(filename, sb.ToString());
        }
    }
}
