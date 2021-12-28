using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SortingText
{
    public class DrawMove
    {
        public StackPanel Content;
        public static TextBox Ping
        {
            get
            {
                return ping;
            }
            set
            {
                if (value.Text == "" || int.Parse(value.Text) < 0)
                    ping.Text = 0.ToString();
                else
                    ping.Text = value.Text;
            }
        }
        public static TextBox ping;
        public DrawMove(StackPanel content)
        {
            Content = content;
        }
        public async Task Alg(string[] Elements)
        {
            Content.Children.Clear();

            foreach (var el in Elements)
                Content.Children.Add(new TextBlock() { Text = el });



            await Task.Delay(100);
        }
    }
}
