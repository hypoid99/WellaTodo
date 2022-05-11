using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing.Printing;
using System.IO;

namespace WellaTodo
{
    public partial class NotePadEditorForm : Form
    {
        static readonly Color BACK_COLOR = Color.White;
        static readonly Color HIGHLIGHT_COLOR = Color.LightCyan;
        static readonly Color SELECTED_COLOR = Color.Cyan;

        static readonly string FONT_NAME = "맑은 고딕";
        static readonly float FONT_SIZE_TEXT = 14.0f;
        static readonly int MAX_COUNT_FONT_SIZE = 16;

        List<string> m_FontName = new List<string>();
        List<float> m_FontSize = new List<float>();

        private string m_FileName;

        int m_linesPrinted;
        string[] m_Printlines;

        private string OpenedDocumentPath { get; set; } = "NoName";
        //public string DefaultSaveDirectory { get; set; } = "c:\\";
        public string DefaultSaveDirectory { get; set; }

        // --------------------------------------------------------------------
        // Properties
        // --------------------------------------------------------------------
        private string _note_RTF;
        public string Note_RTF 
        { 
            get => _note_RTF = richTextBox.Rtf;
            set => richTextBox.Rtf = _note_RTF = value;
        }

        private bool isUnsaved = false;
        public bool IsUnsaved
        {
            get => isUnsaved;
            set
            {
                isUnsaved = value;
                Update_Status();
            }
        }

        public bool IsOpened { get; set; } = false;

        // --------------------------------------------------------------------
        // Constructor
        // --------------------------------------------------------------------
        public NotePadEditorForm()
        {
            InitializeComponent();
        }

        // --------------------------------------------------------------------
        // Form 이벤트
        // --------------------------------------------------------------------
        private void NotePadForm_Load(object sender, EventArgs e)
        {
            Initiate();
        }

        private void NotePadForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsUnsaved)
            {
                DialogResult savePrompt = MessageBox.Show("저장할까요?", "NotePad Editor", MessageBoxButtons.YesNoCancel);

                switch (savePrompt)
                {
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        return;
                    case DialogResult.No:
                        this.DialogResult = DialogResult.No;
                        break;
                    case DialogResult.Yes:
                        this.DialogResult = DialogResult.Yes;
                        break;
                }
            }
        }

        //--------------------------------------------------------------
        // 초기화 및 데이터 로딩, Update Display
        //--------------------------------------------------------------
        private void Initiate()
        {
            // IME(입력기) 및 아시아 언어 지원
            richTextBox.LanguageOption = 0;
            richTextBox.SelectionCharOffset = 0;
            richTextBox.WordWrap = false;

            // 폰트를 등록한다
            if (m_FontName.Count > 0) return;

            foreach (FontFamily font in FontFamily.Families)
            {
                m_FontName.Add(font.Name);
            }

            comboBox_FontSelect.DataSource = m_FontName;

            int cnt = 0;
            for (int i = 0; i < m_FontName.Count; i++)
            {
                if (m_FontName[i] == FONT_NAME)
                {
                    // Default 폰트가 있는지 확인한다
                    comboBox_FontSelect.SelectedIndex = i;
                    cnt++;
                }
            }
            if (cnt == 0)
            {
                // Default 폰트가 없으면 2번째를 선택한다
                comboBox_FontSelect.SelectedIndex = 2;
            }

            // 폰트가 사이즈를 등록한다
            m_FontSize.Add(6);
            m_FontSize.Add(8);
            m_FontSize.Add(9);
            m_FontSize.Add(10);
            m_FontSize.Add(11);
            m_FontSize.Add(12);
            m_FontSize.Add(14);
            m_FontSize.Add(16);
            m_FontSize.Add(18);
            m_FontSize.Add(20);
            m_FontSize.Add(22);
            m_FontSize.Add(24);
            m_FontSize.Add(26);
            m_FontSize.Add(28);
            m_FontSize.Add(36);
            m_FontSize.Add(48);
            m_FontSize.Add(72);

            comboBox_FontSize.DataSource = m_FontSize;
            comboBox_FontSize.SelectedIndex = 4;

            checkBox_AlignLeft.Click += new EventHandler(checkBox_TextAlign_Click);
            checkBox_AlignCenter.Click += new EventHandler(checkBox_TextAlign_Click);
            checkBox_AlignRight.Click += new EventHandler(checkBox_TextAlign_Click);

            string fontName = comboBox_FontSelect.SelectedItem.ToString();
            float fontSize = (float)comboBox_FontSize.SelectedItem;
            FontStyle fontStyle = (checkBox_Bold.Checked ? FontStyle.Bold : 0)
                                 | (checkBox_Italic.Checked ? FontStyle.Italic : 0)
                                 | (checkBox_Underline.Checked ? FontStyle.Underline : 0)
                                 | (checkBox_Strike.Checked ? FontStyle.Strikeout : 0);

            richTextBox.SelectionFont = new Font(fontName, fontSize, fontStyle);

            Update_Status();
        }

        private void UpdatePath()
        {
            m_FileName = $"NotePad - {(IsUnsaved ? "Unsaved -" : "")}{OpenedDocumentPath}";
            Text = m_FileName;
        }

        private void Update_Status()
        {
            if (IsUnsaved)
            {
                Text = "NotePad - 저장안됨";
            }
            else
            {
                Text = "NotePad";
            }
        }

        //--------------------------------------------------------------
        // Command 처리
        //--------------------------------------------------------------
        private void Open_File()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = DefaultSaveDirectory;
                openFileDialog.Filter = "Documentation (*.rtf;*.txt)|*.rtf;*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK && openFileDialog.FileName.Length > 0)
                {
                    OpenedDocumentPath = openFileDialog.FileName;
                    try
                    {
                        if (OpenedDocumentPath.EndsWith(".rtf"))
                        {
                            richTextBox.LoadFile(OpenedDocumentPath);
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
                    IsOpened = true;
                    IsUnsaved = false;
                    UpdatePath();
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
                }
            }
        }

        private void Save_Data()
        {

        }

        // ------------------------------------------------------------
        // 툴바
        // ------------------------------------------------------------
        private void button_Save_Click(object sender, EventArgs e)
        {
            Save_Data();
        }

        private void button_Open_File_Click(object sender, EventArgs e)
        {
            Open_File();
            IsUnsaved = true;
        }

        private void button_Save_File_Click(object sender, EventArgs e)
        {
            Save_As_File();
            IsUnsaved = true;
        }

        private void button_Print_Click(object sender, EventArgs e)
        {
            printDialog1.Document = printDocument1;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
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
            //Console.WriteLine("Font Name : " + fontName);
        }

        private void comboBox_FontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            string fontName = comboBox_FontSelect.SelectedItem.ToString();
            float fontSize = (float)comboBox_FontSize.SelectedItem;
            FontStyle fontStyle = (checkBox_Bold.Checked ? FontStyle.Bold : 0)
                                 | (checkBox_Italic.Checked ? FontStyle.Italic : 0)
                                 | (checkBox_Underline.Checked ? FontStyle.Underline : 0)
                                 | (checkBox_Strike.Checked ? FontStyle.Strikeout : 0);

            richTextBox.SelectionFont = new Font(fontName, fontSize, fontStyle);
            //Console.WriteLine("Font Name : " + fontName);
        }

        private void button_FontSizeUp_Click(object sender, EventArgs e)
        {
            int counter = (int)comboBox_FontSize.SelectedIndex;
            counter++;
            if (counter > MAX_COUNT_FONT_SIZE) counter = MAX_COUNT_FONT_SIZE;
            comboBox_FontSize.SelectedIndex = counter;
        }

        private void button_FontSizeDown_Click(object sender, EventArgs e)
        {
            int counter = (int)comboBox_FontSize.SelectedIndex;
            counter--;
            if (counter < 0) counter = 0;
            comboBox_FontSize.SelectedIndex = counter;
        }

        private void checkBox_Bold_CheckedChanged(object sender, EventArgs e)
        {
            //Console.WriteLine("checkBox_Bold_CheckedChanged : " + checkBox_Bold.Checked);
            //Console.WriteLine("checkBox_Bold_CheckedChanged -> font size : " + richTextBox.SelectionFont.Size);
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
            //Console.WriteLine("checkBox_Italic_CheckedChanged : " + checkBox_Italic.Checked);

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
            //Console.WriteLine("checkBox_Underline_CheckedChanged : " + checkBox_Underline.Checked);

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
            //Console.WriteLine("checkBox_Strike_CheckedChanged : " + checkBox_Strike.Checked);

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
        // 버튼의 상태를 나타내기 위함 (볼드,이탤릭 등)
        private void richTextBox_SelectionChanged(object sender, EventArgs e) 
        {
            if (richTextBox.SelectionFont == null)
            {
                // If the current text selection has more than one font specified, this property is null
                //Console.WriteLine("richTextBox.SelectionFont == null");
                return;
            }

            comboBox_FontSelect.SelectedIndex = m_FontName.IndexOf(richTextBox.SelectionFont.FontFamily.Name);
            comboBox_FontSize.SelectedItem = richTextBox.SelectionFont.Size;

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

        private void richTextBox_Leave(object sender, EventArgs e)
        {

        }

        // ------------------------------------------------------------
        // 프린트 처리
        // ------------------------------------------------------------
        private void 미리보기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.StartPosition = FormStartPosition.CenterParent;
            printPreviewDialog1.Document = printDocument1;
            //printPreviewDialog1.ClientSize = new Size(this.Width, this.Height);
            printPreviewDialog1.MinimumSize = new Size(800, 600);
            printPreviewDialog1.UseAntiAlias = true;

            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_BeginPrint_1(object sender, PrintEventArgs e)
        {
            char[] param = { '\n' };
            if (printDocument1.PrinterSettings.PrintRange == PrintRange.Selection)
            {
                m_Printlines = richTextBox.SelectedText.Split(param);
            }
            else
            {
                m_Printlines = richTextBox.Text.Split(param);
            }
            int i = 0;
            char[] trimParam = { '\r' };
            foreach (string s in m_Printlines)
            {
                m_Printlines[i++] = s.TrimEnd(trimParam);
            }
        }

        private void printDocument1_PrintPage_1(object sender, PrintPageEventArgs e)
        {
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            Brush brush = new SolidBrush(richTextBox.ForeColor);

            while (m_linesPrinted < m_Printlines.Length)
            {
                e.Graphics.DrawString(m_Printlines[m_linesPrinted++],
                    richTextBox.Font, brush, x, y);
                y += 15;
                if (y >= e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }
            }

            m_linesPrinted = 0;
            e.HasMorePages = false;
        }
    }
}
