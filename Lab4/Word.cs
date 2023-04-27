﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    public enum Order {Zero,First,Second,Negative}
    public enum State {Outside,Inside}
    public enum ComparisonDegree {Normal, More }

    internal abstract class Word
    {
        string word;
        public Order Order { get; private set; }

        public Word(string word, Order order)
        {
            this.word = word;
            this.Order = order;
        }

        public override string ToString()
        {
            return word;
        }

        public abstract bool CanConnect(Word word);
    }

    internal class Noun : Word
    {
        public Noun(string word) : base(word, Order.Zero) { }

        public override bool CanConnect(Word word) => true;
    }

    internal class Adjective : Word
    {
        private State state;

        public Adjective(string word, State state) : base(word, Order.First)
        {
            this.state = state;
        }

        public override bool CanConnect(Word word)
        {
            switch (word.Order)
            {
                case Order.Second:
                    if (state == State.Outside) return true;
                    else return false;
                case Order.Negative:
                    return true;
                default:
                    return false;
            }
        }
    }

    internal class Adverb : Word
    {
        private ComparisonDegree degree;
        public Adverb(string word, ComparisonDegree degree) : base(word, Order.Second) 
        {
            this.degree = degree;
        }

        public override bool CanConnect(Word word)
        {
            switch (word.Order)
            {
                case Order.Negative:
                    if (degree== ComparisonDegree.Normal) return true;
                    else return false;
                default: 
                    return false;
            }
        }
    }

    internal class Pronoun : Word
    {
        public Pronoun(string word) : base(word, Order.Negative) { }

        public override bool CanConnect(Word word) => false;
    }

}
