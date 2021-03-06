using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using AOC2020.Structure;
using AOC2020.Search;

namespace AOC2020.Solutions
{
    public class D_05_2 : Solver<int>
    {
        public override int Day => 5;
        public override int Part => 2;
        protected override string Filename => @"Inputs\D_05.input";

        protected override int GetAnswer(string input)
        {
            var seatIds = input.PerNewLine().Select(SeatId).ToList();
            seatIds.Sort();

            for (var i = 0; i < seatIds.Count - 1; i++)
            {
                if (seatIds[i] + 1 != seatIds[i+1])
                {
                    return seatIds[i]+1;
                }
            }

            throw new Exception("Didn't find gap in seat ids");
        }

        private int SeatId(string seat)
        {
            var rowChars = seat.Substring(0, 7);
            var colChars = seat.Substring(7);

            var row = Binary(0, 127, 'F', 'B', rowChars);
            var col = Binary(0, 7, 'L', 'R', colChars);

            return row * 8 + col;
        }

        private int Binary(int low, int high, char lowChar, char highChar, string pattern)
        {
            var factor = (high + 1) / 2;
            for (var i = 0; i < pattern.Length; i++)
            {
                var instruction = pattern[i];
                if (i == pattern.Length - 1)
                {
                    if (instruction == lowChar) return low;
                    else return high;
                }
                else
                {
                    if (instruction == lowChar) high -= factor;
                    else low += factor;

                    factor /= 2;
                }
            }

            throw new Exception("Failed binary search");
        }
    }
}
