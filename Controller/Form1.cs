using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Font = Microsoft.DirectX.Direct3D.Font;

namespace Controller
{
    public partial class Form1 : Form
    {
        Device d3d;
        Element elementCylinder;

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
            DrawElements();
            DrawAxis();
            d3d.EndScene();
            d3d.Present();//Показываем содержимое дублирующего буфера
        }

        private void DrawElements()
        {
            elementCylinder.DrawElement();
        }

        private void SetElements()
        {
            elementCylinder = new Element(d3d, new Vector3(0, 0, 5f), new Vector3(0, 1, 5f));
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
            d3d.Lights[0].Position = new Vector3(0, 0, -10); // Задаем координаты
        }

        private void SetupCamera()
        {
            d3d.Transform.Projection = Matrix.PerspectiveFovLH(
                (float)Math.PI / 4,//высота видимости
                this.Width / this.Height, //ширина видимости
                1.0f, 50.0f);//передний и задний план
            d3d.Transform.View = Matrix.LookAtLH(new Vector3(0, 0, -5f), new Vector3(), new Vector3(0, 1, 0));
        }

        private void DrawAxis()
        {
            // Отключаем использование текущего материала
            d3d.Material = new Material();

            // Создаем вершины сетки координат
            CustomVertex.PositionColored[] gridVertices = new CustomVertex.PositionColored[10];

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


            //axis y
            //gridVertices[6] = new CustomVertex.PositionColored(new Vector3(0, 1, -axisLen), Color.White.ToArgb());
            //gridVertices[7] = new CustomVertex.PositionColored(new Vector3(0, 1, axisLen), Color.White.ToArgb());
            //axis x
            gridVertices[8] = new CustomVertex.PositionColored(new Vector3(1, 0, axisLen), Color.White.ToArgb());
            gridVertices[9] = new CustomVertex.PositionColored(new Vector3(1, 0, -axisLen), Color.White.ToArgb());


            // Рисуем вершины сетки координат
            d3d.VertexFormat = CustomVertex.PositionColored.Format;
            d3d.DrawUserPrimitives(PrimitiveType.LineList, 10, gridVertices);
        }
    }
}
