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
        public MainWindow()
        {
            InitializeComponent();            

            //MoveDisk(towerOne, towerTwo, towerThree, N);

        }
        private void GetTower(Stack<Hoop> hoops, int i, int j)
        {
            var st = new StackPanel
            {
                VerticalAlignment = VerticalAlignment.Bottom
            };

            foreach (var el in hoops)
                st.Children.Add(el.Rec);

            gridTw.Children.Add(st);
            Grid.SetRow(st, i);
            Grid.SetColumn(st, j);
        }
        void MoveDisk(ref Tower one, ref Tower two, ref Tower three, int N)
        {
            if (N > 1)
                MoveDisk(ref one, ref three, ref two, N - 1);

            var t = one.Hoops.Pop();
            three.Hoops.Push(t);
            

            if (N > 1)
                MoveDisk(ref two, ref one, ref three, N - 1);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            gridTw.Children.Clear();
            TowerOne = new Tower(1, int.Parse(N.Text));
            TowerTwo = new Tower(2, int.Parse(N.Text));
            TowerThree = new Tower(3, int.Parse(N.Text));
            TowerOne.GetStartHoops();
            GetTower(TowerOne.Hoops, 0, 0);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            gridTw.Children.Clear();

            MoveDisk(ref TowerOne, ref TowerTwo, ref TowerThree, int.Parse(N.Text));

            GetTower(TowerThree.Hoops, 0, 2);
        }
    }
}
