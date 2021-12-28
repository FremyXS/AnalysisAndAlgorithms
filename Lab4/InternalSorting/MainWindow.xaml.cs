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

namespace InternalSorting
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DrawMove.ping = ping;
        }
        Element[] ArrayN;    
        private void GetNumsBtClick(object sender, RoutedEventArgs e)
        {
            contentst.Children.Clear();
            ArrayN = GetNums(int.Parse(countNums.Text));
            
        }
        private Element[] GetNums(int N)
        {
            var array = new Element[N];

            for (int i = 0; i < N; i++)
            {
                bool t = true;

                while (t)
                {
                    int num = new Random().Next(0, 100);

                    array[i] = new Element(num);
                    t = false;
                    contentst.Children.Add(array[i].Rec);
                }
            }

            return array;
        }

        private void StartSortBtClick(object sender, RoutedEventArgs e)
        {
            var sa = new SortingAlgorithm(contentst, logs);
            if (sortname.ToUpper() == "gnomesort".ToUpper())
                sa.GnomeSort(ArrayN);
            else
                sa.CombSort(ArrayN);
            

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton li = (sender as RadioButton);
            sortname = li.Content.ToString();
        }
        private string sortname;

        private void Minus100Click(object sender, RoutedEventArgs e)
        {
            ping.Text = (int.Parse(ping.Text) - 100).ToString();
        }

        private void Minus10Click(object sender, RoutedEventArgs e)
        {
            ping.Text = (int.Parse(ping.Text) - 10).ToString();
        }

        private void Plus10Click(object sender, RoutedEventArgs e)
        {
            ping.Text = (int.Parse(ping.Text) + 10).ToString();
        }

        private void Plus100Click(object sender, RoutedEventArgs e)
        {
            ping.Text = (int.Parse(ping.Text) + 100).ToString();
        }
    }
}
