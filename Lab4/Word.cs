using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    public enum Order {First,Second,Negative}
    public enum State {Outside,Inside}

    internal class Word
    {
        string word;
        public Order order { get; private set; }
        public State state { get; private set; }

        public Word(string word, Order order,State state)
        {
            this.word = word;
            this.order = order;
            this.state = state;
        }

        public override string ToString()
        {
            return word;
        }
    }
}
