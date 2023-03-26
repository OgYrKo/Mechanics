using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using Font = Microsoft.DirectX.Direct3D.Font;

namespace Controller
{
    public partial class Form1 : Form
    {
        Device d3d;
        Element[] elements;
        const int numericLimit = 90;

        public Form1()
        {
            InitializeComponent();
            SetLimits();
            d3d = null;
        }

        private void SetLimits()
        {
            numElement1.Minimum = numElement2.Minimum = numElement3.Minimum = numElement4.Minimum = numElement5.Minimum = -numericLimit;
            numElement1.Maximum = numElement2.Maximum = numElement3.Maximum = numElement4.Maximum = numElement5.Maximum = numericLimit;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StartSetup(pictureBox1);
            SetupProekcii();
            SetElements();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Vector3 translationVector = new Vector3();
            float rotationX = 0, rotationY = 0;
            // Перемещение камеры вперед и назад
            switch (e.KeyCode) 
            {
                case Keys.Up:
                    translationVector.Y = -0.1f;
                    break;
                case Keys.Down:
                    translationVector.Y = 0.1f;
                    break;
                case Keys.Left:
                    translationVector.X = 0.1f;
                    break;
                case Keys.Right:
                    translationVector.X = -0.1f;
                    break;
                case Keys.PageUp:
                    translationVector.Z = 0.1f;
                    break;
                case Keys.PageDown:
                    translationVector.Z = -0.1f;
                    break;
                case Keys.A:
                    rotationY = 0.1f;
                    break;
                case Keys.D:
                    rotationY = -0.1f;
                    break;
                case Keys.W:
                    rotationX = 0.1f;
                    break;
                case Keys.S:
                    rotationX = -0.1f;
                    break;
            }
            d3d.Transform.View *= Matrix.Translation(translationVector);
            d3d.Transform.View *= Matrix.RotationX(rotationX);
            d3d.Transform.View *= Matrix.RotationY(rotationY);
            
            Invalidate(); // Перерисовываем окно
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            d3d.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.Azure, 1.0f, 0);
            d3d.BeginScene();

            DrawAxis();
            DrawElements();

            d3d.EndScene();
            d3d.Present();//Показываем содержимое дублирующего буфера
        }

        private void DrawElements()
        {
            foreach(Element element in elements)
            {
                element.DrawElement();
            }
        }

        private void SetElements()
        {
            elements = new Element[6];
            float x, y, z;
            const float offset = -1f;
            x = y = z = 2f;

            for(int i = elements.Length - 1; i >= 0; i--)
            {
                Vector3 endPoint = new Vector3(x, y, z);
                if (i % 3 == 0)
                {
                    y += offset;
                }
                else if(i%3 == 1)
                {
                    z += offset;
                }
                else
                {
                    x += offset;
                }
                if(i== elements.Length - 1) elements[i]=new Element(d3d, new Vector3(x, y, z), endPoint);
                else elements[i] = new Element(d3d, new Vector3(x, y, z), endPoint, elements[i+1]);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="renderWindow">like d3d or device</param>
        private void StartSetup(Control renderWindow)
        {
            try
            {
                PresentParameters d3dpp = new PresentParameters();
                d3dpp.BackBufferCount = 1;
                d3dpp.SwapEffect = SwapEffect.Discard;
                d3dpp.Windowed = true; // Выводим графику в окно
                d3dpp.MultiSample = MultiSampleType.None; // Выключаем антиалиасинг
                d3dpp.EnableAutoDepthStencil = true; // Разрешаем создание z-буфера
                d3dpp.AutoDepthStencilFormat = DepthFormat.D16; // Z-буфер в 16 бит
                d3d = new Device(0, // D3D_ADAPTER_DEFAULT - видеоадаптер по
                                    // умолчанию
                DeviceType.Hardware, // Тип устройства - аппаратный ускоритель
                renderWindow, // Окно для вывода графики
                CreateFlags.SoftwareVertexProcessing, d3dpp);
            }
            catch (Exception exc)
            {
                MessageBox.Show(this, exc.Message, "Ошибка инициализации");
                Close(); // Закрываем окно
            }
        }

        private void SetupProekcii()
        {
            SetupLight();
            SetupCamera();
        }

        private void SetupLight()
        {
            // Устанавливаем параметры источника освещения
            // Устанавливаем параметры источника освещения
            d3d.Lights[0].Enabled = true; // Включаем нулевой источник освещения
            d3d.Lights[0].Diffuse = Color.White; // Цвет источника освещения
            d3d.Lights[0].Direction = new Vector3();
            d3d.Lights[0].Position = new Vector3(0, 0, -5f); // Задаем координаты
        }

        private void SetupCamera()
        {
            d3d.Transform.Projection = Matrix.PerspectiveFovLH(
                (float)Math.PI / 4,//высота видимости
                this.Width / this.Height, //ширина видимости
                1.0f, 100.0f);//передний и задний план
            d3d.Transform.View = Matrix.LookAtLH(new Vector3(0, 0, -5f), new Vector3(), new Vector3(0, 1, 0));
        }

        private void DrawAxis()
        {
            // Отключаем использование текущего материала
            d3d.Material = new Material();

            // Создаем вершины сетки координат
            CustomVertex.PositionColored[] gridVertices = new CustomVertex.PositionColored[8];

            int axisLen = 10;

            //axis y
            gridVertices[0] = new CustomVertex.PositionColored(new Vector3(0, -axisLen, 0), Color.White.ToArgb());
            gridVertices[1] = new CustomVertex.PositionColored(new Vector3(0, axisLen, 0), Color.White.ToArgb());
            //axis x
            gridVertices[2] = new CustomVertex.PositionColored(new Vector3(axisLen, 0, 0), Color.White.ToArgb());
            gridVertices[3] = new CustomVertex.PositionColored(new Vector3(-axisLen, 0, 0), Color.White.ToArgb());
            //axis z
            gridVertices[4] = new CustomVertex.PositionColored(new Vector3(0, 0, axisLen), Color.White.ToArgb());
            gridVertices[5] = new CustomVertex.PositionColored(new Vector3(0, 0, -axisLen), Color.White.ToArgb());


            // Рисуем вершины сетки координат
            d3d.VertexFormat = CustomVertex.PositionColored.Format;
            d3d.Transform.World = Matrix.Translation(new Vector3());
            // Создаем вершины сетки координат
            d3d.DrawUserPrimitives(PrimitiveType.LineList, 8, gridVertices);
        }

        private void element1RotateBtn_Click(object sender, EventArgs e)
        {
            RotateButton((int)numElement1.Value, 0);
        }

        private void element2RotateBtn_Click(object sender, EventArgs e)
        {
            RotateButton((int)numElement2.Value, 1);
        }

        private void element3RotateBtn_Click(object sender, EventArgs e)
        {
            RotateButton((int)numElement3.Value, 2);
        }

        private void element4RotateBtn_Click(object sender, EventArgs e)
        {
            RotateButton((int)numElement4.Value, 3);
        }

        private void element5RotateBtn_Click(object sender, EventArgs e)
        {
            RotateButton((int)numElement5.Value, 4);
        }

        private void RotateButton(int degree,int index)
        {
            elements[index].Rotate(degree);
            Invalidate();
        }
    }
}
