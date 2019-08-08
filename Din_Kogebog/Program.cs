using System;
using System.Collections.Generic;

namespace Din_Kogebog
{
    class Program
    {
        static void Main()
        {
            SetupMenu();
            //RecipeList = LoadRecipes();
            MainMenu.Select();


        }

        private static List<Recipe> LoadRecipes()
        {
            throw new NotImplementedException();
        }

        private static void SaveRecipes(List<Recipe> recipes)
        {
            throw new NotImplementedException();
        }

        static Menu MainMenu = new Menu("Din Kogebog", "Hvordan vil du fortsætte?");

        static List<Recipe> RecipeList = new List<Recipe>();

        static void SetupMenu()
        {
            ActionMenuItem AddNewRecipe = new ActionMenuItem("Ny opskrift", NewRecipe);

            Menu GetRecipeMenu = new Menu("Find opskrift");

            Menu ImportExportMenu = new Menu("Importer / eksporter opskrifter");

            Menu SettingsMenu = new Menu("Indstillinger");

            ActionMenuItem Exit = new ActionMenuItem("Luk");


            MainMenu.AddMenuItem(AddNewRecipe);
            MainMenu.AddMenuItem(GetRecipeMenu);
            MainMenu.AddMenuItem(ImportExportMenu);
            MainMenu.AddMenuItem(SettingsMenu);
            MainMenu.AddMenuItem(Exit);
        }

        static private void NewRecipe()
        {
            string name = PromptForInput("Hvad er navnet på din nye opskrift?");
            if(name == null)
            {
                MainMenu.Select();
            }
            else
            {
                Recipe newRecipe = new Recipe(name);

                if (!GetIngredients(newRecipe))
                {
                    MainMenu.Select();
                }
                if (!GetSteps(newRecipe))
                {
                    MainMenu.Select();
                }


                RecipeList.Add(newRecipe);

            }
        }

        private static bool GetSteps(Recipe recipe)
        {
            bool result = true, cont = true;
            while (cont){
                string[] step = PromptForInput($"Fremgangsmåde\n{recipe.Name}\n"
                    + $"{recipe.PrintSteps()}Hvad er næste trin? [nr : trin]").Split(":");
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
                    recipe.AddStep(stepNum, step[1]);
                    cont &= PromptYesNo($"Er der flere trin i {recipe.Name}?");
                }
            }
            return result;
        }

        private static bool GetIngredients(Recipe recipe)
        {
            //TODO: behaviour for already added ingredient
            bool cont = true, result = true;
            while (cont)
            {
                string[] ingredient = PromptForInput($"Ingredienser\n{recipe.Name}\n" +
                    $"{recipe.PrintIngredientList()}Tilføj næste ingrediens: [mængde enhed navn]").ToLower().Split(" ");
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
                    recipe.AddIngredient(ingredient[2], (amount, unit));
                    cont &= PromptYesNo($"Er der flere ingredienser i {recipe.Name}?");
                }
            }
            return result;
        }

        static private string PromptForInput(string question)
        {
            bool cont = true;
            string input = null;
            bool confirm = true;
            while (cont) {
                Console.Clear();
                Console.WriteLine(question);
                if (input == null)
                {
                    input = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine(input);
                }
                Console.WriteLine($"Er: {input} korrekt?");
                if (confirm)
                {
                    ReverseColors();
                    Console.Write("Ja");
                    ReverseColors();
                    Console.Write("/Nej");
                }
                else
                {
                    Console.Write("Ja/");
                    ReverseColors();
                    Console.Write("Nej");
                    ReverseColors();
                }
                ConsoleKeyInfo inkey = Console.ReadKey();
                switch ((int)inkey.Key)
                {
                    case 37: case 39:
                        {
                            //left/right arrow
                            if (confirm)
                                confirm = false;
                            else
                                confirm = true;
                            break;
                        }
                    case 13:
                        {
                            //enter
                            if (confirm)
                            {
                                return input;
                            }
                            else
                            {
                                input = null;
                            }
                            break;
                        }
                    case 27:
                        {
                            //esc
                            return null;
                        }
                    default:
                        break;
                }
                

                
            }

            return input;
        }

        static private bool PromptYesNo(string question)
        {
            bool confirm = true, cont = true;
            while (cont)
            {
                Console.Clear();
                Console.WriteLine(question);
                if (confirm)
                {
                    ReverseColors();
                    Console.Write("Ja");
                    ReverseColors();
                    Console.Write("/Nej");
                }
                else
                {
                    Console.Write("Ja/");
                    ReverseColors();
                    Console.Write("Nej");
                    ReverseColors();
                }
                ConsoleKeyInfo inkey = Console.ReadKey();
                switch ((int)inkey.Key)
                {
                    case 37: case 39:
                        {
                            //left/right arrows
                            if (confirm)
                                confirm = false;
                            else
                                confirm = true;
                            break;
                        }
                    case 27:
                        {
                            //esc
                            cont = confirm = false;
                            break;
                        }
                    case 13:
                        {
                            // enter
                            cont = false;
                            break;
                        }
                    default:
                        break;
                }
            }
            return confirm;
        }

        static private void ReverseColors()
        {
            ConsoleColor background = Console.BackgroundColor;
            ConsoleColor foreground = Console.ForegroundColor;

            Console.BackgroundColor = foreground;
            Console.ForegroundColor = background;
        }
    }
}
