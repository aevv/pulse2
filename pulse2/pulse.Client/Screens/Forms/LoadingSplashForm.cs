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
        private double _currentProgress;

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
            Task.Factory.StartNew(() => _loader.LoadStaticContent(UpdateProgress)).ContinueWith(task => Invoke(new MethodInvoker(Close)));
        }

        private void UpdateProgress(double progress)
        {
            _currentProgress = progress;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var screenBounds = Screen.FromControl(this).Bounds;
            e.Graphics.DrawImage(Image.FromFile("Assets/bg.jpg"), new Point(screenBounds.Width / 2 - (1024 / 2), screenBounds.Height / 2 - (768 / 2)));
            // TODO: This much better.
            e.Graphics.DrawString(string.Format("{0}%", (int)_currentProgress), new Font("Roboto", 20), new SolidBrush(Color.White),
                new PointF((screenBounds.Width / 2) - 200, (screenBounds.Height / 2) + 200));
            Invalidate();
        }
    }
}
