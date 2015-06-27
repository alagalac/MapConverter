using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapConverter.Components
{
    public enum TileType
    {
        None = 0,
        Unknown = 1,

        Flat = 2,

        TopWall = 3,
        RightWall = 4,
        BottomWall = 5,
        LeftWall = 6,

        TopLeftCorner = 7,
        TopRightCorner = 8,
        BottomRightCorner = 9,
        BottomLeftCorner = 10,

        TopLeftAngle = 11,
        TopRightAngle = 12,
        BottomRightAngle = 13,
        BottomLeftAngle = 14
    }
}
