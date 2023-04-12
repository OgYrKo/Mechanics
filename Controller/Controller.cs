using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Threading;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Controller
{
    internal class Controller
    {
        private Shoulder[] elements;
        private Brush brush;
        Form1 form;
        Device device;
        const int ELEMENTS_COUNT = 7;
        const double ROTATE_FREQUENCY = 0.5;//на сколько градусов поворачивать за 1 раз
        const int ROTATE_FREQUENCY_TIME = 1;//частота поворота (ms)
        const float START_Y = 0;//2.5f;
        const float OFFSET = 1;
        Mutex drawMutex;
        Mutex paralelMutex;
        object paralelLock;
        Thread[] threads;
        int loopCount;
        double maxLength;

        public Controller(Form1 form, Device device)
        {
            elements = new Shoulder[ELEMENTS_COUNT];
            threads = new Thread[ELEMENTS_COUNT + 2];
            this.form = form;
            this.device = device;
            MutexInit();
            SetStartPosition();
        }

        private Vector3 GetEndPoint()
        {
            return brush.endPoint;//elements[ELEMENTS_COUNT - 1].endPoint;
        }

        private void SetStartPosition()
        {
            float x, y, z;
            x = z = 0;
            y = START_Y;

            for (int i = 0; i < ELEMENTS_COUNT; i++)
            {
                if (i % 3 == 0)
                {
                    y += OFFSET;
                }
                else if (i % 3 == 1)
                {
                    z += OFFSET;
                }
                else
                {
                    x += OFFSET;
                }
            }

            brush = new Brush(device, new Vector3(x, y, z), new Vector3(x + (ELEMENTS_COUNT % 3 == 0 ? OFFSET / 2 : 0),
                                   y + (ELEMENTS_COUNT % 3 == 1 ? OFFSET / 2 : 0),
                                   z + (ELEMENTS_COUNT % 3 == 2 ? OFFSET / 2 : 0)));

            for (int i = elements.Length - 1; i >= 0; i--)
            {
                Vector3 cylinderEndPoint = new Vector3(x, y, z);
                if (i % 3 == 0)
                {
                    y -= OFFSET;
                }
                else if (i % 3 == 1)
                {
                    z -= OFFSET;
                }
                else
                {
                    x -= OFFSET;
                }
                if(i==0) elements[i] = new Shoulder(device, new Vector3(), cylinderEndPoint, elements[i + 1]);
                else if (i == elements.Length - 1) elements[i] = new Shoulder(device, new Vector3(x, y, z), cylinderEndPoint, brush);
                else elements[i] = new Shoulder(device, new Vector3(x, y, z), cylinderEndPoint, elements[i + 1]);
            }
            maxLength = (brush.endPoint - elements[0].endPoint).Length();
        }

        private void MutexInit()
        {
            drawMutex = new Mutex();
            paralelMutex = new Mutex();
            paralelLock = new object();
        }

        public bool IsWork()
        {
            for (int i = 0; i < threads.Length; i++)
            {
                if (threads[i] != null && threads[i].IsAlive)
                {
                    return true;
                }
            }
            return false;
        }

        public void DrawElements()
        {
            drawMutex.WaitOne();
            foreach (Shoulder element in elements)
            {
                element.DrawElement();
            }
            brush.DrawElement();
            drawMutex.ReleaseMutex();
        }

        public void Rotate(int index, double angle)
        {
            if (threads[index] != null) threads[index].Abort();
            threads[index] = new Thread(new ParameterizedThreadStart(Rotate));
            threads[index].Start((index, angle));
        }

        [Obsolete]
        public void StopThread()
        {
            if (threads == null) return;
            foreach (Thread t in threads)
            {
                if (t != null && t.IsAlive) t.Suspend();
            }
        }

        [Obsolete]
        public void ResumeThread()
        {
            if (threads == null) return;
            foreach (Thread t in threads)
            {
                if (t != null && t.IsAlive) t.Resume();
            }
        }

        public void AbortThread()
        {
            if (threads == null) return;
            for(int i = 0; i < threads.Length; i++)
            {
                if (threads[i] != null && threads[i].IsAlive) threads[i].Abort();
            }
        }

        private void Rotate(object tuple)
        {
            (int, double) typedTuple = ((int, double))tuple;
            int index = typedTuple.Item1;
            double angle = typedTuple.Item2;
            if (double.IsNaN(angle)) return;
            int angleSign = Convert.ToInt32(angle / Math.Abs(angle));
            //получаем элемент начиная с которого будем поворачивать все последующие
            Element element;
            if (index >= ELEMENTS_COUNT) element = brush;
            else element = elements[index];

            //выставляем количество поворотв в соответствии с частотой поворота
            int rotationCount = Math.Abs(Convert.ToInt32(angle / ROTATE_FREQUENCY));

            //поворачиваем звенья необходимое количество раз
            int completeRotationCount = 0;
            for (; completeRotationCount < rotationCount; completeRotationCount++)
            {
                element.Rotate(angleSign * ROTATE_FREQUENCY);
                form.Invalidate();
                Thread.Sleep(ROTATE_FREQUENCY_TIME);
            }

            //если угол имел дробную часть, то доворачиваем на нее
            double rest = Math.Abs(angle) - rotationCount;
            if (rest > 0)
            {
                element.Rotate(rest * angleSign);
                form.Invalidate();
            }
        }

        private Matrix RotationMatrix(double angle)
        {
            angle = Math.PI / 180 * angle;
            Matrix A = new Matrix();
            A.M11 = Convert.ToSingle(Math.Cos(angle));
            A.M12 = Convert.ToSingle(Math.Sin(angle));
            A.M13 = 0;
            A.M21 = Convert.ToSingle(-Math.Sin(angle));
            A.M22 = Convert.ToSingle(Math.Cos(angle));
            A.M23 = 0;
            A.M31 = 0;
            A.M32 = 0;
            A.M33 = 1;
            return A;
        }

        public Vector3 Multiply(Vector3 vector, Matrix matrix)
        {
            float x = vector.X * matrix.M11 + vector.Y * matrix.M21 + vector.Z * matrix.M31;
            float y = vector.X * matrix.M12 + vector.Y * matrix.M22 + vector.Z * matrix.M32;
            float z = vector.X * matrix.M13 + vector.Y * matrix.M23 + vector.Z * matrix.M33;
            return new Vector3(x, y, z);
        }


            private void ParalelFind(object tuple)
        {
            (Item, List<NumericUpDown>) typedTuple = ((Item, List<NumericUpDown>))tuple;
            lock (paralelLock)
            {
                loopCount = 0;
                Item item = typedTuple.Item1;
                List<NumericUpDown> numerics = typedTuple.Item2;
                //while (!Vector3Extencion.Compare(GetEndPoint(), item.centerPoint))
                {
                    //for (int i = 0; i < ELEMENTS_COUNT - 1; i++)
                    //{

                    //    Vector3 copy = GetEndPoint();
                    //    Vector3 endPoint = new Vector3(copy.X, copy.Y, copy.Z);

                    //    Vector3 pointCopy = new Vector3(item.centerPoint.X, item.centerPoint.Y, item.centerPoint.Z);
                    //    double angle = elements[i].GoToPoint(ref pointCopy, endPoint);
                    //    if (i < numerics.Count)
                    //    {
                    //        numerics[i].Invoke(new Action(() => numerics[i].Value = (int)angle));
                    //    }
                    //    if (angle == 0) continue;
                    //    Rotate(i, angle);
                    //    threads[i].Join();
                    //}

                    //////////////////////////////////////////////////////////////////////


                    //Vector3 searchPoint = new Vector3(item.centerPoint.X, item.centerPoint.Y, item.centerPoint.Z);
                    //Vector3[] O = new Vector3[ELEMENTS_COUNT + 1];
                    //for (int i = 0; i < ELEMENTS_COUNT; i++)
                    //{
                    //    O[i] = new Vector3(elements[i].startPoint.X, elements[i].startPoint.Y, elements[i].startPoint.Z);
                    //}
                    ////O[ELEMENTS_COUNT] = new Vector3(brush.endPoint.X, brush.endPoint.Y, brush.endPoint.Z);
                    //O[ELEMENTS_COUNT] = new Vector3(elements[ELEMENTS_COUNT - 1].endPoint.X, elements[ELEMENTS_COUNT - 1].endPoint.Y, elements[ELEMENTS_COUNT - 1].endPoint.Z);

                    //Space[] spaces = new Space[ELEMENTS_COUNT - 1];
                    //double[] angles = new double[ELEMENTS_COUNT - 1];

                    //for (int i = 0; i < ELEMENTS_COUNT - 1; i++)
                    //{
                    //    spaces[i] = new Space(O[i], O[i + 1]);

                    //    angles[i] = spaces[i].GetAngle(searchPoint, O[7]);

                    //    if (i < numerics.Count && !double.IsNaN(angles[i]))
                    //        numerics[i].Invoke(new Action(() => numerics[i].Value = (int)angles[i]));

                    //    searchPoint = spaces[i].GetNewA(searchPoint, angles[i]);
                    //    //searchPoint+=O[i+1];
                    //    //Переводим все точки из 0 системы в 1
                    //    O = spaces[i].PointsToUpperSystem(O.ToList()).ToArray();
                    //}

                    //for (int i = 0; i < angles.Length; i++)
                    //{
                    //    if (angles[i] == 0) continue;
                    //    Rotate(i, angles[i]);
                    //    threads[i].Join();
                    //}
                    /////////////////////////////////////////////////////////////////
                    ///

                    Vector3 searchPoint = new Vector3(item.centerPoint.X, item.centerPoint.Y, item.centerPoint.Z);
                    Vector3[] OiOiplus1 = new Vector3[ELEMENTS_COUNT];

                    OiOiplus1[0] = new Vector3(0, START_Y + OFFSET, 0);

                    Matrix[] A = new Matrix[ELEMENTS_COUNT];
                    for (int i = 0; i < ELEMENTS_COUNT; i++)
                    {
                        OiOiplus1[i] = new Vector3(0, 1, 0);
                    }
                    for (int i = 0; i < ELEMENTS_COUNT-1; i++)
                    {
                        A[i] = new Matrix();
                        if (i % 3 == 0)
                        {
                            A[i].M11 = 1;
                            A[i].M12 = 0;
                            A[i].M13 = 0;
                            A[i].M21 = 0;
                            A[i].M22 = 0;
                            A[i].M23 = 1;
                            A[i].M31 = 0;
                            A[i].M32 = 1;
                            A[i].M33 = 0;
                        }
                        else if (i % 3 == 1)
                        {
                            A[i].M11 = 0;
                            A[i].M12 = 0;
                            A[i].M13 = 1;
                            A[i].M21 = 1;
                            A[i].M22 = 0;
                            A[i].M23 = 0;
                            A[i].M31 = 0;
                            A[i].M32 = 1;
                            A[i].M33 = 0;
                        }
                        else
                        {
                            A[i].M11 = 1;
                            A[i].M12 = 0;
                            A[i].M13 = 0;
                            A[i].M21 = 0;
                            A[i].M22 = 1;
                            A[i].M23 = 0;
                            A[i].M31 = 0;
                            A[i].M32 = 0;
                            A[i].M33 = 1;
                        }
                    }

                    Vector3 OiA = searchPoint;
                    double[] angles = new double[ELEMENTS_COUNT];
                    Vector3 OiO7 = elements[ELEMENTS_COUNT - 1].endPoint;
                    for (int i = 0; i < ELEMENTS_COUNT-1; i++)
                    {

                        Vector3 OiA_1 = Multiply((OiA - OiOiplus1[i]), A[i]);
                        OiA = OiA_1;
                        Vector3 OiO7_1 = Multiply((OiO7 - OiOiplus1[i]), A[i]);
                        OiO7 = OiO7_1;

                        OiA_1.Z = 0;
                        OiO7_1.Z = 0;
                        if (OiA_1.Length() != 0 && OiO7_1.Length() != 0)
                        {

                            float scalar = Vector3.Dot(OiA_1, OiO7_1);
                            float aProjLength = OiA_1.Length();
                            float bProjLength = OiO7_1.Length();
                            float cos = scalar / aProjLength / bProjLength;
                            Vector3 newVector = Vector3.Cross(OiA_1, OiO7_1);
                            if (cos > 1) cos = 1;
                            else if (cos < -1) cos = -1;
                            angles[i] = Math.Sign(newVector.Z) * Math.Acos(cos) * (180 / Math.PI);
                        }
                        else angles[i] = 0;
                        numerics[i].Invoke(new Action(() => numerics[i].Value = (int)angles[i]));
                        OiA =Multiply(OiA, RotationMatrix(angles[i]));
                    }




                    for (int i = 0; i < ELEMENTS_COUNT-1; i++)
                    {
                        if (angles[i] == 0) continue;
                        Rotate(i, -angles[i]);
                        threads[i].Join();
                    }


                    /////////////////////////////////////////////////////////////////



                    form.SetLoopCountLbl(++loopCount);
                }
                while (!brush.IsTouch(item)&&brush.CanGrab())
                {

                    Rotate(ELEMENTS_COUNT, ROTATE_FREQUENCY);
                    threads[ELEMENTS_COUNT].Join();

                }
            }
        }

        public void GoToItem(Item item, ref List<NumericUpDown> numerics)
        {
            if ((item.centerPoint-elements[0].endPoint).Length() - maxLength > 0)
            {
                MessageBox.Show("Точка вне зоны досегаемости!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            threads[ELEMENTS_COUNT+1] = new Thread(new ParameterizedThreadStart(ParalelFind));
            threads[ELEMENTS_COUNT+1].Start((item, numerics));
        }
    }
}
