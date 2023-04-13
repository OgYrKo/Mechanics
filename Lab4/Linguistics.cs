using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    internal class Linguistics
    {
        List<string> firstOrder;
        List<string> secondOrder;
        List<string> negative;
        string mainWord;

        public Linguistics(List<string> firstOrder, List<string> secondOrder, List<string> negative, string mainWord)
        {
            this.firstOrder = firstOrder;
            this.secondOrder = secondOrder;
            this.negative = negative;
            this.mainWord = mainWord;
        }

        public List<string> FirstOrder()
        {
            List<string>strings = new List<string>();
            for(int i = 0; i < firstOrder.Count; i++)
            {
                strings.Add(firstOrder[i]+" "+mainWord);
            }
            return strings;
        }

        public List<string> FirstAndSecondOrder()
        {
            List<string> firstStrings = FirstOrder();
            List<string> strings = new List<string>();

            for (int j = 0; j < secondOrder.Count; j++)
            {
                strings.Add(secondOrder[j] + " " + firstStrings[0]);
                if(firstStrings.Count - 1!=0)
                strings.Add(secondOrder[j] + " " + firstStrings[firstStrings.Count-1]);
            }

            return strings;
        }


        public List<string> All()
        {
            List<string> secondStrings = FirstAndSecondOrder();
            List<string> strings = new List<string>();

            for (int i = 0; i < secondStrings.Count; i++)
            {
                for (int j = 0; j < negative.Count; j++)
                {
                    strings.Add(negative[j] + " " + secondStrings[i]);
                }
            }
            return strings;
        }

    }
}
