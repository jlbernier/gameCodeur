using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRunnerDemo
{
    internal class GCMusicManager
    {
        protected int _nCurrentMusic;
        protected int _nNextMusic;
        protected List<Song> _lstMusics;

        public GCMusicManager()
        {
            _lstMusics = new List<Song>();
            _nCurrentMusic = -1;
            MediaPlayer.Volume = 0;
        }

        public int AddMusic(Song pSong)
        {
            _lstMusics.Add(pSong);
            return _lstMusics.Count - 1;
        }

        public void Update(GameTime gameTime)
        {
            if (_nCurrentMusic != _nNextMusic)
            {
                MediaPlayer.Volume -= 0.01f;
                if (MediaPlayer.Volume <= 0)
                {
                    MediaPlayer.Volume = 0;
                    Song mySong = _lstMusics[_nNextMusic];

                    MediaPlayer.IsRepeating = true;
                    MediaPlayer.Play(mySong);
                    _nCurrentMusic = _nNextMusic;
                    Debug.WriteLine("Start playing music " + _nNextMusic);
                }
            }
            else
            if (_nCurrentMusic == _nNextMusic && MediaPlayer.Volume < 1)
            {
                try
                {
                    MediaPlayer.Volume += 0.01f;
                }
                catch (NullReferenceException)
                {
                    Debug.WriteLine("Mediaplayer NULL !");
                }
            }
        }

        public void PlayMusic(int nMusic)
        {
            if (_nNextMusic != nMusic)
            {
                _nNextMusic = nMusic;
                Debug.WriteLine("Change music from {0} to {1}", _nCurrentMusic, _nNextMusic);
            }
        }
    }
}
