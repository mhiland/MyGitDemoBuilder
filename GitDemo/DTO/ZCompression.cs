using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace GitDemo.DTO
{
    public class ZCompression
    {
        public byte[] Compress(string data)
        {
            var bytes = Encoding.UTF8.GetBytes(data);

            using (var memoryStream = new MemoryStream())
            {
                // Writing the ZLIB header
                memoryStream.WriteByte(0x78); // CMF (Compression Method and flags)
                memoryStream.WriteByte(0x9C); // FLG (Additional flags)
                
                using (var deflateStream = new DeflateStream(memoryStream, CompressionMode.Compress, true))
                {
                    deflateStream.Write(bytes, 0, bytes.Length);
                }

                // Compute Adler-32 checksum and write it
                uint adler32 = ComputeAdler32(bytes);
                memoryStream.WriteByte((byte)((adler32 >> 24) & 0xFF));
                memoryStream.WriteByte((byte)((adler32 >> 16) & 0xFF));
                memoryStream.WriteByte((byte)((adler32 >> 8) & 0xFF));
                memoryStream.WriteByte((byte)(adler32 & 0xFF));
                
                return memoryStream.ToArray();
            }
        }

        public void WriteOut(FileInfo fileInfo, byte[] data)
        {
            File.WriteAllBytes(fileInfo.FullName, data);
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
