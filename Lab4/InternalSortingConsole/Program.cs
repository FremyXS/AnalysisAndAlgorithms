using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;

namespace InternalSortingConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            File.WriteAllLines(@"../../../gnomesortdiagnostic.csv", CombSortTimeDiagnostic());
        }
        private static string[] CombSortTimeDiagnostic()
        {
            var times = new List<string>();

            for(int i = 1; i <= 2000; i++)
            {
                times.Add(DiagnosticCombSort(GetRandomArray(i)));
            }

            return times.ToArray();
        }
        private static int[] GetRandomArray(int t)
        {
            int[] array = new int[t];

            for(int i = 0; i < t; i++)
            {
                array[i] = new Random().Next(1, 2000);
            }

            return array;
        }
        private static string DiagnosticCombSort(int[] array)
        {
            Stopwatch time = new();
            time.Restart();

            for(int i = 0; i< 5; i++)
            {
                GnomeSort((int[])array.Clone());
            }

            return time.Elapsed.Ticks.ToString();
        }
        private static void CombSort(int[] array)
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

                    
                    if (array[i] > array[igap])
                    {
                        Swap(array, i, igap);
                        swaps = true;
                    }

                    ++i;
                }

            }
        }
        private static void Swap(int[] array, int i, int j)
        {
            var temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
        
        //Гномья сортировка
        public static void GnomeSort(int[] array)
        {
            var index = 1;
            var nextIndex = index + 1;

            while (index < array.Length)
            {

                if (array[index - 1] < array[index])
                {
                    index = nextIndex;
                    nextIndex++;
                }
                else
                {
                    Swap(array, index - 1, index);
                    index--;
                    if (index == 0)
                    {
                        index = nextIndex;
                        nextIndex++;
                    }
                }
            }



        }
    }
}
