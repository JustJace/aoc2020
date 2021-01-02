using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using AOC2020.Structure;
using AOC2020.Search;

namespace AOC2020.Solutions
{
    public class D_011_1 : Solver<int>
    {
        public override int Day => 11;
        public override int Part => 1;
        protected override string Filename => @"Inputs\D_11.input";
        protected override int GetAnswer(string input)
        {
            var previous = new CoordinateMap<char>(input.As2DArray<char>(), c => c);
            var hasSeatChanged = true;
            var nextMapString = "";

            while (hasSeatChanged)
            {
                hasSeatChanged = false;
                nextMapString = "";

                for (var r = 0; r < previous.Height; r++)
                {
                    for (var c = 0; c < previous.Width; c++)
                    {
                        var current = previous.Locate(c, r);
                        if (current.Data == '.') { nextMapString += '.';  continue; }

                        var neighbors = CountNeighbors(current);
                        if (current.Data == 'L' && neighbors == 0) { hasSeatChanged = true; nextMapString += '#'; }
                        else if (current.Data == '#' && neighbors >= 4) { hasSeatChanged = true; nextMapString += 'L'; }
                        else { nextMapString += current.Data; }
                    }

                    nextMapString += Environment.NewLine;
                }

                previous = new CoordinateMap<char>(nextMapString.TrimEnd().As2DArray<char>(), c => c);
            }

            return nextMapString.Count(c => c == '#');
        }
        private int CountNeighbors(CoordinateNode<char> node) =>  node.Neighbors.Count(n => n.Data == '#');
    }
}
