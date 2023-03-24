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
        Element elementCylinder;
        Element elementCylinder1;

        public Form1()
        {
            InitializeComponent();
            d3d = null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StartSetup();
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
            //elementCylinder.DrawElement();
            //elementCylinder1.DrawElement();
            foreach(Element element in elements)
            {
                element.DrawElement();
            }
        }

        private void SetElements()
        {
            elements = new Element[6];
            float x, y, z;
            const float offset = 1f;
            x = y = z = 0;

            for(int i=0; i < 6; i++)
            {
                Vector3 startPoint = new Vector3(x, y, z);
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

                elements[i]=new Element(d3d, startPoint, new Vector3(x, y, z), Color.FromArgb(100,150,i*30+15));
            }

            //elements[0] = new Element(d3d, new Vector3(0, 0, 0), new Vector3(0, 1f, 0), Color.Green);
            //elements[1] = new Element(d3d, new Vector3(0, 1f, 0), new Vector3(0, 1f, 1f), Color.Blue);
            //elements[2] = new Element(d3d, new Vector3(0, 1f, 1f), new Vector3(1f, 1f, 1f), Color.Red);
            //elements[3] = new Element(d3d, new Vector3(1f, 1f, 1f), new Vector3(0, 1f, 0), Color.Yellow);
            //elements[4] = new Element(d3d, new Vector3(0, 0, 0), new Vector3(0, 1f, 0), Color.Purple);
            //elements[5] = new Element(d3d, new Vector3(0, 0, 0), new Vector3(0, 1f, 0), Color.Orange);

            //elementCylinder = new Element(d3d, new Vector3(0, 0, 0), new Vector3(0, 1f, 0),Color.Green);
            //elementCylinder1 = new Element(d3d, new Vector3(0, 1f,0), new Vector3(0, 1f, 1f),Color.Blue);
        }

        private void StartSetup()
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
                this, // Окно для вывода графики
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


            //axis x
            gridVertices[6] = new CustomVertex.PositionColored(new Vector3(1, 0, axisLen), Color.White.ToArgb());
            gridVertices[7] = new CustomVertex.PositionColored(new Vector3(1, 0, -axisLen), Color.White.ToArgb());


            // Рисуем вершины сетки координат
            d3d.VertexFormat = CustomVertex.PositionColored.Format;
            d3d.Transform.World = Matrix.Translation(new Vector3());
            // Создаем вершины сетки координат
            d3d.DrawUserPrimitives(PrimitiveType.LineList, 8, gridVertices);
        }
    }
}
