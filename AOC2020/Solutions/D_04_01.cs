using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using AOC2020.Structure;
using AOC2020.Search;

namespace AOC2020.Solutions
{
    public class D_04_1 : Solver<int>
    {
        public override int Day => 4;
        public override int Part => 1;
        protected override string Filename => @"Inputs\D_04.input";

        protected override int GetAnswer(string input)
        {
            return Parse(input).Count(p => p.IsValid);
        }

        private List<Passport> Parse(string input)
        {
            var passports = new List<Passport>();
            var passportData = input.Split(Environment.NewLine + Environment.NewLine);
            foreach (var passportDatum in passportData)
            {
                var passport = new Passport();
                var sublines = passportDatum.Split(Environment.NewLine);

                foreach (var subline in sublines)
                {
                    var fields = subline.Split(' ');

                    foreach (var field in fields)
                    {
                        var (name, value) = field.SplitInTwo(':');
                        passport.Fields[name] = value;
                    }
                }

                passports.Add(passport);
            }
            return passports;
        }

        class Passport
        {
            public Dictionary<string, string> Fields = new Dictionary<string, string>();

            public bool IsValid => Fields.Keys.Count == 8 || (Fields.Keys.Count == 7 && !Fields.Keys.Contains("cid"));
        }
    }
}
