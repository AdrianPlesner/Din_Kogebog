using System;
using System.Collections.Generic;

namespace Din_Kogebog
{
    class Program
    {
        static void Main()
        {
            SetupMenu();
            RecipeList = LoadRecipes();
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

            }

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
