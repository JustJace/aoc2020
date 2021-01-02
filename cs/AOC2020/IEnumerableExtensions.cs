using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2020
{
    public static class IEnumerableExtensions 
    {
        public static OUT[] SelectArray<IN, OUT>(this IEnumerable<IN> enumerable, Func<IN, OUT> selectFn)
        {
            return enumerable.Select(selectFn).ToArray();
        }
    }
}