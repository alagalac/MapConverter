using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapConverter.Components.MapProcessors
{
    public class NormaliseElevation
    {
        public Map Process(Map map)
        {

            map = ProcessHorizontal(map);
            //map = ProcessVertical(map);

            return map;
        }

        private Map ProcessHorizontal(Map map)
        {
            for (int i = 0; i < map.Height; i++)
            {
                int height = 0;
                int? prevHeight = null;

                for (int j = 0; j < map.Width; j++)
                {
                    var tile = map.Tiles[i, j];
                    if (tile.Elevation != map.NullHeight)
                    {
                        if (!prevHeight.HasValue)
                        {
                            prevHeight = tile.Elevation;
                            tile.Elevation = height;
                            continue;
                        }

                        // make sure we only differ from height by at most 1.
                        if (tile.Elevation > prevHeight)
                        {
                            height += 1;
                            prevHeight = tile.Elevation;
                            tile.Elevation = height;
                        }
                        else if (tile.Elevation < prevHeight)
                        {
                            height -= 1;
                            prevHeight = tile.Elevation;
                            tile.Elevation = height;
                        }
                    }
                    else
                    {
                        height = 0;
                        prevHeight = null;
                    }
                }
            }

            return map;
        }

        private Map ProcessVertical(Map map)
        {
            for (int i = 0; i < map.Width; i++)
            {
                int height = 0;
                int? prevHeight = null;
                for (int j = 0; j < map.Height; j++)
                {
                    var tile = map.Tiles[j, i];

                    if (tile.Elevation != map.NullHeight)
                    {
                        if (!prevHeight.HasValue)
                        {
                            prevHeight = tile.Elevation;
                            tile.Elevation = height;
                            continue;
                        }

                        // make sure we only differ from height by at most 1.
                        if (tile.Elevation > prevHeight)
                        {
                            height += 1;
                            prevHeight = tile.Elevation;
                            tile.Elevation = height;
                        }
                        else if (tile.Elevation < prevHeight)
                        {
                            height -= 1;
                            prevHeight = tile.Elevation;
                            tile.Elevation = height;
                        }
                    }
                    else
                    {
                        prevHeight = null;
                        height = 0;
                    }
                }
            }

            return map;
        }
    }
}
