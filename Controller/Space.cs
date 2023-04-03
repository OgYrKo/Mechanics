using Microsoft.DirectX;
using System;
using System.Collections.Generic;

namespace Controller
{
    using Degree = Double;
    internal class Space
    {
        Vector3 old_A;
        Vector3 B;
        List<Vector3> Points;

        double cos_f, sin_f, cos_theta, sin_theta;


        public Space(Vector3 A, Vector3 B, List<Vector3> points)
        {
            this.Points = new List<Vector3>();
            old_A = A;
            this.B = B - A;
            for (int i = 0; i < points.Count; i++)
            {
                this.Points.Add(points[i] - A);
            }
            SetPhi();
            SetTheta();
        }

        public List<Vector3> Rotate(Degree angle)
        {
            List<Vector3> result = new List<Vector3>();

            for (int i = 0; i < Points.Count; i++)
            {
                Vector3 p = VectorToXZ(Points[i]);
                p = VectorToOZ(p);
                p = RotateByZ(p, angle);
                p = VectorFromOZ(p);
                p = VectorFromXZ(p);
                result.Add(p + old_A);
            }
            return result;
        }

        //угол между осью вращения и осью oz
        private void SetPhi()
        {
            double scalar = B.Length();
            cos_f = B.Z / scalar;
            sin_f = Math.Sqrt(1 - cos_f * cos_f);//B.X / scalar;//
        }

        private Vector3 VectorToXZ(Vector3 point)
        {
            return new Vector3((float)(cos_theta * point.X + sin_theta * point.Y), (float)(-sin_theta * point.X + cos_theta * point.Y), point.Z);
        }
        private Vector3 VectorFromXZ(Vector3 point)
        {
            return new Vector3((float)(cos_theta * point.X - sin_theta * point.Y), (float)(sin_theta * point.X + cos_theta * point.Y), point.Z);
        }

        private Vector3 VectorToOZ(Vector3 point)
        {
            return new Vector3((float)(cos_f * point.X - sin_f * point.Z), point.Y, (float)(sin_f * point.X + cos_f * point.Z));
        }

        private Vector3 VectorFromOZ(Vector3 point)
        {
            return new Vector3((float)(cos_f * point.X + sin_f * point.Z), point.Y, (float)(-sin_f * point.X + cos_f * point.Z));
        }

        private Vector3 RotateByZ(Vector3 point, Degree a)
        {
            double cos, sin;
            if (a % 90 == 0)
            {
                Double b = a / 90;
                if ((b % 2) == 1|| (b % 2) == -1)
                {
                    cos = 0;
                    if (b % 4 == 1) sin = 1;
                    else sin = -1;
                }
                else
                {
                    sin = 0;
                    if (b % 4 == 0) cos = 1;
                    else cos = -1;
                }
            }
            else
            {
                double alpha = Math.PI / 180 * a;
                cos = Math.Cos(alpha);
                sin = Math.Sin(alpha);
            }

            return new Vector3((float)(cos * point.X + sin * point.Y), (float)(-sin * point.X + cos * point.Y), point.Z);
        }

        //угол между осью вращения и плоскостью OZ
        private void SetTheta()
        {
            double theta = 0;
            sin_theta = -2;
            cos_theta = -2;
            if (B.X > 0)
            {
                theta = Math.Atan(B.Y / B.X);
            }
            else if (B.X < 0)
            {
                theta = Math.PI + Math.Atan(B.Y / B.X);
            }
            else
            {
                if (B.Y >= 0)
                {
                    sin_theta = 1;
                    cos_theta = 0;
                }
                else
                {
                    sin_theta = -1;
                    cos_theta = 0;
                }
            }
            if (sin_theta == -2) sin_theta = Math.Sin(theta);
            if (cos_theta == -2) cos_theta = Math.Cos(theta);
        }

        private Degree ConvertRadianToDegree(double radian) => radian * 180 / Math.PI;

        public Degree GetAngle()
        {
            for (int i = 0; i < Points.Count; i++)
            {
                Vector3 p = VectorToXZ(Points[i]);
                p = VectorToOZ(p);
                p.Z = 0;
                Points[i] = p;
            }

            Vector3 newVector = Points[0].CrossProduct(Points[1]);
            double numerator = newVector.Length();
            double denominator = Points[0].Length() * Points[1].Length();
            double sin = numerator / denominator;
            if (sin > 1) sin = 1;
            double returnValue = ConvertRadianToDegree(Math.Asin(sin));
            if (newVector.Z >= 0) returnValue *= -1;
            if (sin == 0)
            {
                numerator = Points[0].X * Points[1].X + Points[0].Y * Points[1].Y + Points[0].Z * Points[1].Z;
                double cos = numerator / denominator;
                if (cos == 0) return 0;
                else return 180;
            }
            return returnValue;
        }
    }
}
