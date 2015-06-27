using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapConverter.Components
{
    public class AsciiMap
    {
        public int Columns { get; set; }

        public int Rows { get; set; }

        public decimal xllcorner { get; set; }

        public decimal yllcorner { get; set; }

        public decimal CellSize { get; set; }

        public decimal NoDataValue { get; set; }

        public decimal[,] Data { get; set; }
    }
}
