using Microsoft.VisualStudio.TestTools.UnitTesting;
using Controller;
using System;
using Microsoft.DirectX;

namespace TestKosoyi
{
    [TestClass]
    public class RotationTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Element[] elements,elementsCopy;
            SetElements(out elements);
            SetElements(out elementsCopy);

            int degree = 90;
            elements[0].Rotate(degree);
            CheckElements(elements, elementsCopy);
        }

        private void CheckElements(in Element[] elementModyfied, in Element[] elementStart)
        {
            for(int i = 0; i < elementModyfied.Length; i++)
            {
                CheckElement(elementModyfied[i], elementStart[i],i);
            }
        }

        private void CheckElement(in Element elementModyfied, in Element elementStart,int index)
        {
            Assert.AreEqual(elementModyfied.startPoint, elementStart.startPoint,$"Начальная точка звена {index} некоректна");
            Assert.AreEqual(elementModyfied.endPoint, elementStart.endPoint, $"Конечная точка звена {index} некоректна");
        }

        private void SetElements(out Element[] elements)
        {
            elements = new Element[6];
            float x, y, z;
            const float offset = -1f;
            x = y = z = 2f;

            for (int i = elements.Length - 1; i >= 0; i--)
            {
                Vector3 endPoint = new Vector3(x, y, z);
                if (i % 3 == 0)
                {
                    y += offset;
                }
                else if (i % 3 == 1)
                {
                    z += offset;
                }
                else
                {
                    x += offset;
                }
                
                if (i == elements.Length - 1) elements[i] = new Element(new Vector3(x, y, z), endPoint,null);
                else elements[i] = new Element(new Vector3(x, y, z), endPoint, elements[i + 1]);
            }
        }
    }
}
