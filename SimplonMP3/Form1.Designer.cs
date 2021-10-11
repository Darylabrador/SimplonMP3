﻿
namespace SimplonMP3
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
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
            this.TitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reduceScreenButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bigScreenButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeApp)).BeginInit();
            this.bottomPlayer.SuspendLayout();
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
            this.appTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.draggingOnMouse_active);
            this.appTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mouseMove);
            this.appTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.draggingOnMouse_remove);
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
            this.TitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.draggingOnMouse_active);
            this.TitleBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mouseMove);
            this.TitleBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.draggingOnMouse_remove);
            // 
            // reduceScreenButton
            // 
            this.reduceScreenButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.reduceScreenButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.reduceScreenButton.Image = ((System.Drawing.Image)(resources.GetObject("reduceScreenButton.Image")));
            this.reduceScreenButton.Location = new System.Drawing.Point(1014, 23);
            this.reduceScreenButton.Name = "reduceScreenButton";
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
            this.bigScreenButton.Image = ((System.Drawing.Image)(resources.GetObject("bigScreenButton.Image")));
            this.bigScreenButton.Location = new System.Drawing.Point(1051, 14);
            this.bigScreenButton.Name = "bigScreenButton";
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
            this.closeApp.Image = ((System.Drawing.Image)(resources.GetObject("closeApp.Image")));
            this.closeApp.Location = new System.Drawing.Point(1091, 14);
            this.closeApp.Name = "closeApp";
            this.closeApp.Size = new System.Drawing.Size(20, 20);
            this.closeApp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.closeApp.TabIndex = 7;
            this.closeApp.TabStop = false;
            this.closeApp.Click += new System.EventHandler(this.closeApp_click);
            // 
            // subtitle
            // 
            this.subtitle.AutoSize = true;
            this.subtitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.subtitle.ForeColor = System.Drawing.Color.White;
            this.subtitle.Location = new System.Drawing.Point(25, 91);
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
            this.songArtiste.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
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
            this.songTitle.Font = new System.Drawing.Font("Segoe UI", 19F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.songTitle.ForeColor = System.Drawing.Color.White;
            this.songTitle.Location = new System.Drawing.Point(25, 14);
            this.songTitle.Name = "songTitle";
            this.songTitle.Size = new System.Drawing.Size(211, 36);
            this.songTitle.TabIndex = 8;
            this.songTitle.Text = "Titre du morceau";
            this.songTitle.Click += new System.EventHandler(this.songTitle_click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(108)))), ((int)(((byte)(108)))));
            this.ClientSize = new System.Drawing.Size(1135, 709);
            this.Controls.Add(this.bottomPlayer);
            this.Controls.Add(this.appTitle);
            this.Controls.Add(this.TitleBar);
            this.Controls.Add(this.subtitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "SimplonMP3";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.draggingOnMouse_active);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.draggingOnMouse_remove);
            this.TitleBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.reduceScreenButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bigScreenButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeApp)).EndInit();
            this.bottomPlayer.ResumeLayout(false);
            this.bottomPlayer.PerformLayout();
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
    }
}

