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
            this.totalDuration.Text = "";
            stopPlayer();
            mp3ListFiles.Clear();
            mp3ListFiles = search.Main();
            setListFiles(mp3ListFiles, true);
        }

        public void startPlayer()
        {
            if(aTimer != null) {
                aTimer.Stop();
                aTimer.Dispose();
            }
            
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

                if (isReading)
                {
                    pausePlayer();
                }
                else
                {
                    startPlayer();
                }
                isReading = !isReading;
            }
        }

        public void stopPlayer()
        {
            if (wplayer != null)
            {
                this.totalDuration.Text = "";
                this.time = 0.0;
                wplayer.close();
            }
        }


        public void repeatPlayer(object sender, EventArgs e)
        {
            if (isRepeat)
            {
                wplayer.settings.setMode("loop", false);
                this.repeatSongBottom.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\repeat_button.png");
            }
            else
            {
                isRandom = false;
                wplayer.settings.setMode("loop", true);
                this.repeatSongBottom.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\repeat_button_active.png");
                this.randomSongBottom.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\random_button.png");
            }
            isRepeat = !isRepeat;
        }


        public void randomPlayer(object sender, EventArgs e)
        {
            if (isRandom)
            {
                this.randomSongBottom.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\random_button.png");
            }
            else
            {
                isRepeat = false;
                wplayer.settings.setMode("loop", false);
                this.randomSongBottom.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\random_button_active.png");
                this.repeatSongBottom.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\repeat_button.png");
            }

            isRandom = !isRandom;
        }

        public void setRandomSong()
        {
            var rand = new Random();
            int randIndex = rand.Next(mp3ListFiles.Count - 1);
            selectedSong = mp3ListFiles[randIndex];
            this.songListContainer.Items[randIndex].Selected = true;
            selectedSongIndex = randIndex;
        }

        private void prevHandler(object sender, EventArgs e)
        {
            if (selectedSong != null)
            {
                stopPlayer();

                try
                {
                    setPrevValues(selectedSongIndex);
                }
                catch (ArgumentOutOfRangeException)
                {
                    setPrevValues(fileLength);
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
                    setNexValues(selectedSongIndex, false);
                }
                catch (ArgumentOutOfRangeException)
                {
                    setNexValues(selectedSongIndex, true);
                }
            }
        }

        public void startTimer()
        {
            aTimer = new System.Timers.Timer();
            aTimer.Interval = 1000;
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                double t = Math.Floor(wplayer.currentMedia.duration - wplayer.controls.currentPosition);
                String currentDuration = TimeSpan.FromMinutes(t).ToString();
                this.totalDuration.Invoke(new MethodInvoker(delegate
                {
                    this.totalDuration.Text = currentDuration;
                }));
            }
            catch (System.Runtime.InteropServices.COMException)
            {

            }

        }
    }
}