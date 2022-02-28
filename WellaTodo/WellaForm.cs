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
        BulletinBoardForm bulletinBoard = new BulletinBoardForm();
        OutputForm output = new OutputForm();
        CDataCellForm datacell = new CDataCellForm();
        MainModel model = new MainModel();
        MainController controller;

        public WellaForm()
        {
            InitializeComponent();

            IsMdiContainer = true;
            WindowState = FormWindowState.Maximized;
        }

        // --------------------------------------------------
        // Form 이벤트
        // --------------------------------------------------
        private void WellaForm_Load(object sender, EventArgs e)
        {
            // Controller를 통해 Model의 Observer로 View를 등록한다
            controller = new MainController(model);
            controller.Add_View(toDoList);
            controller.Add_View(calendar);
            controller.Add_View(bulletinBoard);
            controller.Add_View(notePad );
            controller.Add_View(output);
            controller.Add_View(datacell);

            controller.Load_Data_File();

            toDoList.TopLevel = false;
            calculator.TopLevel = false;
            calendar.TopLevel = false;
            bulletinBoard.TopLevel = false;
            notePad.TopLevel = false;

            toDoList.MdiParent = this;
            calendar.MdiParent = this;
            bulletinBoard.MdiParent = this;
            calculator.MdiParent = this;
            notePad.MdiParent = this;

            bulletinBoard.Show();
            calendar.Show();
            notePad.Show();
            toDoList.Show();

            toDoList.Activate();
            LayoutMdi(MdiLayout.TileVertical);
        }

        // --------------------------------------------------
        // 초기화 및 Form 처리
        // --------------------------------------------------
        private Form ShowActiveForm(Form form, Type t)
        {
            if (form == null)
            {
                //Console.WriteLine("ShowActiveForm -> form is null, CreateInstance");
                form = (Form)Activator.CreateInstance(t);
                form.MdiParent = this;
                form.WindowState = FormWindowState.Maximized;
                form.Show();
            }
            else
            {
                if (form.IsDisposed)
                {
                    //Console.WriteLine("ShowActiveForm -> form is disposed, CreateInstance");
                    form = (Form)Activator.CreateInstance(t);
                    form.MdiParent = this;
                    form.WindowState = FormWindowState.Maximized;
                    form.Show();
                }
                else
                {
                    if (form.Visible)
                    {
                        //Console.WriteLine("ShowActiveForm -> form is visible, Hide!!!");
                        //form.Hide();
                    }
                    else
                    {
                        //Console.WriteLine("ShowActiveForm -> form is hiding, Show!!!");
                        //form.Show();
                    }
                    form.Show();
                }
            }
            form.Activate();
            form.BringToFront();
            return form;
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

        // ------------------------------------------------------------
        // 메뉴
        // ------------------------------------------------------------
        private void toDoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toDoList = (MainFrame)ShowActiveForm(toDoList, typeof(MainFrame));
        }

        private void 계산기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            calculator = (CalculatorForm)ShowActiveForm(calculator, typeof(CalculatorForm));
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(CalculatorForm))
                {

                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }
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
            bulletinBoard = (BulletinBoardForm)ShowActiveForm(bulletinBoard, typeof(BulletinBoardForm));
        }

        private void 출력창ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            output = (OutputForm)ShowActiveForm(output, typeof(OutputForm));
        }

        private void cDataCellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            datacell = (CDataCellForm)ShowActiveForm(datacell, typeof(CDataCellForm));
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
            bulletinBoard = (BulletinBoardForm)ShowActiveForm(bulletinBoard, typeof(BulletinBoardForm));
        }

        private void button_Notepad_Click(object sender, EventArgs e)
        {
            notePad = (NotePadForm)ShowActiveForm(notePad, typeof(NotePadForm));
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




/*
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Cyotek.Windows.Forms
{
  public class FontComboBox : ComboBox
  {
  #region  Private Member Declarations  

    private Dictionary<string, Font> _fontCache;
    private int _itemHeight;
    private int _previewFontSize;
    private StringFormat _stringFormat;

  #endregion  Private Member Declarations  

  #region  Public Constructors  

    public FontComboBox()
    {
      _fontCache = new Dictionary<string, Font>();

      this.DrawMode = DrawMode.OwnerDrawVariable;
      this.Sorted = true;
      this.PreviewFontSize = 12;

      this.CalculateLayout();
      this.CreateStringFormat();
    }

  #endregion  Public Constructors  

  #region  Events  

    public event EventHandler PreviewFontSizeChanged;

  #endregion  Events  

  #region  Protected Overridden Methods  

    protected override void Dispose(bool disposing)
    {
      this.ClearFontCache();

      if (_stringFormat != null)
        _stringFormat.Dispose();

      base.Dispose(disposing);
    }

    protected override void OnDrawItem(DrawItemEventArgs e)
    {
      base.OnDrawItem(e);

      if (e.Index > -1 && e.Index < this.Items.Count)
      {
        e.DrawBackground();

        if ((e.State & DrawItemState.Focus) == DrawItemState.Focus)
          e.DrawFocusRectangle();

        using (SolidBrush textBrush = new SolidBrush(e.ForeColor))
        {
          string fontFamilyName;

          fontFamilyName = this.Items[e.Index].ToString();
          e.Graphics.DrawString(fontFamilyName, this.GetFont(fontFamilyName), 
        textBrush, e.Bounds, _stringFormat);
        }
      }
    }

    protected override void OnFontChanged(EventArgs e)
    {
      base.OnFontChanged(e);

      this.CalculateLayout();
    }

    protected override void OnGotFocus(EventArgs e)
    {
      this.LoadFontFamilies();

      base.OnGotFocus(e);
    }

    protected override void OnMeasureItem(MeasureItemEventArgs e)
    {
      base.OnMeasureItem(e);

      if (e.Index > -1 && e.Index < this.Items.Count)
      {
        e.ItemHeight = _itemHeight;
      }
    }

    protected override void OnRightToLeftChanged(EventArgs e)
    {
      base.OnRightToLeftChanged(e);

      this.CreateStringFormat();
    }

    protected override void OnTextChanged(EventArgs e)
    {
      base.OnTextChanged(e);

      if (this.Items.Count == 0)
      {
        int selectedIndex;

        this.LoadFontFamilies();

        selectedIndex = this.FindStringExact(this.Text);
        if (selectedIndex != -1)
          this.SelectedIndex = selectedIndex;
      }
    }

  #endregion  Protected Overridden Methods  

  #region  Public Methods  

    public virtual void LoadFontFamilies()
    {
      if (this.Items.Count == 0)
      {
        Cursor.Current = Cursors.WaitCursor;

        foreach (FontFamily fontFamily in FontFamily.Families)
          this.Items.Add(fontFamily.Name);

        Cursor.Current = Cursors.Default;
      }
    }

  #endregion  Public Methods  

  #region  Public Properties  

    [Browsable(false), DesignerSerializationVisibility
    (DesignerSerializationVisibility.Hidden), 
    EditorBrowsable(EditorBrowsableState.Never)]
    public new DrawMode DrawMode
    {
      get { return base.DrawMode; }
      set { base.DrawMode = value; }
    }

    [Category("Appearance"), DefaultValue(12)]
    public int PreviewFontSize
    {
      get { return _previewFontSize; }
      set
      {
        _previewFontSize = value;

        this.OnPreviewFontSizeChanged(EventArgs.Empty);
      }
    }

    [Browsable(false), DesignerSerializationVisibility
    (DesignerSerializationVisibility.Hidden), 
    EditorBrowsable(EditorBrowsableState.Never)]
    public new bool Sorted
    {
      get { return base.Sorted; }
      set { base.Sorted = value; }
    }

  #endregion  Public Properties  

  #region  Private Methods  

    private void CalculateLayout()
    {
      this.ClearFontCache();

      using (Font font = new Font(this.Font.FontFamily, (float)this.PreviewFontSize))
      {
        Size textSize;

        textSize = TextRenderer.MeasureText("yY", font);
        _itemHeight = textSize.Height + 2;
      }
    }

    private bool IsUsingRTL(Control control)
    {
      bool result;

      if (control.RightToLeft == RightToLeft.Yes)
        result = true;
      else if (control.RightToLeft == RightToLeft.Inherit && control.Parent != null)
        result = IsUsingRTL(control.Parent);
      else
        result = false;

      return result;
    }

  #endregion  Private Methods  

  #region  Protected Methods  

    protected virtual void ClearFontCache()
    {
      if (_fontCache != null)
      {
        foreach (string key in _fontCache.Keys)
          _fontCache[key].Dispose();
        _fontCache.Clear();
      }
    }

    protected virtual void CreateStringFormat()
    {
      if (_stringFormat != null)
        _stringFormat.Dispose();

      _stringFormat = new StringFormat(StringFormatFlags.NoWrap);
      _stringFormat.Trimming = StringTrimming.EllipsisCharacter;
      _stringFormat.HotkeyPrefix = HotkeyPrefix.None;
      _stringFormat.Alignment = StringAlignment.Near;
      _stringFormat.LineAlignment = StringAlignment.Center;

      if (this.IsUsingRTL(this))
        _stringFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
    }

    protected virtual Font GetFont(string fontFamilyName)
    {
      lock (_fontCache)
      {
        if (!_fontCache.ContainsKey(fontFamilyName))
        {
          Font font;

          font = this.GetFont(fontFamilyName, FontStyle.Regular);
          if (font == null)
            font = this.GetFont(fontFamilyName, FontStyle.Bold);
          if (font == null)
            font = this.GetFont(fontFamilyName, FontStyle.Italic);
          if (font == null)
            font = this.GetFont(fontFamilyName, FontStyle.Bold | FontStyle.Italic);
          if (font == null)
            font = (Font)this.Font.Clone();

          _fontCache.Add(fontFamilyName, font);
        }
      }

      return _fontCache[fontFamilyName];
    }

    protected virtual Font GetFont(string fontFamilyName, FontStyle fontStyle)
    {
      Font font;

      try
      {
        font = new Font(fontFamilyName, this.PreviewFontSize, fontStyle);
      }
      catch
      {
        font = null;
      }

      return font;
    }

    protected virtual void OnPreviewFontSizeChanged(EventArgs e)
    {
      if (PreviewFontSizeChanged != null)
        PreviewFontSizeChanged(this, e);

      this.CalculateLayout();
    }

  #endregion  Protected Methods  
  }
}
*/