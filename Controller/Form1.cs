using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Controller
{
    public partial class Form1 : Form
    {
        Device d3d;
        Mesh cylinder;
        Material cylinder_material;
        bool fl = true;

        public Form1()
        {
            InitializeComponent();
            d3d = null;
            SetStyle(ControlStyles.ResizeRedraw, true);
            cylinder = null;
        }

        private void Form1_Load(object sender, EventArgs e)
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
            cylinder = Mesh.Cylinder(d3d, 0.5f, 0.5f, 1.0f, 16, 1);
            cylinder_material = new Material();
            cylinder_material.Diffuse = Color.Yellow;
            cylinder_material.Specular = Color.White;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Перемещение камеры вперед и назад
            if (e.KeyCode == Keys.Up)
            {
                d3d.Transform.View *= Matrix.Translation(new Vector3(0, 0.1f, 0));
            }
            else if (e.KeyCode == Keys.Down)
            {
                d3d.Transform.View *= Matrix.Translation(new Vector3(0, -0.1f, 0));
            }

            // Перемещение камеры вправо и влево
            if (e.KeyCode == Keys.Right)
            {
                d3d.Transform.View *= Matrix.Translation(new Vector3(0.1f, 0, 0));
            }
            else if (e.KeyCode == Keys.Left)
            {
                d3d.Transform.View *= Matrix.Translation(new Vector3(-0.1f, 0, 0));
            }

            // Перемещение камеры вверх и вниз
            if (e.KeyCode == Keys.PageUp)
            {
                d3d.Transform.View *= Matrix.Translation(new Vector3(0, 0, 0.1f));
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                d3d.Transform.View *= Matrix.Translation(new Vector3(0, 0, -0.1f));
            }

            // Поворот камеры вокруг оси Y
            if (e.KeyCode == Keys.A)
            {
                d3d.Transform.View *= Matrix.RotationY(0.1f);
            }
            else if (e.KeyCode == Keys.D)
            {
                d3d.Transform.View *= Matrix.RotationY(-0.1f);
            }

            // Поворот камеры вокруг оси X
            if (e.KeyCode == Keys.W)
            {
                d3d.Transform.View *= Matrix.RotationX(0.1f);
            }
            else if (e.KeyCode == Keys.S)
            {
                d3d.Transform.View *= Matrix.RotationX(-0.1f);
            }

            Invalidate(); // Перерисовываем окно
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            d3d.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.Azure, 1.0f, 0);
            d3d.BeginScene();
            SetupProekcii();
            DrawAxis();
            d3d.Material = cylinder_material;
            d3d.Transform.World =
            Matrix.RotationX((float)Math.PI / 6) *
            Matrix.Translation(new Vector3(0, 0, 5f));
            cylinder.DrawSubset(0);
            d3d.EndScene();
            //Показываем содержимое дублирующего буфера
            d3d.Present();
        }

        private void SetupProekcii()
        {
            // Устанавливаем параметры источника освещения
            // Устанавливаем параметры источника освещения
            d3d.Lights[0].Enabled = true; // Включаем нулевой источник освещения
            d3d.Lights[0].Diffuse = Color.White; // Цвет источника освещения
            d3d.Lights[0].Position = new Vector3(0, 0, 0); // Задаем координаты
            d3d.Transform.Projection = Matrix.PerspectiveFovLH((float)Math.PI / 4,
            this.Width / this.Height, 1.0f, 50.0f);
        }

        private void DrawAxis()
        {
            // Отключаем использование текущего материала
            d3d.Material = new Material();

            // Создаем вершины сетки координат
            CustomVertex.PositionColored[] gridVertices = new CustomVertex.PositionColored[6];

            int axisLen = 10;

            gridVertices[0]= new CustomVertex.PositionColored(new Vector3(0, -axisLen, 0), Color.White.ToArgb());
            gridVertices[1]= new CustomVertex.PositionColored(new Vector3(0, axisLen, 0), Color.White.ToArgb());
            gridVertices[0] = new CustomVertex.PositionColored(new Vector3(axisLen, 0, 0), Color.White.ToArgb());
            gridVertices[1] = new CustomVertex.PositionColored(new Vector3(-axisLen, 0, 0), Color.White.ToArgb());
            gridVertices[0] = new CustomVertex.PositionColored(new Vector3(0, 0, axisLen), Color.White.ToArgb());
            gridVertices[1] = new CustomVertex.PositionColored(new Vector3(0, 0, -axisLen), Color.White.ToArgb());

            

            // Рисуем вершины сетки координат
            d3d.VertexFormat = CustomVertex.PositionColored.Format;
            d3d.DrawUserPrimitives(PrimitiveType.LineList, 3, gridVertices);
        }
    }
}