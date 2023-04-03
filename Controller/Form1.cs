using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Controller
{
    public partial class Form1 : Form
    {
        Device d3d;
        Controller controller;
        Item item;
        List<NumericUpDown> numericUpDownList;
        CustomVertex.PositionColored[] gridVertices;
        const float itemRadius = 0.1f;
        const int numericLimit = 180;

        public Form1()
        {
            InitializeComponent();
            SetNumElementsList();
            d3d = null;
            item = null;
        }

        private void SetNumElementsList()
        {
            numericUpDownList = new List<NumericUpDown>();
            numericUpDownList.Add(numElement1);
            numericUpDownList.Add(numElement2);
            numericUpDownList.Add(numElement3);
            numericUpDownList.Add(numElement4);
            numericUpDownList.Add(numElement5);

            foreach (NumericUpDown numericUpDown in numericUpDownList)
            {
                numericUpDown.Minimum = -numericLimit;
                numericUpDown.Maximum = numericLimit;
            }
            numBrush.Minimum = -numericLimit;
            numBrush.Maximum = numericLimit;
        }

        private void ChangeEnabledNumElements()
        {
            foreach(NumericUpDown numElement in numericUpDownList)
            {
                numElement.Enabled = !numElement.Enabled;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StartSetup(pictureBox1);
            SetupProekcii();
            SetController();
            SetAxis();
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
            if (d3d != null)
            {
                d3d.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.Azure, 1.0f, 0);
                d3d.BeginScene();

                DrawAxis();
                DrawItem();
                DrawElements();

                d3d.EndScene();
                d3d.Present();//Показываем содержимое дублирующего буфера
            }
        }

        private void DrawElements()
        {
            controller.DrawElements();
        }

        private void DrawItem()
        {
            if (item != null)
            {
                item.Draw();
            }
        }

        private void SetController()
        {
            controller = new Controller(this, d3d);
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

        private void SetAxis()
        {
            // Создаем вершины сетки координат
            gridVertices = new CustomVertex.PositionColored[6];

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

        }

        private void DrawAxis()
        {
            // Отключаем использование текущего материала
            d3d.Material = new Material();
            // Рисуем вершины сетки координат
            d3d.VertexFormat = CustomVertex.PositionColored.Format;
            d3d.Transform.World = Matrix.Translation(new Vector3());
            // Создаем вершины сетки координат
            d3d.DrawUserPrimitives(PrimitiveType.LineList, gridVertices.Length, gridVertices);
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

        private void grabButton_Click(object sender, EventArgs e)
        {
            RotateButton((int)numBrush.Value, 6);
        }

        private void RotateButton(double angle, int index)
        {
            controller.Rotate(index, angle);
        }

        private void goToPointButton_Click(object sender, EventArgs e)
        {
            goToPointButton.Visible = false;
            pauseButton.Visible = true;
            controller.GoToPoint(item.centerPoint, ref numericUpDownList);
            Thread t = new Thread(new ThreadStart(CheckStop));
            t.Start();
        }
        private void CheckStop()
        {
            while (controller.IsWork()) ;
            goToPointButton.Visible = true;
            pauseButton.Visible = false;
            ChangeEnabledNumElements();
            Thread t = Thread.CurrentThread;
            t.Abort();
        }
         

        private void resumeButton_Click(object sender, EventArgs e)
        {
            resumeButton.Visible = false;
            pauseButton.Visible = true;
            controller.ResumeThread();
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            resumeButton.Visible = true;
            pauseButton.Visible = false;
            controller.StopThread();
        }

        private void setPointButton_Click(object sender, EventArgs e)
        {
            Vector3 point = new Vector3(Convert.ToSingle(xPointTxt.Text), Convert.ToSingle(yPointTxt.Text), Convert.ToSingle(zPointTxt.Text));
            if (item == null)
                this.item = new Item(d3d, point, itemRadius);
            else
                item.ChangeCenter(point);
            Invalidate();
        }

        private void xPointTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            checkCharecter(sender, e);
        }

        private void yPointTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            checkCharecter(sender, e);
        }

        private void zPointTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            checkCharecter(sender, e);
        }

        private void checkCharecter(object sender,KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != ',')&& (e.KeyChar != '-'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '-') && ((sender as TextBox).Text.IndexOf('-') > -1))
            {
                e.Handled = true;
            }
        }

        
    }
}
