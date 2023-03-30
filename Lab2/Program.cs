using System;
using BA = Lab2.BooleanAlgebra;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool[] A = { false, false, true, true };
            bool[] B = { false, true, false, true };
            Task1(A,B);
            Console.WriteLine("\n\n");
            Task2(A, B);
            Console.WriteLine("\n\n");
            Task3();
            Console.WriteLine("\n\n");
        }

        static private void Task1(in bool[] A,in bool[] B)
        {
            int[] r = new int[5];
            Console.WriteLine($"A\t\tB\t\tNot A or B\tA or Not B\tA and Not B\tA => Not B\tA =>(A => Not B)");
            for (int i = 0; i < A.Length; i++)
            {
                r[0] = BA.Or(BA.Not(A[i]), B[i]) ? 1 : 0;
                r[1] = BA.Or(A[i], BA.Not(B[i])) ? 1 : 0;
                r[2] = BA.And(A[i], BA.Not(B[i])) ? 1 : 0;
                r[3] = BA.Implication(A[i], BA.Not(B[i])) ? 1 : 0;
                r[4] = BA.Implication(A[i], BA.Implication(A[i], BA.Not(B[i]))) ? 1 : 0;
                Console.WriteLine($"{(A[i] ? 1:0)}\t\t{(B[i] ? 1 : 0)}\t\t{r[0]}\t\t{r[1]}\t\t{r[2]}\t\t{r[3]}\t\t{r[4]}");
            }

        }
        static private void Task2(in bool[] A, in bool[] B)
        {
            int[] r = new int[5];
            Console.WriteLine($"A\t\tB\t\tHard\t\tHard\t\tHard\t\tHard\t\tHard");
            for (int i = 0; i < A.Length; i++)
            {
                r[0] = BA.And(BA.Or(BA.Not(A[i]), B[i]), B[i]) ? 1 : 0;
                r[1] = BA.Or(BA.Or(A[i], BA.Not(B[i])),A[i]) ? 1 : 0;
                r[2] = BA.Equivalence(A[i], BA.Not(B[i])) ? 1 : 0;
                r[3] = BA.Equivalence(A[i], BA.Or(A[i], BA.Not(B[i]))) ? 1 : 0;
                r[4] = BA.Equivalence(A[i], BA.Implication(A[i], BA.Not(B[i]))) ? 1 : 0;
                Console.WriteLine($"{(A[i] ? 1 : 0)}\t\t{(B[i] ? 1 : 0)}\t\t{r[0]}\t\t{r[1]}\t\t{r[2]}\t\t{r[3]}\t\t{r[4]}");
            }

        }
        static private void Task3()
        {
            bool[] A = { false, false, false, false,true,true,true,true };
            bool[] B = { false, false, true, true, false, false, true, true };
            bool[] C = { false, true, false, true, false, true, false, true };
            int[] r = new int[4];
            Console.WriteLine($"A\t\tB\t\tC\t\tHard\t\tHard\t\tHard\t\tHard");

            for (int i = 0; i < A.Length; i++)
            {
                r[0] = BA.Or(BA.Or(A[i], BA.Not(B[i])), C[i]) ? 1 : 0;
                r[1] = BA.Equivalence(A[i], BA.Or(BA.Not(B[i]), C[i]))? 1 : 0;
                r[2] = BA.Equivalence(C[i], BA.And(A[i],BA.Not(B[i]))) ? 1 : 0;
                r[3] = BA.Equivalence(A[i], BA.Implication(C[i],B[i])) ? 1 : 0;
                Console.WriteLine($"{(A[i] ? 1 : 0)}\t\t{(B[i] ? 1 : 0)}\t\t{(C[i] ? 1 : 0)}\t\t{r[0]}\t\t{r[1]}\t\t{r[2]}\t\t{r[3]}");
            }

        }
    }
}
