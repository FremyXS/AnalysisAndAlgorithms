using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;

namespace SortingTextConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] array = File.ReadAllText(@"../../../texteng.txt").Split(new char[] { ' ', ',', '\'', '\"', ':', '-', '.', '?', '!', ';' }, StringSplitOptions.RemoveEmptyEntries);
            var times = new List<string>();

            times.Add("BubbleSort;ABCSort");
            for (int i = 0; i < 2000; i++)
            {
                Console.WriteLine(i);
                times.Add(DiagBubble(GetLim(array, i)) + ";" + DiagABS(GetLim(array, i)));
            }

            File.WriteAllLines(@"../../../res8.csv", times.ToArray());
            //Dir.GetWords(array);
            //GnomeSort(array);

        }
        static void BubbleSort(string[] mas)
        {
            string temp;
            for (int i = 0; i < mas.Length; i++)
            {
                for (int j = i + 1; j < mas.Length; j++)
                {
                    if (Compare(mas[i], mas[j]))
                    {
                        temp = mas[i];
                        mas[i] = mas[j];
                        mas[j] = temp;
                    }
                }
            }
        }
        public static void GnomeSort(string[] array)
        {
            var index = 1;
            var nextIndex = index + 1;

            while (index < array.Length)
            {
                if (Compare(array[index], array[index-1]))
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
        public static void CombSortText(string[] array)
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

                    if (Compare(array[i].ToUpper(), array[igap].ToUpper()))
                    {
                        Swap(array, i, igap);
                        swaps = true;
                    }

                    ++i;
                }
            }
        }
        private static bool Compare(string left, string right)
        {

            for(int i = 0; i < Math.Min(left.Length, right.Length); i++)
            {
                if (left[i] > right[i])
                    return true;
                else if(left[i] < right[i])
                    return false;
            }

            if (left.Length > right.Length)
                return true;
            else
                return false;
        }
        private static void Swap(string[] array, int i, int j)
        {
            string temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }        
    
        private static string DiagBubble(string[] array)
        {
            
            Stopwatch timer = new();
            timer.Restart();
            for (int i = 0; i < 5; i++)
                BubbleSort(array);

            timer.Stop();

            return (timer.Elapsed.Ticks/5).ToString();

        }
        private static string DiagABS(string[] array)
        {

            Stopwatch timer = new();
            timer.Restart();
            for (int i = 0; i < 5; i++)
                Dir.GetWords(array);

            timer.Stop();

            return (timer.Elapsed.Ticks / 5).ToString();

        }
        private static string[] GetLim(string[] array, int ind)
        {
            var ar2 = new string[ind + 1];

            for(int i = 0; i <= ind; i++)
            {
                ar2[i] = array[i];
            }

            return ar2;
        }
    }
    public class Dir
    {
        private char Ind { get; }
        //public List<Word> Words = new List<Word>();
        public List<string> Words = new List<string>();
        //public Word Words;
        public Dir(char ind, int track = 0)
        {
            Ind = ind;
        }
        public static List<Dir> Dires { get; private set; } = new List<Dir>();
        public static string ABS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";
        public static void GetWords(string[] array)
        {
            Dires.Clear();

            foreach(var word in array)
            {
                if (!Dires.Any(t => t.Ind == word.ToUpper()[0]))
                    Dires.Add(new Dir(word.ToUpper()[0]));

                AddWord(word, ref Dires.Single(el => el.Ind == word.ToUpper()[0]).Words);
            }

            var res = new List<string>();

            foreach (var el in ABS)
            {

                if (Dires.Any(t => t.Ind == el))
                {
                    res.AddRange(GetRes(Dires.Single(t => t.Ind == el).Words));
                }

            }


        }
        private static void GetRes(List<string> res, Word word)
        {
            if (word != null)
            {
                res.Add(word.Value);

                GetRes(res, word.Next);
            }
               
        }
        private static List<string> GetRes(List<string> array)
        {
            var index = 1;
            var nextIndex = index + 1;

            while (index < array.Count)
            {
                if (Compare(array[index], array[index - 1]))
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

            return array;

        }
        private static void AddWord(string word, ref List<string> words)
        {
            words.Add(word);
        }
        private static void AddWord(string word, ref Word words)
        {
            SortI(word, ref words);
        }
        private static void SortI(string value, ref Word word)
        {
            if (word == null)
                word = new Word(value);
            else
            {
                if (Compare(value.ToLower(), word.Value.ToLower()))
                {
                    //var temp = (Word)word.Clone();
                    word = new Word(value, (Word)word.Clone());
                }
                else
                {
                    SortI(value, ref word.Next);
                }
            }
        }
        private static void SortI(string value, ref List<string> array)
        {
            double gap = array.Count;
            bool swaps = true;

            while (gap > 1 || swaps)
            {
                gap /= 1.247330950103979;

                if (gap < 1)
                    gap = 1;

                int i = 0;
                swaps = false;

                while (i + gap < array.Count)
                {
                    int igap = i + (int)gap;

                    if (Compare(array[i].ToUpper(), array[igap].ToUpper()))
                    {
                        Swap(array, i, igap);
                        swaps = true;
                    }

                    ++i;
                }
            }

        }
        private static bool Compare(string left, string right) // left < right  => false
        {
            for (int i = 1; i < Math.Min(left.Length, right.Length); i++)
            {
                if (left[i] > right[i])
                    return true;
                else if (left[i] < right[i])
                    return false;
            }

            return left.Length > right.Length; // aaa aa
        }
        private static void Swap(List<string> array, int i, int j)
        {
            string temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
        public override string ToString()
        {
            return Ind.ToString();
        }
    }
    public class Word : ICloneable
    {
        public string Value { get; set; }
        public Word Next  = null;
        public Word Old { get; set; } = null;
        public Word(string value)
        {
            Value = value;
        }
        public Word(string value, Word next)
        {
            Value = value;
            Next = next;
        }
        public Word(string value, Word next, Word old)
        {
            Value = value;
            Next = next;
            Old = old;
        }

        public object Clone()
        {
            return new Word(this.Value, Next != null? (Word)this.Next.Clone(): null);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
