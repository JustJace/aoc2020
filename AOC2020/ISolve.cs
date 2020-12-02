using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

public interface ISolve
{
    string Solve();
    int Day { get; }
    int Part { get; }
}

public abstract class Solver<T> : ISolve
{
    public abstract int Day { get; }
    public abstract int Part { get; }
    protected abstract string Filename { get; }

    public string Solve()
    {
        return FormatAnswer(GetAnswer(ReadInput()));
    }

    protected string ReadInput()
    {
        var path = Path.Combine(@"AOC2020", Filename);
        return File.ReadAllText(path);
    }

    protected abstract T GetAnswer(string input);
    protected virtual string FormatAnswer(T answer)
    {
        return answer.ToString();
    }
}