using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapConverter.Components.MapProcessors
{
    public class RemoveLoneBumps
    {
        // Smooth our any line tiles which have a different height. Should never have an instance of 1, 2, 1.
        public Map Process(Map map)
        {

            var successH = false;
            var successV = false;

            var count = 0;
            while (!successH && !successV)
            {
                successH = ProcessHorizontal(ref map);
                successV = ProcessVertical(ref map);
                count++;
                if (count > 2)
                {
                    break;
                }
            }

            return map;
        }

        private bool ProcessHorizontal(ref Map map)
        {
            var success = true;
            for (int row = 0; row < map.Height; row++)
            {
                for (int column = 0; column < map.Width; column++)
                {
                    var tile = map.Tiles[row, column];

                    var left = map.GetLeft(row, column);
                    var right = map.GetRight(row, column);

                    if (right.Elevation < tile.Elevation && left.Elevation < tile.Elevation)
                    {
                        // must deal with null heights.

                        if(right.Elevation == map.NullHeight)
                        {
                            tile.Elevation = (left.Elevation == map.NullHeight) ? map.NullHeight : left.Elevation;
                        }
                        else if (left.Elevation == map.NullHeight)
                        {
                            tile.Elevation = right.Elevation;
                        }
                        else
                        {
                            tile.Elevation = Convert.ToInt32(Math.Floor((right.Elevation + left.Elevation) / 2.0));
                        }

                        success = false;
                    }

                    if (right.Elevation > tile.Elevation && left.Elevation > tile.Elevation)
                    {
                        tile.Elevation = Convert.ToInt32(Math.Ceiling((right.Elevation + left.Elevation) / 2.0));
                        success = false;
                    }
                }
            }

            return success;
        }

        private bool ProcessVertical(ref Map map)
        {
            var success = true;
            for (int column = 0; column < map.Width; column++)
            {
                for (int row = 0; row < map.Width; row++)
                {
                    var tile = map.Tiles[row, column];

                    var top = map.GetTop(row, column);
                    var bottom = map.GetBottom(row, column);

                    if (top.Elevation < tile.Elevation && bottom.Elevation < tile.Elevation)
                    {
                        // must deal with null heights.

                        if (top.Elevation == map.NullHeight)
                        {
                            tile.Elevation = (bottom.Elevation == map.NullHeight) ? map.NullHeight : bottom.Elevation;
                        }
                        else if (bottom.Elevation == map.NullHeight)
                        {
                            tile.Elevation = top.Elevation;
                        }
                        else
                        {
                            tile.Elevation = Convert.ToInt32(Math.Floor((top.Elevation + bottom.Elevation) / 2.0));
                        }

                        success = false;
                    }

                    if (top.Elevation > tile.Elevation && bottom.Elevation > tile.Elevation)
                    {
                        tile.Elevation = Convert.ToInt32(Math.Ceiling((top.Elevation + bottom.Elevation) / 2.0));
                        success = false;
                    }
                }
            }

            return success;
        }
    }
}
