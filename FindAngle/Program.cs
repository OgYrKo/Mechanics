using System;
using System.Numerics;

namespace FindAbgle
{
    enum Plane { xy, xz, yz }

    internal static class Program
    {
        
        static void Main()
        {
            Vector3 A;
            float angle;

            A=new Vector3(0, 0, 0);
            Console.WriteLine("A " + A.ToString());

            angle = GetAngle(A, Plane.xz);

            Console.WriteLine("1)  {0}\n", angle);

            float x=0, y= 3.5f, z=0;
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
                angle= GetAngle(A, plane);
                Console.WriteLine($"{i+2})  {angle}\n\n\n");
            }    


            
        }

        static float GetAngle(Vector3 A, Plane p)
        {
            Vector3 B, O;
            B = new Vector3(2, 6, 2);
            O = new Vector3(1, 1, 0);

            Vector3 AB = B - A;
            Console.WriteLine("AB = B - A = " + B.ToString() + " - " + A.ToString() + " = " + AB.ToString());

            Vector3 AO = O - A;
            Console.WriteLine("AO = O - A = " + O.ToString() + " - " + A.ToString() + " = " + AO.ToString());

            Vector3 AB_p = new Vector3(AB.X, AB.Y, AB.Z);
            

            Vector3 AO_p = new Vector3(AO.X, AO.Y, AO.Z);

            string planeStr = "yz";
            switch (p)
            {
                case Plane.yz:
                    AB_p.X = 0;
                    AO_p.X = 0;
                    planeStr = "yz";
                    break;
                case Plane.xy:
                    AB_p.Z = 0;
                    AO_p.Z = 0;
                    planeStr = "xy";
                    break;
                case Plane.xz:
                    AB_p.Y = 0;
                    AO_p.Y = 0;
                    planeStr = "xz";
                    break;
            }
            Console.WriteLine("AB_" + planeStr + ": " + AB_p.ToString());
            Console.WriteLine("AO_" + planeStr + ": " + AO_p.ToString());
            Vector3 scalarVector = Vector3.Multiply(AB_p, AO_p);
            float scalar = scalarVector.X + scalarVector.Y + scalarVector.Z;

            Console.WriteLine($"AB_p * AO_p = {AB_p.X} * {AO_p.X} + {AB_p.Y} * {AO_p.Y} + {AB_p.Z} * {AO_p.Z} = {scalar}");
            Console.WriteLine($"|AB_p| = {AB_p.Length()}");
            Console.WriteLine($"|AO_p| = {AO_p.Length()}");
            float cosF = scalar / AB_p.Length() / AO_p.Length();
            Console.WriteLine($"cos(f) = AB_p * AO_p / |AB_p| / |AO_p| = {cosF}");
            float angleRad = (float)Math.Acos(cosF);

            return (float)(angleRad * 180 / Math.PI);
        }
    }
}