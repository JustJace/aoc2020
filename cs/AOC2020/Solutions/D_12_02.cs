using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using AOC2020.Structure;
using AOC2020.Search;

namespace AOC2020.Solutions
{
    public class D_012_2 : Solver<int>
    {
        public override int Day => 12;
        public override int Part => 2;
        protected override string Filename => @"Inputs\D_12.input";
        protected override int GetAnswer(string input)
        {
            var sx = 0;
            var sy = 0;

            var wx = 10;
            var wy = 1;

            var rotationMatrix = new Dictionary<int, Func<int, int, (int, int)>>()
            {
                [90]  = (x, y) => (-y,  x),
                [180] = (x, y) => (-x, -y),
                [270] = (x, y) => ( y, -x)
            };

            foreach (var instruction in input.PerNewLine())
            {
                var (action, amount) = instruction.Regex<char, int>("(.)([0-9]+)");
                switch (action)
                {
                    case 'N': wy += amount; break;
                    case 'S': wy -= amount; break;
                    case 'W': wx -= amount; break;
                    case 'E': wx += amount; break;
                    case 'F': sx += amount * wx; sy += amount * wy; break;
                    case 'L': (wx, wy) = rotationMatrix[amount](wx, wy); break;
                    case 'R': (wx, wy) = rotationMatrix[360 - amount](wx, wy); break;
                }
            }

            return Math.Abs(sx) + Math.Abs(sy);
        }
    }
}
