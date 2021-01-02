using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using AOC2020.Structure;
using AOC2020.Search;

namespace AOC2020.Solutions
{
    public class D_018_1 : Solver<long>
    {
        public override int Day => 18;
        public override int Part => 1;
        protected override string Filename => @"Inputs\D_18.input";
        protected override long GetAnswer(string input)
        {
            var problems = input.PerNewLine().Select(l => l.Replace(" ", "")).ToArray();
            return problems.Select(Solve).Sum();
        }

        private long Solve(string problem)
        {
            var answer = 0L;
            var currentOperator = "";
            for (var i = 0; i < problem.Length; i++)
            {
                var value = 0L;
                var parens = 0;
                if (problem[i] == '(')
                {
                    parens++;
                    i++;
                    var inbetween = "";
                    while (parens != 0)
                    {
                        if (problem[i]==')') parens--; if (parens == 0) break;
                        inbetween += problem[i];
                        if (problem[i]=='(') parens++;
                        i++;
                    }
                    value = Solve(inbetween);
                }
                else if (problem[i] == '*')
                {
                    currentOperator = "*";
                    continue;
                }
                else if (problem[i] == '+')
                {
                    currentOperator = "+";
                    continue;
                }
                else
                {
                    value = int.Parse(problem[i].ToString());
                }

                if (currentOperator != "")
                {
                    if (currentOperator == "*")
                    {
                        answer = answer * value;
                    }
                    else
                    {
                        answer = answer + value;
                    }

                    currentOperator = "";
                }
                else
                {
                    answer = value;
                }
            }

            return answer;
        }
    }
}
