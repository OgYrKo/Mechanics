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

namespace Controller
{
    internal class Controller
    {
        private Shoulder[] elements;
        private Brush brush;
        Form form;
        Device device;
        const int ELEMENTS_COUNT = 7;
        const int ROTATE_FREQUENCY = 1;//на сколько градусов поворачивать за 1 раз
        const int ROTATE_FREQUENCY_TIME = 50;//раз в сколько времени поворачивать (ms)
        Mutex drawMutex;
        List<Thread> threads;

        public Controller(Form form, Device device)
        {
            elements = new Shoulder[ELEMENTS_COUNT];
            threads = new List<Thread>();
            this.form = form;
            this.device = device;
            MutexInit();
            SetStartPosition();
        }

        private void SetStartPosition()
        {
            float x, y, z;
            const float offset = 1f;
            x = y = z = 0;

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
            //brush = new Brush(device, new Vector3(x, y, z), new Vector3(x + offset/2, y, z));

            for (int i = elements.Length - 1; i >= 0; i--)
            {
                Vector3 endPoint = new Vector3(x, y, z);
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
                if (i == elements.Length - 1) elements[i] = new Shoulder(device, new Vector3(x, y, z), endPoint, brush);
                else elements[i] = new Shoulder(device, new Vector3(x, y, z), endPoint, elements[i + 1]);
            }
        }

        private void MutexInit()
        {
            drawMutex = new Mutex();
        }

        public bool IsWork()
        {
            for(int i = 0; i < threads.Count; )
            {
                if (!threads[i].IsAlive) threads.Remove(threads[i]);
                else i++;
            }
            return threads.Count > 0;
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

        public void Rotate(int index,double angle)
        {
            Thread thread = new Thread(new ParameterizedThreadStart(Rotate));
            threads.Add(thread);
            thread.Start((index, angle));
            thread.Join();
        }

        public void StopThread()
        {
            if (threads == null) return;
            foreach (Thread t in threads)
            {
                if(t.IsAlive) t.Suspend();
            }
        }

        public void ResumeThread()
        {
            if (threads == null) return;
            foreach (Thread t in threads)
            {
                if (t.IsAlive) t.Resume();
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
                element.Rotate(angleSign*ROTATE_FREQUENCY);
                form.Invalidate();
                Thread.Sleep(ROTATE_FREQUENCY_TIME);
            }

            //если угол имел дробную часть, то доворачиваем на нее
            double rest = Math.Abs(angle) - rotationCount;
            if (rest > 0)
            {
                element.Rotate(rest*angleSign);
                form.Invalidate();
            }
        }


        public void GoToPoint(Vector3 point,ref List<NumericUpDown> numerics)
        {
            double[] angles = new double[ELEMENTS_COUNT];
            //for (int i = 0; i < ELEMENTS_COUNT; i++)
            //{
            //    angles[i] = elements[i].GoToPoint(point);
            //    if (i < numerics.Count)
            //    {
            //        numerics[i].Value = (int)angles[i];
            //        numerics[i].Enabled = false;
            //    }
            //    if (angles[i] == 0) continue;
            //    Rotate(i, angles[i]);
            //}


            for (int i = 0; i < ELEMENTS_COUNT; i++)
            {
                angles[i] = elements[i].GoToPoint(point);
            }
            for (int i = 0; i < ELEMENTS_COUNT && i < numerics.Count; i++)
            {
                numerics[i].Value = (int)angles[i];
                numerics[i].Enabled = false;
            }
            for (int i = 0; i < ELEMENTS_COUNT; i++)
            {
                if (angles[i] == 0) continue;
                Rotate(i, angles[i]);
            }
        }
    }
}
