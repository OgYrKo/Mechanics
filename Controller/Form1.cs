using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Controller
{
    public partial class Form1 : Form
    {

        Device d3d;

        public Form1()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque, true);
            InitializeComponent();
            // Включение обработки сообщений от клавиатуры в окне
            this.KeyPreview = true;
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
                Close(); // Закрываем окно
            }
            
        }
            private void Form1_Paint(object sender, PaintEventArgs e)
            {
                d3d.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.Green, 1.0f, 0);
                SetupCamera();
                //окружность
                float y = 0;
                CustomVertex.PositionColored[] verts = new
                CustomVertex.PositionColored[400];
                for (int i = 0; i < 400; i++)
                {
                    float x = -Convert.ToSingle(Math.Cos(Math.PI / 200 * i));
                    float z = Convert.ToSingle(Math.Sin(Math.PI / 200 * i));
                    verts[i].Position = new Vector3(x, y, z);
                    verts[i].Color = System.Drawing.Color.AliceBlue.ToArgb();
                }
                d3d.BeginScene();
                d3d.VertexFormat = CustomVertex.PositionColored.Format;
                d3d.DrawUserPrimitives(PrimitiveType.LineStrip, 399, verts);
                d3d.EndScene();
                d3d.Present();
                this.Invalidate();
            }
            private void SetupCamera()
            {
                d3d.Transform.Projection = Matrix.PerspectiveFovLH((float)Math.PI / 4,
                this.Width / this.Height, 1.0f, 100.0f);
                d3d.Transform.View = Matrix.LookAtLH(new Vector3(0, 3, 5.0f),
                new Vector3(), new Vector3(0, 1, 0));
                d3d.RenderState.Lighting = false;
        }

    }
}
