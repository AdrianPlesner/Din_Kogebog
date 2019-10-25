using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

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
            return File.Exists("recipies.json") ? JsonConvert.DeserializeObject<List<Recipe>>(File.ReadAllText("recipies.json")) : RecipeList;
        }

        private static void SaveRecipes(List<Recipe> recipes)
        {
            File.WriteAllText("recipies.json", JsonConvert.SerializeObject(RecipeList));
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

            // Main menu config
            MainMenu.AddMenuItem(AddNewRecipe);
            MainMenu.AddMenuItem(GetRecipeMenu);
            MainMenu.AddMenuItem(ImportExportMenu);
            MainMenu.AddMenuItem(SettingsMenu);
            MainMenu.AddMenuItem(Exit);

            // Get Recipe config
            //GetRecipeMenu.AddMenuItem(new ActionMenuItem("Vis alle opskrifter") { SelectAction = () => { foreach (Recipe r in RecipeList) { r.PrintRecipe(); } Console.ReadKey(); } });
            GetRecipeMenu.AddMenuItem(new Menu("Søg efter navn"));
            GetRecipeMenu.AddMenuItem(new Menu("Søg efter ingrediens"));
            GetRecipeMenu.AddMenuItem(new Menu("Søg efter tid"));
            GetRecipeMenu.AddMenuItem(new ActionMenuItem("Tilbage") { SelectAction = MainMenu.Select });
            

            // Import/export config 

            // Settings config

            // Exit config
            Exit.SelectAction = () =>
            {
                SaveRecipes(RecipeList);
                Environment.Exit(0);

            };
        }

        private static void NewRecipe()
        {
            string name = ConsoleHelper.PromptForInput("Hvad er navnet på din nye opskrift?");
            if (name == null)
            {
                MainMenu.Select();
            }
            else
            {
                Recipe newRecipe = new Recipe(name);
                int step = 1;
                bool cont = true;
                while (cont)
                {
                    switch (step)
                    {
                        case -1:
                            {
                                cont = false;
                                MainMenu.Select();
                                break;
                            }
                        case 1:
                            {
                                if (!newRecipe.GetTime())
                                {
                                    step = -1;
                                }
                                else
                                {
                                    step++;
                                }
                                break;
                            }
                        case 2:
                            {
                                if (!newRecipe.GetServings())
                                {
                                    step = -1;
                                }
                                else
                                {
                                    step++;
                                }
                                break;
                            }
                        case 3:
                            {
                                if (!newRecipe.GetIngredients())
                                {
                                    step = -1;
                                }
                                else
                                {
                                    step++;
                                }
                                break;
                            }
                        case 4:
                            {
                                if (!newRecipe.GetSteps())
                                {
                                    step = -1;
                                }
                                else
                                {
                                    step++;
                                }
                                break;
                            }
                        case 5:
                            {
                                if (!newRecipe.GetBaking())
                                {
                                    step = -1;
                                }
                                else
                                {
                                    step++;
                                }
                                break;
                            }
                        case 6:
                            {
                                if (!newRecipe.GetCategories())
                                {
                                    step = -1;
                                }
                                else
                                {
                                    step++;
                                }
                                break;
                            }
                        default:
                            {
                                //done
                                cont = false;
                                break;
                            }
                    }
                }

                RecipeList.Add(newRecipe);
                MainMenu.Select();

            }
        }
    }        
}
