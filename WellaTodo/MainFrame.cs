// copyright honeysoft v0.14

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
    public partial class MainFrame : Form, IView
    {
        IController m_Controller;

        public MainFrame()
        {
            Console.WriteLine(">MainFrame Construction");
            InitializeComponent();
        }

        public void setController(IController controller)
        {
            Console.WriteLine(">MainFrame::setController");
            m_Controller = controller;
        }

        private void splitContainer1_SplitterMoving(object sender, SplitterCancelEventArgs e)
        {

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void splitContainer1_Resize(object sender, EventArgs e)
        {
            Console.WriteLine(">SplitContainer::Resized");
            label1.Width = splitContainer1.SplitterDistance;
            label2.Width = splitContainer1.SplitterDistance;
            label3.Width = splitContainer1.SplitterDistance;
            label4.Width = splitContainer1.SplitterDistance;
            label5.Width = splitContainer1.SplitterDistance;
            label6.Width = splitContainer1.SplitterDistance;

            splitContainer1.Refresh();
        }

        private void splitContainer1_MouseDown(object sender, MouseEventArgs e)
        {
            splitContainer1.IsSplitterFixed = true;
        }

        private void splitContainer1_MouseUp(object sender, MouseEventArgs e)
        {
            splitContainer1.IsSplitterFixed = false;
        }

        private void splitContainer1_MouseMove(object sender, MouseEventArgs e)
        {
            if (splitContainer1.IsSplitterFixed )
            {
                if (e.Button.Equals(MouseButtons.Left))
                {
                    if (splitContainer1.Orientation.Equals(Orientation.Vertical))
                    {
                        if (e.X > 0 && e.X < (splitContainer1.Width))
                        {
                            splitContainer1.SplitterDistance = e.X;

                            label1.Width = splitContainer1.SplitterDistance;
                            label2.Width = splitContainer1.SplitterDistance;
                            label3.Width = splitContainer1.SplitterDistance;
                            label4.Width = splitContainer1.SplitterDistance;
                            label5.Width = splitContainer1.SplitterDistance;
                            label6.Width = splitContainer1.SplitterDistance;

                            splitContainer1.Refresh();
                        }
                    }
                } else
                {
                    splitContainer1.IsSplitterFixed = false;
                }
            }
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            label1.Font = new Font(label1.Font, FontStyle.Underline);
            label1.BackColor = Color.FromArgb(235, 170, 170);
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.Font = new Font(label1.Font, FontStyle.Regular);
            label1.BackColor = Color.FromArgb(255, 190, 190);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Console.WriteLine(">Label_1::clicked");
        }
    }
}

// int yourLabelNameWidth = TextRenderer.MeasureText(yourLabelName.Text, yourLabelName.Font).Width;
// yourSplitContainerName.SplitterDistance = yourLabelName.Left + yourLabelNameWidth;
// yourLabelName.Width = yourLabelName.Left + yourLabelNameWidth;


/*
 * //assign this to the SplitContainer's MouseDown event
    private void splitCont_MouseDown(object sender, MouseEventArgs e)
    {
        // This disables the normal move behavior
        ((SplitContainer)sender).IsSplitterFixed = true;
    }

    //assign this to the SplitContainer's MouseUp event
    private void splitCont_MouseUp(object sender, MouseEventArgs e)
    {
        // This allows the splitter to be moved normally again
        ((SplitContainer)sender).IsSplitterFixed = false;
    }

    //assign this to the SplitContainer's MouseMove event
    private void splitCont_MouseMove(object sender, MouseEventArgs e)
    {
        // Check to make sure the splitter won't be updated by the
        // normal move behavior also
        if (((SplitContainer)sender).IsSplitterFixed)
        {
            // Make sure that the button used to move the splitter
            // is the left mouse button
            if (e.Button.Equals(MouseButtons.Left))
            {
                // Checks to see if the splitter is aligned Vertically
                if (((SplitContainer)sender).Orientation.Equals(Orientation.Vertical))
                {
                    // Only move the splitter if the mouse is within
                    // the appropriate bounds
                    if (e.X > 0 && e.X < ((SplitContainer)sender).Width)
                    {
                        // Move the splitter & force a visual refresh
                        ((SplitContainer)sender).SplitterDistance = e.X;
                        ((SplitContainer)sender).Refresh();
                    }
                }
                // If it isn't aligned vertically then it must be
                // horizontal
                else
                {
                    // Only move the splitter if the mouse is within
                    // the appropriate bounds
                    if (e.Y > 0 && e.Y < ((SplitContainer)sender).Height)
                    {
                        // Move the splitter & force a visual refresh
                        ((SplitContainer)sender).SplitterDistance = e.Y;
                        ((SplitContainer)sender).Refresh();
                    }
                }
            }
            // If a button other than left is pressed or no button
            // at all
            else
            {
                // This allows the splitter to be moved normally again
                ((SplitContainer)sender).IsSplitterFixed = false;
            }
        }
    }
 * 
 * 
 */
