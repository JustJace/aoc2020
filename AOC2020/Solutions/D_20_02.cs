using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using AOC2020.Structure;
using AOC2020.Search;

namespace AOC2020.Solutions
{
    public class D_20_2 : Solver<long>
    {
        public override int Day => 20;
        public override int Part => 2;
        protected override string Filename => @"Inputs\D_20.input";
        private readonly string[] _MoNsTeR_ = new string[] 
        {
            "                  # ",
            "#    ##    ##    ###",
            " #  #  #  #  #  #   "
        };

        protected override long GetAnswer(string input)
        {
            var tiles = ParseTiles(input);
            var aligned = AlignTiles(tiles);
            var trimmed = TrimBorders(aligned);
            var joined = JoinTiles(trimmed);
            var oriented = OrientForMonsters(joined);
            var highlighted = HighlightMonsters(oriented);
            return Roughness(highlighted);
        }

        private void Print(tile[][] tiles)
        {
            for (var tr = 0; tr < tiles.Length; tr++)
            {
                for (var r = 0; r < 10; r++)
                {
                    for (var tc = 0; tc < tiles[tr].Length; tc++)
                    {
                        for (var c = 0; c < 10; c++)
                        {
                            if (tiles[tr][tc] != null)
                                Console.Write(tiles[tr][tc].data[r][c]);
                            else
                                Console.Write(" ");
                        }

                        Console.Write(" ");
                    }

                    Console.WriteLine();
                }

                Console.WriteLine();
                Console.WriteLine();
            }
        }

        private long CornerIdMultiply(tile[][] tiles)
        {
            return (long)tiles[0][0].id
                 * (long)tiles[0][tiles[0].Length - 1].id
                 * (long)tiles[tiles.Length - 1][0].id
                 * (long)tiles[tiles.Length - 1][tiles[0].Length - 1].id;
        }

        private long Roughness(tile tile)
        {
            var roughness = 0;
            for (var r = 0; r < tile.data.Length; r++)
            {
                for (var c = 0; c < tile.data[r].Length; c++)
                {
                    if (tile.data[r][c] == '#') roughness++;
                }
            }
            return roughness;
        }

        private tile HighlightMonsters(tile tile)
        {
            while (FindMonster(tile, out int mr, out int mc))
            {
                for (var r = 0; r < _MoNsTeR_.Length; r++)
                {
                    for (var c = 0; c < _MoNsTeR_[r].Length; c++)
                    {
                        if (_MoNsTeR_[r][c] == '#')
                            tile.data[mr + r][mc + c] = 'O';
                    }
                }
            }

            return tile;
        }

        private tile OrientForMonsters(tile tile)
        {
             // unchanged
            tile.reset();
            if (FindMonster(tile, out _, out _)) return tile;

            // just rotate
            tile.reset();
            tile.rotate90();
            if (FindMonster(tile, out _, out _)) return tile;

            tile.reset();
            tile.rotate180();
            if (FindMonster(tile, out _, out _)) return tile;

            tile.reset();
            tile.rotate270();
            if (FindMonster(tile, out _, out _)) return tile;

            // just flipx
            tile.reset();
            tile.flipX();
            if (FindMonster(tile, out _, out _)) return tile;

            // flipx then rotate
            tile.reset();
            tile.flipX();
            tile.rotate90();
            if (FindMonster(tile, out _, out _)) return tile;

            tile.reset();
            tile.flipX();
            tile.rotate180();
            if (FindMonster(tile, out _, out _)) return tile;

            tile.reset();
            tile.flipX();
            tile.rotate270();
            if (FindMonster(tile, out _, out _)) return tile;

            // rotate then flipx
            tile.reset();
            tile.rotate90();
            tile.flipX();
            if (FindMonster(tile, out _, out _)) return tile;

            tile.reset();
            tile.rotate180();
            tile.flipX();
            if (FindMonster(tile, out _, out _)) return tile;

            tile.reset();
            tile.rotate270();
            tile.flipX();
            if (FindMonster(tile, out _, out _)) return tile;

            // just flipy
            tile.reset();
            tile.flipY();
            if (FindMonster(tile, out _, out _)) return tile;

            // flipy then rotate
            tile.reset();
            tile.flipY();
            tile.rotate90();
            if (FindMonster(tile, out _, out _)) return tile;

            tile.reset();
            tile.flipY();
            tile.rotate180();
            if (FindMonster(tile, out _, out _)) return tile;

            tile.reset();
            tile.flipY();
            tile.rotate270();
            if (FindMonster(tile, out _, out _)) return tile;

            // rotate then flipy
            tile.reset();
            tile.rotate90();
            tile.flipY();
            if (FindMonster(tile, out _, out _)) return tile;

            tile.reset();
            tile.rotate180();
            tile.flipY();
            if (FindMonster(tile, out _, out _)) return tile;

            tile.reset();
            tile.rotate270();
            tile.flipY();
            if (FindMonster(tile, out _, out _)) return tile;

            throw new Exception("no orientation has a monster");
        }

        private bool FindMonster(tile tile, out int mr, out int mc)
        {
            mr = 0; mc = 0;

            for (mr = 0; mr < tile.data.Length - _MoNsTeR_.Length + 1; mr++)
            {
                for (mc = 0; mc < tile.data[mr].Length - _MoNsTeR_[0].Length + 1; mc++)
                {
                    var isMonster = true;
                 
                    for (var r = 0; r < _MoNsTeR_.Length; r++)
                    {
                        for (var c = 0; c < _MoNsTeR_[r].Length; c++)
                        {
                            if (_MoNsTeR_[r][c] == '#')
                            {
                                isMonster = tile.data[mr + r][mc + c] == '#';
                            }

                            if (!isMonster) break;
                        }

                        if (!isMonster) break;
                    }

                    if (isMonster) return true;
                }
            }

            return false;
        }

        private tile JoinTiles(tile[][] tiles)
        {
            var data = tile.emptyData(tiles[0][0].data.Length * tiles.Length);

            for (var tr = 0; tr < tiles.Length; tr++)
            {
                for (var tc = 0; tc < tiles[tr].Length; tc++)
                {
                    var tile = tiles[tr][tc];

                    for (var r = 0; r < tile.data.Length; r++)
                    {
                        for (var c = 0; c < tile.data[r].Length; c++)
                        {
                            data[tr*tile.data.Length + r][tc*tile.data.Length + c] = tile.data[r][c];
                        }
                    }
                }
            }

            return new tile(0, data);
        }

        private tile[][] TrimBorders(tile[][] tiles)
        {
            var removed = new tile[tiles.Length][];
            for (var r = 0; r < tiles.Length; r++)
            {
                removed[r] = new tile[tiles[r].Length];

                for (var c = 0; c < tiles[r].Length; c++)
                {
                    removed[r][c] = TrimBorder(tiles[r][c]);
                }
            }
            return removed;
        }

        private tile TrimBorder(tile tile)
        {
            var data = tile.emptyData(tile.data.Length - 2);

            for (var r = 1; r < tile.data.Length - 1; r++)
            {
                for (var c = 1; c < tile.data[r].Length - 1; c++)
                {
                    data[r - 1][c - 1] = tile.data[r][c];
                }
            }
            
            return new tile(tile.id, data);
        }

        private tile[][] AlignTiles(Dictionary<int, tile> tiles)
        {
            var image = new tile[25][];
            for (var r = 0; r < 25; r++)
            {
                image[r] = new tile[25];
            }

            var minr = 12;
            var maxr = 12;
            var minc = 12;
            var maxc = 12;

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

            var length = maxr - minr + 1;
            var cropped = new tile[length][];

            for (var r = 0; r < length; r++)
            {
                cropped[r] = new tile[length];
                for (var c = 0; c < length; c++)
                {
                    cropped[r][c] = image[minr + r][minc + c];
                }
            }
        
            return cropped;
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

            private List<string> _mutations = new List<string>();
            public string mutations => _mutations.Count == 0 ? "none" : _mutations.Aggregate((s1, s2) => s1 + ", " + s2);

            public tile(int id, char[][] data)
            {
                this.id = id; this.data = data; this.original = data;
            }

            public tile(int id, string data)
                : this(id, data.As2DArray<char>())
            {
            }

            public void reset()
            {
                data = original;
                _mutations.Clear();
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

                _mutations.Add("flipY");
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

                _mutations.Add("flipX");
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

                _mutations.Add("rotate90");
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

                _mutations.Add("rotate180");
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

                _mutations.Add("rotate270");
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
