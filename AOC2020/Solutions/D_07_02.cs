using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using AOC2020.Structure;
using AOC2020.Search;

namespace AOC2020.Solutions
{
    public class D_07_2 : Solver<int>
    {
        public override int Day => 7;
        public override int Part => 2;
        protected override string Filename => @"Inputs\D_07.input";

        protected override int GetAnswer(string input)
        {
            var bags = ParseBags(input);
            var shinyGold = bags["shiny gold"];
            return CountBagsInside(shinyGold);
        }

        private int CountBagsInside(WGraphNode<string> node)
        {
            if (node.Edges.Count == 0) return 0;

            var count = 0;

            foreach (var edge in node.Edges)
            {
                count += edge.Weight * (1 + CountBagsInside(edge.Destination));
            }

            return count;
        }

        private Dictionary<string, WGraphNode<string>> ParseBags(string input)
        {
            var rules = input.Split(Environment.NewLine);
            var regex = @"(.*) bags contain (.+)\.";
            var childRegex = @"(\d+) (.+) bags?";
            var bags = new Dictionary<string, WGraphNode<string>>();

            foreach (var rule in rules)
            {
                var (parent, rawChildren) = rule.Regex<string, string>(regex);

                if (!bags.ContainsKey(parent))
                    bags[parent] = new WGraphNode<string>(parent);

                if (rawChildren.Trim() == "no other bags") continue;

                var children = rawChildren
                         .Split(",")
                         .Select(c => c.Trim())
                         .Select(c => c.Regex<int, string>(childRegex))
                         .Select(t => new { name = t.Item2, amount = t.Item1 });

                foreach (var child in children)
                {
                    if (!bags.ContainsKey(child.name))
                        bags[child.name] = new WGraphNode<string>(child.name);

                    bags[parent].AddEdge(bags[child.name], child.amount);
                }
            }

            return bags;
        }
    }
}
