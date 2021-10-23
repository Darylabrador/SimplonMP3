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
        public void setListFiles(List<Mp3File> listFiles, Boolean resettingFields)
        {
            if (resettingFields)
            {
                titreMorceau = "Titre du morceau";
                artisteMorceau = "Artiste";
                this.totalDuration.Text = "";
                this.songTitle.Text = titreMorceau;
                this.songArtiste.Text = artisteMorceau;
                this.selectedSong = null;
                isReading = false;

                this.songListContainer.SelectedItems.Clear();
                this.songListContainer.Items.Clear();
            }

            fileLength = Int16.Parse(listFiles.Count.ToString());
            if (fileLength > 0)
            {
                listFiles.ForEach(song =>
                {
                    String Title, Artiste, Album, Duree, Action;
                    Title = song.Title == null ? song.Name : song.Title;
                    Artiste = song.Artiste == null ? "" : song.Artiste;
                    Album = "";
                    Duree = song.Duration;
                    Action = "";
                    this.songListContainer.Items.Add(new ListViewItem(new string[] { Title, Artiste, Album, Duree, Action }));
                });

                if (this.songListContainer.Items[0].Text.Length == 0)
                {
                    this.songListContainer.Items[0].Remove();
                }
            }
        }

        public void setPrevValues(int index)
        {
            selectedSongIndex = index - 1;
            this.songListContainer.Items[index].Selected = true;
            selectedSong = mp3ListFiles[index];
            titreMorceau = selectedSong.Title;
            this.songTitle.Text = titreMorceau;
        }

        public void setNexValues(int index, Boolean isError)
        {
            if (!isError)
            {
                selectedSongIndex = index + 1;
                this.songListContainer.Items[index].Selected = true;
                selectedSong = mp3ListFiles[index];
            }
            else
            {
                selectedSong = mp3ListFiles[0];
                this.songListContainer.Items[0].Selected = true;
            }
            titreMorceau = selectedSong.Title;
            this.songTitle.Text = titreMorceau;
        }
    }
}
