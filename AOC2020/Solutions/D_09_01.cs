using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using AOC2020.Structure;
using AOC2020.Search;

namespace AOC2020.Solutions
{
    public class D_09_1 : Solver<long>
    {
        public override int Day => 9;
        public override int Part => 1;
        protected override string Filename => @"Inputs\D_09.input";

        protected override long GetAnswer(string input)
        {
            var numbers = input.PerNewLine().Select(long.Parse).ToArray();
            return FindMissingSumWeakness(numbers);

        }
        private long FindMissingSumWeakness(long[] numbers)
        {
            for (var i = 25; i < numbers.Length; i++)
            {
                var foundValidSum = false;
                for (var pre = i - 25; pre < i - 1; pre++)
                {
                    for (var pre2 = pre + 1; pre2 < i; pre2++)
                    {
                        if (numbers[i] == numbers[pre] + numbers[pre2])
                        {
                            foundValidSum = true;
                            break;
                        }
                    }

                    if (foundValidSum) break;
                }

                if (!foundValidSum) return numbers[i];
            }

            throw new Exception("couldn't find answer");
        }
    }
}
