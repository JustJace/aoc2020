using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using AOC2020.Structure;
using AOC2020.Search;

namespace AOC2020.Solutions
{
    public class D_06_2 : Solver<int>
    {
        public override int Day => 6;
        public override int Part => 2;
        protected override string Filename => @"Inputs\D_06.input";

        protected override int GetAnswer(string input)
        {
            var groups = input.PerDoubleLine();
            var count = 0;
            foreach (var group in groups)
            {
                var dictionary = new Dictionary<char, int>();
                var people = group.PerNewLine();
                foreach (var person in people)
                foreach (var answer in person)
                {
                    if (!dictionary.ContainsKey(answer)) dictionary[answer] = 0;

                    dictionary[answer]++;
                }
                count += dictionary.Values.Count(c => c == people.Length);
            }
            return count;
        }
    }
}
