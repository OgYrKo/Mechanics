using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Controller
{
    public partial class Form1 : Form
    {
        private Device device;
        private CartesianCoordinates axes;

        public Form1()
        {
            InitializeComponent();
            InitializeDevice();
            InitializeAxes();
        }

        private void InitializeDevice()
        {
            PresentParameters presentParams = new PresentParameters();
            presentParams.Windowed = true;
            presentParams.SwapEffect = SwapEffect.Discard;

            device = new Device(0, DeviceType.Hardware, this, CreateFlags.HardwareVertexProcessing, presentParams);
        }

        private void InitializeAxes()
        {
            axes = new CartesianCoordinates(device);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.Black, 1.0f, 0);
            device.BeginScene();

            Matrix worldMatrix = Matrix.Identity;
            Matrix viewMatrix = Matrix.LookAtLH(new Vector3(0, 0, -10), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            Matrix projectionMatrix = Matrix.PerspectiveFovLH((float)Math.PI / 4, ClientSize.Width / (float)ClientSize.Height, 1.0f, 100.0f);

            axes.Draw(worldMatrix, viewMatrix, projectionMatrix);

            device.EndScene();
            device.Present();
        }

    }
}
