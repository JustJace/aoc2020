using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using AOC2020.Structure;
using AOC2020.Search;

namespace AOC2020.Solutions
{
    public class D_25 : Solver<ulong>
    {
        public const ulong INITIAL_SUBJECT = 7;
        public const ulong MOD = 20201227;
        public override int Day => 25;
        public override int Part => 1;
        protected override string Filename => @"Inputs\D_25.input";
        protected override ulong GetAnswer(string input)
        {
            var (key1, key2) = Keys(input);
            return EncryptionKey(key2, DetermineLoopSize(key1));
        }

        private (ulong, ulong) Keys(string input) {
            var keys = input.PerNewLine();
            var key1 = ulong.Parse(keys[0]);
            var key2 = ulong.Parse(keys[1]);
            return (key1, key2);
        }

        private ulong EncryptionKey(ulong keySubject, ulong loops)
        {
            var EK = 1ul;
            for (var i = 0ul; i < loops; i++)
                EK = Loop(keySubject, EK);
            return EK;
        }

        private ulong DetermineLoopSize(ulong key1)
        {
            var current = 1ul;
            var loops = 0ul;
            while (current != key1) {
                current = Loop(INITIAL_SUBJECT, current);
                loops++;
            }
            return loops;
        }

        private ulong Loop(ulong subject, ulong n) => n * subject % MOD;
    }
}
