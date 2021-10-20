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

        private void searchAction(object sender, EventArgs e)
        {
            String searched = this.searchTextBox.Text.Trim();

            if ((isActiveArtisteFilter && searched != "Rechercher un artiste...") || (!isActiveArtisteFilter && searched != "Rechercher un morceau..."))
            {
                this.searchHandler(searched, isActiveArtisteFilter);
            }
            else if (searched == "")
            {
                this.defaultSearch();
            }
            else
            {
                this.defaultSearch();
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

        private void defaultSearch()
        {
            Debug.WriteLine("Tout afficher");
        }

        private void searchHandler(String searchInfo, Boolean isFilterActive)
        {
            if (isFilterActive)
            {
                Debug.WriteLine("Recherche d'un artiste suivant : ");
            }
            else
            {
                Debug.WriteLine("Recherche d'un morceau suivant : ");
            }
            Debug.WriteLine(searchInfo);
        }
    }
}
