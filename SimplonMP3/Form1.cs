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
        private bool dragging   = false;
        private bool firstStart = true;
        private Point startPoint = new Point(0, 0);
        private RecursiveFileSearch search = new RecursiveFileSearch();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mp3ListFiles = search.Main();
            fileLength = Int16.Parse(mp3ListFiles.Count.ToString());
            this.songListContainer.DataSource = mp3ListFiles;
        }

        private void mouseEnterImage_closeApp(object sender, System.EventArgs  e)
        {
            this.closeApp.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\close_button_active.png");
        }

        private void mouseLeaveImage_closeApp(object sender, System.EventArgs e)
        {
            this.closeApp.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\close_button.png");
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
    }
}
