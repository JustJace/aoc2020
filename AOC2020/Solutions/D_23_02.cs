using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using AOC2020.Structure;
using AOC2020.Search;

namespace AOC2020.Solutions
{
    public class D_23_2 : Solver<ulong>
    {
        public override int Day => 23;
        public override int Part => 2;
        protected override string Filename => @"Inputs\D_23.input";
        protected override ulong GetAnswer(string input)
        {
            var labels = input;
            var start = new node
            {
                label = int.Parse(labels[0].ToString())
            };
            var nodes = new Dictionary<int, node>()
            {
                {start.label, start}
            };

            var current = start;

            for (var i = 1; i < labels.Length; i++) 
            {
                var node = new node
                {
                    label = int.Parse(labels[i].ToString())
                };
                nodes[node.label] = node;
                current.next = node;
                current = node;
            }

            for (var i = nodes.Values.Max(n => n.label) + 1; i <= 1000000; i++) 
            {
                var node = new node
                {
                    label = i
                };
                nodes[node.label] = node;
                current.next = node;
                current = node;
            }

            current.next = start;

            return PlayGame(start, nodes);
        }

        private ulong PlayGame(node start, Dictionary<int, node> nodes)
        {
            var current = start;
            for (var i = 0; i < 10000000; i++)
            {
                var pickupcups = new [] { current.next, current.next.next, current.next.next.next};
                current.next = pickupcups.Last().next;

                var j = current.label - 1;
                if (j < 1) j = nodes.Values.Max(n => n.label);
                while (pickupcups.Select(c => c.label).Contains(j))
                {
                    j--;
                    if (j < 1) j = nodes.Values.Max(n => n.label);
                }

                var destinationCup = nodes[j];

                var cup4 = destinationCup.next;

                destinationCup.next = pickupcups[0];

                pickupcups.Last().next = cup4;

                current = current.next;
            }

            var one = nodes[1];
            return (ulong)one.next.label * (ulong)one.next.next.label;
        }

        class node
        {
            public int label;
            public node next;
        }
    }
}