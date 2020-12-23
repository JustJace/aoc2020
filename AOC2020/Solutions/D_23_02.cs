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
            var labels = input.Select(c => int.Parse(c.ToString())).ToArray();
            var (start, nodes) = BuildCups(labels, maxcup: 1000000);
            PlayGame(start, nodes, rounds: 10000000);
            return (ulong)nodes[1].next.label * (ulong)nodes[1].next.next.label;
        }

        private (node, Dictionary<int, node>) BuildCups(int[] labels, int maxcup)
        {
            var start = new node(labels[0]);
            var nodes = new Dictionary<int, node>() {{start.label, start}};
            var current = start;

            for (var i = 1; i < labels.Length; i++) 
            {
                var node = new node(labels[i]);
                current.next = node;
                current = node;
                nodes[node.label] = node;
            }

            for (var i = nodes.Count + 1; i <= maxcup; i++) 
            {
                var node = new node(i);
                current.next = node;
                current = node;
                nodes[node.label] = node;
            }

            current.next = start;

            return (start, nodes);
        }

        private void PlayGame(node current, Dictionary<int, node> nodes, int rounds)
        {
            var max = nodes.Count;
            for (var i = 0; i < rounds; i++)
            {
                var pickupcups = new [] { current.next, current.next.next, current.next.next.next };
                current.next = pickupcups.Last().next;

                var destinationLabel = FindDestinationLabel(current.label - 1, max, pickupcups.SelectArray(c => c.label));
                var destinationCup = nodes[destinationLabel];
                var cup4 = destinationCup.next;

                destinationCup.next = pickupcups[0];
                pickupcups.Last().next = cup4;
                
                current = current.next;
            }
        }

        private int UnderflowCheck(int n, int max) => n < 1 ? max : n;
        private int FindDestinationLabel(int current, int max, int[] pickupLabels)
        {
            current = UnderflowCheck(current, max);
            while (pickupLabels.Contains(current))
            {
                current--;
                current = UnderflowCheck(current, max);
            }
            return current;
        }

        class node
        {
            public node (int label) { this.label = label; }
            public int label;
            public node next;
        }
    }
}