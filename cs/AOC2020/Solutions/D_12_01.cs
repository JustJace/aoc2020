using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using AOC2020.Structure;
using AOC2020.Search;

namespace AOC2020.Solutions
{
    public class D_012_1 : Solver<int>
    {
        public override int Day => 12;
        public override int Part => 1;
        protected override string Filename => @"Inputs\D_12.input";
        protected override int GetAnswer(string input)
        {
            var bearing = 'E';
            var x = 0;
            var y = 0;
            var rotationMatrix = new Dictionary<char, Dictionary<int, char>>()
            {
                ['N'] = new Dictionary<int, char>() { [90] = 'W', [180] = 'S', [270] = 'E' },
                ['S'] = new Dictionary<int, char>() { [90] = 'E', [180] = 'N', [270] = 'W' },
                ['E'] = new Dictionary<int, char>() { [90] = 'N', [180] = 'W', [270] = 'S' },
                ['W'] = new Dictionary<int, char>() { [90] = 'S', [180] = 'E', [270] = 'N' },
            };

            foreach (var instructions in input.Split(Environment.NewLine))
            {
                var (action, amount) = instructions.Regex<char, int>("(.)([0-9]+)");
                switch (action)
                {
                    case 'N':
                    case 'S':
                    case 'W':
                    case 'E': (x, y) = CardinalTranslate(action, amount, x, y); break;
                    case 'F': (x, y) = CardinalTranslate(bearing, amount, x, y); break;
                    case 'L': bearing = rotationMatrix[bearing][amount]; break;
                    case 'R': bearing = rotationMatrix[bearing][360 - amount]; break;
                }
            }

            return Math.Abs(x) + Math.Abs(y);
        }

        private (int, int) CardinalTranslate(char direction, int amount, int x, int y)
        {
            switch (direction)
            {
                case 'N': y += amount; break;
                case 'S': y -= amount; break;
                case 'W': x -= amount; break;
                case 'E': x += amount; break;
            }

            return (x, y);
        }
    }
}
