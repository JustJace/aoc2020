using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using AOC2020.Structure;
using AOC2020.Search;

namespace AOC2020.Solutions
{
    public class D_06_1 : Solver<int>
    {
        public override int Day => 6;
        public override int Part => 1;
        protected override string Filename => @"Inputs\D_06.input";

        protected override int GetAnswer(string input)
        {
            var groups = input.PerDoubleSpaced();
            var count = 0;
            foreach (var group in groups)
            {
                var hashset = new HashSet<char>();
                foreach (var person in group.PerNewLine())
                foreach (var answer in person)
                    hashset.Add(answer);

                count += hashset.Count;
            }
            return count;
        }
    }
}
