using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using AOC2020.Structure;
using AOC2020.Search;

namespace AOC2020.Solutions
{
    public class D_015_2 : Solver<int>
    {
        public override int Day => 15;
        public override int Part => 2;
        protected override string Filename => @"Inputs\D_15.input";
        protected override int GetAnswer(string input)
        {
            var numbers = input.Split(',').Select(int.Parse).ToArray();
            var memory = new Dictionary<int, int>();
            var numbersSpoken = 0;

            for (var i = 0; i < numbers.Length - 1; i++)
            {
                memory[numbers[i]] = i;
                numbersSpoken++;
            }

            var lastNumber = numbers.Last();

            while (numbersSpoken < 30_000_000 - 1)
            {
                if (memory.ContainsKey(lastNumber))
                {
                    var temp = memory[lastNumber];
                    memory[lastNumber] = numbersSpoken;
                    lastNumber = numbersSpoken - temp;
                }
                else
                {
                    memory[lastNumber] = numbersSpoken;
                    lastNumber = 0;
                }

                numbersSpoken++;
            }

            return lastNumber;
        }
    }
}