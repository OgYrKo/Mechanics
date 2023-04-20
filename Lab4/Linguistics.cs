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

        private Noun mainWord { get; set; }

        public Linguistics(List<Word> words, Noun mainWord)
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
            if (words[index].Order == Order.First) firstOrderIndexes.Add(index);
            else if (words[index].Order == Order.Second) secondOrderIndexes.Add(index);
            else if (words[index].Order == Order.Negative) negativeIndexes.Add(index);
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
            foreach (int indexK in negativeIndexes)
            {
                if (words[indexK].CanConnect(mainWord))
                    tw.WriteLine(++indexString + ") " + words[indexK].ToString() + " " + mainWord);
            }


            foreach (int indexI in firstOrderIndexes)
            {
                if (!words[indexI].CanConnect(mainWord)) continue;
                string firstOrder = words[indexI].ToString() + " " + mainWord;
                tw.WriteLine(++indexString + ") " + firstOrder);

                //вывести негативные переменные с переменной и словом из первого уровня
                foreach (int indexK in negativeIndexes)
                {
                    if (!words[indexK].CanConnect(words[indexI])) continue;
                    tw.WriteLine(++indexString + ") " + words[indexK].ToString() + " " + firstOrder);
                }

                //вывести переменную второго уровня с переменной и словом из первого уровня
                foreach (int indexJ in secondOrderIndexes)
                {
                    if (!words[indexJ].CanConnect(words[indexI])) continue;
                    string secondOrder = words[indexJ].ToString() + " " + firstOrder;
                    tw.WriteLine(++indexString + ") " + secondOrder);

                    //негативная переменная
                    //вывести ее с переменной и словом из первого уровня
                    //вывести ее с переменной и словом из второго уровня
                    foreach(int indexK in negativeIndexes)
                    {
                        if (!words[indexK].CanConnect(words[indexJ])) continue;
                        tw.WriteLine(++indexString + ") " + words[indexK].ToString() + " " + secondOrder);
                    }

                }


            }
        }
    }
}
