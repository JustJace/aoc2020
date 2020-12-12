using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using AOC2020.Structure;
using AOC2020.Search;

namespace AOC2020.Solutions
{
    public class D_010_1 : Solver<long>
    {
        public override int Day => 10;
        public override int Part => 1;
        protected override string Filename => @"Inputs\D_10.input";

        protected override long GetAnswer(string input)
        {
            var adapters = input.AsIntArray().ToList();
            adapters.Sort();
            adapters.Add(adapters.Last() + 3);
            var differences = new Dictionary<int, int>();
            var currentJolt = 0;

            for (var i = 0; i < adapters.Count; i++)
            {
                var difference = adapters[i] - currentJolt;
                if (!differences.ContainsKey(difference)) differences[difference] = 0;
                differences[difference]++;
                currentJolt = adapters[i];
            }

            return differences[1] * differences[3];
        }
    }
}
