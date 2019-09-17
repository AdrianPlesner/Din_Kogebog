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

                if (!newRecipe.GetIngredients())
                {
                    MainMenu.Select();
                }
                if (!newRecipe.GetSteps())
                {
                    MainMenu.Select();
                }
                if(!newRecipe.GetBaking())
                {
                    MainMenu.Select();
                }

                //TODO: add number of servings, categories and total time


                RecipeList.Add(newRecipe);

            }
        }
    }        
}
