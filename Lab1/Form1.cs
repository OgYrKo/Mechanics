using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lab1
{
    using Coordinate = Double;
    using Speed = Double;
    using Degree = Double;
    using Radian = Double;
    using Time = Double;

    public partial class Form1 : Form
    {
        const Time T_OFFSET = 0.05;
        const int numericLimit = 15000;
        private bool checkedBoxChengedFlag=false;
        private PointMovement pointMovement;
        double[] x, y;
        Timer timer;
        int tickCounter;

        public Form1()
        {
            InitializeComponent();
            SetLimits();
            SetValues();
        }

        private void SetLimits()
        {
            numX0.Minimum = numY0.Minimum = numXn.Minimum = numYn.Minimum = numV0.Minimum = numAlpha.Minimum = -numericLimit;
            numX0.Maximum = numY0.Maximum = numXn.Maximum = numYn.Maximum = numV0.Maximum = numAlpha.Maximum = numericLimit;
        }

        private void SetValues()
        {
            //SetValuesDschyhov();
            numX0.Value = 0;
            numY0.Value = -10;
            numXn.Value = 10000;
            numYn.Value = 300;
            numV0.Value = 1500;

            //checkBoxAlpha.Checked = true;
            //numAlpha.Value = 2.96819369M;
        }

        private void SetValuesDschyhov()
        {
            numX0.Value = 0;
            numY0.Value = 0;
            numXn.Value = 7500;
            numYn.Value = 0;

            checkBoxV0.Checked = false;
            checkBoxAlpha.Checked = true;
            numAlpha.Value = 30;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            numAlpha.Visible = !numAlpha.Visible;
            if (!checkBoxV0.Checked && !checkedBoxChengedFlag)
            {
                checkedBoxChengedFlag = true;
                checkBoxV0.Checked = true;
                numV0.Visible = true;
                checkedBoxChengedFlag = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            numV0.Visible = !numV0.Visible;
            if (!checkBoxAlpha.Checked && !checkedBoxChengedFlag)
            {
                checkedBoxChengedFlag = true;
                checkBoxAlpha.Checked = true;
                numAlpha.Visible = true;
                checkedBoxChengedFlag = false;
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            stopButton.Visible = true;
            startButton.Visible = false;
            try
            {
                PointMovementInit();
                SetLabels();
                SetPoints();
                SetGraphic();
                DrawMovement();
            }
            catch (Exception exeption)
            {
                StopGraph();
                MessageBox.Show(exeption.Message);
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            StopGraph();
        }

        private void StopGraph()
        {
            stopButton.Visible = false;
            startButton.Visible = true;
            if (timer != null) timer.Stop();
        }

        private void SetPoints()
        {
            Time tMax = pointMovement.GetTByX(pointMovement.pMax.X);
            int pointsCount = (int)(tMax / T_OFFSET);
            x =new Coordinate[pointsCount];
            y =new Coordinate[pointsCount];
            double t = 0;
            for (int i=0;i<pointsCount;i++)
            {
                Point p=pointMovement.GetPointByT(t);
                x[i] = p.X;
                y[i] = p.Y;
                t += T_OFFSET;
            }
        }

        private void SetGraphic()
        {
            Chart_with_Data.Series[0].Points.Clear(); // Очистка точек
            Chart_with_Data.Series[1].Points.Clear();

            // Chart_with_Data.Titles[0].Text = title;
            Chart_with_Data.ChartAreas[0].AxisX.Interval = 2000;
            Chart_with_Data.ChartAreas[0].AxisY.Interval = 100;

            Series S1 = new Series(); 
            double f_min = double.MaxValue, f_max = double.MinValue;
            for (int i = 0; i <= x.Length - 1; i++)
            {
                S1.Points.AddXY(x[i], y[i]);
                f_min = Math.Min(f_min, y[i]);
                f_max = Math.Max(f_max, y[i]);
            }

            S1.ChartType = SeriesChartType.Point;
            S1.MarkerStyle = MarkerStyle.Circle;
            S1.MarkerSize = 5;
            S1.MarkerColor = Color.Red;
            S1.MarkerBorderColor = Color.DarkRed;
            Chart_with_Data.Series[0] = S1;

            Chart_with_Data.ChartAreas[0].AxisX.Minimum = Math.Floor(x[0]);
            Chart_with_Data.ChartAreas[0].AxisX.Maximum = Math.Ceiling(x[x.Length - 1]);
            Chart_with_Data.ChartAreas[0].AxisY.Minimum = Math.Floor(f_min);
            Chart_with_Data.ChartAreas[0].AxisY.Maximum = Math.Ceiling(f_max);

            //////////////// Graph 2
            Series S2 = new Series();
            S2.ChartType = SeriesChartType.Point;
            S2.MarkerStyle = MarkerStyle.Circle;
            S2.MarkerSize = 9;
            S2.MarkerColor = Color.Yellow;
            S2.MarkerBorderColor = Color.Yellow;
            Chart_with_Data.Series[1] = S2;

            Chart_with_Data.Invalidate();
        }

        private void TimerEvent(Object myObject, EventArgs myEventArgs)
        {
            Point p = new Point() { X = x[tickCounter], Y = y[tickCounter] };
            DrawPoint(p);
            if(tickCounter%(0.1/T_OFFSET)==0)
                lblTimeValue.Text = Convert.ToString(tickCounter * T_OFFSET);
            if (tickCounter == x.Length - 1)
            {
                StopGraph();
            }

            tickCounter++;
        }

        private void DrawMovement()
        {
            timer = new Timer();
            tickCounter = 0;
            timer.Tick += new EventHandler(TimerEvent);
            timer.Interval = (int)(T_OFFSET * 1000);
            timer.Start();
        }

        private void DrawPoint(in Point p)
        {
            Chart_with_Data.Series[1].Points.Clear();
            Chart_with_Data.Series[1].Points.AddXY(p.X, p.Y);
            Chart_with_Data.Invalidate();
        }

        private void PointMovementInit()
        {

            Point p0 = new Point() { X = (Coordinate)numX0.Value, Y = (Coordinate)numY0.Value };
            Point pN = new Point() { X = (Coordinate)numXn.Value, Y = (Coordinate)numYn.Value };
            if (checkBoxAlpha.Checked && checkBoxV0.Checked)
            {
                pointMovement = new PointMovement(p0, pN, (Speed)numV0.Value, (Degree)numAlpha.Value);
            }
            else if (checkBoxAlpha.Checked)
            {
                pointMovement = new PointMovement((Degree)numAlpha.Value, p0, pN);
            }
            else
            {
                pointMovement = new PointMovement(p0, pN, (Speed)numV0.Value);
            }
        }

        private void SetLabels()
        {
            if (!checkBoxAlpha.Checked)
            {
                lblAlphaValue.Visible = true;
                lblAlphaValue.Text = Convert.ToString(pointMovement.GetAlpha());
            }
            else if (!checkBoxV0.Checked)
            {
                lblV0Value.Visible = true;
                lblV0Value.Text = Convert.ToString(pointMovement.GetV0());
            }
        }

        
    }
}
