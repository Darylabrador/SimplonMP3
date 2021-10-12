using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace SimplonMP3
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private List<Mp3File> mp3ListFiles = new List<Mp3File>();
        private Mp3File selectedSong = null;
        private String execPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

        private String titreMorceau   = "Titre du morceau";
        // private String artisteMorceau = "Artiste";

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void mouseEnterImage_closeApp(object sender, System.EventArgs  e)
        {
            this.closeApp.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\close_button_active.png");
        }

        private void mouseLeaveImage_closeApp(object sender, System.EventArgs e)
        {
            this.closeApp.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\close_button.png");
        }

        private void syncSongs(object sender, EventArgs e)
        {
            this.songTitle.Text = "Titre du morceau";
            this.songListContainer.SelectedIndex = 0;
            mp3ListFiles = null;
            mp3ListFiles = search.Main();
        }

        private void songListContainer_SelectedValueChanged(object sender, EventArgs e)
        {
            if(selectedSong != null)
            {
                selectedSong.stopPlayer();
            }

            int indexVal = this.songListContainer.SelectedIndex;
            selectedSong = mp3ListFiles[indexVal];
            titreMorceau = selectedSong.Name;
            this.songTitle.Text = titreMorceau;
            selectedSong.startPlayer();
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent(List<Mp3File> files)
        {
            mp3ListFiles = files;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.appTitle = new System.Windows.Forms.Label();
            this.TitleBar = new System.Windows.Forms.Panel();
            this.reduceScreenButton = new System.Windows.Forms.PictureBox();
            this.bigScreenButton = new System.Windows.Forms.PictureBox();
            this.closeApp = new System.Windows.Forms.PictureBox();
            this.subtitle = new System.Windows.Forms.Label();
            this.bottomPlayer = new System.Windows.Forms.Panel();
            this.songArtiste = new System.Windows.Forms.Label();
            this.songTitle = new System.Windows.Forms.Label();
            this.musicContainer = new System.Windows.Forms.Panel();
            this.songListContainer = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.artisteFilterContainer = new System.Windows.Forms.Panel();
            this.songFilterContainer = new System.Windows.Forms.Panel();
            this.syncButton = new System.Windows.Forms.PictureBox();
            this.TitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reduceScreenButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bigScreenButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeApp)).BeginInit();
            this.bottomPlayer.SuspendLayout();
            this.musicContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.syncButton)).BeginInit();
            this.SuspendLayout();
            // 
            // appTitle
            // 
            this.appTitle.AutoSize = true;
            this.appTitle.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.appTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.appTitle.ForeColor = System.Drawing.Color.White;
            this.appTitle.Location = new System.Drawing.Point(25, 15);
            this.appTitle.Name = "appTitle";
            this.appTitle.Size = new System.Drawing.Size(98, 20);
            this.appTitle.TabIndex = 5;
            this.appTitle.Text = "SimplonMP3";
            this.appTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dragging_mouseClick);
            this.appTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mouseMove);
            this.appTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dragging_mouseNotClik);
            // 
            // TitleBar
            // 
            this.TitleBar.AutoSize = true;
            this.TitleBar.BackColor = System.Drawing.Color.Black;
            this.TitleBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TitleBar.Controls.Add(this.reduceScreenButton);
            this.TitleBar.Controls.Add(this.bigScreenButton);
            this.TitleBar.Controls.Add(this.closeApp);
            this.TitleBar.Location = new System.Drawing.Point(0, 0);
            this.TitleBar.Name = "TitleBar";
            this.TitleBar.Size = new System.Drawing.Size(1136, 52);
            this.TitleBar.TabIndex = 1;
            this.TitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dragging_mouseClick);
            this.TitleBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mouseMove);
            this.TitleBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dragging_mouseNotClik);
            // 
            // reduceScreenButton
            // 
            this.reduceScreenButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.reduceScreenButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.reduceScreenButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.reduceScreenButton.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\reduce_button.png");
            this.reduceScreenButton.Location = new System.Drawing.Point(1014, 23);
            this.reduceScreenButton.Name = "reduceScreenButton";
            this.reduceScreenButton.Padding = new System.Windows.Forms.Padding(2);
            this.reduceScreenButton.Size = new System.Drawing.Size(20, 10);
            this.reduceScreenButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.reduceScreenButton.TabIndex = 9;
            this.reduceScreenButton.TabStop = false;
            this.reduceScreenButton.Click += new System.EventHandler(this.reduceScreen_click);
            // 
            // bigScreenButton
            // 
            this.bigScreenButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.bigScreenButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.bigScreenButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bigScreenButton.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\big_screen_button.png");
            this.bigScreenButton.Location = new System.Drawing.Point(1051, 14);
            this.bigScreenButton.Name = "bigScreenButton";
            this.bigScreenButton.Padding = new System.Windows.Forms.Padding(2);
            this.bigScreenButton.Size = new System.Drawing.Size(20, 20);
            this.bigScreenButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.bigScreenButton.TabIndex = 8;
            this.bigScreenButton.TabStop = false;
            this.bigScreenButton.Click += new System.EventHandler(this.bigScreen_click);
            // 
            // closeApp
            // 
            this.closeApp.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.closeApp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.closeApp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closeApp.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\close_button.png");
            this.closeApp.Location = new System.Drawing.Point(1091, 14);
            this.closeApp.Name = "closeApp";
            this.closeApp.Padding = new System.Windows.Forms.Padding(2);
            this.closeApp.Size = new System.Drawing.Size(20, 20);
            this.closeApp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.closeApp.TabIndex = 7;
            this.closeApp.TabStop = false;
            this.closeApp.Click += new System.EventHandler(this.closeApp_click);
            this.closeApp.MouseEnter += new System.EventHandler(this.mouseEnterImage_closeApp);
            this.closeApp.MouseLeave += new System.EventHandler(this.mouseLeaveImage_closeApp);
            // 
            // subtitle
            // 
            this.subtitle.AutoSize = true;
            this.subtitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.subtitle.ForeColor = System.Drawing.Color.White;
            this.subtitle.Location = new System.Drawing.Point(25, 77);
            this.subtitle.Name = "subtitle";
            this.subtitle.Size = new System.Drawing.Size(152, 25);
            this.subtitle.TabIndex = 0;
            this.subtitle.Text = "Ma bibliothèque";
            // 
            // bottomPlayer
            // 
            this.bottomPlayer.BackColor = System.Drawing.Color.Black;
            this.bottomPlayer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bottomPlayer.Controls.Add(this.songArtiste);
            this.bottomPlayer.Controls.Add(this.songTitle);
            this.bottomPlayer.Location = new System.Drawing.Point(0, 609);
            this.bottomPlayer.Name = "bottomPlayer";
            this.bottomPlayer.Size = new System.Drawing.Size(1136, 101);
            this.bottomPlayer.TabIndex = 6;
            // 
            // songArtiste
            // 
            this.songArtiste.AutoSize = true;
            this.songArtiste.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.songArtiste.ForeColor = System.Drawing.Color.White;
            this.songArtiste.Location = new System.Drawing.Point(27, 57);
            this.songArtiste.Name = "songArtiste";
            this.songArtiste.Size = new System.Drawing.Size(71, 25);
            this.songArtiste.TabIndex = 9;
            this.songArtiste.Text = "Artiste";
            this.songArtiste.Click += new System.EventHandler(this.songArtiste_click);
            // 
            // songTitle
            // 
            this.songTitle.AutoSize = true;
            this.songTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.songTitle.ForeColor = System.Drawing.Color.White;
            this.songTitle.Location = new System.Drawing.Point(25, 14);
            this.songTitle.Name = "songTitle";
            this.songTitle.Size = new System.Drawing.Size(211, 36);
            this.songTitle.TabIndex = 8;
            this.songTitle.Text = titreMorceau;
            this.songTitle.Click += new System.EventHandler(this.songTitle_click);
            // 
            // musicContainer
            // 
            this.musicContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.musicContainer.Controls.Add(this.songListContainer);
            this.musicContainer.Controls.Add(this.panel1);
            this.musicContainer.Location = new System.Drawing.Point(25, 132);
            this.musicContainer.Name = "musicContainer";
            this.musicContainer.Size = new System.Drawing.Size(1087, 456);
            this.musicContainer.TabIndex = 7;
            // 
            // songListContainer
            // 
            this.songListContainer.DataSource = files;
            this.songListContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.songListContainer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.songListContainer.DisplayMember = "Name";
            this.songListContainer.ForeColor = System.Drawing.Color.White;
            this.songListContainer.FormattingEnabled = true;
            this.songListContainer.ItemHeight = 15;
            this.songListContainer.Location = new System.Drawing.Point(17, 77);
            this.songListContainer.Name = "songListContainer";
            this.songListContainer.Size = new System.Drawing.Size(1051, 345);
            this.songListContainer.TabIndex = 1;
            this.songListContainer.ValueMember = "Name";
            this.songListContainer.SelectedValueChanged += new System.EventHandler(this.songListContainer_SelectedValueChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(17, 51);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1051, 3);
            this.panel1.TabIndex = 0;
            // 
            // artisteFilterContainer
            // 
            this.artisteFilterContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.artisteFilterContainer.Location = new System.Drawing.Point(584, 73);
            this.artisteFilterContainer.Name = "artisteFilterContainer";
            this.artisteFilterContainer.Size = new System.Drawing.Size(227, 39);
            this.artisteFilterContainer.TabIndex = 8;
            // 
            // songFilterContainer
            // 
            this.songFilterContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.songFilterContainer.Location = new System.Drawing.Point(832, 73);
            this.songFilterContainer.Name = "songFilterContainer";
            this.songFilterContainer.Size = new System.Drawing.Size(280, 39);
            this.songFilterContainer.TabIndex = 9;
            // 
            // syncButton
            // 
            this.syncButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(108)))), ((int)(((byte)(108)))));
            this.syncButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.syncButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.syncButton.Image = ((System.Drawing.Image)(resources.GetObject("syncButton.Image")));
            this.syncButton.Location = new System.Drawing.Point(179, 76);
            this.syncButton.Name = "syncButton";
            this.syncButton.Padding = new System.Windows.Forms.Padding(2);
            this.syncButton.Size = new System.Drawing.Size(31, 30);
            this.syncButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.syncButton.TabIndex = 10;
            this.syncButton.TabStop = false;
            this.syncButton.Click += new System.EventHandler(this.syncSongs);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(108)))), ((int)(((byte)(108)))));
            this.ClientSize = new System.Drawing.Size(1135, 709);
            this.Controls.Add(this.syncButton);
            this.Controls.Add(this.songFilterContainer);
            this.Controls.Add(this.artisteFilterContainer);
            this.Controls.Add(this.musicContainer);
            this.Controls.Add(this.bottomPlayer);
            this.Controls.Add(this.appTitle);
            this.Controls.Add(this.TitleBar);
            this.Controls.Add(this.subtitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "SimplonMP3";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dragging_mouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dragging_mouseNotClik);
            this.TitleBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.reduceScreenButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bigScreenButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeApp)).EndInit();
            this.bottomPlayer.ResumeLayout(false);
            this.bottomPlayer.PerformLayout();
            this.musicContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.syncButton)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion
        private System.Windows.Forms.Label appTitle;
        private System.Windows.Forms.Panel TitleBar;
        private System.Windows.Forms.Label subtitle;
        private System.Windows.Forms.Panel bottomPlayer;
        private System.Windows.Forms.Label songTitle;
        private System.Windows.Forms.Label songArtiste;
        private System.Windows.Forms.PictureBox closeApp;
        private System.Windows.Forms.PictureBox reduceScreenButton;
        private System.Windows.Forms.PictureBox bigScreenButton;
        private System.Windows.Forms.Panel musicContainer;
        private System.Windows.Forms.Panel artisteFilterContainer;
        private System.Windows.Forms.Panel songFilterContainer;
        private System.Windows.Forms.PictureBox syncButton;
        private System.Windows.Forms.Panel panel1;
        private ListBox songListContainer;
    }
}

