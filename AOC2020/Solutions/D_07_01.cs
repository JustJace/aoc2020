using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using AOC2020.Structure;
using AOC2020.Search;

namespace AOC2020.Solutions
{
    public class D_07_1 : Solver<int>
    {
        public override int Day => 7;
        public override int Part => 1;
        protected override string Filename => @"Inputs\D_07.input";

        protected override int GetAnswer(string input)
        {
            var bags = ParseBags(input);

            var shinyGold = bags["shiny gold"];
          
            return BreadthFirstCount(shinyGold) - 1;
        }

        private int BreadthFirstCount(GraphNode<string> start)
        {
            var seen = new HashSet<string>();
            var queue = new Queue<GraphNode<string>>();
            queue.Enqueue(start);

            while (queue.Any())
            {
                var current = queue.Dequeue();
                seen.Add(current.Data);
                foreach (var neighbor in current.Neighbors)
                {
                    queue.Enqueue(neighbor);
                }
            }

            return seen.Count;
        }

        private Dictionary<string, GraphNode<string>> ParseBags(string input)
        {
            var rules = input.Split(Environment.NewLine);
            var regex = @"(.*) bags contain (.+)\.";
            var childRegex = @"\d+ (.+) bags?";
            var bags = new Dictionary<string, GraphNode<string>>();

            foreach (var rule in rules)
            {
                var (parent, rawChildren) = rule.Regex<string, string>(regex);

                if (!bags.ContainsKey(parent)) 
                    bags[parent] = new GraphNode<string>(parent);

                if (rawChildren.Trim() == "no other bags") continue;

                var children = rawChildren
                         .Split(",")
                         .Select(c => c.Trim())
                         .Select(c => c.Regex<string>(childRegex));

                foreach (var child in children)
                {
                    if (!bags.ContainsKey(child)) 
                        bags[child] = new GraphNode<string>(child);

                    bags[child].Neighbors.Add(bags[parent]);
                }
            }

            return bags;
        }
    }
}
