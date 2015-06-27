using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapConverter.Components.MapProcessors
{
    public class TypeTester
    {
        public readonly int[] Elevations;

        private readonly int NullElevation;

        private TileType Type;
        
        public TypeTester(Map map, int row, int column)
        {
            Elevations = new int[9];

            Elevations[0] = map.GetTopLeft(row, column).Elevation;
            Elevations[1] = map.GetTop(row, column).Elevation;
            Elevations[2] = map.GetTopRight(row, column).Elevation;
            Elevations[3] = map.GetLeft(row, column).Elevation;
            Elevations[4] = map.Tiles[row, column].Elevation;
            Elevations[5] = map.GetRight(row, column).Elevation;
            Elevations[6] = map.GetBottomLeft(row, column).Elevation;
            Elevations[7] = map.GetBottom(row, column).Elevation;
            Elevations[8] = map.GetBottomRight(row, column).Elevation;

            this.NullElevation = map.NullHeight;
        }

        public TileType GetTileType()
        {
            if(Elevations[4] == this.NullElevation)
            {
                return TileType.None;
            }

            if (IsTileFlat())
            {
                IsTileAngle();
                return this.Type;
            }

            if (IsTileWall())
            {
                return this.Type;
            }

            if (IsTileCorner())
            {
                return this.Type;
            }

            return TileType.Unknown;
            
        }

        public bool IsTileFlat()
        {
            var mid = Elevations[4];

            if(Elevations[1] >= mid && Elevations[3] >= mid && Elevations[5] >= mid && Elevations[7] >= mid)
            {
                this.Type = TileType.Flat;
                // now test if an angle...
                return true;
            }

            return false;
        }

        public bool IsTileWall()
        {
            var mid = Elevations[4];

            if(Elevations[1] >= mid && Elevations[3] >= mid && Elevations[5] >= mid)
            {
                this.Type = TileType.BottomWall;
                return true;
            }

            if (Elevations[1] >= mid && Elevations[3] >= mid && Elevations[7] >= mid)
            {
                this.Type = TileType.RightWall;
                return true;
            }

            if (Elevations[1] >= mid && Elevations[5] >= mid && Elevations[7] >= mid)
            {
                this.Type = TileType.LeftWall;
                return true;
            }

            if (Elevations[3] >= mid && Elevations[5] >= mid && Elevations[7] >= mid)
            {
                this.Type = TileType.TopWall;
                return true;
            }

            return false;
        }

        public bool IsTileAngle()
        {
            var mid = Elevations[4];

            if (Elevations[0] < mid)
            {
                this.Type = TileType.TopLeftAngle;
                return true;
            }

            if (Elevations[2] < mid)
            {
                this.Type = TileType.TopRightAngle;
                return true;
            }

            if (Elevations[6] < mid)
            {
                this.Type = TileType.BottomLeftAngle;
                return true;
            }

            if (Elevations[8] < mid)
            {
                this.Type = TileType.BottomRightAngle;
                return true;
            }

            return false;
        }

        public bool IsTileCorner()
        {
            var mid = Elevations[4];

            if(Elevations[1] >= mid && Elevations[3] >= mid)
            {
                this.Type = TileType.BottomRightCorner;
                return true;
            }

            if (Elevations[1] >= mid && Elevations[5] >= mid)
            {
                this.Type = TileType.BottomLeftCorner;
                return true;
            }

            if (Elevations[3] >= mid && Elevations[7] >= mid)
            {
                this.Type = TileType.TopRightCorner;
                return true;
            }

            if (Elevations[5] >= mid && Elevations[7] >= mid)
            {
                this.Type = TileType.TopLeftCorner;
                return true;
            }

            return false;
        }
    }
}
