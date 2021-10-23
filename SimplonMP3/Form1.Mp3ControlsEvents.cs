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
        private void songListContainer_SelectedIndexChanged(object sender, EventArgs e)
        {
            stopPlayer();
            if (this.songListContainer.SelectedItems.Count > 0)
            {
                selectedSong = null;
                int indexVal = this.songListContainer.SelectedItems[0].Index;
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
                    if (isRepeat && !isRandom)
                    {
                        startPlayer();
                    }
                    else if (!isRepeat && isRandom)
                    {
                        setRandomSong();
                    }
                    else
                    {
                        isReading = false;
                        stopPlayer();
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
