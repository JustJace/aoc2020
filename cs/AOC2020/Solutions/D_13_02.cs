using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using AOC2020.Structure;
using AOC2020.Search;

namespace AOC2020.Solutions
{
    public class D_013_2 : Solver<ulong>
    {
        public override int Day => 13;
        public override int Part => 2;
        protected override string Filename => @"Inputs\D_13.input";
        protected override ulong GetAnswer(string input)
        {
            var busses = input.PerNewLine()[1].Split(',');
            var remainders = new List<ulong>();
            var mods = new List<ulong>();
            for (var i = 0; i < busses.Length; i++) 
            {
                var bus = busses[i];
                if (bus == "x") continue;
                var busId = ulong.Parse(bus);
                mods.Add(busId);
                remainders.Add(busId - ((ulong)i%busId));
            }

            return AOCMath.CRT(mods.ToArray(), remainders.ToArray());
        }
    }
}