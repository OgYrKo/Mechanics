using System;
using System.Numerics;

namespace FindAbgle
{
    enum Plane { xy, xz, yz }

    internal static class Program
    {
        
        static void Main()
        {
            Console.WriteLine(GetAngle(new Vector3(),Plane.xy));
        }

        static void Test()
        {
            Vector3 A;
            float angle;

            A = new Vector3(0, 0, 0);
            Console.WriteLine("A " + A.ToString());

            angle = GetAngle(A, Plane.xz);

            Console.WriteLine("1)  {0}\n", angle);

            float x = 0, y = 3.5f, z = 0;
            for (int i = 0; i < 5; i++)
            {
                A = new Vector3(x, y, z);
                Plane plane;
                switch (i % 3)
                {
                    case 0:
                        z += 1;
                        plane = Plane.xy;
                        break;
                    case 1:
                        x += 1;
                        plane = Plane.yz;
                        break;
                    default:
                        y += 1;
                        plane = Plane.xz;
                        break;
                }
                angle = GetAngle(A, plane);
                Console.WriteLine($"{i + 2})  {angle}\n\n\n");
            }
        }

        static float GetAngle(Vector3 O, Plane p)
        {
            Vector3 O7, A;
            O7 = new Vector3(2, 6, 2);
            A = new Vector3(1, 1, 0);

            Vector3 OO7 = O7 - O;
            Console.WriteLine("AB = B - A = " + O7.ToString() + " - " + O.ToString() + " = " + OO7.ToString());

            Vector3 OA = A - O;
            Console.WriteLine("AO = O - A = " + A.ToString() + " - " + O.ToString() + " = " + OA.ToString());

            //OO7 = new Vector3(2, 2, 2);
            //OA = new Vector3(1, 0, 1);
            //OO7 = new Vector3(Convert.ToSingle(Math.Sqrt(2)/2-1), 1, 0);
            //OA = new Vector3(1, 2, 2);            
            OA = new Vector3(0.466f, -0.068f, 0);
            OO7 = new Vector3(1, 1, 0);


            Vector3 OO7_p = new Vector3(OO7.X, OO7.Y, OO7.Z);
            Vector3 OA_p = new Vector3(OA.X, OA.Y, OA.Z);

            string planeStr = "yz";
            switch (p)
            {
                case Plane.yz:
                    OO7_p.X = 0;
                    OA_p.X = 0;
                    planeStr = "yz";
                    break;
                case Plane.xy:
                    OO7_p.Z = 0;
                    OA_p.Z = 0;
                    planeStr = "xy";
                    break;
                case Plane.xz:
                    OO7_p.Y = 0;
                    OA_p.Y = 0;
                    planeStr = "xz";
                    break;
            }
            Console.WriteLine("AB_" + planeStr + ": " + OO7_p.ToString());
            Console.WriteLine("AO_" + planeStr + ": " + OA_p.ToString());
            Vector3 scalarVector = Vector3.Multiply(OO7_p, OA_p);
            float scalar = scalarVector.X + scalarVector.Y + scalarVector.Z;

            Console.WriteLine($"AB_p * AO_p = {OO7_p.X} * {OA_p.X} + {OO7_p.Y} * {OA_p.Y} + {OO7_p.Z} * {OA_p.Z} = {scalar}");
            Console.WriteLine($"|AB_p| = {OO7_p.Length()}");
            Console.WriteLine($"|AO_p| = {OA_p.Length()}");
            float cosF = scalar / OO7_p.Length() / OA_p.Length();
            Console.WriteLine($"cos(f) = AB_p * AO_p / |AB_p| / |AO_p| = {cosF}");
            float angleRad = (float)Math.Acos(cosF);

            return (float)(angleRad * 180 / Math.PI);
        }
    }
}