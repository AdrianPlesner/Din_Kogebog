using System;
using System.Collections.Generic;
using System.Linq;

namespace Din_Kogebog
{
    public class Menu : MenuItem
    {
        public Menu(string title) : base(title) { }

        public Menu(string title, string content) : base(title, content) { }

        protected List<MenuItem> Items = new List<MenuItem>();

        private int Selected = 0;

        public void AddMenuItem(MenuItem newItem)
        {
            Items.Add(newItem);

        }

        public override void Select()
        {
            bool cont = true;
            while (cont)
            {
                Draw();
                ConsoleKeyInfo key = Console.ReadKey();

                switch ((int)key.Key)
                {
                    case 40:
                        //Downkey
                        if (Selected == Items.Count() - 1)
                        {
                            Selected = 0;
                        }
                        else
                        {
                            Selected++;
                        }
                        break;
                    case 38:
                        //Upkey
                        if (Selected == 0)
                        {
                            Selected = Items.Count() - 1;
                        }
                        else
                        {
                            Selected--;
                        }
                        break;
                    case 13:
                        //Enter
                        cont = false;
                        Items.ElementAt(Selected).Select();

                        break;
                    case 27:
                        //esc
                        cont = false;
                        break;
                    default:
                        break;
                }
            }
        }

        private void Draw()
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Console.WriteLine("[" + Title.ToUpper() + "]");
            Console.WriteLine(Content);
            Console.WriteLine();
            int count = 0;

            foreach (MenuItem item in Items)
            {
                if (count == Selected)
                {
                    ReverseColors();
                    Console.WriteLine(item.Title);
                    ReverseColors();

                }
                else
                {
                    Console.WriteLine(item.Title);
                }
                count++;
            }

        }

        private void ReverseColors()
        {
            ConsoleColor background = Console.BackgroundColor;
            ConsoleColor foreground = Console.ForegroundColor;

            Console.BackgroundColor = foreground;
            Console.ForegroundColor = background;
        }
    }

    public class MenuItem
    {
        public MenuItem(string title)
        {
            Title = title;
        }

        public MenuItem(string title, string content)
        {
            Title = title;
            Content = content;
        }

        public string Content;

        public string Title { get; set; }

        public virtual void Select()
        {
            Console.Clear();
            Console.WriteLine("[" + Title + "]");
            Console.WriteLine(Content);
        }
    }

}
