using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class BooleanAlgebra
    {
        static public bool Not(bool A) => !A;

        static public bool And(bool A,bool B) => A && B; 

        static public bool Or(bool A,bool B) => A || B;

        static public bool Implication(bool A, bool B)=>Or(Not(A),B);

        static public bool Equivalence(bool A,bool B) => And(Implication(A,B), Implication(B,A));
    }
}
