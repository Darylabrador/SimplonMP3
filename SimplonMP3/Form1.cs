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
        private RecursiveFileSearch search = new RecursiveFileSearch();
        List<Mp3File> files = new List<Mp3File>();
        public Form1()
        {
            files = search.Main();
            InitializeComponent(files);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
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
            // if (this.WindowState == FormWindowState.Normal) this.WindowState = FormWindowState.Maximized;
            // else this.WindowState = FormWindowState.Normal;
        }

        private void reduceScreen_click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void dragging_mouseClick(object sender, MouseEventArgs e)
        {
            dragging = true;
            startPoint = new Point(e.X, e.Y);
        }

        private void dragging_mouseNotClik(object sender, MouseEventArgs e)
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

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
