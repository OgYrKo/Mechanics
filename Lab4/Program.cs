using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    internal class Program
    {
        static void Main(string[] args)
        {

            List<string> input1 = new List<string>() {"важкий","легкий" };
            List<string> input2 = new List<string>() { "дуже", "занадто", "досить" };
            List<string> input3 = new List<string>() { "не" };
            string mainWord = "предмет";

            Linguistics linguistics = new Linguistics(input1, input2, input3,mainWord);
            List<string> r1 = linguistics.FirstOrder();
            List<string> r2 = linguistics.FirstAndSecondOrder();
            List<string> r3 = linguistics.All();
            Print(r1);
            Print(r2);
            Print(r3);
        }

        static void Print(List<string> strings)
        {
            foreach(string s in strings)
            {
                Console.WriteLine(s);
            }
        }
    }
}
