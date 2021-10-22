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
        private String searchText = "Rechercher un morceau...";
        private Boolean isActiveArtisteFilter = false;
        private List<Mp3File> defaultMp3List = new List<Mp3File>();

        private void resetting()
        {
            this.songListContainer.SelectedItems.Clear();
            this.songListContainer.Items.Clear();
            titreMorceau = "Titre du morceau";
            artisteMorceau = "Artiste";
            this.totalDuration.Text = "";
            stopPlayer();
            this.songTitle.Text = titreMorceau;
            this.songArtiste.Text = artisteMorceau;
            this.selectedSong = null;
            isReading = false;

            fileLength = Int16.Parse(defaultMp3List.Count.ToString());
            if (fileLength > 0)
            {
                defaultMp3List.ForEach(song =>
                {
                    String Title, Artiste, Album, Duree, Action;
                    Title = song.Name == null ? song.Name : song.Title;
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

            this.playSongBottom.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\bottom_play.png");
        }

        private void reinitPlayList()
        {
            defaultMp3List.Clear();
            defaultMp3List = mp3ListFiles;
            resetting();
        }

        private void searchAction(object sender, EventArgs e)
        {
            String searched = this.searchTextBox.Text.Trim();

            if ((isActiveArtisteFilter && searched != "Rechercher un artiste...") || (!isActiveArtisteFilter && searched != "Rechercher un morceau..."))
            {
                this.searchHandler(searched, isActiveArtisteFilter);
            }
            else if (searched == "")
            {
                this.reinitPlayList();
            }
            else
            {
                this.reinitPlayList();
            }
        }

        private void toggleSearchArtiste(object sender, EventArgs e)
        {
            isActiveArtisteFilter = this.toggleButton.IsOn;

            if (isActiveArtisteFilter)
            {
                this.searchTextBox.Text = "Rechercher un artiste...";
            }
            else
            {
                this.searchTextBox.Text = "Rechercher un morceau...";
            }
        }

        private void searchHandler(String searchInfo, Boolean isFilterActive)
        {
            defaultMp3List.Clear();

            if (isFilterActive)
            {
                defaultMp3List = mp3ListFiles.FindAll(element => element.Artiste.Contains(searchInfo));
            }
            else
            {
                defaultMp3List = mp3ListFiles.FindAll(element => element.Title.Contains(searchInfo));
            }

            resetting();
        }
    }
}
