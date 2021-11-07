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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Tower TowerOne;
        private Tower TowerTwo;
        private Tower TowerThree;
        private int CountMove;
        public MainWindow()
        {
            InitializeComponent();            
        }
        private void GetTower(Stack<Hoop> hoops, int i, int j, StackPanel st)
        {
            foreach (var el in hoops)
                st.Children.Add(el.Rec);
        }
        private void MoveDiskRec(ref Tower one, ref Tower two, ref Tower three, int N)
        {
            if (N > 1)
                MoveDiskRec(ref one, ref three, ref two, N - 1);

            var t = one.Hoops.Pop();
            three.Hoops.Push(t);
            CountMove++;

            if (N > 1)
                MoveDiskRec(ref two, ref one, ref three, N - 1);
        }
        private void MoveDisk(ref Tower one, ref Tower two, ref Tower three, int N)
        {
            bool cht = (N % 2 == 0);

            while(one.Hoops.Count > 0 || two.Hoops.Count > 0)
            {
                for(int i = 0; i < 3; i++)
                {                    
                    if(one.Hoops.Count == 1 && two.Hoops.Count == 0)
                        three.Hoops.Push(one.Hoops.Pop());
                    if (one.Hoops.Count == 0 && two.Hoops.Count == 1)
                        three.Hoops.Push(two.Hoops.Pop());
                    if (one.Hoops.Count == 0 && two.Hoops.Count == 0)
                        break;

                    if (cht)
                        Moving(ref one, ref two, ref three, i);
                    else
                        Moving(ref one, ref three, ref two, i);

                    CountMove++;
                }

            }

        }      
        private void Moving(ref Tower one, ref Tower two, ref Tower three, int i)
        {
            switch (i)
            {
                case 0:
                    if (two.Hoops.Count == 0 && one.Hoops.Count != 0)
                    {
                        two.Hoops.Push(one.Hoops.Pop());
                    }
                    else if (two.Hoops.Count != 0 && one.Hoops.Count == 0)
                    {
                        one.Hoops.Push(two.Hoops.Pop());
                    }
                    else
                    {
                        if (one.Hoops.Peek() < two.Hoops.Peek())
                            two.Hoops.Push(one.Hoops.Pop());
                        else
                            one.Hoops.Push(two.Hoops.Pop());

                    }
                    break;
                case 1:
                    if (one.Hoops.Count != 0 && three.Hoops.Count == 0)
                    {
                        three.Hoops.Push(one.Hoops.Pop());
                    }
                    else if (one.Hoops.Count == 0 && three.Hoops.Count != 0)
                    {
                        one.Hoops.Push(three.Hoops.Pop());
                    }
                    else
                    {
                        if (one.Hoops.Peek() < three.Hoops.Peek())
                            three.Hoops.Push(one.Hoops.Pop());
                        else
                            one.Hoops.Push(three.Hoops.Pop());
                    }
                    break;
                case 2:
                    if (two.Hoops.Count != 0 && three.Hoops.Count == 0)
                    {
                        three.Hoops.Push(two.Hoops.Pop());
                    }
                    else if (two.Hoops.Count == 0 && three.Hoops.Count != 0)
                    {
                        two.Hoops.Push(three.Hoops.Pop());
                    }
                    else
                    {
                        if (two.Hoops.Peek() < three.Hoops.Peek())
                            three.Hoops.Push(two.Hoops.Pop());
                        else
                            two.Hoops.Push(three.Hoops.Pop());
                    }
                    break;
            }
        }
            
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            st1.Children.Clear();
            st2.Children.Clear();
            st3.Children.Clear();

            CountMove = 0;

            TowerOne = new Tower(1, int.Parse(N.Text));
            TowerTwo = new Tower(2, int.Parse(N.Text));
            TowerThree = new Tower(3, int.Parse(N.Text));

            TowerOne.GetStartHoops();
            GetTower(TowerOne.Hoops, 0, 0, st1);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            st1.Children.Clear();
            st2.Children.Clear();
            st3.Children.Clear();

            MoveDisk(ref TowerOne, ref TowerTwo, ref TowerThree, int.Parse(N.Text));

            GetTower(TowerOne.Hoops, 0, 0, st1);
            GetTower(TowerTwo.Hoops, 0, 1, st2);
            GetTower(TowerThree.Hoops, 0, 2, st3);

            CNT.Text = CountMove.ToString();
        }
    }
}
