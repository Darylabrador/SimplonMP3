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

            if(searched != "Rechercher un morceau...") {
                Debug.WriteLine("Recherche du morceau suivant : ");
                Debug.WriteLine(searched);
            }

            if(searched == "") {
                Debug.WriteLine("Tout afficher");
            }
        }
    }
}
