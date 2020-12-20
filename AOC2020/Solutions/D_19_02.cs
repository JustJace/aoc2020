using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using AOC2020.Structure;
using AOC2020.Search;

namespace AOC2020.Solutions
{
    public class D_019_2 : Solver<long>
    {
        public override int Day => 19;
        public override int Part => 2;
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

            rules[8] = "42 | 42 8";
            rules[11] = "42 31 | 42 11 31";

            var count = 0;

            foreach (var message in sections[1].PerNewLine())
            {
                ValidateRule(message, rules, 0, 0, 0, out List<int> finishIndexes);
                if (finishIndexes.Contains(message.Length))
                {
                    count++;
                    // Console.WriteLine($"{message} is valid");
                }
                else
                {
                    // Console.WriteLine($"{message} is not valid");
                }

                // Console.In.ReadLine();
            }

            // Console.Out.WriteLine(count);

            return count;
        }

        private void ValidateRule(string message, Dictionary<int, string> rules, int parentRuleId, int ruleId, int startIndex, out List<int> finishIndexes)
        {
            // Console.WriteLine($"Depth: {startIndex}\tValidating {ruleId} in #{parentRuleId}: {rules[parentRuleId]}");

            finishIndexes = new List<int>();

            if (startIndex >= message.Length)
            {
                // Console.WriteLine("Index too far");
                return;
            }

            var rule = rules[ruleId];
            if (rule.Contains('"'))
            {
                var chr = rule.Replace("\"", "");
                var valid = message[startIndex].ToString() == chr;

                if (valid)
                {
                    finishIndexes.Add(startIndex + 1);
                }

                // var c = message.Substring(0, startIndex) + '[' + message[startIndex] + ']' + message.Substring(startIndex + 1);
                // Console.WriteLine($"\t\t{c} as {chr} => {valid}");

                return;
            }
            else
            {
                if (rule.Contains("|"))
                {
                    var sections = rule.Split('|').Select(s => s.Trim()).ToArray();

                    var parts = sections[0].Trim().Split(' ').Select(int.Parse).ToArray();

                    ValidateRuleSubRules(message, rules, ruleId, sections[0], startIndex, out List<int> indexes1);
                    ValidateRuleSubRules(message, rules, ruleId, sections[1], startIndex, out List<int> indexes2);

                    finishIndexes.AddRange(indexes1);
                    finishIndexes.AddRange(indexes2);
                }
                else
                {
                    ValidateRuleSubRules(message, rules, ruleId, rule, startIndex, out finishIndexes);
                }
            }
        }

        private void ValidateRuleSubRules(string message, Dictionary<int, string> rules, int parentRuleId, string subRules, int startIndex, out List<int> finishIndexes)
        {
            finishIndexes = new List<int>();
            var parts = subRules.Split(' ').Select(int.Parse).ToArray();

            ValidateRule(message, rules, parentRuleId, parts[0], startIndex, out List<int> indexes);

            if (parts.Length > 1)
            {
                foreach (var index in indexes)
                {
                    ValidateRule(message, rules, parentRuleId, parts[1], index, out List<int> indexes2);

                    if (parts.Length > 2)
                    {
                        foreach (var index2 in indexes2)
                        {
                            ValidateRule(message, rules, parentRuleId, parts[2], index2, out List<int> indexes3);
                            finishIndexes.AddRange(indexes3);
                        }
                    } 
                    else
                    {
                        finishIndexes.AddRange(indexes2);
                    }
                }
            }
            else
            {
                finishIndexes.AddRange(indexes);
            }
        }
    }
}
