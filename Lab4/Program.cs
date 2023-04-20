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
            words.Add(new Adverb("дуже",Degree.Normal));
            words.Add(new Adverb("занадто", Degree.More));
            words.Add(new Adverb("досить", Degree.More));
            words.Add(new Pronoun("не"));

            Linguistics linguistics = new Linguistics(words, new Noun("предмет"));
            linguistics.Result(Console.Out);
        }

    }
}
