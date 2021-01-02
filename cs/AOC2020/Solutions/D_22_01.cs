using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using AOC2020.Structure;
using AOC2020.Search;

namespace AOC2020.Solutions
{
    public class D_22_1 : Solver<long>
    {
        public override int Day => 22;
        public override int Part => 1;
        protected override string Filename => @"Inputs\D_22.input";
        protected override long GetAnswer(string input)
        {
            var sections = input.PerDoubleLine();
            var player1 = ParsePlayer(sections[0]);
            var player2 = ParsePlayer(sections[1]);

            while (player1.Count > 0 && player2.Count > 0)
            {
                var p1card = player1.Dequeue();
                var p2card = player2.Dequeue();

                if (p1card > p2card) 
                {
                    player1.Enqueue(p1card);
                    player1.Enqueue(p2card);
                }
                else
                {
                    player2.Enqueue(p2card);
                    player2.Enqueue(p1card);
                }
            }

            Queue<int> winner = player1.Count > 0 ? player1 : player2;

            var score = 0;
            for (var i = winner.Count; i >= 1; i--)
            {
                score += winner.Dequeue() * i;
            }

            return score;
        }

        private Queue<int> ParsePlayer(string s)
        {
            var cards = s.PerNewLine().Skip(1).Select(int.Parse);
            var queue = new Queue<int>();
            foreach (var card in cards) queue.Enqueue(card);
            return queue;
        }
    }
}