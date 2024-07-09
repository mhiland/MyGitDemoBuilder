using System.IO;
using Ionic.Zlib;

namespace GitDemo.DTO
{
    public class ZCompression
    {
        public byte[] Compress(string data)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(data);

            using (var memoryStream = new MemoryStream())
            {
                using (var zlibStream = new ZlibStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression))
                {
                    zlibStream.Write(bytes, 0, bytes.Length);
                }

                return memoryStream.ToArray();
            }
        }

        public void WriteOut(FileInfo fileInfo, byte[] data)
        {
            File.WriteAllBytes(fileInfo.FullName, data);
        }
    }
}
