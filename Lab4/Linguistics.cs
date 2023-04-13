using System;
using System.Collections.Generic;
using System.IO;
namespace Lab4
{
    internal class Linguistics
    {
        List<Word> words;
        List<int> firstOrderIndexes;
        List<int> secondOrderIndexes;
        List<int> negativeIndexes;
        int indexString;

        public string mainWord { get; set; }

        public Linguistics(List<Word> words, string mainWord)
        {
            this.words = words;
            this.mainWord = mainWord;
            firstOrderIndexes = new List<int>();
            secondOrderIndexes = new List<int>();
            negativeIndexes = new List<int>();
            for (int i = 0; i < words.Count; i++)
            {
                SetOrderIndex(i);
            }
        }

        private void SetOrderIndex(int index)
        {
            if (words[index].order == Order.First) firstOrderIndexes.Add(index);
            else if (words[index].order == Order.Second) secondOrderIndexes.Add(index);
            else if (words[index].order == Order.Negative) negativeIndexes.Add(index);
        }

        public void AddWord(Word word)
        {
            words.Add(word);
            SetOrderIndex(words.Count - 1);
        }

        public void Result(TextWriter tw)
        {
            indexString = 0;
            //вывести слово
            tw.WriteLine(++indexString + ") " + mainWord);
            for (int i = 0; i < firstOrderIndexes.Count; i++)
            {
                int indexI = firstOrderIndexes[i];

                string firstOrder = words[indexI].ToString() + " " + mainWord;
                tw.WriteLine(++indexString + ") " + firstOrder);

                //вывести негативные переменные с переменной и словом из первого уровня
                for (int k = 0; k < negativeIndexes.Count; k++)
                {
                    int indexK = negativeIndexes[k];
                    tw.WriteLine(++indexString + ") " + words[indexK].ToString() + " " + firstOrder);
                }
                if (words[indexI].state == State.Outside)
                {

                    //вывести переменную второго уровня с переменной и словом из первого уровня
                    for (int j = 0; j < secondOrderIndexes.Count; j++)
                    {
                        int indexJ = secondOrderIndexes[j];
                        string secondOrder = words[indexJ].ToString() + " " + firstOrder;
                        tw.WriteLine(++indexString + ") " + secondOrder);

                        //негативная переменная
                        //вывести ее с переменной и словом из первого уровня
                        //вывести ее с переменной и словом из второго уровня
                        for (int k = 0; k < negativeIndexes.Count; k++)
                        {
                            int indexK = negativeIndexes[k];
                            tw.WriteLine(++indexString + ") " + words[indexK].ToString() + " " + secondOrder);
                        }

                    }
                }
                
            }
        }
    }
}
