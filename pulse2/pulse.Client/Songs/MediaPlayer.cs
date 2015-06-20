using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using pulse.Client.Audio;
using pulse.Client.Graphics.Engine.Util;
using pulse.Client.Graphics.Interface;

namespace pulse.Client.Songs
{
    class MediaPlayer
    {
        private static MediaPlayer _instance;
        private static object _sync = new object();

        public static MediaPlayer Instance
        {
            get
            {
                lock (_sync)
                    return _instance ?? (_instance = new MediaPlayer());
            }
        }

        private MediaPlayer()
        {
            Colour = Color4.White;
            AutoPlay = true;
            _library = SongLibrary.Instance;
        }

        public bool Visible { get; set; }
        public PointF Location { get; set; }
        public SizeF Size { get; set; }
        public float Rotation { get; set; }
        public Color4 Colour { get; set; }
        public ShapeType Shape { get; set; }
        public float Depth { get; set; }

        public bool AutoPlay { get; set; }

        public float Volume { get; set; }

        private Song _currentSong;
        private SongLibrary _library;

        public Song CurrentSong
        {
            get { return _currentSong; }
            set
            {
                if (_currentSong != null)
                    _currentSong.Sound.Stop();

                if (value == null)
                    return;

                _currentSong = value;
                
                if (AutoPlay)
                    _currentSong.Sound.Play();
            }
        }

        public void Play()
        {
            if (_currentSong != null)
                _currentSong.Sound.Play();
        }

        public void Pause()
        {
            if (_currentSong != null)
                _currentSong.Sound.Pause();
        }

        public void Stop()
        {
            if (_currentSong != null)
                _currentSong.Sound.Stop();
        }

        public void PlayRandom()
        {
            _currentSong = _library.GetRandomSong(); 

            if (_currentSong != null)
                _currentSong.Sound.Play();
        }
    }
}
