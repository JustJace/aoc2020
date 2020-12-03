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
            var posX = dx;
            var posY = dy;
            var trees = 0;

            while (posY < map.Length)
            {
                if (map[posY][posX % map[0].Length] == '#') trees++;

                posX += dx;
                posY += dy;
            }

            return trees;
        }
    }
}
