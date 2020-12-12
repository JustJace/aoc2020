using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using AOC2020.Structure;
using AOC2020.Search;

namespace AOC2020.Solutions
{
    public class D_010_2 : Solver<ulong>
    {
        public override int Day => 10;
        public override int Part => 2;
        protected override string Filename => @"Inputs\D_10.input";

        protected override ulong GetAnswer(string input)
        {
            var adapters = input.AsIntArray().ToList();
            adapters.Sort();
            adapters.Add(adapters.Last() + 3);
            
            var jolts = new Dictionary<int, GraphNode<int>>();
            jolts[0] = new GraphNode<int>(0);

            foreach (var jolt in adapters)
            {
                var joltNode = new GraphNode<int>(jolt);
                foreach (var otherJolt in jolts.Values)
                    if (jolt - otherJolt.Data <= 3)
                        otherJolt.Neighbors.Add(joltNode);
                jolts[jolt] = joltNode;
            }

            var memory = new Dictionary<int, ulong>();

            return CountPaths(jolts, memory, jolts[0]);
        }

        private ulong CountPaths(Dictionary<int, GraphNode<int>> jolts, Dictionary<int, ulong> memory, GraphNode<int> joltNode)
        {
            if (joltNode.Neighbors.Count == 0) return 1;

            var count = 0ul;

            foreach (var node in joltNode.Neighbors)
            {
                if (memory.ContainsKey(node.Data))  count += memory[node.Data];
                else                                count += CountPaths(jolts, memory, node);
            }

            memory[joltNode.Data] = count;

            return count;
        }
    }
}
