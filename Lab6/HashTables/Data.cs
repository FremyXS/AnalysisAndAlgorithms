using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTables
{
    public class Data
    {
        public string Name { get; }
        public string Date { get; }
        public Data(string name, string date)
        {
            Name = name;
            Date = date;
        }
        public static List<Data> Datas { get; private set; } = new List<Data>();
        public static void GetDatas(int count)
        {
            for(int i = 0; i < count; i++)
            {
                Datas.Add(new Data(GetRandomName(), GerRandomDate()));
            }
        }
        private static string GerRandomDate()
        {
            string date = "";

            date += new Random().Next(1, 32).ToString() + ".";
            date += new Random().Next(1, 13).ToString() + ".";
            date += new Random().Next(1980, 2021).ToString();

            return date;
        }
        private static string GetRandomName()
        {
            int leinght = new Random().Next(1, 11);
            string name = "";

            for(int i = 0; i < leinght; i++)
            {
                name += new Random().Next('a', 'z' + 1);
            }

            return name;
        }
        public override int GetHashCode()
        {
            int one = 0;
            int three = 0;
            int two = 0;

            var dat = Date.Split('.');

            for(int i = 0; i < dat.Length; i++)
            {
                int N = 0;

                if (i == 0)
                    N = 31;
                else if (i == 1)
                    N = 12;
                else
                    N = 35;

                for(int j = 0; j < dat[i].Length; j++)
                {
                    if (i == 0)
                        one += int.Parse(dat[i]) * N;
                    else if (i == 1)
                        two += int.Parse(dat[i]) * N;
                    else
                        three += int.Parse(dat[i]) * N;
                }
            }

            return (one + two + three)/3;
        }
    }
}
