using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using pulse.Client.Audio;
using pulse.Client.Graphics.Interface;

namespace pulse.Client.Songs
{
    class MediaPlayer : IRenderable
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
        public float Depth { get; set; }

        public bool AutoPlay { get; set; }

        private Sound _currentSound;
        private ChartGroup _currentChart;
        private SongLibrary _library;

        public Sound CurrentSound
        {
            get { return _currentSound; }
            set
            {
                if (_currentSound != null)
                    _currentSound.Stop();

                if (value == null)
                    return;

                _currentSound = value;
                
                if (AutoPlay)
                    _currentSound.Play();
            }
        }

        public ChartGroup CurrentChart
        {
            get { return _currentChart; }
        }

        public void Play()
        {
            if (_currentSound != null)
                _currentSound.Play();
        }

        public void Pause()
        {
            if (_currentSound != null)
                _currentSound.Pause();
        }

        public void Stop()
        {
            if (_currentSound != null)
                _currentSound.Stop();
        }

        public void PlayRandom()
        {
            var song = _library.GetRandomSong();
            _currentSound = song.Item2;
            _currentChart = song.Item1;
            if (_currentSound != null)
                _currentSound.Play();
        }

        public void OnRenderFrame(FrameEventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
