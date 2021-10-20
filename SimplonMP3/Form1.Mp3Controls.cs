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
    public partial class Form1
    {
        private static System.Timers.Timer aTimer;
        private WMPLib.WindowsMediaPlayer wplayer = null;
        private String execPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
        private List<Mp3File> mp3ListFiles = new List<Mp3File>();
        private Mp3File selectedSong = null;
        private int fileLength = 0;
        private int selectedSongIndex;
        private String titreMorceau = "Titre du morceau";
        private String artisteMorceau = "Artiste";
        private Boolean isReading = false;
        private Boolean isRandom = false;
        private Boolean isRepeat = false;
        private double time = 0.0;


        private void syncSongs(object sender, EventArgs e)
        {
            stopPlayer();
            this.selectedSong = null;
            this.songListContainer.SelectedIndex = 0;
            mp3ListFiles = null;
            mp3ListFiles = search.Main();
            this.songListContainer.DataSource = mp3ListFiles;
            this.firstStart = true;
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

        private void togglePlay(object sender, EventArgs e)
        {
            if (selectedSong != null)
            {
                pausePlayer();

                if (isReading)
                {
                    isReading = false;
                }
                else
                {
                    isReading = true;
                    startPlayer();
                }
            }
        }

        public void stopPlayer()
        {
            if (wplayer != null)
            {
                this.totalDuration.Text = "";
                wplayer.close();
            }
        }


        public void repeatPlayer(object sender, EventArgs e)
        {
            if (isRepeat)
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
            if (isRandom)
            {
                isRandom = false;
                this.randomSongBottom.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\random_button.png");
            }
            else
            {
                isRandom = true;
                this.randomSongBottom.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\random_button_active.png");

                isRepeat = false;
                wplayer.settings.setMode("loop", false);
                this.repeatSongBottom.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\repeat_button.png");
            }
        }

        public void setRandomSong()
        {
            var rand = new Random();
            int randIndex = rand.Next(mp3ListFiles.Count - 1);
            selectedSong = mp3ListFiles[randIndex];
            this.songListContainer.SelectedIndex = randIndex;
            selectedSongIndex = randIndex;
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
                catch (ArgumentOutOfRangeException)
                {
                    selectedSong = mp3ListFiles[0];
                    this.songListContainer.SelectedIndex = 0;
                    titreMorceau = selectedSong.Title;
                    this.songTitle.Text = titreMorceau;
                }
            }
        }

        public void startTimer()
        {
            this.totalDuration.Text = "";
            aTimer = new System.Timers.Timer();
            aTimer.Interval = 1000;
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            double t = Math.Floor(wplayer.currentMedia.duration - wplayer.controls.currentPosition);
            String currentDuration = TimeSpan.FromMinutes(t).ToString();
            this.totalDuration.Invoke(new MethodInvoker(delegate
            {
                this.totalDuration.Text = currentDuration;
            }));
        }
    }
}
