using System.Collections.Generic;
using System.Linq;

namespace AOC2020.Solutions
{
    public class D_22_2 : Solver<long>
    {
        public override int Day => 22;
        public override int Part => 2;
        protected override string Filename => @"Inputs\D_22.input";
        protected override long GetAnswer(string input)
        {
            var sections = input.PerDoubleLine();
            var player1 = ParsePlayer(sections[0]);
            var player2 = ParsePlayer(sections[1]);
            var (player, winningdeck) = RCombat(player1, player2);
            var score = 0;

            for (var i = winningdeck.Count; i >= 1; i--)
                score += winningdeck.Dequeue() * i;
                
            return score;
        }

        private (Player, Queue<int>) RCombat(Queue<int> player1, Queue<int> player2)
        {
            var seen = new HashSet<string>();

            while (player1.Count > 0 && player2.Count > 0)
            {
                var roundKey = AsKey(player1, player2);
                if (seen.Contains(roundKey)) return (Player.One, player1);
                else seen.Add(roundKey);

                var p1card = player1.Dequeue();
                var p2card = player2.Dequeue();
                var winner = Player.One;

                if (player1.Count >= p1card && player2.Count >= p2card)
                {
                    (winner, _) = RCombat(new Queue<int>(player1.Take(p1card)), new Queue<int>(player2.Take(p2card)));
                }
                else
                {
                    winner = p1card > p2card ? Player.One : Player.Two;
                }

                if (winner == Player.One)
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

            return player1.Count > 0 ? (Player.One, player1) : (Player.Two, player2);
        }

        private string AsKey(Queue<int> p1, Queue<int> p2) 
        {
            return p1.Concat(new int[] { 0 }).Concat(p2).Select(c => c.ToString()).Aggregate((a, b) => a + "," + b);
        }

        private Queue<int> ParsePlayer(string s)
        {
            var cards = s.PerNewLine().Skip(1).Select(int.Parse);
            var queue = new Queue<int>();
            foreach (var card in cards) queue.Enqueue(card);
            return queue;
        }

        enum Player { One, Two }
    }
}