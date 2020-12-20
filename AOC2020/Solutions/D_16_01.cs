using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using AOC2020.Structure;
using AOC2020.Search;

namespace AOC2020.Solutions
{
    public class D_016_1 : Solver<int>
    {
        public override int Day => 16;
        public override int Part => 1;
        protected override string Filename => @"Inputs\D_16.input";
        protected override int GetAnswer(string input)
        {
            var sections = input.PerDoubleLine();
            var rules = sections[0].PerNewLine().Select(l => 
            {
                var (name, low1, high1, low2, high2) = l.Regex<string, int,int,int,int>(@"(.+): ([0-9]+)-([0-9]+) or ([0-9]+)-([0-9]+)");
                return new rule(name, low1, high1, low2, high2);
            })
            .ToArray();

            var answer = 0;
            var tickets = sections[2].PerNewLine();

            foreach (var ticket in tickets.Skip(1))
            {
                var values = ticket.Split(',').Select(int.Parse);

                foreach (var value in values)
                {
                    var foundMatch = false;
                    for (var i = 0; i < rules.Length; i++)
                    {
                        if (ValidateValueForRule(value, rules[i]))
                        {
                            foundMatch = true;
                            break;
                        }
                    }

                    if (!foundMatch)
                    {
                        answer += value;
                        break;
                    }
                }
            }

            return answer;
        }

        private bool ValidateValueForRule(int v, rule rule)
        {
            return (v >= rule.low1 && v <= rule.high1)
                || (v >= rule.low2 && v <= rule.high2);
        }
        
        struct rule
        {
            public rule(string name, int low1, int high1, int low2, int high2)
            {
                this.name = name; 
                this.low1 = low1; 
                this.high1 = high1;
                this.low2 = low2;
                this.high2 = high2;
            }

            public int low1 { get; set; }
            public int high1 { get; set; }
            public int low2 { get; set; }
            public int high2 { get; set; }
            public string name { get; set; }

            public override string ToString()
            {
                return $"{name}: ({low1}-{high1}) or ({low2}-{high2})";
            }
        }
    }
}