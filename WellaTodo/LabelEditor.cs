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
    public partial class LabelEditor : Form
    {
        private TextBox textBox;

        public LabelEditor()
        {
            InitializeComponent();

            textBox = new TextBox();

            textBox.Dock = DockStyle.Fill;
            textBox.Location = new Point(0, 0);
            textBox.Name = "textBox";
            textBox.Size = new Size(100, 20);
            textBox.TabIndex = 0;
            textBox.KeyDown += new KeyEventHandler(OnKeyDown);

            AutoSize = true;
            ClientSize = new Size(100, 20);
            Controls.Add(textBox);
            FormBorderStyle = FormBorderStyle.None;
            MinimumSize = new Size(100, 20);
            Name = "LabelEditor";
            StartPosition = FormStartPosition.Manual;
        }

        public override string Text
        {
            get
            {
                if (textBox == null)
                    return String.Empty;

                return textBox.Text;
            }
            set
            {
                if (textBox != null)
                {
                    textBox.Text = value;
                    ResizeEditor();
                }
            }
        }

        private void ResizeEditor()
        {
            var size = TextRenderer.MeasureText(textBox.Text, textBox.Font);
            size.Width += 20;

            this.Size = size;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Escape:
                    DialogResult = DialogResult.Cancel;
                    Close();
                    break;
                case Keys.Return:
                    DialogResult = DialogResult.OK;
                    Close();
                    break;
            }
        }
    }
}
