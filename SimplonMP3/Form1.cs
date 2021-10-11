using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimplonMP3
{
    public partial class Form1 : Form
    {
        private bool dragging = false;
        private Point startPoint = new Point(0, 0);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("loading");
        }

        private void songTitle_click(object sender, EventArgs e)
        {

        }

        private void songArtiste_click(object sender, EventArgs e)
        {

        }

        private void closeApp_click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bigScreen_click(object sender, EventArgs e)
        {
            // System.Diagnostics.Debug.WriteLine("big screen handler");
            // if (this.WindowState == FormWindowState.Normal) this.WindowState = FormWindowState.Maximized;
            // else this.WindowState = FormWindowState.Normal;
        }

        private void reduceScreen_click(object sender, EventArgs e)
        {
            // System.Diagnostics.Debug.WriteLine("reduce screen handler");
            this.WindowState = FormWindowState.Minimized;
        }

        private void draggingOnMouse_active(object sender, MouseEventArgs e)
        {
            dragging = true;
            startPoint = new Point(e.X, e.Y);
        }

        private void draggingOnMouse_remove(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void mouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this.startPoint.X, p.Y - this.startPoint.Y);
            }
        }
    }
}
