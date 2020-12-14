using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using AOC2020.Structure;
using AOC2020.Search;

namespace AOC2020.Solutions
{
    public class D_013_1 : Solver<int>
    {
        public override int Day => 13;
        public override int Part => 1;
        protected override string Filename => @"Inputs\D_13.input";
        protected override int GetAnswer(string input)
        {
            var lines = input.PerNewLine();
            var start = int.Parse(lines[0]);
            var earliest = int.MaxValue;
            var busId = 0;
            var busses = lines[1].Split(',').Where(b => b != "x").Select(int.Parse).ToList();

            foreach (var bus in busses)
            {
                var nextDeparture = 0;
                var mult = 1;

                while (nextDeparture < start)
                {
                    nextDeparture = bus * mult++;
                }

                if (nextDeparture < earliest)
                {
                    earliest = nextDeparture;
                    busId = bus;
                }
            }

            return (earliest - start) * busId;
        }
    }
}
