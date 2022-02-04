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
    public partial class NotePadForm : Form, IView, IModelObserver
    {
        public event ViewHandler<IView> View_Changed_Event;

        MainController m_Controller;

        List<string> m_FontName = new List<string>();
        List<float> m_FontSize = new List<float>();

        public NotePadForm()
        {
            InitializeComponent();

            Initiate();
        }

        public void SetController(MainController controller)
        {
            m_Controller = controller;
        }

        public void Update_View(IModel m, ModelEventArgs e)
        {

        }

        private void Initiate()
        {
            FontFamily[] fontList = new System.Drawing.Text.InstalledFontCollection().Families;

            foreach (var item in fontList)
            {
                m_FontName.Add(item.Name);
            }

            comboBox_FontSelect.DataSource = m_FontName;
            comboBox_FontSelect.SelectedIndex = 10;

            for (int i = 1; i < 50; i++)
            {
                m_FontSize.Add(i);
            }

            comboBox_FontSize.DataSource = m_FontSize;
            comboBox_FontSize.SelectedIndex = 10;
        }

        // ------------------------------------------------------------
        // 메뉴
        // ------------------------------------------------------------
        private void 새로만들기ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        // ------------------------------------------------------------
        // 툴바
        // ------------------------------------------------------------

    }
}
