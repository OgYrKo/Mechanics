using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Word> words = new List<Word>();

            words.Add(new Adjective("важкий", State.Outside));
            words.Add(new Adjective("легкий", State.Outside));
            words.Add(new Adverb("дуже",ComparisonDegree.Normal));
            words.Add(new Adverb("занадто", ComparisonDegree.More));
            words.Add(new Adverb("досить", ComparisonDegree.More));
            words.Add(new Pronoun("не"));
            words.Add(new Noun("предмет"));
            words.Add(new Noun("рюкзак"));

            Linguistics linguistics = new Linguistics(words);
            linguistics.Result(Console.Out);
        }

    }
}
