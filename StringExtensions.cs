using System;
using System.Linq;
using System.Text.RegularExpressions;

public static class StringExtensions
{
    public static int[] AsIntArray(this string input)
    {
        return input.Split(Environment.NewLine).Select(int.Parse).ToArray();
    }

    public static string[] AsStringArray(this string input)
    {
        return input.Split(Environment.NewLine).ToArray();
    }

    public static char[] AsCharArray(this string input)
    {
        return input.Split(Environment.NewLine).Select(char.Parse).ToArray();
    }

    public static double[] AsDoubleArray(this string input)
    {
        return input.Split(Environment.NewLine).Select(double.Parse).ToArray();
    }
    
    public static long[] AsLongArray(this string input)
    {
        return input.Split(Environment.NewLine).Select(long.Parse).ToArray();
    }

    public static T1 Regex<T1>(this string input, string pattern)
    {
        var match = System.Text.RegularExpressions.Regex.Match(input, pattern);
        return (T1)Parse<T1>(match.Groups[1].ToString());
    }

    public static (T1, T2) Regex<T1, T2>(this string input, string pattern)
    {
        var match = System.Text.RegularExpressions.Regex.Match(input, pattern);
        return
        (
            (T1)Parse<T1>(match.Groups[1].ToString()),
            (T2)Parse<T2>(match.Groups[2].ToString())
        );
    }

    public static (T1, T2, T3) Regex<T1, T2, T3>(this string input, string pattern)
    {
        var match = System.Text.RegularExpressions.Regex.Match(input, pattern);
        return
        (
            (T1)Parse<T1>(match.Groups[1].ToString()),
            (T2)Parse<T2>(match.Groups[2].ToString()),
            (T3)Parse<T3>(match.Groups[3].ToString())
        );
    }

    public static (T1, T2, T3, T4) Regex<T1, T2, T3, T4>(this string input, string pattern)
    {
        var match = System.Text.RegularExpressions.Regex.Match(input, pattern);
        return
        (
            Parse<T1>(match.Groups[1].ToString()),
            Parse<T2>(match.Groups[2].ToString()),
            Parse<T3>(match.Groups[3].ToString()),
            Parse<T4>(match.Groups[4].ToString())
        );
    }

    private static TYPE Parse<TYPE>(string input)
    {
        object value;

        if (typeof(TYPE) == typeof(int))
        {
            value = int.Parse(input);
        }
        else if (typeof(TYPE) == typeof(long))
        {
            value = long.Parse(input);
        }
        else if (typeof(TYPE) == typeof(double))
        {
            value = double.Parse(input);
        }
        else if (typeof(TYPE) == typeof(string))
        {
            value = input;
        }
        else if (typeof(TYPE) == typeof(char))
        {
            value = char.Parse(input);
        }
        else
        {
            throw new Exception("Trying to parse unexpected value type");
        }

        return (TYPE)value;
    }
}