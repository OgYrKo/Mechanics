using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    internal class ImpreciseLogic
    {
        static public float Not(float A) => (float)(1-A);

        static public float And(float A, float B) => Math.Min(A,B);

        static public float Or(float A, float B) => Math.Max(A, B);

        static public float Implication(float A, float B) => Or(Not(A), B);

        static public float Equivalence(float A, float B) => And(Implication(A, B), Implication(B, A));
    }
}
