using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using AOC2020.Structure;
using AOC2020.Search;

namespace AOC2020.Solutions
{
    public class D_014_1 : Solver<ulong>
    {
        public override int Day => 14;
        public override int Part => 1;
        protected override string Filename => @"Inputs\D_14.input";
        protected override ulong GetAnswer(string input)
        {
            var lines = input.PerNewLine();
            var memory = new Dictionary<ulong, ulong>();
            var mask = "";

            foreach (var line in lines)
            {
                if (line.StartsWith("mask"))
                {
                    mask = line.Split(' ').Last();
                    continue;
                }

                var (address, value) = line.Regex<ulong, ulong>(@"mem\[(\d+)\] = (\d+)");

                var masked = ApplyMask(mask, value);

                memory[address] = masked;
            }

            var x = 0ul;

            foreach (var value in memory.Values) x += value;

            return x;
        }

        private ulong ApplyMask(string mask, ulong value)
        {
            var binary = Convert.ToString((long)value, 2).PadLeft(36, '0');
            var result = "";

            for (var i = 0; i < binary.Length; i++)
            {
                if (mask[i] == 'X')
                    result += binary[i];
                else
                    result += mask[i];
            }

            return Convert.ToUInt64(result.TrimStart('0'), 2);
        }
    }
}