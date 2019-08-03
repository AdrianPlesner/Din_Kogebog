using System;
using System.Collections.Generic;
namespace Din_Kogebog
{
    public enum Unit
    {
        g,
        kg,
        dl,
        l,
        spsk,
        tsk,
        knsp,
        stk,
        ml
    }

    public class Recipe
    {
        public Recipe(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        private Dictionary<string, (double, Unit)> Ingredients = new Dictionary<string, (double, Unit)>();

        private SortedList<int, string> Steps = new SortedList<int, string>(); 

        public int Difficulty { 
            get
            {
                return Difficulty;
            }
            set
            {  
                if (value > 0 && value < 5) 
                {
                    Difficulty = value; 
                } 
            } 
        }

        public int Time {
            get
            {
                return Time;
            }
            set
            {
                if(value > 0)
                {
                    Time = value;
                }
            }
        }

        public int Servings { get; set; }

        public bool Baking { get; set; }

        public int BakingTime { get; set; }

        public string BakingMode { get; set; }

        public List<string> Categories = new List<string>();

        public void AddIngredient(string ingredient, (double, Unit) amount)
        {
            Ingredients.Add(ingredient, amount);
        }

        public void EditIngredient(string ingredient, (double, Unit) newAmount)
        {
            Ingredients[ingredient] = newAmount;
        }

        //TODO: Add removing method

        public string PrintIngredientList()
        {
            string result = "";

            foreach(var kvPair in Ingredients)
            {
                result += $"{kvPair.Value.Item1} {PrintUnit(kvPair.Value.Item2)} {kvPair.Key} \n";
            }

            return result;
        }

        public void AddStep(int num, string step)
        {
            Steps.Add(num, step);
            //TODO:funrionality to change order of steps easily
        }

        public void EditStep(int num, string newStep)
        {
            Steps[num] = newStep;
        }

        //TODO: Add removing method

        public string PrintSteps()
        {
            string result = "";

            foreach(var item in Steps)
            {
                result += $"{item.Key} {item.Value} \n";
            }

            return result;
        }

        public void ConvertSize(int newSize)
        {
            throw new NotImplementedException();
        }

        public void PrintRecipe()
        {
            Console.Clear();
            Console.WriteLine(Name);
            Console.WriteLine("Servings: " + Servings + "\t" + "Difficulty: " + Difficulty + "\t" + ");
            //TODO: Add print difficulty and coocking time
            Console.WriteLine(PrintCategories());
            //TODO: Add funktionality for bakeing

            Console.WriteLine(PrintIngredientList());
            Console.WriteLine(PrintSteps());
        }

        public string PrintCategories()
        {
            if (Categories.Count == 0)
            {
                return null;
            }
            else
            {
                string result = "Categories: ";
                foreach (var category in Categories)
                {
                    result += category + " | ";
                }
                return result;
            }
        }

        private string PrintUnit(Unit u) 
        {
            
            switch (u)
            {
                case Unit.g:
                    return "g";
                case Unit.dl:
                    return "dl";
                case Unit.kg:
                    return "kg";
                case Unit.knsp:
                    return "knsp";
                case Unit.l:
                    return "l";
                case Unit.ml:
                    return "ml";
                case Unit.spsk:
                    return "spsk";
                case Unit.stk:
                    return "stk";
                case Unit.tsk:
                    return "tsk";
                default:
                    return "Nan";
                
            }
        }


    }
}
