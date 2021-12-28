using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace SortingTablesConsole
{
    class Program
    {
        private const string PATH_LEFT = @"../../../left.txt";
        private const string PATH_RIGHT = @"../../../right.txt";
        static void Main(string[] args)
        {
            var array = GetRandomArray(2027);
            //FirstSort(array);
            //SplittingIntoFiles(array, 1);
            DiagnosticCode();

        }
        private static void DiagnosticCode()
        {
            int[] array = new int[2000];
            var t = new List<string>();

            for (int i = 0; i < 500; i++)
            {
                Console.WriteLine(i);
                t.Add(MergeSortingTime(GetLimit(array, i)));
            }

            File.WriteAllLines(@"../../../res2.csv", t.ToArray());
        }
        private static string MergeSortingTime(int[] array)
        {
            Stopwatch time = new();
            time.Restart();

            for(int i = 0; i < 5; i++)
            {
                SplittingIntoFiles(array);
            }
            time.Stop();
            return (time.Elapsed.Ticks/5).ToString();
        }
        private static void SplittingIntoFiles(int[] array, int jump = 1)
        {
            File.WriteAllText(PATH_LEFT, "");
            File.WriteAllText(PATH_RIGHT, "");

            for (int i = 0; i < array.Length;)
            {                
                OneComb(ref i, jump, array, PATH_LEFT);

                if (i >= array.Length)
                    break;

                OneComb(ref i, jump, array, PATH_RIGHT);

                Sort1(array, i - jump*2);
            }

            if (!IsSort(array))
                SplittingIntoFiles(array, jump*2);
        }
        private static void OneComb(ref int i, int jump, int[]array, string path)
        {
            for (int j = 0; j < jump; j++)
            {
                if (i + j >= array.Length) break;
                WriteIntoFile(path, array[i + j].ToString());
            }

            i += jump;
        }
        private static void Sort1(int[] array, int k) 
        {
            var left = File.ReadAllLines(PATH_LEFT);
            var right = File.ReadAllLines(PATH_RIGHT);
            int l = 0, r = 0;


            while(l < left.Length && r < right.Length)
            {
                if(int.Parse(left[l]) < int.Parse(right[r]))
                {
                    array[k] = int.Parse(left[l]);
                    l++;

                }
                else
                {
                    array[k] = int.Parse(right[r]);
                    r++;
                }

                k++;
            }

            while(l != left.Length || r != right.Length)
            {
                if (l == left.Length)
                {
                    array[k] = int.Parse(right[r]);
                    r++;
                }
                else
                {
                    array[k] = int.Parse(left[l]);
                    l++;
                }

                k++;

            }

            File.WriteAllText(PATH_LEFT, "");
            File.WriteAllText(PATH_RIGHT, "");
        }
        
        private static bool IsSort(int[] array)
        {
            for(int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] > array[i])
                    return false;
            }

            return true;
        }
        private static void WriteIntoFile(string path, string word)
        {
            System.IO.StreamWriter writer = new System.IO.StreamWriter(path, true);
            writer.WriteLine(word);
            writer.Close();
        }
        private static int[] GetRandomArray(int i)
        {
            int[] array = new int[i];
            bool sort = true;
            for(int j = 0; j < i; j++)
            {
                while (sort)
                {
                    int num = new Random().Next(1, 10000);

                    if (!array.Contains(num))
                    {
                        array[j] = num;
                        sort = false;
                    }
                }

                sort = true;
            }

            return array;
        }
        private static int[] GetLimit(int[] array, int i)
        {
            var ar = new int[i + 1];

            for (int j = 0; j <= i; j++)
                ar[j] = array[j];

            return ar;
        }
    }
    
}
