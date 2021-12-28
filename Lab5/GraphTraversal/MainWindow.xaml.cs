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
using Microsoft.Win32;

namespace GraphTraversal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var path = File.ReadAllLines(@"../../../matrix.txt");
            Host.SetMatrix(path.ToList());
            DG = new DrawGraph(Content, Logs);
            DG.StartPosition();
            DG.Drawing();

        }
        public static DrawGraph DG { get; private set; }
        private void DFS_Click(object sender, RoutedEventArgs e)
        {
            var t = ((Button)sender).Name;
            var alg = new Algorithm();

            if (t == "DFS")
            {
                alg.DFS(startDfs.Text);
            }
            else if (t == "BFS")
            {
                alg.BFS(startBfs.Text);
            }
            else if(t == "MaxFlow")
            {
                alg.MaximumFlow(startMax.Text, endMax.Text);
            }
            else if(t == "ShortWayBt")
            {
                alg.SearchShortWay(startSW.Text);
            }
            else
            {
                alg.Clear();
            }
            
        }

        private void AddHostClick(object sender, RoutedEventArgs e)
        {
            Host.AddHost();
        }

        private void AddConnectClick(object sender, RoutedEventArgs e)
        {
            Host.AddConnect(start.Text, end.Text, flow.Text, isOriented.IsChecked);
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            //List<string> save = new List<string>();

            //for(var i = 0; i < Host.Hostes.Count; i++)
            //{
            //    string str = "";

            //    for (var j = 0; j < Host.Hostes.Count; j++)
            //    {
            //        if (Host.Hostes[i].Connections.Any(el => el.EndHost.Name == "x" + j.ToString()))
            //        {
            //            str += j.ToString() + " ";
            //        }
            //        else
            //            str += "0" + " ";
            //    }
            //    str += "\n";

            //    save.Add(str);
            //}

            //File.WriteAllLines(@"../../../saves/save1.txt", save.ToArray());

            var sfd = new SaveFileDialog();
            sfd.ShowDialog();
            Settings.SaveGraph(sfd.FileName);
            MessageBox.Show("Файл сохранён");
        }

        private void LoadClick(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.ShowDialog();

            if (ofd.FileName != "")
            {
                var path = File.ReadAllLines(ofd.FileName);
                //Host.SetMatrix(path.ToList()); 
                //DG = new DrawGraph(Content, Logs);
                //DG.StartPosition();
                
                Settings.LoadGraph(ofd.FileName);
                DG.Drawing();

            }
            else
                MessageBox.Show("Файл не выбран");
        }
    }
}
