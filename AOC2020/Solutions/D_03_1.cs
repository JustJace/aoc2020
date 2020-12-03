using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2020.Solutions
{
    public class D_03_1 : Solver<int>
    {
        public override int Day => 3;
        public override int Part => 1;
        protected override string Filename => @"Inputs\D_03.input";

        protected override int GetAnswer(string input)
        {
            var map = input.As2DArray<char>();
            var dx = 3;
            var dy = 1;
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
