using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using AOC2020.Structure;
using AOC2020.Search;

namespace AOC2020.Solutions
{
    public class D_04_2 : Solver<int>
    {
        public override int Day => 4;
        public override int Part => 2;
        protected override string Filename => @"Inputs\D_04.input";

        protected override int GetAnswer(string input)
        {
            return Parse(input).Count(p => p.IsValid());
        }
        private List<Passport> Parse(string input)
        {
            var passports = new List<Passport>();
            var passportData = input.Split(Environment.NewLine + Environment.NewLine);
            foreach (var datum in passportData)
            {
                var passport = new Passport();
                var sublines = datum.Split(Environment.NewLine);

                foreach (var subline in sublines)
                {
                    var fields = subline.Split(' ');

                    foreach (var field in fields)
                    {
                        var (name, value) = field.SplitTwo(':');
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

            public bool IsValid()
            {
                return (Fields.Keys.Count == 8 || (Fields.Keys.Count == 7 && !Fields.Keys.Contains("cid"))) 
                    && Fields.All(kvp => IsValidField(kvp.Key, kvp.Value));
            }

            private bool IsValidField(string field, string value)
            {
                switch (field)
                {
                    case "byr": return int.TryParse(value, out int byr) && byr >= 1920 && byr <= 2002;
                    case "iyr": return int.TryParse(value, out int iyr) && iyr >= 2010 && iyr <= 2020;
                    case "eyr": return int.TryParse(value, out int eyr) && eyr >= 2020 && eyr <= 2030;
                    case "hgt": 
                        var regex = @"(\d+)(in|cm)";
                        if (Regex.IsMatch(value, regex))
                        {
                            var (hgt, unit) = value.Regex<int, string>(regex);
                            if (unit == "cm")
                            {
                                return hgt >= 150 && hgt <= 193;
                            }
                            else
                            {
                                return hgt >= 59 && hgt <= 76;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    case "hcl": return Regex.IsMatch(value, "#[0-9a-f]{6}");
                    case "ecl": return new [] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }.Contains(value);
                    case "pid": return Regex.IsMatch(value, "^[0-9]{9}$");
                    case "cid": return true;
                    default: throw new Exception("Unexpected field name");
                }
            }
        }
    }
}
