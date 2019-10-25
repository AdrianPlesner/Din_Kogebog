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

            GetRecipeMenu.AddMenuItem(new Menu("Søg efter navn"));
            GetRecipeMenu.AddMenuItem(new Menu("Søg efter ingrediens"));
            GetRecipeMenu.AddMenuItem(new Menu("Søg efter tid"));
            GetRecipeMenu.AddMenuItem(new ActionMenuItem("Tilbage"));
        }

        static private void NewRecipe()
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
