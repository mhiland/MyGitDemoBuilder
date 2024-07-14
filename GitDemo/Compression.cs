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
            using (var memoryStream = new MemoryStream())
            {
                // Writing the ZLIB header
                memoryStream.WriteByte(0x78); // CMF (Compression Method and flags)
                memoryStream.WriteByte(0x9C); // FLG (Additional flags)

                using (var deflateStream = new DeflateStream(memoryStream, CompressionMode.Compress, true))
                {
                    deflateStream.Write(inData, 0, inData.Length);
                }

                // Compute Adler-32 checksum and write it
                uint adler32 = ComputeAdler32(inData);
                memoryStream.WriteByte((byte)((adler32 >> 24) & 0xFF));
                memoryStream.WriteByte((byte)((adler32 >> 16) & 0xFF));
                memoryStream.WriteByte((byte)((adler32 >> 8) & 0xFF));
                memoryStream.WriteByte((byte)(adler32 & 0xFF));

                outData = memoryStream.ToArray();
            }
        }

        public static void DecompressData(byte[] inData, out byte[] outData)
        {
            using (var inMemoryStream = new MemoryStream(inData))
            using (var outMemoryStream = new MemoryStream())
            {
                // Skipping the ZLIB header (first two bytes)
                inMemoryStream.ReadByte();
                inMemoryStream.ReadByte();

                using (var deflateStream = new DeflateStream(inMemoryStream, CompressionMode.Decompress))
                {
                    deflateStream.CopyTo(outMemoryStream);
                }

                // Verify Adler-32 checksum
                outData = outMemoryStream.ToArray();
                var computedAdler32 = ComputeAdler32(outData);
                var storedAdler32 = (uint)(
                    (inData[inData.Length - 4] << 24) |
                    (inData[inData.Length - 3] << 16) |
                    (inData[inData.Length - 2] << 8) |
                    inData[inData.Length - 1]
                );

                if (computedAdler32 != storedAdler32)
                {
                    throw new InvalidDataException("Adler-32 checksum does not match.");
                }
            }
        }

        private static uint ComputeAdler32(byte[] data)
        {
            const uint MOD_ADLER = 65521;
            uint a = 1, b = 0;

            foreach (byte bt in data)
            {
                a = (a + bt) % MOD_ADLER;
                b = (b + a) % MOD_ADLER;
            }

            return (b << 16) | a;
        }
    }
}
