using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Un4seen.Bass;

namespace pulse.Client.Audio
{
    class Sound
    {
        private int _handle;

        public Sound(int handle)
        {
            _handle = handle;
        }

        public void Play()
        {
            Bass.BASS_ChannelPlay(_handle, false);
        }

        public void Stop()
        {
            Bass.BASS_ChannelStop(_handle);
        }

        public void Pause()
        {
            Bass.BASS_ChannelPause(_handle);
        }
    }
}
