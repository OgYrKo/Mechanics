namespace Lab1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.lblX0 = new System.Windows.Forms.Label();
            this.lblY0 = new System.Windows.Forms.Label();
            this.lblXn = new System.Windows.Forms.Label();
            this.lblYn = new System.Windows.Forms.Label();
            this.lblAlpha = new System.Windows.Forms.Label();
            this.lblV0 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblV0Value = new System.Windows.Forms.Label();
            this.lblAlphaValue = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.numV0 = new System.Windows.Forms.NumericUpDown();
            this.numAlpha = new System.Windows.Forms.NumericUpDown();
            this.numYn = new System.Windows.Forms.NumericUpDown();
            this.numXn = new System.Windows.Forms.NumericUpDown();
            this.numY0 = new System.Windows.Forms.NumericUpDown();
            this.numX0 = new System.Windows.Forms.NumericUpDown();
            this.checkBoxV0 = new System.Windows.Forms.CheckBox();
            this.checkBoxAlpha = new System.Windows.Forms.CheckBox();
            this.Chart_with_Data = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTimeValue = new System.Windows.Forms.Label();
            this.stopButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numV0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAlpha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numYn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numXn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numX0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Chart_with_Data)).BeginInit();
            this.SuspendLayout();
            // 
            // lblX0
            // 
            this.lblX0.AutoSize = true;
            this.lblX0.Location = new System.Drawing.Point(41, 36);
            this.lblX0.Name = "lblX0";
            this.lblX0.Size = new System.Drawing.Size(26, 16);
            this.lblX0.TabIndex = 0;
            this.lblX0.Text = "x0: ";
            // 
            // lblY0
            // 
            this.lblY0.AutoSize = true;
            this.lblY0.Location = new System.Drawing.Point(212, 36);
            this.lblY0.Name = "lblY0";
            this.lblY0.Size = new System.Drawing.Size(27, 16);
            this.lblY0.TabIndex = 1;
            this.lblY0.Text = "y0: ";
            // 
            // lblXn
            // 
            this.lblXn.AutoSize = true;
            this.lblXn.Location = new System.Drawing.Point(41, 86);
            this.lblXn.Name = "lblXn";
            this.lblXn.Size = new System.Drawing.Size(26, 16);
            this.lblXn.TabIndex = 2;
            this.lblXn.Text = "xn: ";
            // 
            // lblYn
            // 
            this.lblYn.AutoSize = true;
            this.lblYn.Location = new System.Drawing.Point(212, 86);
            this.lblYn.Name = "lblYn";
            this.lblYn.Size = new System.Drawing.Size(24, 16);
            this.lblYn.TabIndex = 3;
            this.lblYn.Text = "yn:";
            // 
            // lblAlpha
            // 
            this.lblAlpha.AutoSize = true;
            this.lblAlpha.Location = new System.Drawing.Point(26, 134);
            this.lblAlpha.Name = "lblAlpha";
            this.lblAlpha.Size = new System.Drawing.Size(44, 16);
            this.lblAlpha.TabIndex = 4;
            this.lblAlpha.Text = "alpha:";
            // 
            // lblV0
            // 
            this.lblV0.AutoSize = true;
            this.lblV0.Location = new System.Drawing.Point(41, 184);
            this.lblV0.Name = "lblV0";
            this.lblV0.Size = new System.Drawing.Size(29, 16);
            this.lblV0.TabIndex = 5;
            this.lblV0.Text = "V0: ";
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.stopButton);
            this.panel1.Controls.Add(this.lblTimeValue);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblV0Value);
            this.panel1.Controls.Add(this.lblAlphaValue);
            this.panel1.Controls.Add(this.startButton);
            this.panel1.Controls.Add(this.numV0);
            this.panel1.Controls.Add(this.numAlpha);
            this.panel1.Controls.Add(this.numYn);
            this.panel1.Controls.Add(this.numXn);
            this.panel1.Controls.Add(this.numY0);
            this.panel1.Controls.Add(this.numX0);
            this.panel1.Controls.Add(this.checkBoxV0);
            this.panel1.Controls.Add(this.checkBoxAlpha);
            this.panel1.Controls.Add(this.lblX0);
            this.panel1.Controls.Add(this.lblV0);
            this.panel1.Controls.Add(this.lblY0);
            this.panel1.Controls.Add(this.lblAlpha);
            this.panel1.Controls.Add(this.lblXn);
            this.panel1.Controls.Add(this.lblYn);
            this.panel1.Location = new System.Drawing.Point(889, 133);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(373, 312);
            this.panel1.TabIndex = 6;
            // 
            // lblV0Value
            // 
            this.lblV0Value.AutoSize = true;
            this.lblV0Value.Location = new System.Drawing.Point(91, 184);
            this.lblV0Value.Name = "lblV0Value";
            this.lblV0Value.Size = new System.Drawing.Size(44, 16);
            this.lblV0Value.TabIndex = 16;
            this.lblV0Value.Text = "label2";
            this.lblV0Value.Visible = false;
            // 
            // lblAlphaValue
            // 
            this.lblAlphaValue.AutoSize = true;
            this.lblAlphaValue.Location = new System.Drawing.Point(91, 137);
            this.lblAlphaValue.Name = "lblAlphaValue";
            this.lblAlphaValue.Size = new System.Drawing.Size(44, 16);
            this.lblAlphaValue.TabIndex = 15;
            this.lblAlphaValue.Text = "label1";
            this.lblAlphaValue.Visible = false;
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(139, 261);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(109, 32);
            this.startButton.TabIndex = 14;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // numV0
            // 
            this.numV0.Location = new System.Drawing.Point(91, 182);
            this.numV0.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numV0.Name = "numV0";
            this.numV0.Size = new System.Drawing.Size(85, 22);
            this.numV0.TabIndex = 13;
            // 
            // numAlpha
            // 
            this.numAlpha.Location = new System.Drawing.Point(91, 132);
            this.numAlpha.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numAlpha.Name = "numAlpha";
            this.numAlpha.Size = new System.Drawing.Size(85, 22);
            this.numAlpha.TabIndex = 12;
            this.numAlpha.Visible = false;
            // 
            // numYn
            // 
            this.numYn.Location = new System.Drawing.Point(245, 86);
            this.numYn.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numYn.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numYn.Name = "numYn";
            this.numYn.Size = new System.Drawing.Size(85, 22);
            this.numYn.TabIndex = 11;
            this.numYn.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // numXn
            // 
            this.numXn.Location = new System.Drawing.Point(91, 84);
            this.numXn.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numXn.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numXn.Name = "numXn";
            this.numXn.Size = new System.Drawing.Size(85, 22);
            this.numXn.TabIndex = 10;
            this.numXn.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // numY0
            // 
            this.numY0.Location = new System.Drawing.Point(245, 36);
            this.numY0.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numY0.Name = "numY0";
            this.numY0.Size = new System.Drawing.Size(85, 22);
            this.numY0.TabIndex = 9;
            this.numY0.Value = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            // 
            // numX0
            // 
            this.numX0.Location = new System.Drawing.Point(91, 34);
            this.numX0.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numX0.Name = "numX0";
            this.numX0.Size = new System.Drawing.Size(85, 22);
            this.numX0.TabIndex = 8;
            // 
            // checkBoxV0
            // 
            this.checkBoxV0.AutoSize = true;
            this.checkBoxV0.Checked = true;
            this.checkBoxV0.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxV0.Location = new System.Drawing.Point(221, 187);
            this.checkBoxV0.Name = "checkBoxV0";
            this.checkBoxV0.Size = new System.Drawing.Size(18, 17);
            this.checkBoxV0.TabIndex = 7;
            this.checkBoxV0.UseVisualStyleBackColor = true;
            this.checkBoxV0.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBoxAlpha
            // 
            this.checkBoxAlpha.AutoSize = true;
            this.checkBoxAlpha.Location = new System.Drawing.Point(221, 132);
            this.checkBoxAlpha.Name = "checkBoxAlpha";
            this.checkBoxAlpha.Size = new System.Drawing.Size(18, 17);
            this.checkBoxAlpha.TabIndex = 6;
            this.checkBoxAlpha.UseVisualStyleBackColor = true;
            this.checkBoxAlpha.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Chart_with_Data
            // 
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            chartArea1.AxisX.LabelStyle.Format = "0.0";
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisX2.IsLabelAutoFit = false;
            chartArea1.AxisX2.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            chartArea1.AxisX2.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisX2.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisY.IsLabelAutoFit = false;
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            chartArea1.AxisY.LabelStyle.Format = "0.0";
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisY2.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisY2.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.Name = "ChartArea1";
            this.Chart_with_Data.ChartAreas.Add(chartArea1);
            this.Chart_with_Data.Location = new System.Drawing.Point(81, 34);
            this.Chart_with_Data.Name = "Chart_with_Data";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series1.Name = "Series1";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series2.MarkerSize = 10;
            series2.Name = "Series2";
            this.Chart_with_Data.Series.Add(series1);
            this.Chart_with_Data.Series.Add(series2);
            this.Chart_with_Data.Size = new System.Drawing.Size(731, 490);
            this.Chart_with_Data.TabIndex = 7;
            this.Chart_with_Data.Text = "chart1";
            title1.Font = new System.Drawing.Font("Courier New", 12F);
            title1.Name = "Title1";
            title1.Text = "РУХ МАТЕРІАЛЬНОЇ ТОЧКИ В ПОЛІ СИЛИ ТЯЖІННЯ";
            this.Chart_with_Data.Titles.Add(title1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 231);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 16);
            this.label1.TabIndex = 17;
            this.label1.Text = "Time:";
            // 
            // lblTimeValue
            // 
            this.lblTimeValue.AutoSize = true;
            this.lblTimeValue.Location = new System.Drawing.Point(91, 231);
            this.lblTimeValue.Name = "lblTimeValue";
            this.lblTimeValue.Size = new System.Drawing.Size(14, 16);
            this.lblTimeValue.TabIndex = 18;
            this.lblTimeValue.Text = "0";
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(139, 261);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(109, 32);
            this.stopButton.TabIndex = 19;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Visible = false;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1292, 560);
            this.Controls.Add(this.Chart_with_Data);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numV0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAlpha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numYn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numXn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numX0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Chart_with_Data)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblX0;
        private System.Windows.Forms.Label lblY0;
        private System.Windows.Forms.Label lblXn;
        private System.Windows.Forms.Label lblYn;
        private System.Windows.Forms.Label lblAlpha;
        private System.Windows.Forms.Label lblV0;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown numV0;
        private System.Windows.Forms.NumericUpDown numAlpha;
        private System.Windows.Forms.NumericUpDown numYn;
        private System.Windows.Forms.NumericUpDown numXn;
        private System.Windows.Forms.NumericUpDown numY0;
        private System.Windows.Forms.NumericUpDown numX0;
        private System.Windows.Forms.CheckBox checkBoxV0;
        private System.Windows.Forms.CheckBox checkBoxAlpha;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label lblAlphaValue;
        private System.Windows.Forms.Label lblV0Value;
        private System.Windows.Forms.DataVisualization.Charting.Chart Chart_with_Data;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTimeValue;
        private System.Windows.Forms.Button stopButton;
    }
}

