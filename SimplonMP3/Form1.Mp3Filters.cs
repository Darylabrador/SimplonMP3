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
        private void reinitPlayList()
        {
            titreMorceau = "Titre du morceau";
            artisteMorceau = "Artiste";
            this.totalDuration.Text = "";
            defaultMp3List = mp3ListFiles;
            this.songListContainer.DataSource = defaultMp3List;
            stopPlayer();
            this.songTitle.Text = titreMorceau;
            this.songArtiste.Text = artisteMorceau;
            this.selectedSong = null;
            isReading = false;
            this.playSongBottom.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\bottom_play.png");
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
            if (isFilterActive)
            {
                defaultMp3List = mp3ListFiles.FindAll(element => element.Artiste.Contains(searchInfo));
            }
            else
            {
                defaultMp3List = mp3ListFiles.FindAll(element => element.Name.Contains(searchInfo) || element.Title.Contains(searchInfo));
            }

            titreMorceau = "Titre du morceau";
            artisteMorceau = "Artiste";
            this.totalDuration.Text = "";
            this.songListContainer.DataSource = defaultMp3List;
            stopPlayer();
            this.songTitle.Text = titreMorceau;
            this.songArtiste.Text = artisteMorceau;
            this.selectedSong = null;
            isReading = false;
            this.playSongBottom.Image = System.Drawing.Image.FromFile(execPath + @"\assets\img\bottom_play.png");
        }
    }
}
