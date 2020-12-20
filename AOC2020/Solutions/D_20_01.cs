using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using AOC2020.Structure;
using AOC2020.Search;

namespace AOC2020.Solutions
{
    public class D_20_1 : Solver<long>
    {
        public override int Day => 20;
        public override int Part => 1;
        protected override string Filename => @"Inputs\D_20.input";
        protected override long GetAnswer(string input)
        {
            var tiles = ParseTiles(input);

            var image = new tile[145][];
            for (var r = 0; r < 145; r++)
            {
                image[r] = new tile[145];
            }

            var minr = 72;
            var maxr = 72;
            var minc = 72;
            var maxc = 72;

            image[minr][minc] = tiles.First().Value;
            tiles.Remove(tiles.First().Key);

            while (tiles.Any())
            {
                var tile = PlaceTile(image, tiles, out int r, out int c);
                image[r][c] = tile;
                tiles.Remove(tile.id);
                if (r < minr) minr = r;
                if (r > maxr) maxr = r;
                if (c < minc) minc = c;
                if (c > maxc) maxc = c;
            }

            return (long)image[minr][minc].id
                 * (long)image[minr][maxc].id
                 * (long)image[maxr][minc].id
                 * (long)image[maxr][maxc].id;
        }

        private tile PlaceTile(tile[][] image, Dictionary<int, tile> tiles, out int r, out int c)
        {
            r = 0; c = 0;
            for (var ir = 0; ir < image.Length; ir++)
            {
                for (var ic = 0; ic < image[ir].Length; ic++)
                {
                    if (image[ir][ic] == null) continue;

                    foreach (var tile in tiles.Values)
                    {
                        if (CanConnectAnyMutation(image[ir][ic], tile, out string direction))
                        {
                            switch (direction)
                            {
                                case "left":  r = ir; c = ic - 1; break;
                                case "right": r = ir; c = ic + 1; break;
                                case "up":    r = ir - 1; c = ic; break;
                                case "down":  r = ir + 1; c = ic; break;
                            }
                            
                            if (CanPlaceTile(image, r, c, tile))
                            {
                                return tile;
                            }
                        }
                    }
                }
            }

            throw new Exception("didn't find a tile to place");
        }

        private bool CanPlaceTile(tile[][] image, int r, int c, tile tile)
        {
            // left
            if (image[r][c - 1] != null)
            {
                if (!CanConnectDirection(tile, image[r][c-1], "left")) return false;
            }

            // right
            if (image[r][c + 1] != null)
            {
                if (!CanConnectDirection(tile, image[r][c+1], "right")) return false;
            }

            // up
            if (image[r - 1][c] != null)
            {
                if (!CanConnectDirection(tile, image[r - 1][c], "up")) return false;
            }

            // down
            if (image[r + 1][c] != null)
            {
                if (!CanConnectDirection(tile, image[r + 1][c], "down")) return false;
            }

            return true;
        }

        private bool CanConnectAnyMutation(tile placed, tile tile, out string direction)
        {
            // unchanged
            tile.reset();
            if (CanConnect(placed, tile, out direction)) return true;

            // just rotate
            tile.rotate90();
            if (CanConnect(placed, tile, out direction)) return true;

            tile.reset();
            tile.rotate180();
            if (CanConnect(placed, tile, out direction)) return true;

            tile.reset();
            tile.rotate270();
            if (CanConnect(placed, tile, out direction)) return true;

            // just flipx
            tile.reset();
            tile.flipX();
            if (CanConnect(placed, tile, out direction)) return true;

            // flipx then rotate
            tile.reset();
            tile.flipX();
            tile.rotate90();
            if (CanConnect(placed, tile, out direction)) return true;

            tile.reset();
            tile.flipX();
            tile.rotate180();
            if (CanConnect(placed, tile, out direction)) return true;

            tile.reset();
            tile.flipX();
            tile.rotate270();
            if (CanConnect(placed, tile, out direction)) return true;

            // rotate then flipx
            tile.reset();
            tile.rotate90();
            tile.flipX();
            if (CanConnect(placed, tile, out direction)) return true;

            tile.reset();
            tile.rotate180();
            tile.flipX();
            if (CanConnect(placed, tile, out direction)) return true;

            tile.reset();
            tile.rotate270();
            tile.flipX();
            if (CanConnect(placed, tile, out direction)) return true;

            // just flipy
            tile.reset();
            tile.flipY();
            if (CanConnect(placed, tile, out direction)) return true;

            // flipy then rotate
            tile.reset();
            tile.flipY();
            tile.rotate90();
            if (CanConnect(placed, tile, out direction)) return true;

            tile.reset();
            tile.flipY();
            tile.rotate180();
            if (CanConnect(placed, tile, out direction)) return true;

            tile.reset();
            tile.flipY();
            tile.rotate270();
            if (CanConnect(placed, tile, out direction)) return true;

            // rotate then flipy
            tile.reset();
            tile.rotate90();
            tile.flipY();
            if (CanConnect(placed, tile, out direction)) return true;

            tile.reset();
            tile.rotate180();
            tile.flipY();
            if (CanConnect(placed, tile, out direction)) return true;

            tile.reset();
            tile.rotate270();
            tile.flipY();
            if (CanConnect(placed, tile, out direction)) return true;

            return false;
        }

        private bool CanConnect(tile placed, tile tile, out string direction)
        {
            if (CanConnectDirection(placed, tile, "left")) { direction = "left"; return true; }
            if (CanConnectDirection(placed, tile, "right")) { direction = "right";  return true; }
            if (CanConnectDirection(placed, tile, "up")) { direction = "up"; return true; }
            if (CanConnectDirection(placed, tile, "down")) { direction = "down"; return true; }
            direction = "none"; return false;
        }

        private bool CanConnectDirection(tile placed, tile tile, string direction)
        {
            switch (direction)
            {
                case "left":  return placed.left() == tile.right();
                case "right": return placed.right() == tile.left();
                case "up":    return placed.top() == tile.bottom();
                case "down":  return placed.bottom() == tile.top();
                default: throw new Exception("unexpected direction");
            }
        }

        private Dictionary<int, tile> ParseTiles(string input)
        {
            var tiles = new Dictionary<int, tile>();

            foreach (var section in input.PerDoubleLine())
            {
                var lines = section.PerNewLine();
                var id = lines[0].Regex<int>(@"Tile (\d+):");

                var data = tile.emptyData(10);
                for (var r = 0; r < data.Length; r++)
                {
                    for (var c = 0; c < data.Length; c++)
                    {
                        data[r][c] = lines[r + 1][c];
                    }
                }

                tiles[id] = new tile(id, data);
            }

            return tiles;
        }

        class tile
        {
            public int id;
            public char[][] data;
            private char[][] original;

            public List<string> mutations = new List<string>();

            public tile(int id, char[][] data)
            {
                this.id = id; this.data = data; this.original = data;
            }

            public void reset()
            {
                data = original;
                mutations.Clear();
            }

            public string top() => new string(data[0]);
            public string bottom() => new string(data[data.Length - 1]);
            public string left() => new string(data.Select(r => r[0]).ToArray());
            public string right() => new string(data.Select(r => r[data.Length - 1]).ToArray());
 
            public void flipY()
            {
                var flipped = emptyData(data.Length);
                for (var r = 0; r < data.Length; r++)
                {
                    for (var c = 0; c < data.Length; c++)
                    {
                        flipped[data.Length - r - 1][c] = data[r][c];
                    }
                }

                data = flipped;

                mutations.Add("flipY");
            }

            public void flipX()
            {
                var flipped = emptyData(data.Length);
                for (var r = 0; r < data.Length; r++)
                {
                    for (var c = 0; c < data.Length; c++)
                    {
                        flipped[r][data.Length - c - 1] = data[r][c];
                    }
                }

                data = flipped;

                mutations.Add("flipX");
            }

            public void rotate90()
            {
                var rotated = emptyData(data.Length);
                for (var r = 0; r < data.Length; r++)
                {
                    for (var c = 0; c < data.Length; c++)
                    {
                        rotated[c][data.Length - r - 1] = data[r][c];
                    }
                }

                data = rotated;

                mutations.Add("rotate180");
            }

            // Equal to flipX and flipY
            public void rotate180()
            {
                var rotated = emptyData(data.Length);
                for (var r = 0; r < data.Length; r++)
                {
                    for (var c = 0; c < data.Length; c++)
                    {
                        rotated[data.Length - r - 1][data.Length - c - 1] = data[r][c];
                    }
                }

                data = rotated;

                mutations.Add("rotate180");
            }

            public void rotate270()
            {
                var rotated = emptyData(data.Length);
                for (var r = 0; r < data.Length; r++)
                {
                    for (var c = 0; c < data.Length; c++)
                    {
                        rotated[data.Length - c - 1][r] = data[r][c];
                    }
                }

                data = rotated;

                mutations.Add("rotate180");
            }

            public void print()
            {
                Console.WriteLine($"Tile {id}:");
                for (var r = 0; r < data.Length; r++)
                {
                    for (var c = 0; c < data.Length; c++)
                    {
                        Console.Write(data[r][c]);
                    }
                    Console.WriteLine();
                }
            }

            public static char[][] emptyData(int length)
            {
                var empty = new char[length][];
                for (var r = 0; r < length; r++)
                {
                    empty[r] = new char[length];
                }
                return empty;
            }
        }
    }
}
