using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace Aoc2019.Solutions
{
    public class D_02_1 : Solver<int>
    {
        public override int Day => 2;
        public override int Part => 1;
        protected override string Filename => @"Inputs\D_02.input";

        protected override int GetAnswer(string input)
        {
            var valid = 0;

            foreach (var line in input.Split(Environment.NewLine))
            {
                var (lo, hi, letter, password) = line.Regex<int, int, char, string>(@"([\d]+)-([\d]+) ([a-z]): ([a-z]+)");
                var letterCount = password.Count(c => c == letter);
                
                if (letterCount >= lo && letterCount <= hi) valid++;
            }

            return valid;
        }
    }
}
