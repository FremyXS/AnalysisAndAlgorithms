using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
           // TimeAnalysis<int>.ProgramExecutionOne();
            TimeAnalysis<int>.ProgramExecutionTwo();

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
        public static List<TimeAnalysis<BigInteger>> ResultesInvolutionOne { get; private set; } = new List<TimeAnalysis<BigInteger>>();
        public static List<TimeAnalysis<BigInteger>> ResultesInvolutionTwo { get; private set; } = new List<TimeAnalysis<BigInteger>>();
        public static List<TimeAnalysis<BigInteger>> ResultesInvolutionThree { get; private set; } = new List<TimeAnalysis<BigInteger>>();
        public static List<TimeAnalysis<BigInteger>> ResultesInvolutionFour { get; private set; } = new List<TimeAnalysis<BigInteger>>();
        public static List<TimeAnalysis<int[]>> ResultesBubbleSort { get; private set; } = new List<TimeAnalysis<int[]>>();
        public static List<TimeAnalysis<int[]>> ResultesQuickSort { get; private set; } = new List<TimeAnalysis<int[]>>();
        public static List<TimeAnalysis<int[]>> ResultesTimSort { get; private set; } = new List<TimeAnalysis<int[]>>();
        public static List<TimeAnalysis<Matrix>> ResultesMatrixMult { get; private set; } = new List<TimeAnalysis<Matrix>>();
        public static void ProgramExecutionOne()
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

            for (int i = 0; i < 2000; i++)
                Involution(GetLimitedArray(array, i));

            WriteDateInteger(ResultesSumElement, "sum_elements");
            WriteDateInteger(ResultesMultElement, "mult_elements");
            WriteDateInteger(ResultesMethodGornera, "methodgornera_elements");
            WriteDateInteger(ResultesInvolutionOne, "involution_elements_one");
            WriteDateInteger(ResultesInvolutionTwo, "involution_elements_two");
            WriteDateInteger(ResultesInvolutionThree, "involution_elements_three");
            WriteDateInteger(ResultesInvolutionFour, "involution_elements_four");
            WriteDateArray(ResultesBubbleSort, "bubble_sort");
            WriteDateArray(ResultesQuickSort, "quick_sort");
            WriteDateArray(ResultesTimSort, "tim_sort");
        }
        private static int[] GetNums()
        {
            var array = new int[2000];

            for(int i = 0; i < 2000; i++)
            {
                bool t = true;

                while (t)
                {
                    int num = new Random().Next(0, 3000);

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
        private static void Involution(int[] array)
        {
            const int x = 2;
            TimeSpan sumTime;

            var startT = DateTime.Now;
            for (var i = 0; i < 5; i++)
                InvolutionFunctionOne(array.Last(), x);
            sumTime = DateTime.Now - startT;
            ResultesInvolutionOne.Add(new TimeAnalysis<BigInteger>(array.Length, InvolutionFunctionOne(array.Last(), x), sumTime / 5));

            startT = DateTime.Now;
            for (var i = 0; i < 5; i++)
                InvolutionFunctionTwo(array.Last(), x);
            sumTime = DateTime.Now - startT;
            ResultesInvolutionTwo.Add(new TimeAnalysis<BigInteger>(array.Length, InvolutionFunctionTwo(array.Last(), x), sumTime / 5));

            startT = DateTime.Now;
            for (var i = 0; i < 5; i++)
                InvolutionFunctionThree(array.Last(), x);
            sumTime = DateTime.Now - startT;
            ResultesInvolutionThree.Add(new TimeAnalysis<BigInteger>(array.Length, InvolutionFunctionThree(array.Last(), x), sumTime / 5));

            startT = DateTime.Now;
            for (var i = 0; i < 5; i++)
                InvolutionFunctionFour(array.Last(), x);
            sumTime = DateTime.Now - startT;
            ResultesInvolutionFour.Add(new TimeAnalysis<BigInteger>(array.Length, InvolutionFunctionFour(array.Last(), x), sumTime / 5));
        }
        private static BigInteger InvolutionFunctionOne(int n, int x)
        {
            BigInteger res = 1;
            int k = 0;

            while(k < n)
            {
                res *= x;
                k = k + 1;
            }

            return res;
        }
        private static BigInteger InvolutionFunctionTwo(int n, int x)
        {
            BigInteger res = 1;
            if (n == 0) return 1;
            else
            {
                res = InvolutionFunctionTwo(n / 2, x);

                if (n % 2 == 1)
                    res = res * res * x;
                else
                    res = res * res;

                return res;
            }
        }
        private static BigInteger InvolutionFunctionThree(int n, int x)
        {
            BigInteger res;
            BigInteger c = x; int k = n;

            if(k % 2 == 1) res = c;
            else res = 1;

            while (k != 0)
            {
                k = k / 2;
                c = c * c;

                if (k % 2 == 1) res = res * c;
            }

            return res;
        }
        private static BigInteger InvolutionFunctionFour(int n, int x)
        {
            BigInteger res = 1;
            BigInteger c = x; int k = n;

            while(k != 0)
            {
                if(k % 2 == 0)
                {
                    c = c * c;
                    k = k / 2;
                }
                else
                {
                    res = res * c;
                    k = k - 1;
                }
            }

            return res;
        }
        private static void WriteDateInteger(List<TimeAnalysis<BigInteger>> res, string name)
            =>File.WriteAllLines(@$"../../../{name}.csv",
                res.OrderBy(e => e.AverageTime).Select(e => (e.AverageTime.TotalMilliseconds * 10000).ToString().Replace(',', '.') + ';' + e.CountN.ToString()).ToArray());
        private static void WriteDateArray(List<TimeAnalysis<int[]>> res, string name)
            =>File.WriteAllLines(@$"../../../{name}.csv",
                res.OrderBy(e => e.AverageTime).Select(e => (e.AverageTime.TotalMilliseconds * 10000).ToString().Replace(',', '.') + ';' + e.CountN.ToString()).ToArray());
        public static void ProgramExecutionTwo()
        {
            Matrix matrixA = new Matrix(2000), matrixB = new Matrix(2000);

            for (int i = 0; i < 2000; i++)
            {
                GetMatrix(matrixA, i);
                GetMatrix(matrixB, i);
            }

            for (int i = 0; i < 2000; i++)
                MatrixMult(matrixA, matrixB, i);

            WriteDateMatrix(ResultesMatrixMult, "matrix_mult");
        }
        private static void GetMatrix(Matrix matrix, int N)
        {
            int[] array = new int[2000];
            for(int i = 0; i < 2000; i++)
            {
                array[i] = new Random().Next(100);
            }

            Matrix.AddStr(array, N, matrix);
        }
        private static void MatrixMult(Matrix matrixA, Matrix matrixB, int N)
        {
            Console.WriteLine(N);

            TimeSpan sumTime;
            var startT = DateTime.Now;

            for (int i = 0; i < 5; i++)
                MatrixMultFuncion(matrixA, matrixB, N);

            sumTime = DateTime.Now - startT;

            ResultesMatrixMult.Add(new TimeAnalysis<Matrix>(N+1, MatrixMultFuncion(matrixA, matrixB, N), sumTime / 5));
        }
        private static Matrix MatrixMultFuncion(Matrix matrixA, Matrix matrixB, int N)
        {
            Matrix res = new Matrix(N + 1);
            int[] t = new int[N + 1];

            for(int i = 0; i <= N; i++)
            {
                for(int j = 0; j <= N; j++)
                {
                    t[j] = matrixA.Strings[i].Str[j] * matrixB.Strings[i].Str[j];                    
                }
                Matrix.AddStr(t, i, res);
            }

            return res;
        }
        private static void WriteDateMatrix(List<TimeAnalysis<Matrix>> res, string name)
           => File.WriteAllLines(@$"../../../{name}.csv",
               res.OrderBy(e => e.AverageTime).Select(e => (e.AverageTime.TotalMilliseconds * 10000).ToString().Replace(',', '.') + ';' + e.CountN.ToString()).ToArray());

        public class Matrix
        {
            public OneString[] Strings { get; private set; }
            public Matrix(int N)
            {
                Strings = new OneString[N];
            }
            public static void AddStr(int[] str, int n, Matrix matrix)
            {
                matrix.Strings[n] = new OneString(str);
            }

        }
        public class OneString
        {
            public int[] Str { get; }
            public OneString(int[] str)
            {
                Str = str;
            }
        }
    }
}
