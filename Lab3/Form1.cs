using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IL = Lab3.ImpreciseLogic;

namespace Lab3
{
    
    public partial class Form1 : Form
    {
        List<Label> resultLabels;

        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            SetResultLabels();
            SetLostFocusEvent();
        }

        private void SetResultLabels()
        {
            resultLabels = new List<Label>();
            resultLabels.Add(result1);
            resultLabels.Add(result2);
            resultLabels.Add(result3);
            resultLabels.Add(result4);
            resultLabels.Add(result5);
        }

        private void SetLostFocusEvent()
        {
            txtA.LostFocus += CheckNum;
            txtB.LostFocus += CheckNum;
            txtC.LostFocus += CheckNum;
        }

        private void CheckNum(object sender, EventArgs e)
        {
            float num = Convert.ToSingle(((TextBox)sender).Text);
            if (num < 0 || num > 1) ((TextBox)sender).Text = "0";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.SelectedIndex == 0 || comboBox1.SelectedIndex == 1)
            {
                SetLabelVisibility(false);

            }
            else if (comboBox1.SelectedIndex == 2)
            {
                SetLabelVisibility(true);
            }
            
        }

        //true - для 3-го, false для 1 та 2
        private void SetLabelVisibility(bool flag)
        {
            lblC.Visible = flag;
            txtC.Visible = flag;
            lbl5.Visible = !flag;
            result5.Visible = !flag;
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            float[] results=new float[] { };
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    results = Calc1();
                    break;
                case 1:
                    results = Calc2();
                    break;
                case 2:
                    results = Calc3();
                    break;
            }
            for(int i = 0; i < results.Length&& i<resultLabels.Count; i++)
            {
                resultLabels[i].Text=results[i].ToString();
            }
        }

        private float[] Calc1()
        {
            float[] r = new float[5];
            float A =Convert.ToSingle(txtA.Text);
            float B =Convert.ToSingle(txtB.Text);

            r[0] = IL.Or(IL.Not(A), B);
            r[1] = IL.Or(A, IL.Not(B));
            r[2] = IL.And(A, IL.Not(B));
            r[3] = IL.Implication(A, IL.Not(B));
            r[4] = IL.Equivalence(A, IL.Implication(A, IL.Not(B)));

            return r;
        }

        private float[] Calc2()
        {
            float[] r = new float[5];
            float A = Convert.ToSingle(txtA.Text);
            float B = Convert.ToSingle(txtB.Text);
            r[0] = IL.And(IL.Or(IL.Not(A), B), B);
            r[1] = IL.Or(IL.Or(A, IL.Not(B)), A);
            r[2] = IL.Equivalence(A, IL.Not(B));
            r[3] = IL.Equivalence(A, IL.Or(A, IL.Not(B)));
            r[4] = IL.Equivalence(A, IL.Implication(A, IL.Not(B)));

            return r;
        }

        private float[] Calc3()
        {
            float[] r = new float[5];
            float A = Convert.ToSingle(txtA.Text);
            float B = Convert.ToSingle(txtB.Text); 
            float C = Convert.ToSingle(txtC.Text);
            r[0] = IL.Or(IL.Or(A, IL.Not(B)), C);
            r[1] = IL.Equivalence(A, IL.Or(IL.Not(B), C));
            r[2] = IL.Equivalence(C, IL.And(A, IL.Not(B)));
            r[3] = IL.Equivalence(A, IL.Implication(C, B));

            return r;
        }

        private void txtA_KeyPress(object sender, KeyPressEventArgs e)
        {
            checkCharecter(sender, e);
        }

        private void txtB_KeyPress(object sender, KeyPressEventArgs e)
        {
            checkCharecter(sender, e);
        }

        private void txtC_KeyPress(object sender, KeyPressEventArgs e)
        {
            checkCharecter(sender, e);
        }

        private void checkCharecter(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != ','))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
        }
    }
}
