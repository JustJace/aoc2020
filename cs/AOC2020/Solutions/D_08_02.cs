using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using AOC2020.Structure;
using AOC2020.Search;

namespace AOC2020.Solutions
{
    public class D_08_2 : Solver<int>
    {
        public override int Day => 8;
        public override int Part => 2;
        protected override string Filename => @"Inputs\D_08.input";

        protected override int GetAnswer(string input)
        {
            input = input.Replace("+", "");
            var programLength = input.Split(Environment.NewLine).Count();

            for (var i = 0; i < programLength; i++)
            {
                var program = ParseProgram(input);

                var checkInstruction = program[i];
                if (checkInstruction.op == "nop")
                {
                    program[i].op = "jmp";
                }
                else if (checkInstruction.op == "jmp")
                {
                    program[i].op = "nop";
                }
                else
                {
                    continue;
                }

                var accumulator = 0;
                var ptr = 0;

                RunProgram(program, ref ptr, ref accumulator);

                if (ptr == programLength)
                {
                    return accumulator;
                }
            }

            throw new Exception("didn't find answer");
        }

        private void RunProgram(Dictionary<int, instruction> program, ref int ptr, ref int acc)
        {
            var seen = new HashSet<int>();

            while (true)
            {
                if (seen.Contains(ptr))
                {
                    return;
                }
                else if (ptr == program.Count)
                {
                    return;
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

        class instruction
        {
            public instruction(string op, int arg) { this.op = op; this.arg = arg; }
            public string op { get; set; }
            public int arg { get; set; }
        }
    }
}
