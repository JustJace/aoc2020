using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using AOC2020.Structure;
using AOC2020.Search;

namespace AOC2020.Solutions
{
    public class D_018_2 : Solver<long>
    {
        public override int Day => 18;
        public override int Part => 2;
        protected override string Filename => @"Inputs\D_18.input";
        protected override long GetAnswer(string input)
        {
            var problems = input.PerNewLine().Select(l => l.Replace(" ", "")).ToArray();
            var answer = 0L;
            foreach (var problem in problems)
            {
                answer += Evaluate(Parse(problem));
            } 
            return answer;
        }

        private node Parse(string problem)
        {
            var nodes = new List<node>();
            var currentOperator = "";
            expression previous = null;
            expression next = null;
            for (var i = 0; i < problem.Length; i++)
            {
                next = NextTerm(problem.Substring(i), out int fastforward); i+= fastforward;

                if (currentOperator != "" && previous != null)
                {
                    nodes.Add(new node
                    {
                        left = previous,
                        right = next,
                        op = currentOperator
                    });
                    previous = next;
                    currentOperator = "";
                }
                else if (previous == null)
                {
                    previous = next;
                }
                
                if (i < problem.Length)
                {
                    currentOperator = problem[i].ToString();
                }
            }

            while (nodes.Count(n => n.op == "+") > 0 && nodes.Count > 1)
            {
                var plusNode = nodes.First(n => n.op == "+");
                var index = nodes.IndexOf(plusNode);

                var eval = Evaluate(plusNode);

                if (index - 1 >= 0)
                {
                    nodes[index - 1].right = new expression { type = "value", term = eval.ToString() };
                }

                if (index + 1 < nodes.Count)
                {
                    nodes[index + 1].left = new expression { type = "value", term = eval.ToString() };
                }

                nodes.Remove(plusNode);
            }

            while (nodes.Count > 1)
            {
                var multNode = nodes.First(n => n.op == "*");
                var index = nodes.IndexOf(multNode);

                var eval = Evaluate(multNode);

                if (index - 1 >= 0)
                {
                    nodes[index - 1].right = new expression { type = "value", term = eval.ToString() };
                }

                if (index + 1 < nodes.Count)
                {
                    nodes[index + 1].left = new expression { type = "value", term = eval.ToString() };
                }

                nodes.Remove(multNode);
            }

            return nodes.First();
        }

        private long Evaluate(node node)
        {
            var left = node.left.type == "nomial" ? Evaluate(Parse(node.left.term)) : long.Parse(node.left.term);
            var right = node.right.type == "nomial" ? Evaluate(Parse(node.right.term)) : long.Parse(node.right.term);
            return node.op == "*" ? left * right : left + right;
        }

        private expression NextTerm(string problem, out int fastforward)
        {
            // Console.WriteLine("next: " + problem);
            fastforward = 1;
            if (problem[0] == '(')
            {
                var term = "";
                var parens = 1;
                var i = 1;
                while (parens != 0)
                {
                    if (problem[i]==')') parens--; 
                    if (parens == 0) { break; }
                    term += problem[i];
                    if (problem[i]=='(') parens++;
                    i++;
                }

                fastforward = i + 1;

                return new expression { type = "nomial", term = term };
            }
            else return new expression { type = "value", term = problem[0].ToString() };
        }

        class node
        {
            public string op;
            public expression left;
            public expression right;
        }

        class expression
        {
            public string type;
            public string term;
        }
    }
}
