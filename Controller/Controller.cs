﻿using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Threading;

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
        Mutex drawMutex;
        Mutex paralelMutex;
        object paralelLock;
        Thread[] threads;
        int loopCount;
        double controllerLength;

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
            const float offset = 1f;
            x = z = 0;
            y = 2.5f;

            for (int i = 0; i < ELEMENTS_COUNT; i++)
            {
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
            }

            brush = new Brush(device, new Vector3(x, y, z), new Vector3(x + (ELEMENTS_COUNT % 3 == 0 ? offset / 2 : 0),
                                   y + (ELEMENTS_COUNT % 3 == 1 ? offset / 2 : 0),
                                   z + (ELEMENTS_COUNT % 3 == 2 ? offset / 2 : 0)));

            for (int i = elements.Length - 1; i >= 0; i--)
            {
                Vector3 cylinderEndPoint = new Vector3(x, y, z);
                if (i % 3 == 0)
                {
                    y -= offset;
                }
                else if (i % 3 == 1)
                {
                    z -= offset;
                }
                else
                {
                    x -= offset;
                }
                if(i==0) elements[i] = new Shoulder(device, new Vector3(), cylinderEndPoint, elements[i + 1]);
                else if (i == elements.Length - 1) elements[i] = new Shoulder(device, new Vector3(x, y, z), cylinderEndPoint, brush);
                else elements[i] = new Shoulder(device, new Vector3(x, y, z), cylinderEndPoint, elements[i + 1]);
            }
            controllerLength = (brush.endPoint - elements[0].endPoint).Length();
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

        private void ParalelFind(object tuple)
        {
            (Item, List<NumericUpDown>) typedTuple = ((Item, List<NumericUpDown>))tuple;
            lock (paralelLock)
            {
                loopCount = 0;
                Item item = typedTuple.Item1;
                List<NumericUpDown> numerics = typedTuple.Item2;


                while (!GetEndPoint().Equals(item.centerPoint))
                {
                    for (int i = 0; i < ELEMENTS_COUNT - 1; i++)
                    {

                        Vector3 endPoint = GetEndPoint();
                        double angle = elements[i].GoToPoint(item.centerPoint, endPoint);
                        
                        numerics[i].Invoke(new Action(() => numerics[i].Value = (int)angle));
                        
                        if (angle == 0) 
                            continue;
                        Rotate(i, angle);
                        threads[i].Join();
                    }

                    form.SetLoopCountLbl(++loopCount);

                    }

                    ///////////////////////////////////////////////////////////////////
                    ///


                    //Vector3 searchPoint = item.centerPoint;
                    //Vector3 endPoint = GetEndPoint();
                    //double[] angles = new double[ELEMENTS_COUNT - 1];
                    //while (!Vector3Extencion.Compare(endPoint, searchPoint))
                    //{
                    //    for (int i = 0; i < ELEMENTS_COUNT - 1; i++)
                    //    {
                    //        double angle = elements[i].GoToPoint(searchPoint, endPoint);
                    //        if (angle == 0) continue;

                    //        Space space = new Space(elements[i].startPoint, elements[i].endPoint);
                    //        searchPoint = space.RotatePoints(new List<Vector3>() { searchPoint }, -angle)[0];
                    //        angles[i] += angle;
                    //    }

                    //    form.SetLoopCountLbl(++loopCount);

                    //}
                    //for (int i = 0; i < ELEMENTS_COUNT - 1; i++)
                    //{
                    //    numerics[i].Invoke(new Action(() => numerics[i].Value = (int)angles[i]));
                    //    Rotate(i, angles[i]);
                    //}

                    ///////////////////////////////////////////////////////////////////////

                    while (!brush.IsTouch(item))
                {

                    Rotate(ELEMENTS_COUNT, ROTATE_FREQUENCY);
                    threads[ELEMENTS_COUNT].Join();

                }
            }
        }

        public void GoToItem(Item item, ref List<NumericUpDown> numerics)
        {
            if ((item.centerPoint-elements[0].endPoint).Length() - controllerLength > 0)
            {
                MessageBox.Show("Точка вне зоны досегаемости!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            threads[ELEMENTS_COUNT+1] = new Thread(new ParameterizedThreadStart(ParalelFind));
            threads[ELEMENTS_COUNT+1].Start((item, numerics));
        }
    }
}
