using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using AOC2020.Structure;
using AOC2020.Search;

namespace AOC2020.Solutions
{
    public class D_019_1 : Solver<long>
    {
        public override int Day => 19;
        public override int Part => 1;
        protected override string Filename => @"Inputs\D_19.input";
        protected override long GetAnswer(string input)
        {
            var sections = input.PerDoubleLine();
            var rules = new Dictionary<int, string>();
            foreach (var rule in sections[0].PerNewLine())
            {
                var (number, x) = rule.Regex<int, string>(@"(\d+):(.+)");
                rules[number] = x.Trim();
            }

            var count = 0;

            foreach (var message in sections[1].PerNewLine())
            {
                if (ValidateRule(message, rules, 0, 0, out int finishIndex)
                 && finishIndex == message.Length)
                {
                    count++;
                    // Console.WriteLine($"{message} is valid");
                }
                else
                {
                    // Console.WriteLine($"{message} is not valid");
                }
            }

            return count;
        }

        private bool ValidateRule(string message, Dictionary<int, string> rules, int ruleId, int startIndex, out int finishIndex)
        {
            // Console.WriteLine($"Validating Rule #{ruleId}: {rules[ruleId]}");
            finishIndex = startIndex;
            var rule = rules[ruleId];
            if (rule.Contains('"'))
            {
                var chr = rule.Replace("\"", "");
                finishIndex++;
                var valid = message[startIndex].ToString() == chr;
                var c = message.Substring(0, startIndex) + '[' + message[startIndex] + ']' + message.Substring(startIndex + 1);
                // Console.WriteLine($"Validating  {c} as {chr} => {valid}");
                return valid;
            }
            else
            {
                if (rule.Contains("|"))
                {
                    var sections = rule.Split('|');

                    var parts = sections[0].Trim().Split(' ').Select(int.Parse).ToArray();

                    var partOneValid = ValidateRule(message, rules, parts[0], startIndex, out finishIndex)
                                    && (parts.Length == 1 || ValidateRule(message, rules, parts[1], finishIndex, out finishIndex));
                                    // && (parts.Length == 2 || ValidateRule(message, rules, parts[2], finishIndex, out finishIndex));

                    if (partOneValid) return true;

                    parts = sections[1].Trim().Split(' ').Select(int.Parse).ToArray();

                    return ValidateRule(message, rules, parts[0], startIndex, out finishIndex)
                        && (parts.Length == 1 || ValidateRule(message, rules, parts[1], finishIndex, out finishIndex));
                        // && (parts.Length == 2 || ValidateRule(message, rules, parts[2], finishIndex, out finishIndex));
                }
                else
                {
                    var parts = rule.Split(' ').Select(int.Parse).ToArray();

                    return ValidateRule(message, rules, parts[0], startIndex, out finishIndex)
                        && (parts.Length == 1 || ValidateRule(message, rules, parts[1], finishIndex, out finishIndex));
                        // && (parts.Length == 2 || ValidateRule(message, rules, parts[2], finishIndex, out finishIndex));
                }
            }
        }
    }
}

// not 60