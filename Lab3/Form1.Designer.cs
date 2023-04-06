namespace Lab3
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.txtA = new System.Windows.Forms.TextBox();
            this.txtB = new System.Windows.Forms.TextBox();
            this.lblA = new System.Windows.Forms.Label();
            this.lblB = new System.Windows.Forms.Label();
            this.lblC = new System.Windows.Forms.Label();
            this.lbl1 = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.lbl3 = new System.Windows.Forms.Label();
            this.lbl4 = new System.Windows.Forms.Label();
            this.txtC = new System.Windows.Forms.TextBox();
            this.lbl5 = new System.Windows.Forms.Label();
            this.result1 = new System.Windows.Forms.Label();
            this.result2 = new System.Windows.Forms.Label();
            this.result3 = new System.Windows.Forms.Label();
            this.result4 = new System.Windows.Forms.Label();
            this.result5 = new System.Windows.Forms.Label();
            this.btnCalc = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(269, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Завдання:";
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.comboBox1.Location = new System.Drawing.Point(377, 60);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // txtA
            // 
            this.txtA.Location = new System.Drawing.Point(154, 171);
            this.txtA.Name = "txtA";
            this.txtA.Size = new System.Drawing.Size(66, 22);
            this.txtA.TabIndex = 2;
            this.txtA.Text = "0";
            this.txtA.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtA_KeyPress);
            // 
            // txtB
            // 
            this.txtB.Location = new System.Drawing.Point(154, 254);
            this.txtB.Name = "txtB";
            this.txtB.Size = new System.Drawing.Size(66, 22);
            this.txtB.TabIndex = 3;
            this.txtB.Text = "0";
            this.txtB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtB_KeyPress);
            // 
            // lblA
            // 
            this.lblA.AutoSize = true;
            this.lblA.Location = new System.Drawing.Point(104, 174);
            this.lblA.Name = "lblA";
            this.lblA.Size = new System.Drawing.Size(19, 16);
            this.lblA.TabIndex = 4;
            this.lblA.Text = "A:";
            // 
            // lblB
            // 
            this.lblB.AutoSize = true;
            this.lblB.Location = new System.Drawing.Point(104, 257);
            this.lblB.Name = "lblB";
            this.lblB.Size = new System.Drawing.Size(19, 16);
            this.lblB.TabIndex = 5;
            this.lblB.Text = "B:";
            // 
            // lblC
            // 
            this.lblC.AutoSize = true;
            this.lblC.Location = new System.Drawing.Point(104, 334);
            this.lblC.Name = "lblC";
            this.lblC.Size = new System.Drawing.Size(19, 16);
            this.lblC.TabIndex = 6;
            this.lblC.Text = "C:";
            this.lblC.Visible = false;
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(454, 174);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(17, 16);
            this.lbl1.TabIndex = 7;
            this.lbl1.Text = "1:";
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Location = new System.Drawing.Point(454, 213);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(17, 16);
            this.lbl2.TabIndex = 8;
            this.lbl2.Text = "2:";
            // 
            // lbl3
            // 
            this.lbl3.AutoSize = true;
            this.lbl3.Location = new System.Drawing.Point(454, 254);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(17, 16);
            this.lbl3.TabIndex = 9;
            this.lbl3.Text = "3:";
            // 
            // lbl4
            // 
            this.lbl4.AutoSize = true;
            this.lbl4.Location = new System.Drawing.Point(454, 294);
            this.lbl4.Name = "lbl4";
            this.lbl4.Size = new System.Drawing.Size(17, 16);
            this.lbl4.TabIndex = 10;
            this.lbl4.Text = "4:";
            // 
            // txtC
            // 
            this.txtC.Location = new System.Drawing.Point(154, 334);
            this.txtC.Name = "txtC";
            this.txtC.Size = new System.Drawing.Size(66, 22);
            this.txtC.TabIndex = 11;
            this.txtC.Text = "0";
            this.txtC.Visible = false;
            this.txtC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtC_KeyPress);
            // 
            // lbl5
            // 
            this.lbl5.AutoSize = true;
            this.lbl5.Location = new System.Drawing.Point(454, 334);
            this.lbl5.Name = "lbl5";
            this.lbl5.Size = new System.Drawing.Size(17, 16);
            this.lbl5.TabIndex = 12;
            this.lbl5.Text = "5:";
            // 
            // result1
            // 
            this.result1.AutoSize = true;
            this.result1.Location = new System.Drawing.Point(512, 174);
            this.result1.Name = "result1";
            this.result1.Size = new System.Drawing.Size(14, 16);
            this.result1.TabIndex = 13;
            this.result1.Text = "0";
            // 
            // result2
            // 
            this.result2.AutoSize = true;
            this.result2.Location = new System.Drawing.Point(512, 213);
            this.result2.Name = "result2";
            this.result2.Size = new System.Drawing.Size(14, 16);
            this.result2.TabIndex = 14;
            this.result2.Text = "0";
            // 
            // result3
            // 
            this.result3.AutoSize = true;
            this.result3.Location = new System.Drawing.Point(512, 257);
            this.result3.Name = "result3";
            this.result3.Size = new System.Drawing.Size(14, 16);
            this.result3.TabIndex = 15;
            this.result3.Text = "0";
            // 
            // result4
            // 
            this.result4.AutoSize = true;
            this.result4.Location = new System.Drawing.Point(512, 294);
            this.result4.Name = "result4";
            this.result4.Size = new System.Drawing.Size(14, 16);
            this.result4.TabIndex = 16;
            this.result4.Text = "0";
            // 
            // result5
            // 
            this.result5.AutoSize = true;
            this.result5.Location = new System.Drawing.Point(512, 334);
            this.result5.Name = "result5";
            this.result5.Size = new System.Drawing.Size(14, 16);
            this.result5.TabIndex = 17;
            this.result5.Text = "0";
            // 
            // btnCalc
            // 
            this.btnCalc.Location = new System.Drawing.Point(345, 392);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(91, 46);
            this.btnCalc.TabIndex = 18;
            this.btnCalc.Text = "Calculate";
            this.btnCalc.UseVisualStyleBackColor = true;
            this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnCalc);
            this.Controls.Add(this.result5);
            this.Controls.Add(this.result4);
            this.Controls.Add(this.result3);
            this.Controls.Add(this.result2);
            this.Controls.Add(this.result1);
            this.Controls.Add(this.lbl5);
            this.Controls.Add(this.txtC);
            this.Controls.Add(this.lbl4);
            this.Controls.Add(this.lbl3);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.lblC);
            this.Controls.Add(this.lblB);
            this.Controls.Add(this.lblA);
            this.Controls.Add(this.txtB);
            this.Controls.Add(this.txtA);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox txtA;
        private System.Windows.Forms.TextBox txtB;
        private System.Windows.Forms.Label lblA;
        private System.Windows.Forms.Label lblB;
        private System.Windows.Forms.Label lblC;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.Label lbl3;
        private System.Windows.Forms.Label lbl4;
        private System.Windows.Forms.TextBox txtC;
        private System.Windows.Forms.Label lbl5;
        private System.Windows.Forms.Label result1;
        private System.Windows.Forms.Label result2;
        private System.Windows.Forms.Label result3;
        private System.Windows.Forms.Label result4;
        private System.Windows.Forms.Label result5;
        private System.Windows.Forms.Button btnCalc;
    }
}

