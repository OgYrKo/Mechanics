using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sw = System.Console;
namespace Lab4
{
    internal class Linguistics
    {

        List<Word> words;
        string mainWord;

        public Linguistics(List<Word> words,string mainWord)
        {
            this.words = words;
            this.mainWord = mainWord;
        }

        public void AddWord(Word word)
        {
            words.Add(word);
        }

        public void Result()//StreamWriter sw)
        {
            
            //вывести слово
            sw.WriteLine(mainWord);
            //цикл
            for(int i = 0; i < words.Count; i++)
            {
                if (words[i].order == Order.First)
                {
                    string firstOrder = words[i].ToString() + " " + mainWord;
                    sw.WriteLine(firstOrder);
                    if(words[i].state == State.Outside)
                    {
                        
                        //найти переменную второго уровня
                        //вывести ее с переменной и словом из первого уровня
                        for(int j = 0; j < words.Count; j++)
                        {
                            if (words[j].order == Order.Second)
                            {
                                string secondOrder = words[j].ToString() + " " + firstOrder;
                                sw.WriteLine(secondOrder);
                                
                                //найти переменную негативную
                                //вывести ее с переменной и словом из первого уровня
                                //вывести ее с переменной и словом из второго уровня
                                for(int k=0; k < words.Count; k++)
                                {
                                    if (words[k].order == Order.Negative)
                                    {
                                        sw.WriteLine(words[k].ToString() + " " + firstOrder);
                                        sw.WriteLine(words[k].ToString() + " " + secondOrder);
                                    }
                                }
                            }
                        }

                    }
                    else if(words[i].state == State.Inside)
                    {
                        //найти переменную негативную
                        //вывести ее с переменной и словом из первого уровня
                        for (int j = 0; j < words.Count; j++)
                        {
                            if (words[j].order == Order.Negative)
                            {
                                sw.WriteLine(words[j].ToString() + " " + firstOrder);
                            }
                        }
                    }
                }
            }
        }
    }
}
