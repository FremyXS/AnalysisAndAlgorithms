using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hanoi_Towers
{
    class DrawTowers
    {
        public Grid Content { get; }
        public DrawTowers(Grid content)
        {
            Content = content;
        }
        public void GetTower(Stack<Hoop> hoops, int i, int j)
        {
            Content.Children.Add(GetStack(hoops, i, j));
        }
        private StackPanel GetStack(Stack<Hoop> hoops, int i, int j)
        {
            var st = new StackPanel
            {
                VerticalAlignment = VerticalAlignment.Bottom,                
            };

            foreach (var el in hoops)
                st.Children.Add(el.Rec);

            Grid.SetRow(st, i);
            Grid.SetColumn(st, j);

            return st;
        }
        public static void MoveDisk(ref Tower one, ref Tower two, ref Tower three, int N)
        {
            if (N > 1)
                MoveDisk(ref one, ref three, ref two, N - 1);

            three.Hoops.Push(one.Hoops.Pop());

            if (N > 1)
                MoveDisk(ref two, ref one, ref three, N - 1);
        }
    }
}
