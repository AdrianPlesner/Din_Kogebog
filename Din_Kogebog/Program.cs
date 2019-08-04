using System;

namespace Din_Kogebog
{
    class Program
    {
        static void Main(string[] args)
        {
            SetupMenu();
            MainMenu.Select();
        }

        static Menu MainMenu = new Menu("Din Kogebog", "Hvordan vil du fortsætte?");

        static Menu GetRecipeMenu = new Menu("Find opskrift");

        static Menu ImportExportMenu = new Menu("Importer / eksporter opskrifter");

        static Menu SettingsMenu = new Menu("Indstillinger");

        static MenuItem Exit = new MenuItem("Luk", "Gemmer og lukker");

        static void SetupMenu()
        {
            MainMenu.AddMenuItem(GetRecipeMenu);
            MainMenu.AddMenuItem(ImportExportMenu);
            MainMenu.AddMenuItem(SettingsMenu);
            MainMenu.AddMenuItem(Exit);
        }
    }
}
