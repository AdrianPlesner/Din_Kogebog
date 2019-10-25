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

        public int Time { get; set; }

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

        public bool GetTime()
        {

            string input = ConsoleHelper.PromptForInput("Hvor lang tid tager det at lave opskriften? [minutter] (ca)");
            int time;
            while(!Int32.TryParse(input, out time))
            {
                input = ConsoleHelper.PromptForInput("Dit input er af forkert format, prøv at skrive et tal i hele minutter. :) \nHvor lang tid tager det at lave opskriften? [minutter] (ca)");
                if(input == null)
                {
                    return false;
                }
            }
            Time = time;
            return true;


        }

        public void AddStep(int num, string step)
        {
            //TODO: formater teskt til at starte med stort og slutte på punktum
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

        public bool GetServings()
        {
            string input = ConsoleHelper.PromptForInput("Hvor mange portioner giver opskriften? (ca)");
            int servings;
            while (!Int32.TryParse(input, out servings))
            {
                Console.WriteLine("Dit input er af forkert format, prøv at skrive et helt tal. :)");
                input = ConsoleHelper.PromptForInput("Hvor mange portioner giver opskriften? (ca)");
                if (input == null)
                {
                    return false;
                }
            }
            Servings = servings;
            return true;
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
            if (BakingTime != default)
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

        public bool GetCategories()
        {
            string[] input = ConsoleHelper.PromptForInput("Hvilke kategorier går denne opskrift ind under? [kat1,kat2,kat3,...] (Fx: aftensmad, brød, desert) ").Split(", ");
            if(input == null)
            {
                return false;
            }
            foreach( string s in input){
                s.Replace(s[0], char.ToUpper(s[0]));
                Categories.Add(s);
            }
                
            return true;

        }

        public bool GetIngredients()
        {
            //TODO: behaviour for already added ingredient
            bool cont = true, result = true;
            while (cont)
            {
                string[] ingredient = ConsoleHelper.PromptForInput($"Ingredienser\n{Name}\n" +
                    $"{PrintIngredientList()}Tilføj næste ingrediens: [mængde enhed navn]").ToLower().Split(" ");
                //TODO: fail check if wrong number of arguments
                if (ingredient == null)
                {
                    result = cont = false;
                }
                else
                {
                    Unit unit = Recipe.ParseUnit(ingredient[1]);
                    if (unit == Unit.nan)
                    {
                        Console.WriteLine("Ukendt enhed! Prøv igen. :)");
                        Console.ReadKey();
                        continue;
                    }
                    bool correct = double.TryParse(ingredient[0], out double amount);
                    if (!correct)
                    {
                        Console.WriteLine("Mængden skal være et tal! Prøv igen. :)");
                        Console.ReadKey();
                        continue;
                    }
                    AddIngredient(ingredient[2], (amount, unit));
                    cont &= ConsoleHelper.PromptYesNo($"Er der flere ingredienser i {Name}?");
                }
            }
            return result;
        }

        public bool GetSteps()
        {
            bool result = true, cont = true;
            while (cont)
            {
                string[] step = ConsoleHelper.PromptForInput($"Fremgangsmåde\n{Name}\n"
                    + $"{PrintSteps()}Hvad er næste trin? [nr : trin]").Split(":");
                //TODO: fail check for wrong number of arguments
                if (step == null)
                {
                    result = cont = false;
                }
                else
                {
                    bool correctNum = int.TryParse(step[0], out int stepNum);
                    if (!correctNum)
                    {
                        Console.WriteLine("Trinnet skal have et nummer der er et tal. Prøv igen :)");
                        Console.ReadKey();
                        continue;
                    }
                    AddStep(stepNum, step[1]);
                    cont &= ConsoleHelper.PromptYesNo($"Er der flere trin i {Name}?");
                }
            }
            return result;
        }

        public bool GetBaking()
        {
            if (ConsoleHelper.PromptYesNo($"Skal {Name} bages?"))
            {
                //TODO: securing
                Baking = true;
                BakingMode = ConsoleHelper.PromptForInput("Hvilken indstilling skal ovnen have? fx varmluft, alm ovn");

                int.TryParse(ConsoleHelper.PromptForInput("Hvilken temperatur [C] skal ovnen have?"),out int temp);
                BakingTemperature = temp;

                int.TryParse(ConsoleHelper.PromptForInput("Hvor lang tid skal retten være i ovnen?[min]"), out int time);
                BakingTime = time;

                return true;

            }
            else
            {
                Baking = false;
                return true;
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
