using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace GitDemo
{
    public class Compression
    {
        public byte[] Compress(string data)
        {
            var bytes = Encoding.UTF8.GetBytes(data);

            CompressData(bytes, out var outData);

            return outData;
        }

        public void WriteOut(FileInfo fileInfo, byte[] data)
        {
            if (!Directory.Exists(fileInfo.DirectoryName))
                Directory.CreateDirectory(fileInfo.DirectoryName ?? throw new InvalidOperationException());

            File.WriteAllBytes(fileInfo.FullName, data);
        }

        public string Decompress(FileInfo fileInfo)
        {
            DecompressData(File.ReadAllBytes(fileInfo.FullName), out var outData);

            return Encoding.UTF8.GetString(outData);
        }

        public static void CompressData(byte[] inData, out byte[] outData)
        {
            using (var outMemoryStream = new MemoryStream())
            {
                using (var gzipStream = new GZipStream(outMemoryStream, CompressionLevel.Optimal))
                {
                    gzipStream.Write(inData, 0, inData.Length);
                }
                outData = outMemoryStream.ToArray();
            }
        }

        public static void DecompressData(byte[] inData, out byte[] outData)
        {
            using (var inMemoryStream = new MemoryStream(inData))
            using (var outMemoryStream = new MemoryStream())
            {
                using (var gzipStream = new GZipStream(inMemoryStream, CompressionMode.Decompress))
                {
                    gzipStream.CopyTo(outMemoryStream);
                }
                outData = outMemoryStream.ToArray();
            }
        }
    }
}
