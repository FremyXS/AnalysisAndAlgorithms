using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace InternalSorting
{
    public class SortingAlgorithm
    {
        public StackPanel Content;
        public StackPanel Logs;
        public DrawMove dw; 
        public SortingAlgorithm(StackPanel content, StackPanel logs)
        {
            Content = content;
            Logs = logs;
            dw = new DrawMove(content);
        }
        private void AddLog(string text, Brush brush)
        {
            Logs.Children.Add(new TextBlock { Text = text, Foreground = brush });
        }
        async Task GrowMove(Element[] array, int i, int j, Brush one)
        {
            array[i].Rec.Stroke = one;
            array[j].Rec.Stroke = one;
            await dw.Alg(array);

            array[i].Rec.Stroke = Brushes.Black;
            array[j].Rec.Stroke = Brushes.Black;
        }
        async Task Swap(Element[]array, int i, int j)
        {
            await GrowMove(array, i, j, Brushes.Blue);

            var temp = array[i];
            array[i] = array[j];
            array[j] = temp;

            AddLog($"array[{i}] <-> array[{j}]", Brushes.Blue);

            await GrowMove(array, i, j, Brushes.Blue);
        }
        public async Task CombSort(Element[] array)
        {
            double gap = array.Length;
            bool swaps = true;

            while (gap > 1 || swaps)
            {
                gap /= 1.247330950103979;

                if (gap < 1)
                    gap = 1;

                int i = 0;
                swaps = false;

                while (i + gap < array.Length)
                {
                    int igap = i + (int)gap;

                    AddLog($"array[{i}] > array[{igap}]({array[i].Num} > {array[igap].Num})?", Brushes.Red);
                    await GrowMove(array, i, igap, Brushes.Red);

                    if (array[i].Num > array[igap].Num)
                    {
                        AddLog("Yes", Brushes.Black);
                        await Swap(array, i, igap);
                        swaps = true;
                    }

                    ++i;
                }

            }
        }

        void Swap1(Element[] array, int i, int j)
        {
            var temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        //Гномья сортировка
        public async Task GnomeSort(Element[] array)
        {
            var index = 1;
            var nextIndex = index + 1;

            while (index < array.Length)
            {
                AddLog($"array[{index - 1}] > array[{index}]({array[index-1].Num} > {array[index].Num})?", Brushes.Red);
                await GrowMove(array, index - 1, index, Brushes.Red);

                if (array[index - 1].Num < array[index].Num)
                {
                    index = nextIndex;
                    nextIndex++;
                }
                else
                {
                    await Swap(array, index - 1, index);
                    index--;
                    if (index == 0)
                    {
                        index = nextIndex;
                        nextIndex++;
                    }
                }
            }

            await dw.Alg(array);
        }
        

    }
    public static class Funcions
    {        
        public static int IndexOf(this Element[] array, Element obj)
        {
            int Count = 0;
            foreach(var el in array)
            {
                if (Count >= 100 || Count < 0) 
                    Console.WriteLine(2);

                if (el.Equals(obj))
                    return Count;



                Count++;
            }

            return -1;
        }
    }
}
