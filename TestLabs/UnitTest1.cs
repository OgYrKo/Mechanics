using Lab1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Security.Cryptography.X509Certificates;

namespace TestLabs
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethodCheckY()
        {
            Point p0 = new Point() { X = 0, Y = -10 };
            Point pMax = new Point() { X = 10000, Y = 300 };
            int v0 = 1500;
            double alpha = 2.96819369;

            PointMovement pointMovement = new PointMovement(p0, pMax, v0,alpha);

            double y = pointMovement.GetYByX(pMax.X, alpha * Math.PI / 180);

            Assert.AreEqual(Convert.ToInt32(y),pMax.Y , $"Y is incorrect. Y: {y}");
        }

        [TestMethod]
        public void TestMethodFindAlpha()
        {
            Point p0 = new Point() { X = 0, Y = -10 };
            Point pMax = new Point() { X = 10000, Y = 300 };
            int v0 = 1500;


            PointMovement pointMovement = new PointMovement(p0, pMax, v0);
            var alpha = pointMovement.GetAlpha();
            Assert.AreEqual(Math.Round(alpha), 3, $"alpha is incorrect. ALpha: {alpha}");
        }

        [TestMethod]
        public void TestMethodDshyhovFindV0()
        {
            Point p0 = new Point() { X = 0, Y = 0 };
            Point pMax = new Point() { X = 7500, Y = 0 };
            int alpha=30;//291.3254;


            PointMovement pointMovement = new PointMovement(alpha,p0, pMax);
            double v0 = pointMovement.GetV0();
            Assert.AreEqual(Convert.ToInt32(v0), 291, $"V0 is incorrect. V0: {v0}");

        }

        [TestMethod]
        public void TestMethodDshyhovFindAlpha()
        {
            Point p0 = new Point() { X = 0, Y = 0 };
            Point pMax = new Point() { X = 7500, Y = 0 };
            double v0 = Math.Sqrt( 5 * 9806.65 * Math.Sqrt(3));


            PointMovement pointMovement = new PointMovement(p0, pMax, v0);
            var alpha = pointMovement.GetAlpha();
            Assert.AreEqual(alpha, 30, $"alpha is incorrect. ALpha: {v0}");

        }

        [TestMethod]
        public void TestMethodDschyhovCheckTime()
        {
            Point p0 = new Point() { X = 0, Y = 0 };
            Point pMax = new Point() { X = 7500, Y = 0 };
            double v0 = Math.Sqrt(5 * 9806.65 * Math.Sqrt(3));
            double alpha = 30;
            PointMovement pointMovement = new PointMovement(p0, pMax, v0,alpha);
            

            double T = pointMovement.GetTByX(7500);
            Point p = pointMovement.GetPointByT(T);
            Assert.AreEqual(Convert.ToInt32(pMax.X), Convert.ToInt32(p.X), $"X is incorrect. X: {p.X}");
            Assert.AreEqual(Convert.ToInt32(pMax.Y), Convert.ToInt32(p.Y), $"Y is incorrect. Y: {p.Y}");
        }

        [TestMethod]
        public void TestMethodSimchenkoFindV0()
        {
            Point p0 = new Point() { X = 0, Y = 0 };
            Point pMax = new Point() { X = 8500, Y = 0 };
            int alpha = 35;


            PointMovement pointMovement = new PointMovement(alpha, p0, pMax);
            double v0 = pointMovement.GetV0();
            Assert.AreEqual((int)v0, 297, $"V0 is incorrect. V0: {v0}");

        }

        [TestMethod]
        public void TestMethodSimchenkoFindAlpha()
        {
            Point p0 = new Point() { X = 0, Y = 0 };
            Point pMax = new Point() { X = 8500, Y = 0 };
            double v0 = 297.83579552;


            PointMovement pointMovement = new PointMovement(p0, pMax, v0);
            var alpha = pointMovement.GetAlpha();
            Assert.AreEqual(alpha, 35, $"alpha is incorrect. ALpha: {alpha}");

        }

        [TestMethod]
        public void TestMethodCheckTime()
        {
            Point p0 = new Point() { X = 0, Y = -10 };
            Point pMax = new Point() { X = 10000, Y = 300 };
            int v0 = 1500;

            PointMovement pointMovement = new PointMovement(p0, pMax, v0);
            var alpha = pointMovement.GetAlpha();

            double T = pointMovement.GetTByX(10000);
            Point p = pointMovement.GetPointByT(T);
            Assert.AreEqual(Convert.ToInt32(pMax.X), Convert.ToInt32(p.X), $"X is incorrect. X: {p.X}");
            Assert.AreEqual(Convert.ToInt32(pMax.Y), Convert.ToInt32(p.Y), $"Y is incorrect. Y: {p.Y}");
        }
    }
}
