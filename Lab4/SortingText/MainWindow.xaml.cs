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

namespace SortingText
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string[] array;
        public SortingAlgorithm Sorting;
        public MainWindow()
        {
            InitializeComponent();
            Sorting = new SortingAlgorithm(content);
            array = File.ReadAllText(@"../../../text.txt").Split(new char[] { ',', '.', '!', '?', ' ', '(', ')', '\r', '\n', '\"', '\'' }, StringSplitOptions.RemoveEmptyEntries);

            foreach(var el in array)
            {
                content.Children.Add(new TextBlock() { Text = el });
            }

        }
        private void SortClick(object sender, RoutedEventArgs e)
        {

            var sa = new SortingAlgorithm(content);
            sa.AbsSort(array);
        }

        private void CountWordsClick(object sender, RoutedEventArgs e)
        {
            UniqueWord.CountUniqueWords(array);

            countWords.Text = "UniqueWords = " + UniqueWord.UniqueWords.Count.ToString();

            foreach(var el in UniqueWord.UniqueWords)
            {
                uniqueWords.Children.Add(new TextBlock { Text = $"{el.Word} = {el.Count}" });
            }
        }
    }
}
