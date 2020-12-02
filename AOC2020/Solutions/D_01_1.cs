using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AOC2020.Solutions
{
    public class D_01_1 : Solver<int>
    {
        public override int Day => 1;
        public override int Part => 1;
        protected override string Filename => @"Inputs\D_01.input";
        
        protected override int GetAnswer(string input)
        {
            var numbers = input.AsIntArray();

            for (var i = 0; i < numbers.Length - 1; i++) 
            for (var j = i + 1; j < numbers.Length; j++) 
                if (numbers[i] + numbers[j] == 2020) 
                    return numbers[i] * numbers[j];

            throw new Exception();
        }
    }
}