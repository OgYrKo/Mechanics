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

            words.Add(new Word("важкий", Order.First, State.Outside));
            words.Add(new Word("легкий", Order.First, State.Outside));
            words.Add(new Word("дуже", Order.Second, State.Outside));
            words.Add(new Word("занадто", Order.Second, State.Outside));
            words.Add(new Word("досить", Order.Second, State.Outside));
            words.Add(new Word("не", Order.Negative, State.Outside));

            Linguistics linguistics = new Linguistics(words, "предмет");
            //StreamWriter sw = new StreamWriter(Console.Out);
            linguistics.Result();
        }

    }
}
