using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using AOC2020.Structure;
using AOC2020.Search;

namespace AOC2020.Solutions
{
    public class D_08_1 : Solver<int>
    {
        public override int Day => 8;
        public override int Part => 1;
        protected override string Filename => @"Inputs\D_08.input";

        protected override int GetAnswer(string input)
        {
            input = input.Replace("+", "");

            var accumulator = 0;
            var ptr = 0;

            var program = ParseProgram(input);

            RunProgram(program, ptr, ref accumulator);

            return accumulator;
        }

        private void RunProgram(Dictionary<int, instruction> program, int ptr, ref int acc)
        {
            var seen = new HashSet<int>();

            while (true)
            {
                if (seen.Contains(ptr))
                {
                    break;
                }

                seen.Add(ptr);

                var instruction = program[ptr];

                if (instruction.op == "nop")
                {
                    ptr++;
                    continue;
                }
                else if (instruction.op == "acc")
                {
                    acc += instruction.arg;
                    ptr++;
                }
                else if (instruction.op == "jmp")
                {
                    ptr += instruction.arg;
                }
            }
        }

        private Dictionary<int, instruction> ParseProgram(string input)
        {
            var program = new Dictionary<int, instruction>();
            var lines = input.Split(Environment.NewLine);
            for (var i = 0; i < lines.Length; i++)
            {
                var (op, arg) = lines[i].Regex<string, int>(@"(...) ([0-9\-]+)");
                program[i] = new instruction(op, arg);
            }
            return program;
        }

        struct instruction
        {
            public instruction(string op, int arg) { this.op = op; this.arg = arg; }
            public string op { get; set; }
            public int arg { get; set; }
        }
    }
}
