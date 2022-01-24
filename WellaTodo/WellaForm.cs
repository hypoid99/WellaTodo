﻿using System;
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
    public partial class WellaForm : Form
    {
        MainFrame toDoList = new MainFrame();
        CalendarForm calendar = new CalendarForm();
        CalculatorForm calculator = new CalculatorForm();
        OutputForm output = new OutputForm();
        MainModel model = new MainModel();
        MainController controller;

        public WellaForm()
        {
            InitializeComponent();

            IsMdiContainer = true;
            WindowState = FormWindowState.Maximized;
        }

        private void WellaForm_Load(object sender, EventArgs e)
        {
            controller = new MainController(model);
            controller.Add_View(toDoList);
            controller.Add_View(calendar);
            controller.Add_View(output);

            controller.Load_Data_File();

            toDoList.TopLevel = false;
            calculator.TopLevel = false;
            calendar.TopLevel = false;

            toDoList.MdiParent = this;
            calendar.MdiParent = this;
            calculator.MdiParent = this;

            calendar.Show();
            toDoList.Show();

            toDoList.Activate();
            LayoutMdi(MdiLayout.TileVertical);
    }

        private Form ShowActiveForm(Form form, Type t)
        {
            if (form == null)
            {
                form = (Form)Activator.CreateInstance(t);
                form.MdiParent = this;
                form.WindowState = FormWindowState.Maximized;
                form.Show();
            }
            else
            {
                if (form.IsDisposed)
                {
                    form = (Form)Activator.CreateInstance(t);
                    form.MdiParent = this;
                    form.WindowState = FormWindowState.Maximized;
                    form.Show();
                }
                else
                {
                    if (form.Visible)
                    {
                        form.Hide();
                    }
                    else
                    {
                        form.Show();
                    }
                }
            }
            return form;
        }

        private void toDoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toDoList = (MainFrame)ShowActiveForm(toDoList, typeof(MainFrame));
        }

        private void 계산기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            calculator = (CalculatorForm)ShowActiveForm(calculator, typeof(CalculatorForm));
        }

        private void 달력ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            calendar = (CalendarForm)ShowActiveForm(calendar, typeof(CalendarForm));
        }

        private void 메모ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 출력창ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            output = (OutputForm)ShowActiveForm(output, typeof(OutputForm));
        }

        private void 종료ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 캐스케이드ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void 수직정렬ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void 수평정렬ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void 아이콘화ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void 정보ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginSettingForm info = new LoginSettingForm();
            info.StartPosition = FormStartPosition.CenterParent;
            info.ShowDialog();
        }

        private void button_Todo_Click(object sender, EventArgs e)
        {
            toDoList = (MainFrame)ShowActiveForm(toDoList, typeof(MainFrame));
        }

        private void button_Calendar_Click(object sender, EventArgs e)
        {
            calendar = (CalendarForm)ShowActiveForm(calendar, typeof(CalendarForm));
        }

        private void button_Calculator_Click(object sender, EventArgs e)
        {
            calculator = (CalculatorForm)ShowActiveForm(calculator, typeof(CalculatorForm));
        }

        private void button_Memo_Click(object sender, EventArgs e)
        {

        }

        private void button_Output_Click(object sender, EventArgs e)
        {
            output = (OutputForm)ShowActiveForm(output, typeof(OutputForm));
        }

        private void button_Cascade_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void button_Vertical_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void button_Horizontal_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 저장ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 저장ToolStripButton_Click(object sender, EventArgs e)
        {

        }
    }
}
