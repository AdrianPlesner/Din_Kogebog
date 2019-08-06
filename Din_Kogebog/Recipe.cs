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
        ml,
        nan
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

        public int BakingTemperature { get; set; }

        public List<string> Categories = new List<string>();

        public void AddIngredient(string ingredient, (double, Unit) amount)
        {
            Ingredients.Add(ingredient, amount);
        }

        public void EditIngredient(string ingredient, (double, Unit) newAmount)
        {
            Ingredients[ingredient] = newAmount;
        }

        public void RemoveIngredient(string ingredient)
        {
            Ingredients.Remove(ingredient);
            //TODO: add "are you sure"
        }

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
            if(num == Steps.Count+1)
            {
                Steps.Add(num, step);
            }
            else if(num > Steps.Count)
            {
                AddStep(Steps.Count + 1, step);
            }
            else
            {
                for(int i = Steps.Count; i >= num; i--)
                {
                    string buffer = Steps[i];
                    Steps.Add(i + 1, buffer);
                    Steps.Remove(i);
                }
                Steps.Add(num, step);
            }
        }

        public void EditStep(int num, string newStep)
        {
            Steps[num] = newStep;
        }

        public void RemoveStep(int num)
        {
            int end = Steps.Count;

            Steps.Remove(num);
            
            for(int i = num + 1; i <= end; i++)
            {
                string buffer = Steps[i];
                Steps.Add(i - 1, buffer);
                Steps.Remove(i);
            }
        }

        public string PrintSteps()
        {
            string result = "";

            foreach(var item in Steps)
            {
                result += $"{item.Key}. {item.Value} \n";
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
            Console.WriteLine("Portioner: " + Servings + "\t" + "Sværhedsgrad: "
                + Difficulty + "\t" + "Tid: " + Time);
            Console.WriteLine(PrintCategories());
            Console.WriteLine(PrintBaking());
            
            Console.WriteLine(PrintIngredientList());
            Console.WriteLine(PrintSteps());
        }

        public string PrintBaking()
        {
            if (Baking)
            {
                return $"Bages i {BakingTime} ved {BakingTemperature}" +
                    $" grader, ved {BakingMode}";
            }
            else
            {
                return null;
            }
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

        public static Unit ParseUnit(string unit)
        {
            if (unit == "g")
                return Unit.g;
            else if (unit == "dl")
                return Unit.dl;
            else if (unit == "kg")
                return Unit.kg;
            else if (unit == "knsp")
                return Unit.knsp;
            else if (unit == "l")
                return Unit.l;
            else if (unit == "ml")
                return Unit.ml;
            else if (unit == "spsk")
                return Unit.spsk;
            else if (unit == "stk")
                return Unit.stk;
            else if (unit == "tsk")
                return Unit.tsk;
            else
                return Unit.nan;
        }


    }
}
