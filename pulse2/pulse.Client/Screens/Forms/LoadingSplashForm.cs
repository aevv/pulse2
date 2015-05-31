using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using pulse.Client.Songs;

namespace pulse.Client.Screens.Forms
{
    public partial class LoadingSplashForm : Form
    {
        private readonly PulseLoader _loader;
        public LoadingSplashForm(PulseLoader loader)
        {
            InitializeComponent();

            TopMost = true;
            DoubleBuffered = true;
            ShowInTaskbar = false;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            BackColor = Color.FromArgb(255, 0, 255);
            TransparencyKey = Color.FromArgb(255, 0, 255);

            _loader = loader;
        }

        protected override void OnLoad(EventArgs e)
        {
            Task.Factory.StartNew(_loader.LoadStaticContent).ContinueWith(task => Invoke(new MethodInvoker(Close)));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var screenBounds = Screen.FromControl(this).Bounds;
            e.Graphics.DrawImage(Image.FromFile("Assets/bg.jpg"), new Point(screenBounds.Width / 2 - (1024 / 2), screenBounds.Height / 2 - (768 / 2)));
            Invalidate();
        }
    }
}
