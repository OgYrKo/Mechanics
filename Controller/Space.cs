using Microsoft.DirectX;
using System;
using System.Collections.Generic;

namespace Controller
{
    using Degree = Double;
    internal class Space
    {
        Vector3 translation;
        Vector3 vector;

        double cos_f, sin_f, cos_theta, sin_theta;


        public Space(Vector3 O, Vector3 O1)
        {
            translation = O;
            this.vector = O1 - translation;
            SetPhi();
            SetTheta();
        }

        public List<Vector3> PointsToUpperSystem(List<Vector3> points)
        {
            for(int i=0;i<points.Count;i++)
            {
                points[i] = ToUpperSystem(points[i]-translation);
            }
            return points;
        }

        //public List<Vector3> PointsToLowerSystem(List<Vector3> points)
        //{
        //    for (int i = 0; i < points.Count; i++)
        //    {
        //        points[i] = ToLowerSystem(points[i]);
        //    }
        //    return points;
        //}

        public Vector3 ToUpperSystem(Vector3 vector) => VectorToOZ(VectorToXZ(vector));
        public Vector3 ToLowerSystem(Vector3 vector) => VectorFromXZ(VectorFromOZ(vector));


        public List<Vector3> RotatePoints(List<Vector3> points, Degree angle)
        {
            List<Vector3> result = new List<Vector3>();

            for (int i = 0; i < points.Count; i++)
            {
                Vector3 p = ToUpperSystem(points[i]-translation);
                p = RotateByZ(p, angle);
                p = ToLowerSystem(p);
                result.Add(p + translation);
            }
            return result;
        }

        //угол между осью вращения и осью oz
        private void SetPhi()
        {
            double scalar = vector.Length();
            cos_f = vector.Z / scalar;
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
            if (vector.X > 0)
            {
                theta = Math.Atan(vector.Y / vector.X);
            }
            else if (vector.X < 0)
            {
                theta = Math.PI + Math.Atan(vector.Y / vector.X);
            }
            else
            {
                if (vector.Y >= 0)
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

        public Degree GetAngle(Vector3 A, Vector3 O7)
        {
            Vector3 OA=A-translation;
            Vector3 OO1 = vector;
            Vector3 OO7 = O7-translation;

            Vector3 O1A_1 = ToUpperSystem(OA - OO1);
            Vector3 O1O7_1 = ToUpperSystem(OO7 - OO1);

            O1A_1.Z = 0;
            O1O7_1.Z = 0;
            

            float scalar = O1A_1.X * O1O7_1.X + O1A_1.Y * O1O7_1.Y + O1A_1.Z * O1O7_1.Z;
            float aProjLength = O1A_1.Length();
            float bProjLength = O1O7_1.Length();
            float cos = scalar / aProjLength / bProjLength;
            Vector3 newVector = Vector3.Cross(O1A_1, O1O7_1);
            if (cos > 1) cos = 1;
            else if (cos < -1) cos = -1;
            double returnValue = Math.Acos(cos) * (180 / Math.PI);
            if (newVector.Z >= 0) return returnValue;
            else return -returnValue;

        }

        public Vector3 GetNewA(Vector3 A, Degree angle)
        {
            Vector3 OA = A - translation;
            Vector3 OO1 = vector;

            Vector3 O1A_1 = ToUpperSystem(OA - OO1);
            Vector3 O1A_1_new = RotateByZ(O1A_1, -angle);
            return O1A_1_new;
        }
    }
}
