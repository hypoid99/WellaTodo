using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace WellaTodo
{
    public partial class NotePadForm : Form, IView, IModelObserver
    {
        public event ViewHandler<IView> View_Changed_Event;

        static readonly Color BACK_COLOR = Color.White;
        static readonly Color HIGHLIGHT_COLOR = Color.LightCyan;
        static readonly Color SELECTED_COLOR = Color.Cyan;

        static readonly string FONT_NAME = "맑은 고딕";
        static readonly float FONT_SIZE_TEXT = 14.0f;
        static readonly int FONT_MAX_SIZE = 50;

        MainController m_Controller;

        List<string> m_FontName = new List<string>();
        List<float> m_FontSize = new List<float>();

        private string m_FileName;
        private string OpenedDocumentPath { get; set; } = "NoName";
        public string DefaultSaveDirectory { get; set; } = "c:\\";
        public bool IsOpened { get; set; } = false;
        private bool isUnsaved = false;
        public bool IsUnsaved
        {
            get
            {
                return isUnsaved;
            }
            set
            {
                isUnsaved = value;
                UpdatePath();
            }
        }

        public NotePadForm()
        {
            InitializeComponent();
        }

        public void SetController(MainController controller)
        {
            m_Controller = controller;
        }

        public void Update_View(IModel m, ModelEventArgs e)
        {

        }

        private void NotePadForm_Load(object sender, EventArgs e)
        {
            Initiate();
        }

        private void Initiate()
        {
            Console.WriteLine("Initiate");

            richTextBox.WordWrap = false;
            //richTextBox.SelectionCharOffset = 0;

            FontFamily[] fontList = new System.Drawing.Text.InstalledFontCollection().Families;
            foreach (var item in fontList)
            {
                m_FontName.Add(item.Name);
            }
            comboBox_FontSelect.DataSource = m_FontName;

            int cnt = 0;    
            for (int i = 0; i < m_FontName.Count; i++)
            {
                if (m_FontName[i] == FONT_NAME)
                {
                    comboBox_FontSelect.SelectedIndex = i;
                    cnt++;
                }
            }
            if (cnt == 0)
            {
                comboBox_FontSelect.SelectedIndex = 2;
            }

            for (int i = 1; i <= FONT_MAX_SIZE; i++)
            {
                m_FontSize.Add(i);
            }
            comboBox_FontSize.DataSource = m_FontSize;
            comboBox_FontSize.SelectedIndex = 15;

            checkBox_AlignLeft.Click += new EventHandler(checkBox_TextAlign_Click);
            checkBox_AlignCenter.Click += new EventHandler(checkBox_TextAlign_Click);
            checkBox_AlignRight.Click += new EventHandler(checkBox_TextAlign_Click);
        }

        private void UpdatePath()
        {
            m_FileName = $"{(IsUnsaved ? "*" : "")}{OpenedDocumentPath} - NotePad";
            //Console.WriteLine("m_FileName : " + m_FileName);
            Text = m_FileName;
        }

        private void New_File()
        {
            IsOpened = false;
            richTextBox.Text = String.Empty;
            OpenedDocumentPath = "NoName";
            UpdatePath();
        }

        private void Open_File()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = DefaultSaveDirectory;
                openFileDialog.Filter = "Documentation (*.rtf;*.pdf;*.txt)|*.rtf;*.pdf;*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK &&
                    openFileDialog.FileName.Length > 0)
                {
                    OpenedDocumentPath = openFileDialog.FileName;
                    IsOpened = true;
                    UpdatePath();

                    try
                    {
                        if (OpenedDocumentPath.EndsWith(".rtf"))
                        {
                            richTextBox.LoadFile(OpenedDocumentPath);
                        }
                        else if (OpenedDocumentPath.EndsWith(".pdf"))
                        {
                            MessageBox.Show("PDF is not supported");

                            IsOpened = false;
                            richTextBox.Text = String.Empty;
                            OpenedDocumentPath = "NoName";
                            IsUnsaved = false;
                            UpdatePath();
                        }
                        else
                        {
                            var fileStream = openFileDialog.OpenFile();
                            using (StreamReader reader = new StreamReader(fileStream))
                            {
                                richTextBox.Text = reader.ReadToEnd();
                            }
                        }
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show("Can't Open File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        private void Save_File()
        {
            try
            {
                if (IsOpened) // 파일이 이미 열려 있는 경우 경로를 따라 저장
                {
                    var dirPath = OpenedDocumentPath.Substring(0, OpenedDocumentPath.LastIndexOf(Path.DirectorySeparatorChar) + 1);
                    Directory.CreateDirectory(dirPath); // 디렉토리가 없으면 생성

                    // rtf인 경우 서식을 사용하여 저장
                    richTextBox.SaveFile(OpenedDocumentPath,
                                         OpenedDocumentPath.EndsWith(".rtf") ? RichTextBoxStreamType.RichText : RichTextBoxStreamType.PlainText);
                }
                else // 파일이 새 파일이면 저장 대화 상자를 호출
                {
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.InitialDirectory = DefaultSaveDirectory;
                        saveFileDialog.Filter = "서식이 있는 텍스트 (*.rtf)|*.rtf|일반 텍스트 (*.txt)|*.txt|All files (*.*)|*.*";
                        saveFileDialog.FilterIndex = 1;
                        saveFileDialog.RestoreDirectory = true;

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            var dirPath = saveFileDialog.FileName.Substring(0, saveFileDialog.FileName.LastIndexOf(Path.DirectorySeparatorChar) + 1);
                            Directory.CreateDirectory(dirPath); // 디렉토리가 없으면 생성

                            // rtf인 경우 서식을 사용하여 저장
                            richTextBox.SaveFile(saveFileDialog.FileName,
                                saveFileDialog.FileName.EndsWith(".rtf") ? RichTextBoxStreamType.RichText : RichTextBoxStreamType.PlainText);

                            OpenedDocumentPath = saveFileDialog.FileName;
                            IsOpened = true;
                            IsUnsaved = false;
                            UpdatePath();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Save_As_File()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = DefaultSaveDirectory;
                saveFileDialog.Filter = "서식이 있는 텍스트 (*.rtf)|*.rtf|일반 텍스트 (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var dirPath = saveFileDialog.FileName.Substring(0, saveFileDialog.FileName.LastIndexOf(Path.DirectorySeparatorChar) + 1);
                    Directory.CreateDirectory(dirPath);

                    // rtf인 경우 서식을 사용하여 저장
                    richTextBox.SaveFile(saveFileDialog.FileName,
                        saveFileDialog.FileName.EndsWith(".rtf") ? RichTextBoxStreamType.RichText : RichTextBoxStreamType.PlainText);

                    OpenedDocumentPath = saveFileDialog.FileName;
                    IsOpened = true;
                    IsUnsaved = false;
                    UpdatePath();
                }
            }
        }

        // ------------------------------------------------------------
        // 메뉴
        // ------------------------------------------------------------
        private void 새로만들기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New_File();
        }

        private void 열기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Open_File();
        }

        private void 저장ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save_File();
        }

        private void 다른이름으로저장ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save_As_File();
        }

        private void 인쇄ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 미리보기ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 모두선택ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.SelectAll();
        }

        // ------------------------------------------------------------
        // 툴바
        // ------------------------------------------------------------
        private void button_New_Click(object sender, EventArgs e)
        {
            New_File();
        }

        private void button_Open_Click(object sender, EventArgs e)
        {
            Open_File();
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            Save_File();
        }

        private void button_Print_Click(object sender, EventArgs e)
        {

        }

        private void button_Undo_Click(object sender, EventArgs e)
        {
            richTextBox.Undo();
        }

        private void button_Redo_Click(object sender, EventArgs e)
        {
            if (richTextBox.CanRedo == true && richTextBox.RedoActionName != "Delete")
            {
                richTextBox.Redo();
            }
        }

        private void button_Cut_Click(object sender, EventArgs e)
        {
            if (richTextBox.SelectionLength > 0) richTextBox.Cut();
        }

        private void button_Copy_Click(object sender, EventArgs e)
        {
            if (richTextBox.SelectionLength > 0) richTextBox.Copy();
        }

        private void button_Paste_Click(object sender, EventArgs e)
        {
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text)) richTextBox.Paste();
        }

        private void comboBox_FontSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine("comboBox_FontSelect_SelectedIndexChanged");

            string fontName = comboBox_FontSelect.SelectedItem.ToString();
            float fontSize;

            if (comboBox_FontSize.SelectedItem == null)
            {
                fontSize = FONT_SIZE_TEXT;
            }
            else
            {
                fontSize = (float)comboBox_FontSize.SelectedItem;
            }

            FontStyle fontStyle = (checkBox_Bold.Checked ? FontStyle.Bold : 0) 
                                 | (checkBox_Italic.Checked ? FontStyle.Italic : 0)
                                 | (checkBox_Underline.Checked ? FontStyle.Underline : 0)
                                 | (checkBox_Strike.Checked ? FontStyle.Strikeout : 0);

            richTextBox.SelectionFont = new Font(fontName, fontSize, fontStyle);
        }

        private void comboBox_FontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine("comboBox_FontSize_SelectedIndexChanged");

            string fontName = comboBox_FontSelect.SelectedItem.ToString();
            float fontSize = (float)comboBox_FontSize.SelectedItem;
            FontStyle fontStyle = (checkBox_Bold.Checked ? FontStyle.Bold : 0)
                                 | (checkBox_Italic.Checked ? FontStyle.Italic : 0)
                                 | (checkBox_Underline.Checked ? FontStyle.Underline : 0)
                                 | (checkBox_Strike.Checked ? FontStyle.Strikeout : 0);

            richTextBox.SelectionFont = new Font(fontName, fontSize, fontStyle);
        }

        private void button_FontSizeUp_Click(object sender, EventArgs e)
        {
            Console.WriteLine("button_FontSizeUp_Click");

            int size = (int)comboBox_FontSize.SelectedIndex;
            size++;
            if (size > FONT_MAX_SIZE) size = FONT_MAX_SIZE;
            comboBox_FontSize.SelectedIndex = size;
        }

        private void button_FontSizeDown_Click(object sender, EventArgs e)
        {
            Console.WriteLine("button_FontSizeDown_Click");

            int size = (int)comboBox_FontSize.SelectedIndex;
            size--;
            if (size < 1) size = 1;
            comboBox_FontSize.SelectedIndex = size;
        }

        private void checkBox_Bold_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("checkBox_Bold_CheckedChanged : " + checkBox_Bold.Checked);
            Console.WriteLine("checkBox_Bold_CheckedChanged -> font size : " + richTextBox.SelectionFont.Size);

            if (checkBox_Bold.Checked)
            {
                richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, richTextBox.SelectionFont.Style | FontStyle.Bold);
            }
            else
            {
                richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, richTextBox.SelectionFont.Style & ~FontStyle.Bold);
            }

            Update_CheckBox_Status();
        }

        private void checkBox_Italic_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("checkBox_Italic_CheckedChanged : " + checkBox_Italic.Checked);

            if (checkBox_Italic.Checked)
            {
                richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, richTextBox.SelectionFont.Style | FontStyle.Italic);
            }
            else
            {
                richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, richTextBox.SelectionFont.Style & ~FontStyle.Italic);
            }

            Update_CheckBox_Status();
        }

        private void checkBox_Underline_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("checkBox_Underline_CheckedChanged : " + checkBox_Underline.Checked);

            if (checkBox_Underline.Checked)
            {
                richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, richTextBox.SelectionFont.Style | FontStyle.Underline);
            }
            else
            {
                richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, richTextBox.SelectionFont.Style & ~FontStyle.Underline);
            }

            Update_CheckBox_Status();
        }

        private void checkBox_Strike_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("checkBox_Strike_CheckedChanged : " + checkBox_Strike.Checked);

            if (checkBox_Strike.Checked)
            {
                richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, richTextBox.SelectionFont.Style | FontStyle.Strikeout);
            }
            else
            {
                richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, richTextBox.SelectionFont.Style & ~FontStyle.Strikeout);
            }

            Update_CheckBox_Status();
        }

        private void button_TextColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    richTextBox.SelectionColor = colorDialog.Color;
                    button_TextColor.FlatAppearance.BorderColor = colorDialog.Color;
                }
            }
        }

        private void button_TextFillColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    richTextBox.SelectionBackColor = colorDialog.Color;
                    button_TextFillColor.FlatAppearance.BorderColor = colorDialog.Color;
                }
            }
        }

        private void checkBox_TextAlign_Click(object sender, EventArgs e)
        {
            CheckBox sd = (CheckBox)sender;

            if (sd.Checked)
            {

                checkBox_AlignLeft.Checked = false;
                checkBox_AlignCenter.Checked = false;
                checkBox_AlignRight.Checked = false;

                ((CheckBox)sender).Checked = true;

                switch (sd.Name)
                {
                    case "checkBox_AlignLeft":
                        richTextBox.SelectionAlignment = HorizontalAlignment.Left;
                        break;
                    case "checkBox_AlignCenter":
                        richTextBox.SelectionAlignment = HorizontalAlignment.Center;
                        break;
                    case "checkBox_AlignRight":
                        richTextBox.SelectionAlignment = HorizontalAlignment.Right;
                        break;
                }
            }
            else
            {
                richTextBox.SelectionAlignment = HorizontalAlignment.Left;
            }

            switch (richTextBox.SelectionAlignment)
            {
                case HorizontalAlignment.Left:
                    checkBox_AlignLeft.Checked = true;
                    break;
                case HorizontalAlignment.Center:
                    checkBox_AlignCenter.Checked = true;
                    break;
                case HorizontalAlignment.Right:
                    checkBox_AlignRight.Checked = true;
                    break;
            }

            Update_CheckBox_Status();
        }

        private void button_IndentDec_Click(object sender, EventArgs e)
        {
            richTextBox.SelectionIndent -= 20;
        }

        private void button_IndentInc_Click(object sender, EventArgs e)
        {
            richTextBox.SelectionIndent += 20;
        }

        private void Update_CheckBox_Status()
        {
            checkBox_Bold.BackColor = checkBox_Bold.Checked ? HIGHLIGHT_COLOR : BACK_COLOR;
            checkBox_Italic.BackColor = checkBox_Italic.Checked ? HIGHLIGHT_COLOR : BACK_COLOR;
            checkBox_Underline.BackColor = checkBox_Underline.Checked ? HIGHLIGHT_COLOR : BACK_COLOR;
            checkBox_Strike.BackColor = checkBox_Strike.Checked ? HIGHLIGHT_COLOR : BACK_COLOR;

            checkBox_AlignLeft.BackColor = checkBox_AlignLeft.Checked ? HIGHLIGHT_COLOR : BACK_COLOR;
            checkBox_AlignCenter.BackColor = checkBox_AlignCenter.Checked ? HIGHLIGHT_COLOR : BACK_COLOR;
            checkBox_AlignRight.BackColor = checkBox_AlignRight.Checked ? HIGHLIGHT_COLOR : BACK_COLOR;
        }

        // ------------------------------------------------------------
        // RichTextBox
        // ------------------------------------------------------------
        private void richTextBox_SelectionChanged(object sender, EventArgs e)
        {
            //Console.WriteLine("richTextBox_SelectionChanged"); // 변화를 툴바에 알린다

            if (richTextBox.SelectionFont == null)
            {
                MessageBox.Show("richTextBox.SelectionFont == null");
                return;
            }

            comboBox_FontSelect.SelectedIndex = m_FontName.IndexOf(richTextBox.SelectionFont.FontFamily.Name);
            comboBox_FontSize.SelectedItem = richTextBox.SelectionFont.Size;
            Console.WriteLine("richTextBox_SelectionChanged -> "
                              + richTextBox.SelectionFont.FontFamily.Name
                              + "["
                              + richTextBox.SelectionFont.Size
                              + "]");

            checkBox_Bold.Checked = richTextBox.SelectionFont.Bold;
            checkBox_Italic.Checked = richTextBox.SelectionFont.Italic;
            checkBox_Underline.Checked = richTextBox.SelectionFont.Underline;
            checkBox_Strike.Checked = richTextBox.SelectionFont.Strikeout;

            button_TextColor.FlatAppearance.BorderColor = richTextBox.SelectionColor;
            button_TextFillColor.FlatAppearance.BorderColor = richTextBox.SelectionBackColor;

            checkBox_AlignLeft.Checked = false;
            checkBox_AlignCenter.Checked = false;
            checkBox_AlignRight.Checked = false;

            switch (richTextBox.SelectionAlignment)
            {
                case HorizontalAlignment.Left:
                    checkBox_AlignLeft.Checked = true;
                    break;
                case HorizontalAlignment.Center:
                    checkBox_AlignCenter.Checked = true;
                    break;
                case HorizontalAlignment.Right:
                    checkBox_AlignRight.Checked = true;
                    break;
            }
        }

        private void richTextBox_TextChanged(object sender, EventArgs e)
        {
            IsUnsaved = true;
        }
    }
}
