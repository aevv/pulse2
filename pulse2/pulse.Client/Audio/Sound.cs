using System;
using Un4seen.Bass;

namespace pulse.Client.Audio
{
    class Sound : IDisposable
    {
        private readonly int _handle;
        private readonly string _path;
        private float _volume;
        private bool _disposed;

        public string Path { get { return _path; } }
        public bool Stopped { get; private set; }
        public bool Paused { get; private set; }

        public Sound(int handle, string path)
        {
            _handle = handle;
            _path = path;
            _volume = 1.0f;
        }

        public float Frequency
        {
            get
            {
                float frequencyTemp = 0.0f;
                Bass.BASS_ChannelGetAttribute(_handle, BASSAttribute.BASS_ATTRIB_FREQ, ref frequencyTemp);
                return frequencyTemp;
            }
            set
            {
                Bass.BASS_ChannelSetAttribute(_handle, BASSAttribute.BASS_ATTRIB_FREQ, value);
            }
        }

        public float FrequencyPercentage
        {
            get
            {
                float frequencyTemp = 0.0f;
                Bass.BASS_ChannelGetAttribute(_handle, BASSAttribute.BASS_ATTRIB_FREQ, ref frequencyTemp);
                return (frequencyTemp/AudioManager.Frequency)*100;
            }
            set
            {
                var baseFrequency = AudioManager.Frequency;
                Bass.BASS_ChannelSetAttribute(_handle, BASSAttribute.BASS_ATTRIB_FREQ, baseFrequency*value);
            }
        }

        public double Length
        {
            get
            {
                return Bass.BASS_ChannelBytes2Seconds(_handle,
                    Bass.BASS_ChannelGetLength(_handle, BASSMode.BASS_POS_BYTES)) * 1000;
            }
        }

        public float Volume
        {
            get
            {
                return _volume;
            }
            set
            {
                Bass.BASS_ChannelSetAttribute(_handle, BASSAttribute.BASS_ATTRIB_VOL, value);
                _volume = value;
            }
        }

        public double Position
        {
            get
            {
                return Bass.BASS_ChannelBytes2Seconds(_handle, Bass.BASS_ChannelGetPosition(_handle));
            }
            set
            {
                Bass.BASS_ChannelSetPosition(_handle, Bass.BASS_ChannelSeconds2Bytes(_handle, value));
            }
        }

        public void Play()
        {
            if (!Paused && !Stopped)
                return;

            Bass.BASS_ChannelPlay(_handle, false);
            Paused = false;
            Stopped = false;
        }

        public void Stop()
        {
            if (Stopped)
                return;

            Bass.BASS_ChannelStop(_handle);
            Stopped = true;
            Paused = false;
        }

        public void Pause()
        {
            if (!Stopped && Paused)
                return;

            Bass.BASS_ChannelPause(_handle);
            Stopped = false;
            Paused = true;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                Bass.BASS_StreamFree(_handle);
                AudioManager.Unload(_path);
                _disposed = true;
            }
        }
    }
}
