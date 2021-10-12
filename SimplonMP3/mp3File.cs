using WMPLib;

namespace SimplonMP3
{
    public class Mp3File
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public bool isSelected { get; set; } = false;
        WMPLib.WindowsMediaPlayer wplayer = null;

        public void startPlayer()
        {
            wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = Path;
            wplayer.controls.play();
        }

        public void stopPlayer()
        {
            wplayer.close();
        }
    }
}
