using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SortingText
{
    public class SortingAlgorithm
    {
        public StackPanel Content;
        public StackPanel Logs;
        public DrawMove dw;
        public SortingAlgorithm(StackPanel content)
        {
            Content = content;
            dw = new DrawMove(content);
        }
        public async Task AbsSort(string[] array)
        {
            await GetWords(array, dw);
        }
        public static List<Dir> Dires { get; private set; } = new List<Dir>();
        public static string ABS = "ABCDEFJHIGKLMNOPQRSTUVWXYZ";
        public async Task GetWords(IEnumerable<string> array, DrawMove dw)
        {
            foreach (var word in array)
            {
                if (!Dires.Any(t => t.Ind == word.ToUpper()[0]))
                    Dires.Add(new Dir(word.ToUpper()[0]));

                AddWord(word);

            }

            var res = new List<string>();

            foreach (var el in ABS)
            {

                if (Dires.Any(t => t.Ind == el))
                    GetRes(res, Dires.Single(t => t.Ind == el).Words);

            }

            await dw.Alg(res.ToArray());
        }
        private void GetRes(List<string> res, Word word)
        {
            if (word != null)
            {
                res.Add(word.Value);

                GetRes(res, word.Next);
            }

        }
        private void AddWord(string word)
        {
            SortI(word, ref Dires.Single(el => el.Ind == word.ToUpper()[0]).Words);
        }
        private void SortI(string value, ref Word word)
        {
            if (word == null)
                word = new Word(value);
            else
            {
                if (Compare(value, word.Value))
                {
                    var temp = (Word)word.Clone();
                    word = new Word(value, temp);
                }
                else
                {
                    SortI(value, ref word.Next);
                }
            }
        }
        private bool Compare(string left, string right) // left < right  => false
        {
            for (int i = 1; i < Math.Min(left.Length, right.Length); i++)
            {
                if (left[i] < right[i])
                    return true;
                else if (left[i] > right[i])
                    return false;
            }

            return left.Length < right.Length; // aaa aa
        }
        public class Dir
        {
            public char Ind { get; }
            public Word Words = null;
            public Dir(char ind)
            {
                Ind = ind;
            }
            
            public override string ToString()
            {
                return Ind.ToString();
            }
        }
        public class Word : ICloneable
        {
            public string Value;
            public Word Next = null;
            public Word(string value)
            {
                Value = value;
            }
            public Word(string value, Word next)
            {
                Value = value;
                Next = next;
            }

            public object Clone()
            {
                return new Word(this.Value, Next != null ? (Word)this.Next.Clone() : null);
            }

            public override string ToString()
            {
                return Value;
            }
        }
    }
    
}
