using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Aoc2019.Solutions
{
    public class D_01_2 : Solver<int>
    {
        public override int Day => 1;
        public override int Part => 2;
        protected override string Filename => @"Inputs\D_01.input";

        protected override int GetAnswer(string input)
        {
            var numbers = input.AsIntArray();
            
            for (var i = 0; i < numbers.Length - 2; i++) 
            for (var j = i + 1; j < numbers.Length - 1; j++) 
            for (var k = j + 1; k < numbers.Length; k++) 
                if (numbers[i] + numbers[j] + numbers[k] == 2020) 
                    return numbers[i] * numbers[j] * numbers[k];

            throw new Exception();
        }
    }
}        
        