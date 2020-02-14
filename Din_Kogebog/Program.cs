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
            GetRecipeMenu.AddMenuItem(new Menu("Søg efter navn")); //TODO: Implemet

            GetRecipeMenu.AddMenuItem(new Menu("Søg efter ingrediens")); //TODO: Implemet, search multiple ingredients

            GetRecipeMenu.AddMenuItem(new Menu("Søg efter kategori")); //TODO: Implemet

            GetRecipeMenu.AddMenuItem(new Menu("Søg efter sværhedsgrad")); //TODO: Implement

            GetRecipeMenu.AddMenuItem(new Menu("Søg efter tid")); //TODO: Implemet

            GetRecipeMenu.AddMenuItem(new ActionMenuItem("Tilbage") {
                SelectAction = MainMenu.Select
            });


            // Import/export config 
            ImportExportMenu.AddMenuItem(new ActionMenuItem("Importer opskrifter") {
                SelectAction = () =>
                {
                    ImportRecipes();
                    ImportExportMenu.Select();
                }
            });
            ImportExportMenu.AddMenuItem(new ActionMenuItem("Eksporter opskrifter") { //TODO: Add select which to export
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
            //TODO: Add setings menus

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
            Console.ReadKey();
        }

        private static void ImportRecipes()
        {
            string path = ConsoleHelper.PromptForInput("Giv stien til hvor du vil importere opskrifter fra");
            bool cont = true;
            do
            {
                if (Path.GetExtension(path) == ".zip")
                {
                    if (File.Exists(path))
                    {
                        ZipArchive zip = new ZipArchive(new FileStream(path,FileMode.Open) );
                        
                        cont = false;
                    }
                    else
                    {
                        path = ConsoleHelper.PromptForInput("Stien existerer ikke. Prøv igen");
                    }

                }
                else
                {
                    path = ConsoleHelper.PromptForInput("Opskrifter skal importeres fra en .zip fil");

                }
            } while (cont);
            Console.ReadKey();

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
