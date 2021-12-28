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
using System.IO;
using System.Globalization;

namespace SortingTables
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
        public List<Table> Rows;
        private void tablesgr_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void SortClock(object sender, RoutedEventArgs e)
        {
            var sa = new SortingAlgorithm(content, logs);
            sa.MergeSort(Rows.ToArray());
            GetTable();
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton li = (sender as RadioButton);
            sortname = li.Content.ToString();
        }
        public static string sortname;

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            string[] tv = new string[0];
            columns.Children.Clear();
            SortColumns.Clear();
            logs.Children.Clear();
            switch (sortname)
            {
                case "Country":
                    tv = File.ReadAllLines(@"../../../CountriesT.txt");
                    break;
                case "Chemicals":
                    tv = File.ReadAllLines(@"../../../ChemicalsT.txt");
                    break;
                case "Words":
                    tv = File.ReadAllText(@"../../../text.txt").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    break;
                default:
                    break;
            }

            //columns.Children.Add(Table.GetStack(Table.GetTable(tv[0].Split(new char[] { ';' })), Brushes.White));
            var cl = tv[0].Split(new char[] { ';' });
            for (int i = 0; i < cl.Length; i++)
            {
                columns.Children.Add(GetBt(cl[i], i));
            }
                

            Rows = new List<Table>();

            for (var i = 1; i < tv.Length; i++)
            {
                var row = tv[i].Split(new char[] { ';' });
                Rows.Add(Table.GetTable(row));
            }

            SortColumns.OrderBy(el => el);
            GetTable();
        }
        private void GetTable()
        {
            content.Children.Clear();
            foreach(var el in Rows)
            {
                content.Children.Add(Table.GetStack(el, Brushes.White));
            }
        }
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
        private Button GetBt(string name, int i)
        {
            var bt = new Button();
            bt.Content = name;
            bt.Click += Bt_Click;
            switch (i)
            {
                case 0:
                    bt.Click += Bt_Click1;
                    break;
                case 1:
                    bt.Click += Bt_Click2;
                    break;
                case 2:
                    bt.Click += Bt_Click3;
                    break;
                case 3:
                    bt.Click += Bt_Click4;
                    break;
                case 4:
                    bt.Click += Bt_Click5;
                    break;
                default:
                    break;
            }

            return bt;
        }
        public static List<int> SortColumns = new List<int>();
        private void Bt_Click5(object sender, RoutedEventArgs e)
        {
            if (SortColumns.Contains(4))
                SortColumns.Remove(4);
            else
                SortColumns.Add(4);
        }

        private void Bt_Click4(object sender, RoutedEventArgs e)
        {
            if (SortColumns.Contains(3))
                SortColumns.Remove(3);
            else
                SortColumns.Add(3);
        }

        private void Bt_Click3(object sender, RoutedEventArgs e)
        {
            if (SortColumns.Contains(2))
                SortColumns.Remove(2);
            else
                SortColumns.Add(2);
        }

        private void Bt_Click2(object sender, RoutedEventArgs e)
        {
            if (SortColumns.Contains(1))
                SortColumns.Remove(1);
            else
                SortColumns.Add(1);
        }

        private void Bt_Click1(object sender, RoutedEventArgs e)
        {
            if (SortColumns.Contains(0))
                SortColumns.Remove(0);
            else
                SortColumns.Add(0);
        }

        private void Bt_Click(object sender, RoutedEventArgs e)
        {
            if(((Button)sender).Background == Brushes.Red)
            {
                ((Button)sender).Background = default(Brush);
            }
            else
            {
                ((Button)sender).Background = Brushes.Red;
            }
        }
    }
    
}
