using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace HashTables
{
    class Program
    {
        static void Main(string[] args)
        {
            //var dic = GetDictonary(100000, 1000);
            //CalculationFilling();
            //FillFactor();
            //SearchLengthChains(dic);

            //GetDictonary();

            var dic = new MyDictionary<Data, string>(1000);
            Data.GetDatas(100000);

            foreach (var el in Data.Datas)
                dic.Add(el, el.Name);

            Console.WriteLine();

            GetMaxAndMin(dic, 10);
        }
        public static void GetMaxAndMin(MyDictionary<Data, string> dic, int N = 100)
        {
            var min = dic.BucketsFilling().OrderBy(el => el[1]).Take(N).ToList();
            var top = dic.BucketsFilling().OrderByDescending(el => el[1]).Take(N).ToList();

            Console.WriteLine("топ {0} больших", N);

            foreach(var el in top)
                Console.WriteLine("{0} -> {1} ",el[0], el[1]);

            Console.WriteLine("топ {0} меньших", N);

            foreach (var el in min)
                Console.WriteLine("{0} -> {1} ", el[0], el[1]);

            Console.WriteLine("Средння");

            Console.WriteLine((top.Sum(el => el[1]) + min.Sum(el => el[1])) / (N*2));
        }
        private static void GetDictonary(int countEl = 400, int countDic = 200)
        {
            var dic = new MyDictionary<int, int>(countDic);

            int[] arr = new int[countEl];

            for (int i = 0; i < arr.Length; i++)
                arr[i] = new Random().Next();

            for (int i = 0; i < arr.Length; i++)
                dic.Add(i, arr[i]);

            foreach(KeyValuePair<int, int> el in dic)
            {
                Console.WriteLine(dic);
            }
        }        
        private static void CalculationFilling()
        {
            var timeres = new List<string>();
            int[] array = new int[10000];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = new Random().Next();
            }

            for(int i = 0; i < array.Length; i++)
            {
                Console.WriteLine(i);
                var time = new Stopwatch();
                time.Restart();
                var time2 = CalculationFilling(array, i);
                time.Stop();
                timeres.Add(((time2 / time.Elapsed.TotalMilliseconds) * 100).ToString());
            }

            File.WriteAllLines(@"../../../res.csv", timeres.ToArray());
        }
        private static double CalculationFilling(int[] array, int N)
        {
            var dir = new MyDictionary<int,int>(1000);
            var time = new Stopwatch();

            double res = 0;
            for(int i = 0; i <= N; i++)
            {
                time.Restart();
                dir.Add(i, array[i]);
                time.Stop();
                res += time.Elapsed.TotalMilliseconds;
            }

            return res;
        }
    }
}
