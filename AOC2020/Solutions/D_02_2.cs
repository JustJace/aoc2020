using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2020.Solutions
{
    public class D_02_2 : Solver<int>
    {
        public override int Day => 2;
        public override int Part => 2;
        protected override string Filename => @"Inputs\D_02.input";

        protected override int GetAnswer(string input)
        {
            var valid = 0;

            foreach (var line in input.Split(Environment.NewLine))
            {
                var (lo, hi, letter, password) = line.Regex<int, int, char, string>(@"([0-9]+)-([0-9]+) ([a-z]): ([a-z]+)");

                if (password[lo-1] == letter ^ password[hi-1] == letter) valid++;
            }

            return valid;
        }
    }
}        
        