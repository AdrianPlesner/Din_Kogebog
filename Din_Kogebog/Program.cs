using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.IO.Compression;
using System.Globalization;

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
            return File.Exists("Recipes/fullList.json") ? JsonConvert.DeserializeObject<List<Recipe>>(File.ReadAllText("Recipes/fullList.json")) : RecipeList;
        }

        private static void SaveRecipes(List<Recipe> recipes)
        {
            if (!Directory.Exists("Recipes"))
            {
                Directory.CreateDirectory("Recipes");
            }
            File.WriteAllText("Recipes/fullList.json", JsonConvert.SerializeObject(recipes));
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
            GetRecipeMenu.AddMenuItem(new ActionMenuItem("Vis alle opskrifter") {
                SelectAction = () => {
                    Console.Clear();
                    Console.WriteLine(PrintAllRecipes());
                    Console.ReadKey();
                    GetRecipeMenu.Select();
                }
            });
            GetRecipeMenu.AddMenuItem(new Menu("Søg efter navn"));

            GetRecipeMenu.AddMenuItem(new Menu("Søg efter ingrediens"));

            GetRecipeMenu.AddMenuItem(new Menu("Søg efter tid"));

            GetRecipeMenu.AddMenuItem(new ActionMenuItem("Tilbage") {
                SelectAction = MainMenu.Select
            });


            // Import/export config 
            ImportExportMenu.AddMenuItem(new ActionMenuItem("Importer opskrifter"));
            ImportExportMenu.AddMenuItem(new ActionMenuItem("Eksporter opskrifter") {
                SelectAction = () => {
                    ExportRecipes();
                    ImportExportMenu.Select();
                }
            });
            ImportExportMenu.AddMenuItem(new ActionMenuItem("Tilbage")
            {
                SelectAction = MainMenu.Select
            }) ;

            
            // Settings config

            // Exit config
            Exit.SelectAction = () =>
            {
                SaveRecipes(RecipeList);
                Environment.Exit(0);

            };
        }

        private static void ExportRecipes()
        {
            SaveRecipes(RecipeList);
            string path = ConsoleHelper.PromptForInput("Giv stien til hvor du vil have dine opskrifter eksporteret");
            string date = DateTime.Now.ToString().Replace("/","-").Replace(":","-").Replace(" ","");
            path = @"/Users/" + path + "/RecExport" + date + ".zip";
            Console.WriteLine(path);
            ZipFile.CreateFromDirectory("Recipes",path);
        }

        private static string PrintAllRecipes()
        {
            string result = null;
            foreach (Recipe r in RecipeList)
            {
                result += r.PrintRecipe();
            }
            return result;
            
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
                        case 7:
                            {
                                if(!newRecipe.GetDifficulty())
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
