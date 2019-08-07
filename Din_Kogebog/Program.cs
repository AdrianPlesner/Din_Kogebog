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
            throw new NotImplementedException();
        }

        private static bool GetIngredients(Recipe recipe)
        {
            //TODO: already added ingredient, text should be all lowercase
            bool cont = true;
            bool result = true;
            while (cont)
            {
                string[] ingredient = PromptForInput($"Tilføj ingredienser\n{recipe.Name}\n" +
                    $"{recipe.PrintIngredientList()}Tilføj næste ingrediens: [mængde enhed navn]").Split(" ");
                if (ingredient == null)
                {
                    result = cont = false;
                }
                else
                {
                    Unit unit = Recipe.ParseUnit(ingredient[1]);
                    if(unit == Unit.nan)
                    {
                        Console.WriteLine("Ukendt enhed! Prøv igen. :)");
                        Console.ReadKey();
                        continue;
                    }
                    else
                    {
                        bool correct = double.TryParse(ingredient[0], out double amount);
                        if (!correct)
                        {
                            Console.WriteLine("Mængden skal være et tal! Prøv igen. :)");
                            Console.ReadKey();
                            continue;
                        }
                        else
                        {
                            recipe.AddIngredient(ingredient[2], (amount, unit));
                            bool confirm = true;
                            bool con2 = true;
                            while (con2)
                            {
                                Console.Clear();
                                Console.WriteLine($"{recipe.Name}\n{recipe.PrintIngredientList()}Er der flere ingredienser?");
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
                                    case 37:
                                    case 39:
                                        {
                                            if (confirm)
                                                confirm = false;
                                            else
                                                confirm = true;
                                            break;
                                        }
                                    case 13:
                                        {
                                            con2 = false;
                                            if (confirm)
                                            {
                                                cont = true;
                                            }
                                            else
                                            {
                                                cont = false;
                                                result = true;
                                            }
                                            
                                            break;
                                        }
                                    case 27:
                                        {
                                            cont = false;
                                            con2 = false;
                                            break;
                                        }
                                    default:
                                        break;
                                }

                            }
                        }
                    }
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
                            if (confirm)
                                confirm = false;
                            else
                                confirm = true;
                            break;
                        }
                    case 13:
                        {
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
                            return null;
                        }
                    default:
                        break;
                }
                

                
            }

            return input;
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
