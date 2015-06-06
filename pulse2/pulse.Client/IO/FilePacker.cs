using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace pulse.Client.IO
{
    public class FilePacker
    {
        public void PackObjectToFile<T>(T obj, string path)
        {
            var dirInfo = new DirectoryInfo(path);

            if (dirInfo.Parent != null)
                dirInfo = dirInfo.Parent;

            if (!dirInfo.Exists)
                return;

            var serialised = JsonConvert.SerializeObject(obj);
            var compressed = Compress(Encoding.UTF8.GetBytes(serialised));
            File.WriteAllBytes(path, compressed);
        }

        public T DeserialisePackedFile<T>(string path)
        {
            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(LoadPackedFile(path)));
        }

        public byte[] LoadPackedFile(string path)
        {
            if (!File.Exists(path))
                return new byte[0];

            return Decompress(File.ReadAllBytes(path));
        }

        private byte[] Decompress(byte[] toDecompress)
        {
            using (var stream = new MemoryStream(toDecompress))
            {
                using (var gzip = new GZipStream(stream, CompressionMode.Decompress))
                {
                    using (var result = new MemoryStream())
                    {
                        gzip.CopyTo(result);
                        return result.ToArray();
                    }
                }
            }
        }

        private byte[] Compress(byte[] toCompress)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (GZipStream gzip = new GZipStream(stream, CompressionMode.Compress, true))
                {
                    gzip.Write(toCompress, 0, toCompress.Length);
                }
                return stream.ToArray();
            }
        }
    }
}
