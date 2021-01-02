using System.Linq;

namespace AOC2020
{
    public static class AOCMath
    {
        public static int GCD(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a | b;
        }

        public static int LCM(int a, int b)
        {
            return (a / GCD(a, b)) * b;
        }

        public static long GCD(long a, long b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a | b;
        }

        public static long LCM(long a, long b)
        {
            return (a / GCD(a, b)) * b;
        }

        public static ulong GCD(ulong a, ulong b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a | b;
        }

        public static ulong LCM(ulong a, ulong b)
        {
            return (a / GCD(a, b)) * b;
        }

        // Stolen from somewhere on the internet.
        public static ulong CRT(ulong[] mods, ulong[] remainders)
        {
            ulong prod = mods.Aggregate(1ul, (i, j) => i * j);
            ulong p;
            ulong sm = 0;
            for (int i = 0; i < mods.Length; i++)
            {
                p = prod / mods[i];
                sm += remainders[i] * ModularMultiplicativeInverse(p, mods[i]) * p;
            }
            return sm % prod;
        }
 
        private static ulong ModularMultiplicativeInverse(ulong a, ulong mod)
        {
            ulong b = a % mod;
            for (ulong x = 1; x < mod; x++)
            {
                if ((b * x) % mod == 1)
                {
                    return x;
                }
            }
            return 1;
        }
    }
}