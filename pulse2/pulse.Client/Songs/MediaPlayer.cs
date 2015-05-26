using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public PointF Location { get; set; }
        public SizeF Size { get; set; }
        public float Rotation { get; set; }
        public Color4 Colour { get; set; }

        public bool AutoPlay { get; set; }

        private Sound _currentSound;

        public Sound CurrentSound
        {
            get { return _currentSound; }
            set
            {
                _currentSound.Stop();

                if (value == null)
                    return;

                _currentSound = value;
                
                if (AutoPlay)
                    _currentSound.Play();
            }
        }

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
        }

        public void Play()
        {
            _currentSound.Play();
        }

        public void Pause()
        {
            _currentSound.Pause();
        }

        public void Stop()
        {
            _currentSound.Stop();
        }

        public void OnRenderFrame(FrameEventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
