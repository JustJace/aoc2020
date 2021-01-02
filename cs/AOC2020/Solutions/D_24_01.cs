using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using AOC2020.Structure;
using AOC2020.Search;

namespace AOC2020.Solutions
{
    public class D_24_1 : Solver<long>
    {
        public override int Day => 24;
        public override int Part => 1;
        protected override string Filename => @"Inputs\D_24.input";
        protected override long GetAnswer(string input)
        {
            var hash = new HashSet<string>();
            foreach (var line in input.PerNewLine())
            {
                var (x, y, z) = LocateHexTile(line);
                var key = $"{x},{y},{z}";
                if (hash.Contains(key)) hash.Remove(key);
                else hash.Add(key);
            }
            return hash.Count;
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
                    case "w": x--; y++; break;
                    case "e": x++; y--; break;
                    default: throw new Exception("failed tokenize");
                }
            }
            return (x,y,z);
        }

        private List<string> Tokenize(string line)
        {
            var tokens = new List<string>();

            for (var i = 0; i < line.Length; i++)
            {
                var next = i+1;
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