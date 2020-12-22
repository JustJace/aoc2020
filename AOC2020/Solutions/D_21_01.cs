using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using AOC2020.Structure;
using AOC2020.Search;

namespace AOC2020.Solutions
{
    public class D_21_1 : Solver<long>
    {
        public override int Day => 21;
        public override int Part => 1;
        protected override string Filename => @"Inputs\D_21.input";
        protected override long GetAnswer(string input)
        {
            var (foods, ingredients, allergens) = Parse(input);

            var allergenNotPossibleIngredientMap = new Dictionary<string, HashSet<string>>();

            foreach (var allergen in allergens)
            {
                allergenNotPossibleIngredientMap[allergen] = new HashSet<string>();

                foreach (var food in foods)
                {
                    if (food.allergens.Contains(allergen))
                    {
                        foreach (var ing in ingredients.Keys.Except(food.ingredients))
                        {
                            allergenNotPossibleIngredientMap[allergen].Add(ing);
                        }
                    }
                }
            }

            var allergenIngredients = new List<string>();

            while (allergenIngredients.Count < allergens.Count)
            {
                var highest = allergenNotPossibleIngredientMap.OrderByDescending(kvp => kvp.Value.Count).First();
                var allergenIngredient = ingredients.Keys.Except(highest.Value).First();
                allergenNotPossibleIngredientMap.Remove(highest.Key);
                foreach (var other in allergenNotPossibleIngredientMap)
                {
                    allergenNotPossibleIngredientMap[other.Key].Add(allergenIngredient);
                }

                allergenIngredients.Add(allergenIngredient);
            }

            var count = 0;

            foreach (var food in foods)
            {
                count += food.ingredients.Except(allergenIngredients).Count();
            }

            return count;
        }

        private (List<food>, Dictionary<string, int>, HashSet<string>) Parse(string input)
        {
            var foods = new List<food>();
            var allIngredients = new Dictionary<string, int>();
            var allAllergens = new HashSet<string>();

            foreach (var line in input.PerNewLine())
            {
                var sections = line.Split('(').Select(s => s.Trim()).ToArray();
                var ingredients = ParseIngredients(sections[0]);
                var allergens = ParseAllergens(sections[1]);
                foods.Add(new food
                {
                    ingredients = ingredients,
                    allergens = allergens
                });

                ingredients.ForEach(i => 
                {
                    if (!allIngredients.ContainsKey(i)) allIngredients[i] = 0;
                    allIngredients[i]++;
                });
                allergens.ForEach(a => allAllergens.Add(a));
            }

            return (foods, allIngredients, allAllergens);
        }

        private List<string> ParseIngredients(string s)
        {
            return s.Split(' ').ToList();
        }
        private List<string> ParseAllergens(string s)
        {
            return s
                .Replace("contains", "")
                .Replace(")", "")
                .Trim()
                .Split(',')
                .Select(a => a.Trim())
                .ToList();
        }

        class food
        {
            public List<string> ingredients = new List<string>();
            public List<string> allergens = new List<string>();
        }
    }
}