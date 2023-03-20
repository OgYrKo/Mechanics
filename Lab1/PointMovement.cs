using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    using Coordinate = Double;
    using Speed = Double;
    using Degree = Double;
    using Radian = Double;
    using Time = Double;
    

    public struct Point
    {
        public Coordinate X;
        public Coordinate Y;
    }

    public class PointMovement
    {
        public Point p0 { get; private set; }
        public Point pMax { get; private set; }
        public Speed v0 { get; private set; }
        public Degree alphaDegree { get; private set; }
        private Radian alphaRadian;
        const double G = 9.80665;
        

        public PointMovement(Point p0, Point pMax, Speed v0)
        {
            this.p0 = p0;
            this.pMax = pMax;
            this.v0 = v0;
            SetAlpha();
        }

        public PointMovement(Degree alpha, Point p0, Point pMax)
        {
            this.p0 = p0;
            this.pMax = pMax;
            this.alphaDegree = alpha;
            this.alphaRadian = ConvertDegreeToRadian(alpha);
            SetV0();
        }

        public PointMovement(Point p0, Point pMax, Speed v0, Degree alpha)
        {
            this.p0 = p0;
            this.pMax = pMax;
            this.v0 = v0;
            this.alphaDegree = alpha;
            this.alphaRadian = ConvertDegreeToRadian(alpha);
        }


        public Point GetHMax()
        {
                Coordinate x = v0 * v0 * Math.Sin(2 * alphaRadian) / 2 / G;
                double sinA = Math.Sin(alphaRadian);
                Coordinate y = v0 * v0 * sinA * sinA / 2.0 / G;
                return new Point() { X = x, Y = y };
        }

        public Time GetFallTime() => 2.0 * v0 * Math.Sin(alphaRadian) / G;

        public Speed GetSpeedByT(Time t)
        {
            Speed Vx = GetVx();
            Speed Vy = GetVy(t);
            return Math.Sqrt(Vx * Vx + Vy * Vy);
        }

        public Point GetPointByT(Time t) => new Point() { X = GetXByT(t), Y = GetYByT(t) };

        private Coordinate GetXByT(Time t) => t * v0 * Math.Cos(alphaRadian);

        private Coordinate GetYByT(Time t) => t * v0 * Math.Sin(alphaRadian) - G * t * t / 2.0 + p0.Y;

        public Coordinate GetYByX(Coordinate X,Radian a)=>X*(Math.Tan(a) - G * X / 2.0 / v0 / v0 / Math.Cos(a) / Math.Cos(a)) + p0.Y;

        public Time GetTByX(Coordinate X) => X / v0 / Math.Cos(alphaRadian);

        private Speed GetVx() => v0 * Math.Cos(alphaRadian);

        private Speed GetVy(Time t) => v0 * Math.Sin(alphaRadian) - G * t;

        private void SetAlpha()
        {
            double eps = 0.00000001;
            int alphaMin = 0;
            int alphaMax = 90;
            int curentAlpha;
            int nearestAlpha = Int32.MaxValue;
            Coordinate nearestDistance=Int32.MaxValue;

            do
            {
                curentAlpha = (alphaMin + alphaMax) / 2;
                Coordinate yDifference = pMax.Y - GetYByX(pMax.X, ConvertDegreeToRadian(curentAlpha));

                if (yDifference < Math.Abs(nearestDistance))
                {
                    nearestDistance = yDifference;
                    nearestAlpha = curentAlpha;
                }

                if (yDifference < -eps)
                {
                    //сужаем рабочую зону массива с правой стороны
                    alphaMax = curentAlpha - 1;
                }
                else if (yDifference > eps)
                {
                    //сужаем рабочую зону массива с левой стороны
                    alphaMin = curentAlpha + 1;
                }
                else break;
            } while (alphaMin <= alphaMax);



            int intAlpha= nearestAlpha;
            curentAlpha = 0;
            if (nearestAlpha != curentAlpha)
            {
                if(nearestDistance < 0)
                {
                    intAlpha = nearestAlpha - 1;
                }
                alphaMin = 0;
                alphaMax = Convert.ToInt32(1 / eps);
                do
                {
                    curentAlpha = (alphaMin + alphaMax) / 2;
                    Coordinate yDifference = pMax.Y - GetYByX(pMax.X, ConvertDegreeToRadian(intAlpha+curentAlpha*eps));

                    if (yDifference < -eps)
                    {
                        //сужаем рабочую зону массива с правой стороны
                        alphaMax = curentAlpha - 1;
                    }
                    else if (yDifference > eps)
                    {
                        //сужаем рабочую зону массива с левой стороны
                        alphaMin = curentAlpha + 1;
                    }
                    else break;
                } while (alphaMin <= alphaMax);
            }

            alphaDegree = intAlpha + curentAlpha *eps;
            alphaRadian = ConvertDegreeToRadian(alphaDegree);
        }

        private void SetV0()
        {
            if (pMax.Y == 0 && p0.Y == 0)
            {
                Coordinate xAvg = pMax.X / 2;
                v0 = Math.Sqrt(2 * xAvg * G / Math.Sin(2 * alphaRadian));
            }
            else
            {
                throw new Exception("Початкова або кінцева точка знаходиться за межами земної поверхні");
            }
        }

        private Radian ConvertDegreeToRadian(Degree degree) => degree * Math.PI / 180;

        private Degree ConvertRadianToDegree(Radian radian) => radian * 180 / Math.PI;

        public Degree GetAlpha() => alphaDegree;

        public Speed GetV0() => v0;
    }
    
}
