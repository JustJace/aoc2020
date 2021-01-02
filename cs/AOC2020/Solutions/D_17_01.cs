using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using AOC2020.Structure;
using AOC2020.Search;

namespace AOC2020.Solutions
{
    public class D_017_1 : Solver<long>
    {
        public override int Day => 17;
        public override int Part => 1;
        protected override string Filename => @"Inputs\D_17.input";
        protected override long GetAnswer(string input)
        {
            var initialState = input.As2DArray<char>();
            var state = new bool[25][][];

            for (var z = 0; z < 25; z++)
            {
                state[z] = new bool[25][];

                for (var r = 0; r < 25; r++)
                {
                    state[z][r] = new bool[25];
                }
            }
            
            for (var r = 0; r < initialState.Length; r++)
            {
                for (var c = 0; c < initialState[r].Length; c++)
                {
                    if (initialState[r][c] == '#')
                    {
                        state[0][r][c] = true;
                    }
                }
            }

            // for (var i = 0; i < 6; i++)
            // {
            //     state = NextState(state);

            //     for (var z = 0; z < 25; z++)
            //     {
            //         Console.WriteLine($"z={z}");
            //         for (var r = 0; r < 25; r++)
            //         {
            //             for (var c = 0; c < 25; c++)
            //             {
            //                 Console.Write(state[z][r][c] ? '#' : '.');
            //             }
            //             Console.WriteLine();
            //         }
            //         Console.WriteLine();
            //     }

            //     Console.ReadLine();
            // }

            var answer = 0;

            for (var z = 0; z < 25; z++)
            {
                for (var r = 0; r < 25; r++)
                {
                    for (var c = 0; c < 25; c++)
                    {
                        if (state[z][r][c]) answer++;
                    }
                }
            }

            return answer;
        }
        private bool[][][] NextState(bool[][][] state)
        {
            var next = new bool[25][][];
            for (var z = 0; z < 25; z++)
            {
                next[z] = new bool[25][];

                for (var r = 0; r < 25; r++)
                {
                    next[z][r] = new bool[25];
                }
            }

            for (var z = 0; z < 25; z++)
            {
                for (var r = 0; r < 25; r++)
                {
                    for (var c = 0; c < 25; c++)
                    {
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

                        if (state[bottom][up][left]) count++;
                        if (state[bottom][up][cCenter]) count++;
                        if (state[bottom][up][right]) count++;
                        if (state[bottom][rCenter][left]) count++;
                        if (state[bottom][rCenter][cCenter]) count++;
                        if (state[bottom][rCenter][right]) count++;
                        if (state[bottom][down][left]) count++;
                        if (state[bottom][down][cCenter]) count++;
                        if (state[bottom][down][right]) count++;

                        if (state[middle][up][left]) count++;
                        if (state[middle][up][cCenter]) count++;
                        if (state[middle][up][right]) count++;
                        if (state[middle][rCenter][left]) count++;
                        // if (state[middle][rCenter][cCenter]) count++;
                        if (state[middle][rCenter][right]) count++;
                        if (state[middle][down][left]) count++;
                        if (state[middle][down][cCenter]) count++;
                        if (state[middle][down][right]) count++;

                        if (state[top][up][left]) count++;
                        if (state[top][up][cCenter]) count++;
                        if (state[top][up][right]) count++;
                        if (state[top][rCenter][left]) count++;
                        if (state[top][rCenter][cCenter]) count++;
                        if (state[top][rCenter][right]) count++;
                        if (state[top][down][left]) count++;
                        if (state[top][down][cCenter]) count++;
                        if (state[top][down][right]) count++;

                        if (state[z][r][c] && (count == 2 || count == 3))
                        {
                            next[z][r][c] = true;
                        }
                        else if (!state[z][r][c] && count == 3)
                        {
                            next[z][r][c] = true;
                        }
                        else
                        {
                            next[z][r][c] = false;
                        }
                    }
                }
            }
            
            return next;
        }
    }
}
