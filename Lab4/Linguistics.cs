using System;
using System.Collections.Generic;
using System.IO;
namespace Lab4
{
    internal class Linguistics
    {
        List<Word> words;
        List<int> zeroOrderIndexes;
        List<int> firstOrderIndexes;
        List<int> secondOrderIndexes;
        List<int> negativeIndexes;
        int indexString;

        public Linguistics(List<Word> words)
        {
            this.words = words;
            zeroOrderIndexes = new List<int>();
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
            switch (words[index].Order)
            {
                case Order.Zero:
                    zeroOrderIndexes.Add(index);
                    break;
                case Order.First:
                    firstOrderIndexes.Add(index);
                    break;
                case Order.Second:
                    secondOrderIndexes.Add(index);
                    break;
                case Order.Negative:
                    negativeIndexes.Add(index);
                    break;
                
            }
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
            foreach (int indexM in zeroOrderIndexes)
            {
                Word mainWord = words[indexM];


                tw.WriteLine(++indexString + ") " + mainWord);
                foreach (int indexK in negativeIndexes)
                {
                    if (mainWord.CanConnect(words[indexK]))
                        tw.WriteLine(++indexString + ") " + words[indexK].ToString() + " " + mainWord);
                }


                foreach (int indexI in firstOrderIndexes)
                {
                    if (!mainWord.CanConnect(words[indexI])) continue;
                    string firstOrder = words[indexI].ToString() + " " + mainWord;
                    tw.WriteLine(++indexString + ") " + firstOrder);

                    //вывести негативные переменные с переменной и словом из первого уровня
                    foreach (int indexK in negativeIndexes)
                    {
                        if (!words[indexI].CanConnect(words[indexK])) continue;
                        tw.WriteLine(++indexString + ") " + words[indexK].ToString() + " " + firstOrder);
                    }

                    //вывести переменную второго уровня с переменной и словом из первого уровня
                    foreach (int indexJ in secondOrderIndexes)
                    {
                        if (!words[indexI].CanConnect(words[indexJ])) continue;
                        string secondOrder = words[indexJ].ToString() + " " + firstOrder;
                        tw.WriteLine(++indexString + ") " + secondOrder);

                        //негативная переменная
                        //вывести ее с переменной и словом из первого уровня
                        //вывести ее с переменной и словом из второго уровня
                        foreach (int indexK in negativeIndexes)
                        {
                            if (!words[indexJ].CanConnect(words[indexK])) continue;
                            tw.WriteLine(++indexString + ") " + words[indexK].ToString() + " " + secondOrder);
                        }

                    }
                }
            }
        }
    }
}
