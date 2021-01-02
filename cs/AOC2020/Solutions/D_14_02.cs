using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using AOC2020.Structure;
using AOC2020.Search;

namespace AOC2020.Solutions
{
    public class D_014_2 : Solver<ulong>
    {
        public override int Day => 14;
        public override int Part => 2;
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

                var masked = ApplyMask(mask, address);
                var possibilities = Possibilities(masked);

                foreach (var possible in possibilities)
                {
                    memory[Convert.ToUInt64(possible, 2)] = value;
                }
            }

            var x = 0ul;

            foreach (var value in memory.Values)
            {
                x += value;
            }

            return x;
        }

        private ulong ComputeSum(string binary)
        {
            var values = new List<ulong>();
            var binaries = new List<string>();

            var possibles = Possibilities(binary);

            var x = 0ul;
            foreach (var possibility in possibles)
            {
                x += Convert.ToUInt64(possibility, 2);
            }
            
            return x;
        }

        private List<string> Possibilities(string binary)
        {
            if (!binary.Contains('X')) return new List<string>() { binary };

            var options = new List<string>();
            var i = binary.IndexOf('X');
            
            options.AddRange(Possibilities(binary.Substring(0, i) + '0' + binary.Substring(i + 1)));
            options.AddRange(Possibilities(binary.Substring(0, i) + '1' + binary.Substring(i + 1)));

            return options;
        }

        private string ApplyMask(string mask, ulong value)
        {
            var binary = Convert.ToString((long)value, 2).PadLeft(36, '0');
            var result = "";

            for (var i = 0; i < binary.Length; i++)
            {
                if (mask[i] == '0')
                    result += binary[i];
                else if (mask[i] == '1')
                    result += '1';
                else if (mask[i] == 'X')
                    result += 'X';
                else
                    result += mask[i];
            }

            return result;
        }
    }
}