using System;
namespace Din_Kogebog
{
    public class ActionMenuItem : MenuItem
    {
        public ActionMenuItem(string title, Action action) :
            this(title)
        {
            SelectAction = action;
        }

        public ActionMenuItem(string title) : base(title)
        {
        }

        public Action SelectAction { get; set; }


        public override void Select()
        {
            Console.Clear();
            SelectAction?.Invoke();
        }
    }
}
