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
    public partial class Popup : ToolStripDropDown
    {
        public Control Content { get; private set; }
        private ToolStripControlHost _host;
        private Control _opener;
        private Popup _ownerPopup;
        private Popup _childPopup;

        public Popup()
        {
            InitializeComponent();
        }

        public Popup(Control content)
        {
            InitializeComponent();

            if (content == null)
            {
                throw new ArgumentNullException("content");
            }
            Content = content;

            AutoSize = false;
            DoubleBuffered = true;
            ResizeRedraw = true;

            _host = new ToolStripControlHost(content);

            Padding = Margin = _host.Padding = _host.Margin = Padding.Empty;

            //if (NativeMethods.IsRunningOnMono) content.Margin = Padding.Empty;

            MinimumSize = content.MinimumSize;
            content.MinimumSize = content.Size;

            MaximumSize = content.MaximumSize;
            content.MaximumSize = content.Size;

            Size = content.Size;

            //if (NativeMethods.IsRunningOnMono) _host.Size = content.Size;

            TabStop = content.TabStop = true;

            content.Location = Point.Empty;

            Items.Add(_host);

            content.Disposed += (sender, e) =>
            {
                content = null;
                Dispose(true);
            };

            content.RegionChanged += (sender, e) => UpdateRegion();
            //content.Paint += (sender, e) => PaintSizeGrip(e);

            UpdateRegion();
        }

        protected void UpdateRegion()
        {
            if (Region != null)
            {
                Region.Dispose();
                Region = null;
            }
            if (Content.Region != null)
            {
                Region = Content.Region.Clone();
            }
        }

        public void Show(Control control)
        {
            Console.WriteLine("Popup::Show(Control control)");
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }
            Show(control, control.ClientRectangle);
        }

        public void Show(Control control, Rectangle area)
        {
            Console.WriteLine("Popup::Show(Control control, Rectangle area)");
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }
            SetOwnerItem(control);

            //_resizableTop = _resizableLeft = false;
            Point location = control.PointToScreen(new Point(area.Left, area.Top + area.Height));
            Rectangle screen = Screen.FromControl(control).WorkingArea;
            if (location.X + Size.Width > (screen.Left + screen.Width))
            {
                location.X = (screen.Left + screen.Width) - Size.Width;
            }
            if (location.Y + Size.Height > (screen.Top + screen.Height))
            {
                location.Y -= Size.Height + area.Height;
            }
            location = control.PointToClient(location);

            Show(control, location, ToolStripDropDownDirection.BelowRight);
        }

        private void SetOwnerItem(Control control)
        {
            if (control == null)
            {
                return;
            }
            if (control is Popup)
            {
                Popup popupControl = control as Popup;
                _ownerPopup = popupControl;
                _ownerPopup._childPopup = this;
                OwnerItem = popupControl.Items[0];
                return;
            }
            else if (_opener == null)
            {
                _opener = control;
            }
            if (control.Parent != null)
            {
                SetOwnerItem(control.Parent);
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
