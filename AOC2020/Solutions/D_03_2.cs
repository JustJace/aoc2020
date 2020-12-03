using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2020.Solutions
{
    public class D_03_2 : Solver<long>
    {
        public override int Day => 3;
        public override int Part => 2;
        protected override string Filename => @"Inputs\D_03.input";

        protected override long GetAnswer(string input)
        {
            var map = input.As2DArray<char>();
            return TreesViaSlope(map, 1, 1)
                 * TreesViaSlope(map, 3, 1)
                 * TreesViaSlope(map, 5, 1)
                 * TreesViaSlope(map, 7, 1)
                 * TreesViaSlope(map, 1, 2);
        }

        private long TreesViaSlope(char[][] map, int dx, int dy)
        {
            var positionX = dx;
            var positionY = dy;
            var trees = 0;

            while (positionY < map.Length)
            {
                if (positionX >= map[0].Length) positionX = positionX % map[0].Length;
                if (map[positionY][positionX] == '#') trees++;

                positionX += dx;
                positionY += dy;
            }

            return trees;
        }
    }
}
