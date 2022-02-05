// ----------------------------------------------------------
// Wella Todo
// ----------------------------------------------------------

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
    public partial class WellaForm : Form
    {
        static readonly string WM_WINDOW_CAPTION = "Wella Todo v0.95";

        MainFrame toDoList = new MainFrame();
        CalendarForm calendar = new CalendarForm();
        CalculatorForm calculator = new CalculatorForm();
        NotePadForm notePad = new NotePadForm();
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
            controller.Add_View(notePad );
            controller.Add_View(output);

            controller.Load_Data_File();

            toDoList.TopLevel = false;
            calculator.TopLevel = false;
            calendar.TopLevel = false;
            notePad.TopLevel = false;

            toDoList.MdiParent = this;
            calendar.MdiParent = this;
            calculator.MdiParent = this;
            notePad.MdiParent = this;

            calendar.Show();
            toDoList.Show();
            notePad.Show();

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


        private void 노트패드ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notePad = (NotePadForm)ShowActiveForm(notePad, typeof(NotePadForm));
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

        // ------------------------------------------------------------
        // 메뉴
        // ------------------------------------------------------------

        private void 새로만들기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("저장할까요?", WM_WINDOW_CAPTION, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                controller.Save_Data_File();
            }

            controller.New_Data_File();
        }

        private void 열기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1 = new OpenFileDialog()
            {
                FileName = "*.dat",
                Filter = "Data files (*.dat)|*.dat",
                Title = "Open Task Data file"
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                controller.Open_Data_File(filePath);
            }
        }

        private void 저장ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("저장할까요?", WM_WINDOW_CAPTION, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                controller.Save_Data_File();
            }
        }

        private void 인쇄ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form activeChild = this.ActiveMdiChild;

            if (activeChild == toDoList)
            {
                controller.Print_Data_File();
            }

            if (activeChild == calendar)
            {
                controller.Print_Data_File();
            }
        }

        private void 잘라내기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form childForm = this.ActiveMdiChild;
            Control focusedControl = FindFocus(childForm.ActiveControl);

            if (focusedControl is TextBox)
            {
                TextBox theBox = focusedControl as TextBox;

                if (theBox.SelectedText != "")
                {
                    Clipboard.SetDataObject(theBox.SelectedText);
                    theBox.SelectedText = "";
                }
            }
        }

        private void 복사ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form childForm = this.ActiveMdiChild;
            Control focusedControl = FindFocus(childForm.ActiveControl);

            if (focusedControl is TextBox)
            {
                TextBox theBox = focusedControl as TextBox;

                if (theBox.SelectedText != "")
                {
                    Clipboard.SetDataObject(theBox.SelectedText);
                }
            }
        }

        private void 붙여넣기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form childForm = this.ActiveMdiChild;
            Control focusedControl = FindFocus(childForm.ActiveControl);

            if (focusedControl is TextBox)
            {
                TextBox theBox = focusedControl as TextBox;

                IDataObject data = Clipboard.GetDataObject();
                if (data.GetDataPresent(DataFormats.Text))
                {
                    theBox.SelectedText = data.GetData(DataFormats.Text).ToString();
                }
            }
        }

        // ------------------------------------------------------------
        // 툴바
        // ------------------------------------------------------------

        private void 새로만들기ToolStripButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("저장할까요?", WM_WINDOW_CAPTION, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                controller.Save_Data_File();
            }

            controller.New_Data_File();
        }

        private void 열기ToolStripButton_Click(object sender, EventArgs e)
        {
            openFileDialog1 = new OpenFileDialog()
            {
                FileName = "*.dat",
                Filter = "Data files (*.dat)|*.dat",
                Title = "Open Task Data file"
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                controller.Open_Data_File(filePath);
            }
        }
        private void 저장ToolStripButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("저장할까요?", WM_WINDOW_CAPTION, MessageBoxButtons.YesNo) == DialogResult.Yes) 
            {
                controller.Save_Data_File();
            }
        }

        private void 인쇄ToolStripButton_Click(object sender, EventArgs e)
        {
            Form activeChild = this.ActiveMdiChild;

            if (activeChild == toDoList)
            {
                controller.Print_Data_File();
            }

            if (activeChild == calendar)
            {
                controller.Print_Data_File();
            }
        }

        private Control FindFocus(Control control)
        {
            foreach (Control ctr in control.Controls)
            {
                if (ctr.Focused)
                {
                    return ctr;
                }
                else if (ctr.ContainsFocus)
                {
                    return FindFocus(ctr);
                }
            }
            return null;
        }

        private void 잘라내기ToolStripButton_Click(object sender, EventArgs e)
        {
            Form childForm = this.ActiveMdiChild;
            Control focusedControl = FindFocus(childForm.ActiveControl);

            if (focusedControl is TextBox)
            {
                TextBox theBox = focusedControl as TextBox;

                if (theBox.SelectedText != "")
                {
                    Clipboard.SetDataObject(theBox.SelectedText);
                    theBox.SelectedText = "";
                }
            }
        }

        private void 복사ToolStripButton_Click(object sender, EventArgs e)
        {
            Form childForm = this.ActiveMdiChild;
            Control focusedControl = FindFocus(childForm.ActiveControl);

            if (focusedControl is TextBox)
            {
                TextBox theBox = focusedControl as TextBox;

                if (theBox.SelectedText != "")
                {
                    Clipboard.SetDataObject(theBox.SelectedText);
                }
            }
        }

        private void 붙여넣기ToolStripButton_Click(object sender, EventArgs e)
        {
            Form childForm = this.ActiveMdiChild;
            Control focusedControl = FindFocus(childForm.ActiveControl);

            if (focusedControl is TextBox)
            {
                TextBox theBox = focusedControl as TextBox;

                IDataObject data = Clipboard.GetDataObject();
                if (data.GetDataPresent(DataFormats.Text))
                {
                    theBox.SelectedText = data.GetData(DataFormats.Text).ToString();
                }
            }
        }

        private void 도움말ToolStripButton_Click(object sender, EventArgs e)
        {
            LoginSettingForm info = new LoginSettingForm();
            info.StartPosition = FormStartPosition.CenterParent;
            info.ShowDialog();
        }

    }
}

/*
Random random = new Random(); // Make it critiacally depended.

private void button1_Click(object sender, EventArgs e)
{
    Panel toDoElement = new Panel();

    toDoElement.Name = "panel" + (TodoListFlowLayout.Controls.Count + 1);
    toDoElement.BackColor = Color.FromArgb(123, random.Next(222), random.Next(222));
    toDoElement.Size = new Size(TodoListFlowLayout.ClientSize.Width, 50);

    TodoListFlowLayout.Controls.Add(toDoElement);
    TodoListFlowLayout.Controls.SetChildIndex(toDoElement, 0);

    toDoElement.Paint += (ss, ee) => { ee.Graphics.DrawString(toDoElement.Name, Font, Brushes.White, 22, 11); };
    TodoListFlowLayout.Invalidate();
}

*/