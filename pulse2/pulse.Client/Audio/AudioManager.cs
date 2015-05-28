using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Fx;

namespace pulse.Client.Audio
{
    static class AudioManager
    {
        private static readonly Dictionary<string, Sound> _audios = new Dictionary<string, Sound>(); 
        private static bool _initialised;

        public static int Frequency { get; set; }

        public static void Initialise()
        {
            if (_initialised) return;

            Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            BassFx.LoadMe();

            _initialised = true;
        }

        public static Sound LoadSound(byte[] audioBytes)
        {
            Initialise();

            var file = GCHandle.Alloc(audioBytes, GCHandleType.Pinned);

            int handle = Bass.BASS_StreamCreateFile(file.AddrOfPinnedObject(), 0, audioBytes.Length,
               BASSFlag.BASS_MUSIC_PRESCAN | BASSFlag.BASS_MUSIC_DECODE);
            Bass.BASS_StreamPutData(handle, audioBytes, audioBytes.Length);

            handle = BassFx.BASS_FX_TempoCreate(handle, BASSFlag.BASS_MUSIC_PRESCAN);

            return new Sound(handle, "");
        }

        public static Sound LoadSound(string path)
        {
            Initialise();

            if (_audios.ContainsKey(path))
                return _audios[path];

            if (!File.Exists(path))
            {
                return null;
            }

            int handle = Bass.BASS_StreamCreateFile(path, 0, 0,
                BASSFlag.BASS_STREAM_DECODE | BASSFlag.BASS_MUSIC_PRESCAN);

            handle = BassFx.BASS_FX_TempoCreate(handle, BASSFlag.BASS_MUSIC_PRESCAN);

            if (handle == 0)
            {
                return null;
            }

            var sound = new Sound(handle, path);
            _audios.Add(path, sound);
            return sound;
        }

        public static void Unload(string path)
        {
            if (!_audios.ContainsKey(path)) 
                return;

            _audios[path].Dispose();
            _audios.Remove(path);
        }
    }
}
