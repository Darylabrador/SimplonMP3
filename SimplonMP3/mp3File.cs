using WMPLib;

namespace SimplonMP3
{
    public class Mp3File
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public bool isSelected { get; set; } = false;
        WMPLib.WindowsMediaPlayer wplayer = null;
        double time = 0.0;
        public void startPlayer()
        {
            wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = Path;
            if(time != 0.0)
            {
                wplayer.controls.currentPosition = time;
            }
            wplayer.controls.play();
        }

        public void pausePlayer()
        {
            wplayer.controls.pause();
            time = wplayer.controls.currentPosition;
        }

        public void stopPlayer()
        {
            if(wplayer != null)
            {
                wplayer.close(); 
            }
        }
    }
}
