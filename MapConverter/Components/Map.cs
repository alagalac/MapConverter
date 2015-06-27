using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapConverter.Components
{
    public class Map
    {
        public int NullHeight { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public Tile[,] Tiles { get; set; }

        public Tile GetLeft(int row, int column)
        {
            if (column == 0)
            {
                return Tiles[row, column];
            }

            return Tiles[row, column - 1];
        }

        public Tile GetRight(int row, int column)
        {
            if (column == Width - 1)
            {
                return Tiles[row, column];
            }

            return Tiles[row, column + 1];
        }

        public Tile GetTop(int row, int column)
        {
            if (row == 0)
            {
                return Tiles[row, column];
            }

            return Tiles[row - 1, column];
        }

        public Tile GetBottom(int row, int column)
        {
            if (row == Height - 1)
            {
                return Tiles[row, column];
            }

            return Tiles[row + 1, column];
        }

        public Tile GetTopLeft(int row, int column)
        {
            if (row == 0 || column == 0)
            {
                return Tiles[row, column];
            }

            return Tiles[row - 1, column - 1];
        }

        public Tile GetTopRight(int row, int column)
        {
            if (row == 0 || column == Width - 1)
            {
                return Tiles[row, column];
            }

            return Tiles[row - 1, column + 1];
        }

        public Tile GetBottomRight(int row, int column)
        {
            if (row == Height - 1 || column == Width - 1)
            {
                return Tiles[row, column];
            }

            return Tiles[row + 1, column + 1];
        }

        public Tile GetBottomLeft(int row, int column)
        {
            if (row == Height - 1 || column == 0)
            {
                return Tiles[row, column];
            }

            return Tiles[row + 1, column - 1];
        }
    }
}
