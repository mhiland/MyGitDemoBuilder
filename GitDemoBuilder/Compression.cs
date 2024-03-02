using System;
using System.IO;
using System.Text;
using zlib;

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
            using (var outZStream = new ZOutputStream(outMemoryStream, zlibConst.Z_DEFAULT_COMPRESSION))
            using (Stream inMemoryStream = new MemoryStream(inData))
            {
                CopyStream(inMemoryStream, outZStream);
                outZStream.finish();
                outData = outMemoryStream.ToArray();
            }
        }

        public static void DecompressData(byte[] inData, out byte[] outData)
        {
            using (var outMemoryStream = new MemoryStream())
            using (var outZStream = new ZOutputStream(outMemoryStream))
            using (Stream inMemoryStream = new MemoryStream(inData))
            {
                CopyStream(inMemoryStream, outZStream);
                outZStream.finish();
                outData = outMemoryStream.ToArray();
            }
        }

        public static void CopyStream(Stream input, Stream output)
        {
            var buffer = new byte[2000];
            int len;
            while ((len = input.Read(buffer, 0, 2000)) > 0)
            {
                output.Write(buffer, 0, len);
            }
            output.Flush();
        }
    }
}
