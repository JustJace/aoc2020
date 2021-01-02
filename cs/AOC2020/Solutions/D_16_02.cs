using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using AOC2020.Structure;
using AOC2020.Search;

namespace AOC2020.Solutions
{
    public class D_016_2 : Solver<long>
    {
        public override int Day => 16;
        public override int Part => 2;
        protected override string Filename => @"Inputs\D_16.input";
        protected override long GetAnswer(string input)
        {
            var sections = input.PerDoubleLine();
            var rules = sections[0].PerNewLine().Select(l => 
            {
                var (name, low1, high1, low2, high2) = l.Regex<string, int,int,int,int>(@"(.+): ([0-9]+)-([0-9]+) or ([0-9]+)-([0-9]+)");
                return new rule(name, low1, high1, low2, high2);
            })
            .ToArray();

            var validTickets = sections[2]
                .PerNewLine()
                .Skip(1)
                .Where(t => IsValidTicket(t, rules))
                .ToArray();

            var order = GetOrder(validTickets, rules);
            var myTicketValues = sections[1].PerNewLine()[1].Split(',').Select(int.Parse).ToArray();

            // foreach (var kvp in order.OrderBy(kvp => kvp.Key))
            // {
            //     Console.WriteLine($"{rules[kvp.Key].ToString().PadRight(42, ' ')} \t index {kvp.Value} \t => {myTicketValues[kvp.Value]}");
            // }

            // Console.In.ReadLine();

            return (long)myTicketValues[order[0]]
                 * (long)myTicketValues[order[1]]
                 * (long)myTicketValues[order[2]]
                 * (long)myTicketValues[order[3]]
                 * (long)myTicketValues[order[4]]
                 * (long)myTicketValues[order[5]];
        }

        private Dictionary<int,int> GetOrder(string[] validTickets, rule[] rules)
        {
            var ruleIndexPossibles = new Dictionary<int, List<int>>();
            for (var i = 0; i < rules.Length; i++)
            {
                ruleIndexPossibles[i] = Enumerable.Range(0, rules.Length).ToList();
            }

            foreach (var ticket in validTickets)
            {
                var values = ticket.Split(',').Select(int.Parse).ToArray();
                for (var v = 0; v < values.Length; v++)
                {
                    for (var r = 0; r < rules.Length; r++)
                    {
                        if (!ValidateValueForRule(values[v], rules[r]))
                        {
                            ruleIndexPossibles[r].Remove(v);
                        }
                    }
                }
            }

            var order = new Dictionary<int, int>();

            while (order.Count < rules.Length)
            {
                var single = ruleIndexPossibles.First(kvp => kvp.Value.Count == 1);
                order[single.Key] = single.Value.First();
                ruleIndexPossibles.Remove(single.Key);

                foreach (var possible in ruleIndexPossibles)
                {
                    possible.Value.Remove(single.Value.First());
                }
            }

            return order;
        }

        private bool IsValidTicket(string ticket, rule[] rules)
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
                    return false;
                }
            }

            return true;
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
