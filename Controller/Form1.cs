using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Controller
{
    public partial class Form1 : Form
    {
        Device d3d;
        float angle = 0;
        public Form1()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque, true);
            InitializeComponent();
            d3d = null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                PresentParameters d3dpp = new PresentParameters();
                d3dpp.BackBufferCount = 1;
                d3dpp.SwapEffect = SwapEffect.Discard;
                d3dpp.Windowed = true;
                d3dpp.MultiSample = MultiSampleType.None;
                d3dpp.EnableAutoDepthStencil = true;
                d3dpp.AutoDepthStencilFormat = DepthFormat.D16;
                d3d = new Device(0, DeviceType.Hardware, this,
                CreateFlags.SoftwareVertexProcessing, d3dpp);
            }
            catch (Exception exc)
            {
                MessageBox.Show(this, exc.Message, "Ошибка инициализации");
                Close(); // Закрываем окно }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Проверка, нажата ли клавиша
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right ||
                e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                // Получение направления перемещения камеры
                Vector3 direction;
                if (e.KeyCode == Keys.Left)
                    direction.X = -1.0f;
                else if (e.KeyCode == Keys.Right)
                    direction.X = 1.0f;
                else if (e.KeyCode == Keys.Up)
                    direction.Z = 1.0f;
                else direction.Z = -1.0f;

                // Перемещение камеры на заданное расстояние в указанном направлении
                //camera.Position += direction * moveSpeed;

                //// Обновление матрицы вида камеры
                //d3d.Transform.View = Matrix.LookAtLH(camera.Position, camera.Target, camera.UpVector);
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
            {
                d3d.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.Green, 1.0f, 0);
                SetupCamera();
                for (double teta = 0; teta <= 2 * Math.PI; teta += 0.1)
                {
                    List<CustomVertex.PositionColored> myList =
                    new List<CustomVertex.PositionColored>();
                    for (double fi = 0; fi <= 2 * Math.PI; fi += 0.01)
                    {
                        float x = Convert.ToSingle(Math.Cos(fi) * Math.Cos(teta));
                        float y = Convert.ToSingle(Math.Cos(fi) * Math.Sin(teta));
                        float z = Convert.ToSingle(Math.Sin(fi));
                        CustomVertex.PositionColored one =
                        new CustomVertex.PositionColored();
                        one.Position = new Vector3(x, y, z);
                        myList.Add(one);
                    }
                    d3d.BeginScene();
                    d3d.VertexFormat = CustomVertex.PositionColored.Format;
                    CustomVertex.PositionColored[] verts = new CustomVertex.PositionColored[myList.Count];
                    for (int i = 0; i < myList.Count; i++)
                        verts[i] = myList[i];
                    d3d.DrawUserPrimitives(PrimitiveType.LineStrip, verts.Length - 1,
                    verts);
                    d3d.EndScene();
                }
                d3d.Present();
                this.Invalidate();
            }

            private void SetupCamera()
            {
                d3d.Transform.Projection = Matrix.PerspectiveFovLH((float)Math.PI / 4, this.Width / this.Height, 1.0f, 100.0f);
                d3d.Transform.View = Matrix.LookAtLH(new Vector3(0, 3, 5.0f), new Vector3(), new Vector3(0, 1, 0));
                d3d.RenderState.Lighting = false;
                d3d.Transform.World = Matrix.RotationAxis(new Vector3(1, 0, 1), angle / (float)Math.PI);
                angle += 0.1f;
            }
     } 
}