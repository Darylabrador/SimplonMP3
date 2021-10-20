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
        private void songListContainer_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!firstStart)
            {
                if (selectedSong != null)
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

                if (titreMorceau == null)
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

            firstStart = false;
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
                    if (isRandom)
                    {
                        setRandomSong();
                    }
                    else
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
    }
}
