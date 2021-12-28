using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingText
{
    public class UniqueWord
    {
        public string Word { get; }
        public int Count { get; private set; } = 1;
        public UniqueWord(string word)
        {
            Word = word;
        }
        public static List<UniqueWord> UniqueWords { get; private set; } = new List<UniqueWord>();
        public static void CountUniqueWords(string[] array)
        {
            foreach(var word in array)
            {
                if (!UniqueWords.Any(el => el.Word.ToUpper() == word.ToUpper()))
                {
                    UniqueWords.Add(new UniqueWord(word));
                }
                else
                    UniqueWords.Single(el => el.Word.ToUpper() == word.ToUpper()).Count++;

            }
        }
    }
}
