using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using AOC2020.Structure;
using AOC2020.Search;

namespace AOC2020.Solutions
{
    public class D_24_2 : Solver<long>
    {
        public override int Day => 24;
        public override int Part => 2;
        protected override string Filename => @"Inputs\D_24.input";
        protected override long GetAnswer(string input)
        {
            var hash = SetInitialState(input);

            for (var i = 0; i < 100; i++)
            {
                hash = NextState(hash);
            }

            return hash.Count;
        }

        private HashSet<string> NextState(HashSet<string> hash)
        {
            var next = new HashSet<string>();

            foreach (var key in hash)
            {
                var (x, y, z) = FromKey(key);
                var neighbors = Neighbors(x, y, z);
                var black = neighbors.Count(n => hash.Contains(Key(n)));
                if (black == 1 || black == 2)
                {
                    next.Add(Key(x, y, z));
                }

                foreach (var neighbor in neighbors)
                {
                    if (hash.Contains(Key(neighbor))) continue;
                    
                    var whiteNeighbors = Neighbors(neighbor);
                    var whitesblack = whiteNeighbors.Count(n => hash.Contains(Key(n)));
                    if (whitesblack == 2)
                    {
                        next.Add(Key(neighbor));
                    }
                }
            }

            return next;
        }

        private List<(int, int, int)> Neighbors((int, int, int) tuple) => Neighbors(tuple.Item1, tuple.Item2, tuple.Item3);
        private List<(int, int, int)> Neighbors(int x, int y, int z)
        {
            var neighbors = new List<(int, int, int)>();

            neighbors.Add((x, y + 1, z - 1));
            neighbors.Add((x, y - 1, z + 1));
            neighbors.Add((x + 1, y, z - 1));
            neighbors.Add((x - 1, y, z + 1));
            neighbors.Add((x - 1, y + 1, z));
            neighbors.Add((x + 1, y - 1, z));

            return neighbors;
        }

        private (int, int, int) FromKey(string s)
        {
            var xyz = s.Split(',').Select(int.Parse).ToArray();
            return (xyz[0], xyz[1], xyz[2]);
        }
        private string Key(int x, int y, int z) => $"{x},{y},{z}";
        private string Key((int, int, int) tuple) => Key(tuple.Item1, tuple.Item2, tuple.Item3);

        private HashSet<string> SetInitialState(string input)
        {
            var hash = new HashSet<string>();
            foreach (var line in input.PerNewLine())
            {
                var (x, y, z) = LocateHexTile(line);
                var key = Key(x, y, z);
                if (hash.Contains(key)) hash.Remove(key);
                else hash.Add(key);
            }
            return hash;
        }

        private (int, int, int) LocateHexTile(string line)
        {
            int x = 0;
            int y = 0;
            int z = 0;

            foreach (var dir in Tokenize(line))
            {
                switch (dir)
                {
                    case "nw": y++; z--; break;
                    case "se": y--; z++; break;
                    case "ne": x++; z--; break;
                    case "sw": x--; z++; break;
                    case "e":  x++; y--; break;
                    case "w":  x--; y++; break;
                    default: throw new Exception("failed tokenize");
                }
            }
            return (x, y, z);
        }

        private List<string> Tokenize(string line)
        {
            var tokens = new List<string>();

            for (var i = 0; i < line.Length; i++)
            {
                var next = i + 1;
                switch (line[i])
                {
                    case 'n':
                        if (next < line.Length && line[next] == 'w')
                            tokens.Add("nw");
                        else if (next < line.Length && line[next] == 'e')
                            tokens.Add("ne");
                        else throw new Exception("failed token");
                        i++;
                        break;
                    case 's':
                        if (next < line.Length && line[next] == 'w')
                            tokens.Add("sw");
                        else if (next < line.Length && line[next] == 'e')
                            tokens.Add("se");
                        else throw new Exception("failed token");
                        i++;
                        break;
                    case 'w': tokens.Add("w"); break;
                    case 'e': tokens.Add("e"); break;
                    default: throw new Exception("failed token");
                }
            }

            return tokens;
        }
    }
}
