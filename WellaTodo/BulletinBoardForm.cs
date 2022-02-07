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
    public partial class BulletinBoardForm : Form, IView, IModelObserver
    {
        public event ViewHandler<IView> View_Changed_Event;

        MainController m_Controller;

        Post_it note = new Post_it();

        public BulletinBoardForm()
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

        private void BulletinBoardForm_Load(object sender, EventArgs e)
        {
            Initiate();
        }

        private void Initiate()
        {
            panel_Bulletin.Controls.Add(note);
        }
    }
}
