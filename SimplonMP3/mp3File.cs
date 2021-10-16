using WMPLib;
using System;

namespace SimplonMP3
{

    public class Mp3File
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Artiste { get; set; }
        public double Duration { get; set; }
        public string Path { get; set; }

        WMPLib.WindowsMediaPlayer wplayer = null;

        public void getProperties(string fileName,  string filePath)
        {
            Name = fileName;
            Path = filePath;

            wplayer = new WMPLib.WindowsMediaPlayer();
            IWMPMedia mediaInfo = wplayer.newMedia(filePath);
            for (int i = 0; i < mediaInfo.attributeCount; i++)
            {
                switch (mediaInfo.getAttributeName(i).ToString())
                {
                    case "Author":
                        Artiste = mediaInfo.getItemInfo(mediaInfo.getAttributeName(i)).ToString();
                        break;
                    case "Duration":
                        Duration = mediaInfo.duration;
                        break;
                    case "Title":
                        Title = mediaInfo.getItemInfo(mediaInfo.getAttributeName(i)).ToString();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
