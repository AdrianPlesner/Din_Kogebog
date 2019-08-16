using System;
namespace Din_Kogebog
{
    public static class ConsoleHelper
    {
        public static string PromptForInput(string question)
        {
            bool cont = true;
            string input = null;
            bool confirm = true;
            while (cont)
            {
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
                    case 37:
                    case 39:
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
                                input = default;
                            }
                            break;
                        }
                    case 27:
                        {
                            //esc
                            return default;
                        }
                    default:
                        break;
                }
            }

            return default;
        }

        public static bool PromptYesNo(string question)
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
                    case 37:
                    case 39:
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

        private static void ReverseColors()
        {
            ConsoleColor background = Console.BackgroundColor;
            ConsoleColor foreground = Console.ForegroundColor;

            Console.BackgroundColor = foreground;
            Console.ForegroundColor = background;
        }

    }
}
