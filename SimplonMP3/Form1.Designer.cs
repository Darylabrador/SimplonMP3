using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Timers;
using WMPLib;

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
        private int selectedSongIndex;
        private String execPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
        private String titreMorceau = "Titre du morceau";
        private Boolean isReading   = false;
        private Boolean isRandom   = false;
        private Boolean isRepeat = false;
        private String artisteMorceau = "Artiste";
        private int fileLength = 0;
        WMPLib.WindowsMediaPlayer wplayer = null;
        double time = 0.0;

        private static System.Timers.Timer aTimer;

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

        public void setRandomSong()
        {
            var rand = new Random();
            int randIndex = rand.Next(mp3ListFiles.Count - 1);
            selectedSong = mp3ListFiles[randIndex];
            this.songListContainer.SelectedIndex = randIndex;
            selectedSongIndex = randIndex;
        }

        public void startPlayer()
        {
            wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.PlayStateChange += new WMPLib._WMPOCXEvents_PlayStateChangeEventHandler(player_PlayStateChange);
            wplayer.settings.setMode("loop", false);
            wplayer.URL = selectedSong.Path;
            if (time != 0.0)
            {
                wplayer.controls.currentPosition = time;
            }
            wplayer.controls.play();
            startTimer();
        }

        public void pausePlayer()
        {
            wplayer.controls.pause();
            time = wplayer.controls.currentPosition;
        }

        public void repeatPlayer(object sender, EventArgs e)
        {
            if(isRepeat)
            {
                wplayer.settings.setMode("loop", false);
                isRepeat = false;
                this.repeatSongBottom.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\repeat_button.png");
            }
            else
            {
                isRepeat = true;
                wplayer.settings.setMode("loop", true);
                this.repeatSongBottom.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\repeat_button_active.png");

                isRandom = false;
                this.randomSongBottom.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\random_button.png");
            }
        }

        public void randomPlayer(object sender, EventArgs e)
        {
            if(isRandom)
            {
                isRandom = false;
                this.randomSongBottom.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\random_button.png");
            } else
            {
                isRandom = true;
                this.randomSongBottom.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\random_button_active.png");

                isRepeat = false;
                wplayer.settings.setMode("loop", false);
                this.repeatSongBottom.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\repeat_button.png");
            }
        }

        public void stopPlayer()
        {
            if (wplayer != null)
            {
                wplayer.close();
            }
        }

        private void songListContainer_SelectedValueChanged(object sender, EventArgs e)
        {
            if(selectedSong != null)
            {
                stopPlayer();
            }
            selectedSong = null;
            int indexVal = this.songListContainer.SelectedIndex;
            selectedSongIndex = indexVal;
            selectedSong = mp3ListFiles[indexVal];
            titreMorceau = selectedSong.Title;
            artisteMorceau = selectedSong.Artiste;
            this.songArtiste.Text = artisteMorceau;

            if(titreMorceau == null)
            {
                this.songTitle.Text = selectedSong.Name;
            }
            else
            {
                this.songTitle.Text = titreMorceau;
            }

            startPlayer();
            isReading = true;
        }


        private void togglePlay(object sender, EventArgs e)
        {
            if(selectedSong != null)
            {
                pausePlayer();

                if (isReading)
                {
                    isReading = false;
                } else
                {
                    isReading = true;
                    startPlayer();
                }
            }
        }

        private void prevHandler(object sender, EventArgs e)
        {
            if (selectedSong != null)
            {
                stopPlayer();

                try
                { 
                    selectedSongIndex = selectedSongIndex - 1;
                    this.songListContainer.SelectedIndex = selectedSongIndex;
                    selectedSong = mp3ListFiles[selectedSongIndex];
                    titreMorceau = selectedSong.Title;
                    this.songTitle.Text = titreMorceau;
                }
                catch (ArgumentOutOfRangeException)
                {
                    selectedSongIndex = fileLength - 1;
                    selectedSong = mp3ListFiles[selectedSongIndex];
                    this.songListContainer.SelectedIndex = selectedSongIndex;
                    titreMorceau = selectedSong.Title;
                    this.songTitle.Text = titreMorceau;
                }
            }
        }

        private void nextHandler(object sender, EventArgs e)
        {
            if (selectedSong != null)
            {
                stopPlayer();

                try
                {
                    selectedSongIndex = selectedSongIndex + 1;
                    this.songListContainer.SelectedIndex = selectedSongIndex;
                    selectedSong = mp3ListFiles[selectedSongIndex];
                    titreMorceau = selectedSong.Title;
                    this.songTitle.Text = titreMorceau;
                } 
                catch(ArgumentOutOfRangeException)
                {
                    selectedSong = mp3ListFiles[0];
                    this.songListContainer.SelectedIndex = 0;
                    titreMorceau = selectedSong.Title;
                    this.songTitle.Text = titreMorceau;   
                }
            } 
        }

        public void player_PlayStateChange(int newState)
        {
            switch (newState)
            {
                case 0:    // Undefined
                    this.playSongBottom.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\bottom_play.png");
                    break;

                case 1:    // Stopped
                    this.playSongBottom.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\bottom_play.png");
                    break;

                case 2:    // Paused
                    this.playSongBottom.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\bottom_play.png");
                    break;

                case 3:    // Playing
                    isReading = true;  
                    this.playSongBottom.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\bouton-pause.png");
                    break;

                case 4:    // ScanForward
                    break;

                case 5:    // ScanReverse
                    break;

                case 6:    // Buffering
                    this.playSongBottom.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\bottom_play.png");
                    break;

                case 7:    // Waiting
                    this.playSongBottom.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\bottom_play.png");
                    break;

                case 8:    // MediaEnded
                    if(isRandom)
                    {
                        setRandomSong();
                    } else
                    {
                        isReading = false;
                        this.totalDuration.Text = "";
                    }
                    this.playSongBottom.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\bottom_play.png");
                    break;

                case 9:    // Transitioning
                    this.playSongBottom.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\bottom_play.png");
                    break;

                case 10:   // Ready
                    this.playSongBottom.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\bouton-pause.png");
                    break;

                case 11:   // Reconnecting
                    this.playSongBottom.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\bottom_play.png");
                    break;

                case 12:   // Last
                    break;

                default:
                    this.playSongBottom.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\bottom_play.png");
                    break;
            }
        }

        public void startTimer()
        {
            // Set the timer to fire an event every second and start the timer.
            aTimer = new System.Timers.Timer();
            aTimer.Interval = 1000;
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            // Subtract the current position from the duration of the current media to get
            // the time remaining. Use the Math.floor method to round the result down to the
            // nearest whole number.
            double t = Math.Floor(wplayer.currentMedia.duration - wplayer.controls.currentPosition);

            // Display the time remaining in the current media.
            String currentDuration = TimeSpan.FromMinutes(t).ToString();
            this.totalDuration.Invoke(new MethodInvoker(delegate
            {
                this.totalDuration.Text = currentDuration;
            }));
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent(List<Mp3File> files)
        {
            mp3ListFiles = files;
            fileLength = Int16.Parse(files.Count.ToString());
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
            this.playSongBottom = new System.Windows.Forms.PictureBox();
            this.nextSongBottom = new System.Windows.Forms.PictureBox();
            this.prevSongBottom = new System.Windows.Forms.PictureBox();
            this.repeatSongBottom = new System.Windows.Forms.PictureBox();
            this.randomSongBottom = new System.Windows.Forms.PictureBox();
            this.totalDuration = new System.Windows.Forms.Label();
            this.TitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reduceScreenButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bigScreenButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeApp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playSongBottom)).BeginInit();
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
            this.bottomPlayer.Controls.Add(this.totalDuration);
            this.bottomPlayer.Controls.Add(this.playSongBottom);
            this.bottomPlayer.Controls.Add(this.prevSongBottom);
            this.bottomPlayer.Controls.Add(this.nextSongBottom);
            this.bottomPlayer.Controls.Add(this.repeatSongBottom);
            this.bottomPlayer.Controls.Add(this.randomSongBottom);
            this.bottomPlayer.Location = new System.Drawing.Point(0, 609);
            this.bottomPlayer.Name = "bottomPlayer";
            this.bottomPlayer.Size = new System.Drawing.Size(1136, 101);
            this.bottomPlayer.TabIndex = 6;
            // 
            // totalDuration
            // 
            this.totalDuration.AutoSize = true;
            this.totalDuration.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.totalDuration.ForeColor = System.Drawing.Color.White;
            this.totalDuration.Location = new System.Drawing.Point(545, 75);
            this.totalDuration.Name = "totalDuration";
            this.totalDuration.Size = new System.Drawing.Size(71, 25);
            this.totalDuration.TabIndex = 9;
            this.totalDuration.Text = "";
            // 
            // playSongBottom
            // 
            this.playSongBottom.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.playSongBottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.playSongBottom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.playSongBottom.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\bottom_play.png");
            this.playSongBottom.Location = new System.Drawing.Point(550, 35);
            this.playSongBottom.Name = "playSongBottom";
            this.playSongBottom.Padding = new System.Windows.Forms.Padding(2);
            this.playSongBottom.Size = new System.Drawing.Size(40, 40);
            this.playSongBottom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.playSongBottom.TabIndex = 7;
            this.playSongBottom.TabStop = false;
            this.playSongBottom.Click += new System.EventHandler(this.togglePlay);
            // 
            // repeatSongBottom
            // 
            this.repeatSongBottom.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.repeatSongBottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.repeatSongBottom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.repeatSongBottom.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\repeat_button.png");
            this.repeatSongBottom.Location = new System.Drawing.Point(660, 43);
            this.repeatSongBottom.Name = "prevSongBottom";
            this.repeatSongBottom.Padding = new System.Windows.Forms.Padding(2);
            this.repeatSongBottom.Size = new System.Drawing.Size(25, 25);
            this.repeatSongBottom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.repeatSongBottom.TabIndex = 7;
            this.repeatSongBottom.TabStop = false;
            this.repeatSongBottom.Click += new System.EventHandler(this.repeatPlayer);
            // 
            // randomSongBottom
            // 
            this.randomSongBottom.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.randomSongBottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.randomSongBottom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.randomSongBottom.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\random_button.png");
            this.randomSongBottom.Location = new System.Drawing.Point(450, 41);
            this.randomSongBottom.Name = "prevSongBottom";
            this.randomSongBottom.Padding = new System.Windows.Forms.Padding(2);
            this.randomSongBottom.Size = new System.Drawing.Size(30, 30);
            this.randomSongBottom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.randomSongBottom.TabIndex = 7;
            this.randomSongBottom.TabStop = false;
            this.randomSongBottom.Click += new System.EventHandler(this.randomPlayer);
            // 
            // prevSongBottom
            // 
            this.prevSongBottom.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.prevSongBottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.prevSongBottom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.prevSongBottom.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\skip_button_prev.png");
            this.prevSongBottom.Location = new System.Drawing.Point(500, 41);
            this.prevSongBottom.Name = "prevSongBottom";
            this.prevSongBottom.Padding = new System.Windows.Forms.Padding(2);
            this.prevSongBottom.Size = new System.Drawing.Size(30, 30);
            this.prevSongBottom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.prevSongBottom.TabIndex = 7;
            this.prevSongBottom.TabStop = false;
            this.prevSongBottom.Click += new System.EventHandler(this.prevHandler);
            // 
            // nextSongBottom
            // 
            this.nextSongBottom.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.nextSongBottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.nextSongBottom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.nextSongBottom.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\skip_button_next.png");
            this.nextSongBottom.Location = new System.Drawing.Point(610, 41);
            this.nextSongBottom.Name = "nextSongBottom";
            this.nextSongBottom.Padding = new System.Windows.Forms.Padding(2);
            this.nextSongBottom.Size = new System.Drawing.Size(30, 30);
            this.nextSongBottom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.nextSongBottom.TabIndex = 7;
            this.nextSongBottom.TabStop = false;
            this.nextSongBottom.Click += new System.EventHandler(this.nextHandler);
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
            this.songArtiste.Text = artisteMorceau;
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
            this.syncButton.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\sync.png");
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
        private System.Windows.Forms.Label totalDuration;
        private System.Windows.Forms.PictureBox closeApp;
        private System.Windows.Forms.PictureBox reduceScreenButton;
        private System.Windows.Forms.PictureBox bigScreenButton;
        private System.Windows.Forms.Panel musicContainer;
        private System.Windows.Forms.Panel artisteFilterContainer;
        private System.Windows.Forms.Panel songFilterContainer;
        private System.Windows.Forms.PictureBox syncButton;
        private System.Windows.Forms.Panel panel1;
        private ListBox songListContainer;
        private System.Windows.Forms.PictureBox playSongBottom;
        private System.Windows.Forms.PictureBox nextSongBottom;
        private System.Windows.Forms.PictureBox prevSongBottom;
        private System.Windows.Forms.PictureBox repeatSongBottom;
        private System.Windows.Forms.PictureBox randomSongBottom;
    }
}

