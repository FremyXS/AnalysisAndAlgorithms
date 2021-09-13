using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            TimeAnalysis<int>.ProgramExecution();
        }
    }
    public class TimeAnalysis<T>
    {
        public int CountN { get; }
        public T Result { get; }
        public TimeSpan AverageTime { get; }
        public TimeAnalysis(int countN, T result, TimeSpan averageTime)
        {
            CountN = countN;
            Result = result;
            AverageTime = averageTime;
        }
        public static List<TimeAnalysis<BigInteger>> ResultesSumElement { get; private set; } = new List<TimeAnalysis<BigInteger>>();
        public static List<TimeAnalysis<BigInteger>> ResultesMultElement { get; private set; } = new List<TimeAnalysis<BigInteger>>();
        public static List<TimeAnalysis<BigInteger>> ResultesMethodGornera { get; private set; } = new List<TimeAnalysis<BigInteger>>();
        public static List<TimeAnalysis<int[]>> ResultesBubbleSort { get; private set; } = new List<TimeAnalysis<int[]>>();
        public static List<TimeAnalysis<int[]>> ResultesQuickSort { get; private set; } = new List<TimeAnalysis<int[]>>();
        public static List<TimeAnalysis<int[]>> ResultesTimSort { get; private set; } = new List<TimeAnalysis<int[]>>();
        public static void ProgramExecution()
        {
            int[] array = GetNums();

            for (int i = 0; i < 2000; i++)
                SumElements(GetLimitedArray(array, i));

            for (int i = 0; i < 2000; i++)
                MultElements(GetLimitedArray(array, i));

            for (int i = 0; i < 2000; i++)
                MethodGornera(GetLimitedArray(array, i));

            for (int i = 0; i < 2000; i++)
                BubbleSort(GetCopysArray(GetLimitedArray(array, i)));

            for (int i = 0; i < 2000; i++)
                QuickSort(GetCopysArray(GetLimitedArray(array, i)));

            for (int i = 0; i < 2000; i++)
                TimSort(GetCopysArray(GetLimitedArray(array, i)));
        }
        private static int[] GetNums()
        {
            var array = new int[2000];

            for(int i = 0; i < 2000; i++)
            {
                bool t = true;

                while (t)
                {
                    int num = new Random().Next();

                    if (!array.Contains(num))
                    {
                        array[i] = num;
                        t = false;
                    }
                }
            }

            return array;
        }
        private static int[] GetLimitedArray(int[] array, int countN)
        {
            int[] arrayNew = new int[countN + 1];

            for(int i = 0; i <= countN; i++)
            {
                arrayNew[i] = array[i];
            }

            return arrayNew;
        }
        private static List<int[]> GetCopysArray(int[] array)
            => new List<int[]>()
            {
                (int[])array.Clone(),
                (int[])array.Clone(),
                (int[])array.Clone(),
                (int[])array.Clone(),
                (int[])array.Clone()
            };

        private static void SumElements(int[] array)
        {

            TimeSpan sumTime;
            var startT = DateTime.Now;

            for (int i = 0; i < 5; i++)
                SumElementsFunction(array);

            sumTime = DateTime.Now - startT;

            ResultesSumElement.Add(new TimeAnalysis<BigInteger>(array.Length, SumElementsFunction(array), sumTime / 5));
        }
        private static BigInteger SumElementsFunction(int[] array)
        {
            BigInteger rez = 0;

            foreach (var num in array)
                rez += num;

            return rez;
        }
        private static void MultElements(int[] array)
        {
            TimeSpan sumTime;
            var startT = DateTime.Now;

            for (int i = 0; i < 5; i++)
                MultElementsFunction(array);

            sumTime = DateTime.Now - startT;

            ResultesMultElement.Add(new TimeAnalysis<BigInteger>(array.Length, MultElementsFunction(array), sumTime / 5));
        }
        private static BigInteger MultElementsFunction(int[] array)
        {
            BigInteger rez = 1;

            foreach (var num in array)
                rez *= num;

            return rez;
        }
        private static void MethodGornera(int[] array)
        {
            TimeSpan sumTime;
            var startT = DateTime.Now;

            for (var i = 0; i < 5; i++)
                MethodGorneraFunction(array);

            sumTime = DateTime.Now - startT;

            ResultesMethodGornera.Add(new TimeAnalysis<BigInteger>(array.Length, MethodGorneraFunction(array), sumTime / 5));
        }
        private static BigInteger MethodGorneraFunction(int[] array)
        {
            BigInteger rez = 0;
            const double x = 1.5;

            for (int i = array.Length - 1; i >= 0; i--)
            {
                if (i is 0)
                    rez += array[i];
                else
                    rez += (BigInteger)(array[i] * x);
            }

            return rez;
        }
        private static void BubbleSort(List<int[]> arrays)
        {
            TimeSpan sumTime;
            var startT = DateTime.Now;

            for (int i = 0; i < 5; i++)
                BubbleSortFunction(arrays[i]);

            sumTime = DateTime.Now - startT;

            ResultesBubbleSort.Add(new TimeAnalysis<int[]>(arrays[0].Length, arrays[0], sumTime / 5));
        }
        private static void BubbleSortFunction(int[] array)
        {
            var i = 0;
            var t = true;

            while (t)
            {
                t = false;

                for (var j = 0; j <= array.Length - i - 2; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        var num = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = num;
                        t = true;
                    }
                }

                i++;
            }
        }
        private static void QuickSort(List<int[]> arrays)
        {
            TimeSpan sumTime;
            var startT = DateTime.Now;

            for (int i = 0; i < 5; i++)
                QuickSortFunction(arrays[i], 0, arrays[i].Length - 1);

            sumTime = DateTime.Now - startT;

            ResultesQuickSort.Add(new TimeAnalysis<int[]>(arrays[0].Length, arrays[0], sumTime / 5));
        }
        private static void QuickSortFunction(int[] array, int minIndex, int maxIndex)
        {
            if (minIndex >= maxIndex) return;

            var pivotIndex = Partition(array, minIndex, maxIndex);
            QuickSortFunction(array, minIndex, pivotIndex - 1);
            QuickSortFunction(array, pivotIndex + 1, maxIndex);
        }
        private static void Swap(ref int x, ref int y)
        {
            var t = x;
            x = y;
            y = t;
        }
        private static int Partition(int[] array, int minIndex, int maxIndex)
        {
            var pivot = minIndex - 1;

            for (var i = minIndex; i < maxIndex; i++)
            {
                if (array[i] < array[maxIndex])
                {
                    pivot++;
                    Swap(ref array[pivot], ref array[i]);
                }
            }

            pivot++;
            Swap(ref array[pivot], ref array[maxIndex]);
            return pivot;
        }
        private static void TimSort(List<int[]> arrays)
        {
            TimeSpan sumTime;
            var startT = DateTime.Now;

            for (int i = 0; i < 5; i++)
                TimSortFunction(arrays[i], arrays[i].Length - 1);

            sumTime = DateTime.Now - startT;

            ResultesTimSort.Add(new TimeAnalysis<int[]>(arrays[0].Length, arrays[0], sumTime / 5));
        }
        private static void TimSortFunction(int[] array, int n)
        {
            const int run = 32;

            for (int i = 0; i < n; i += run)
                InsertionSort(array, i, Math.Min((i + run - 1), (n - 1)));

            for (int size = run; size < n; size = 2 * size)
            {
                for (int left = 0; left < n; left += 2 * size)
                {
                    int mid = left + size - 1;
                    int right = Math.Min((left + 2 * size - 1), (n - 1));

                    if (mid < right)
                        Merge(array, left, mid, right);
                }
            }
        }
        private static void Merge(int[] array, int l, int m, int r)
        {
            int len1 = m - l + 1, len2 = r - m;
            int[] left = new int[len1], right = new int[len2];

            for (int x = 0; x < len1; x++)
                left[x] = array[l + x];

            for (int x = 0; x < len2; x++)
                right[x] = array[m + 1 + x];

            int i = 0, j = 0, k = 1;

            while (i < len1 && j < len2)
            {
                if (left[i] <= right[j])
                {
                    array[k] = left[i];
                    i++;
                }
                else
                {
                    array[k] = right[j];
                    j++;
                }
                k++;
            }

            while (i < len1)
            {
                array[k] = left[i];
                k++; i++;
            }

            while (j < len2)
            {
                array[k] = right[j];
                k++; j++;
            }
        }
        private static void InsertionSort(int[] array, int left, int right)
        {
            for (int i = left + 1; i <= right; i++)
            {
                int temp = array[i];
                int j = i - 1;

                while (j >= left && array[j] > temp)
                {
                    array[j + 1] = array[j];
                    j--;
                }

                array[j + 1] = temp;
            }
        }
    }
}
