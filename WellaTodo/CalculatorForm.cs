using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WellaTodo
{
    public partial class CalculatorForm : Form
    {
        private double savedValue;
        private double memory;
        private char op = '\0';
        private bool calcFlag = false; // 계산 완료
        private bool opFlag = false;   // 연산자 버튼
        private bool memFlag = false;  // 메모리 버튼

        public CalculatorForm()
        {
            InitializeComponent();

            KeyPreview = true;

            button_MC.Enabled = false;
            button_MR.Enabled = false;

            button_Num0.Click += new EventHandler(NumberButton_Click);
            button_Num1.Click += new EventHandler(NumberButton_Click);
            button_Num2.Click += new EventHandler(NumberButton_Click);
            button_Num3.Click += new EventHandler(NumberButton_Click);
            button_Num4.Click += new EventHandler(NumberButton_Click);
            button_Num5.Click += new EventHandler(NumberButton_Click);
            button_Num6.Click += new EventHandler(NumberButton_Click);
            button_Num7.Click += new EventHandler(NumberButton_Click);
            button_Num8.Click += new EventHandler(NumberButton_Click);
            button_Num9.Click += new EventHandler(NumberButton_Click);
        }

        private void CalculatorForm_Load(object sender, EventArgs e)
        {
            textBox_Result.Text = "0";
        }

        private void Update_Calc_Display(string str)
        {
            double v = Double.Parse(str);
            textBox_Result.Text = commaProcedure(v, str); // 3점 표기
            textBox_Exp.Focus();  // 키 포커서 제거
        }

        private string commaProcedure(double v, string s)
        {
            int pos = 0;
            if (s.Contains("."))
            {
                pos = s.Length - s.IndexOf('.');	// 소수점 아래 자리수 + 1
                if (pos == 1) return s;  // 맨 뒤에 소수점이 있으면 그대로 리턴
                string formatStr = "{0:N" + (pos - 1) + "}";
                s = string.Format(formatStr, v);
            }
            else
                s = string.Format("{0:N0}", v);
            return s;
        }

        //
        // 이벤트 처리
        //
        private void NumberButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (textBox_Result.Text == "0" || opFlag == true || memFlag == true || calcFlag == true)
            {
                textBox_Result.Text = btn.Text;
                opFlag = false;
                memFlag = false;
                calcFlag = false;
            }
            else
            {
                textBox_Result.Text = textBox_Result.Text + btn.Text;
            }

            Update_Calc_Display(textBox_Result.Text);
        }

        private void button_Point_Click(object sender, EventArgs e)
        {
            if (textBox_Result.Text.Contains("."))
                return;
            else
                textBox_Result.Text += ".";

            Update_Calc_Display(textBox_Result.Text);
        }

        private void button_Plusminus_Click(object sender, EventArgs e)
        {
            double v = Double.Parse(textBox_Result.Text);
            textBox_Result.Text = (-v).ToString();

            Update_Calc_Display(textBox_Result.Text);
        }

        private void button_Plus_Click(object sender, EventArgs e)
        {
            savedValue = Double.Parse(textBox_Result.Text);
            textBox_Exp.Text = textBox_Result.Text + " + ";
            op = '+';
            opFlag = true;

            Update_Calc_Display(textBox_Result.Text);
        }

        private void button_Minus_Click(object sender, EventArgs e)
        {
            savedValue = Double.Parse(textBox_Result.Text);
            textBox_Exp.Text = textBox_Result.Text + " - ";
            op = '-';
            opFlag = true;

            Update_Calc_Display(textBox_Result.Text);
        }

        private void button_Time_Click(object sender, EventArgs e)
        {
            savedValue = Double.Parse(textBox_Result.Text);
            textBox_Exp.Text = textBox_Result.Text + " × ";
            op = '*';
            opFlag = true;

            Update_Calc_Display(textBox_Result.Text);
        }

        private void button_Divide_Click(object sender, EventArgs e)
        {
            savedValue = Double.Parse(textBox_Result.Text);
            textBox_Exp.Text = textBox_Result.Text + " ÷ ";
            op = '/';
            opFlag = true;

            Update_Calc_Display(textBox_Result.Text);
        }

        private void button_Enter_Click(object sender, EventArgs e)
        {
            Double v = Double.Parse(textBox_Result.Text);
            switch (op)
            {
                case '+':
                    textBox_Result.Text = (savedValue + v).ToString();
                    break;
                case '-':
                    textBox_Result.Text = (savedValue - v).ToString();
                    break;
                case '*':
                    textBox_Result.Text = (savedValue * v).ToString();
                    break;
                case '/':
                    textBox_Result.Text = (savedValue / v).ToString();
                    break;
            }
            textBox_Exp.Text = "";
            calcFlag = true;

            Update_Calc_Display(textBox_Result.Text);
        }

        private void button_Root_Click(object sender, EventArgs e)
        {
            textBox_Exp.Text = "√(" + textBox_Result.Text + ") ";
            textBox_Result.Text = Math.Sqrt(Double.Parse(textBox_Result.Text)).ToString();
            calcFlag = true;

            Update_Calc_Display(textBox_Result.Text);
        }

        private void button_Square_Click(object sender, EventArgs e)
        {
            textBox_Exp.Text = "sqr(" + textBox_Result.Text + ") ";
            textBox_Result.Text = (Double.Parse(textBox_Result.Text) * Double.Parse(textBox_Result.Text)).ToString();
            calcFlag = true;

            Update_Calc_Display(textBox_Result.Text);
        }

        private void button_Inverse_Click(object sender, EventArgs e)
        {
            textBox_Exp.Text = "1 / (" + textBox_Result.Text + ") ";
            textBox_Result.Text = (1 / Double.Parse(textBox_Result.Text)).ToString();
            calcFlag = true;

            Update_Calc_Display(textBox_Result.Text);
        }

        private void button_CE_Click(object sender, EventArgs e)
        {
            textBox_Result.Text = "0";

            Update_Calc_Display(textBox_Result.Text);
        }

        private void button_C_Click(object sender, EventArgs e)
        {
            textBox_Result.Text = "0";
            textBox_Exp.Text = "";
            savedValue = 0;
            op = '\0';
            opFlag = false;

            Update_Calc_Display(textBox_Result.Text);
        }

        private void button_BS_Click(object sender, EventArgs e)
        {
            if (textBox_Result.Text == "0") return;
            textBox_Result.Text = textBox_Result.Text.Remove(textBox_Result.Text.Length - 1);
            if (textBox_Result.Text.Length == 0)
            {
                textBox_Result.Text = "0";
            }

            Update_Calc_Display(textBox_Result.Text);
        }

        private void button_MS_Click(object sender, EventArgs e)
        {
            memory = Double.Parse(textBox_Result.Text);
            button_MC.Enabled = true;
            button_MR.Enabled = true;
            memFlag = true;

            Update_Calc_Display(textBox_Result.Text);
        }

        private void button_MR_Click(object sender, EventArgs e)
        {
            textBox_Result.Text = memory.ToString();
            memFlag = true;

            Update_Calc_Display(textBox_Result.Text);
        }

        private void button_MC_Click(object sender, EventArgs e)
        {
            textBox_Result.Text = "0";
            memory = 0;
            button_MC.Enabled = false;
            button_MR.Enabled = false;

            Update_Calc_Display(textBox_Result.Text);
        }

        private void button_MP_Click(object sender, EventArgs e)
        {
            memory += Double.Parse(textBox_Result.Text);

            Update_Calc_Display(textBox_Result.Text);
        }

        private void button_MM_Click(object sender, EventArgs e)
        {
            memory -= Double.Parse(textBox_Result.Text);

            Update_Calc_Display(textBox_Result.Text);
        }

        private void CalculatorForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void CalculatorForm_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    button_Enter.PerformClick();
                    break;
                case Keys.D0:
                    button_Num0.PerformClick();
                    break;
                case Keys.D1:
                    button_Num1.PerformClick();
                    break;
                case Keys.D2:
                    button_Num2.PerformClick();
                    break;
                case Keys.D3:
                    button_Num3.PerformClick();
                    break;
                case Keys.D4:
                    button_Num4.PerformClick();
                    break;
                case Keys.D5:
                    button_Num5.PerformClick();
                    break;
                case Keys.D6:
                    button_Num6.PerformClick();
                    break;
                case Keys.D7:
                    button_Num7.PerformClick();
                    break;
                case Keys.D8:
                    button_Num8.PerformClick();
                    break;
                case Keys.D9:
                    button_Num9.PerformClick();
                    break;
                case Keys.C:
                    button_C.PerformClick();
                    break;
                case Keys.Back:
                    button_BS.PerformClick();
                    break;
                case Keys.OemPeriod:
                    button_Point.PerformClick();
                    break;
                case Keys.Oemplus:
                    button_Plus.PerformClick();
                    break;
                case Keys.OemMinus:
                    button_Minus.PerformClick();
                    break;
                case Keys.NumPad0:
                    button_Num0.PerformClick();
                    break;
                case Keys.NumPad1:
                    button_Num1.PerformClick();
                    break;
                case Keys.NumPad2:
                    button_Num2.PerformClick();
                    break;
                case Keys.NumPad3:
                    button_Num3.PerformClick();
                    break;
                case Keys.NumPad4:
                    button_Num4.PerformClick();
                    break;
                case Keys.NumPad5:
                    button_Num5.PerformClick();
                    break;
                case Keys.NumPad6:
                    button_Num6.PerformClick();
                    break;
                case Keys.NumPad7:
                    button_Num7.PerformClick();
                    break;
                case Keys.NumPad8:
                    button_Num8.PerformClick();
                    break;
                case Keys.NumPad9:
                    button_Num9.PerformClick();
                    break;
                case Keys.Decimal:
                    button_Point.PerformClick();
                    break;
                case Keys.Add:
                    button_Plus.PerformClick();
                    break;
                case Keys.Subtract:
                    button_Minus.PerformClick();
                    break;
                case Keys.Multiply:
                    button_Time.PerformClick();
                    break;
                case Keys.Divide:
                    button_Divide.PerformClick();
                    break;
            }
            e.Handled = false;
            e.SuppressKeyPress = false;

            Update_Calc_Display(textBox_Result.Text);
        }

        private void CalculatorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
