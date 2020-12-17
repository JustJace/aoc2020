using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using AOC2020.Structure;
using AOC2020.Search;

namespace AOC2020.Solutions
{
    public class D_017_2 : Solver<long>
    {
        public override int Day => 17;
        public override int Part => 2;
        protected override string Filename => @"Inputs\D_17.input";
        protected override long GetAnswer(string input)
        {
            var initialState = input.As2DArray<char>();
            var state = new bool[25][][][];

            for (var t = 0; t < 25; t++)
            {
                state[t] = new bool[25][][];

                for (var z = 0; z < 25; z++)
                {
                    state[t][z] = new bool[25][];

                    for (var r = 0; r < 25; r++)
                    {
                        state[t][z][r] = new bool[25];
                    }
                }
            }

            for (var r = 0; r < initialState.Length; r++)
            {
                for (var c = 0; c < initialState[r].Length; c++)
                {
                    if (initialState[r][c] == '#')
                    {
                        state[0][0][r][c] = true;
                    }
                }
            }

            for (var i = 0; i < 6; i++)
            {
                state = NextState(state);
            }

            var answer = 0;

            for (var t = 0; t < 25; t++)
            {
                for (var z = 0; z < 25; z++)
                {
                    for (var r = 0; r < 25; r++)
                    {
                        for (var c = 0; c < 25; c++)
                        {
                            if (state[t][z][r][c]) answer++;
                        }
                    }
                }

            }

            return answer;
        }
        private bool[][][][] NextState(bool[][][][] state)
        {
            var next = new bool[25][][][];
            for (var t = 0; t < 25; t++)
            {
                next[t] = new bool[25][][];

                for (var z = 0; z < 25; z++)
                {
                    next[t][z] = new bool[25][];

                    for (var r = 0; r < 25; r++)
                    {
                        next[t][z][r] = new bool[25];
                    }
                }
            }

            for (var t = 0; t < 25; t++)
            {
                for (var z = 0; z < 25; z++)
                {
                    for (var r = 0; r < 25; r++)
                    {
                        for (var c = 0; c < 25; c++)
                        {
                            var past = t - 1; if (past < 0) past = 24;
                            var present = t;
                            var future = t + 1; if (future >= 25) future = 0;

                            var bottom = z - 1; if (bottom < 0) bottom = 24;
                            var middle = z;
                            var top = z + 1; if (top >= 25) top = 0;

                            var up = r - 1; if (up < 0) up = 24;
                            var rCenter = r;
                            var down = r + 1; if (down >= 25) down = 0;

                            var left = c - 1; if (left < 0) left = 24;
                            var cCenter = c;
                            var right = c + 1; if (right >= 25) right = 0;

                            var count = 0;

                            if (state[past][bottom][up][left]) count++;
                            if (state[past][bottom][up][cCenter]) count++;
                            if (state[past][bottom][up][right]) count++;
                            if (state[past][bottom][rCenter][left]) count++;
                            if (state[past][bottom][rCenter][cCenter]) count++;
                            if (state[past][bottom][rCenter][right]) count++;
                            if (state[past][bottom][down][left]) count++;
                            if (state[past][bottom][down][cCenter]) count++;
                            if (state[past][bottom][down][right]) count++;

                            if (state[past][middle][up][left]) count++;
                            if (state[past][middle][up][cCenter]) count++;
                            if (state[past][middle][up][right]) count++;
                            if (state[past][middle][rCenter][left]) count++;
                            if (state[past][middle][rCenter][cCenter]) count++;
                            if (state[past][middle][rCenter][right]) count++;
                            if (state[past][middle][down][left]) count++;
                            if (state[past][middle][down][cCenter]) count++;
                            if (state[past][middle][down][right]) count++;

                            if (state[past][top][up][left]) count++;
                            if (state[past][top][up][cCenter]) count++;
                            if (state[past][top][up][right]) count++;
                            if (state[past][top][rCenter][left]) count++;
                            if (state[past][top][rCenter][cCenter]) count++;
                            if (state[past][top][rCenter][right]) count++;
                            if (state[past][top][down][left]) count++;
                            if (state[past][top][down][cCenter]) count++;
                            if (state[past][top][down][right]) count++;

                            if (state[present][bottom][up][left]) count++;
                            if (state[present][bottom][up][cCenter]) count++;
                            if (state[present][bottom][up][right]) count++;
                            if (state[present][bottom][rCenter][left]) count++;
                            if (state[present][bottom][rCenter][cCenter]) count++;
                            if (state[present][bottom][rCenter][right]) count++;
                            if (state[present][bottom][down][left]) count++;
                            if (state[present][bottom][down][cCenter]) count++;
                            if (state[present][bottom][down][right]) count++;

                            if (state[present][middle][up][left]) count++;
                            if (state[present][middle][up][cCenter]) count++;
                            if (state[present][middle][up][right]) count++;
                            if (state[present][middle][rCenter][left]) count++;
                            // if (state[present][middle][rCenter][cCenter]) count++;
                            if (state[present][middle][rCenter][right]) count++;
                            if (state[present][middle][down][left]) count++;
                            if (state[present][middle][down][cCenter]) count++;
                            if (state[present][middle][down][right]) count++;

                            if (state[present][top][up][left]) count++;
                            if (state[present][top][up][cCenter]) count++;
                            if (state[present][top][up][right]) count++;
                            if (state[present][top][rCenter][left]) count++;
                            if (state[present][top][rCenter][cCenter]) count++;
                            if (state[present][top][rCenter][right]) count++;
                            if (state[present][top][down][left]) count++;
                            if (state[present][top][down][cCenter]) count++;
                            if (state[present][top][down][right]) count++;

                            if (state[future][bottom][up][left]) count++;
                            if (state[future][bottom][up][cCenter]) count++;
                            if (state[future][bottom][up][right]) count++;
                            if (state[future][bottom][rCenter][left]) count++;
                            if (state[future][bottom][rCenter][cCenter]) count++;
                            if (state[future][bottom][rCenter][right]) count++;
                            if (state[future][bottom][down][left]) count++;
                            if (state[future][bottom][down][cCenter]) count++;
                            if (state[future][bottom][down][right]) count++;

                            if (state[future][middle][up][left]) count++;
                            if (state[future][middle][up][cCenter]) count++;
                            if (state[future][middle][up][right]) count++;
                            if (state[future][middle][rCenter][left]) count++;
                            if (state[future][middle][rCenter][cCenter]) count++;
                            if (state[future][middle][rCenter][right]) count++;
                            if (state[future][middle][down][left]) count++;
                            if (state[future][middle][down][cCenter]) count++;
                            if (state[future][middle][down][right]) count++;

                            if (state[future][top][up][left]) count++;
                            if (state[future][top][up][cCenter]) count++;
                            if (state[future][top][up][right]) count++;
                            if (state[future][top][rCenter][left]) count++;
                            if (state[future][top][rCenter][cCenter]) count++;
                            if (state[future][top][rCenter][right]) count++;
                            if (state[future][top][down][left]) count++;
                            if (state[future][top][down][cCenter]) count++;
                            if (state[future][top][down][right]) count++;

                            if (state[t][z][r][c] && (count == 2 || count == 3))
                            {
                                next[t][z][r][c] = true;
                            }
                            else if (!state[t][z][r][c] && count == 3)
                            {
                                next[t][z][r][c] = true;
                            }
                            else
                            {
                                next[t][z][r][c] = false;
                            }
                        }
                    }
                }
            }

            return next;
        }
    }
}
