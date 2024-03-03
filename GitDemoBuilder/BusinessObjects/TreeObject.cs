using System.Text;

namespace GitDemo.DTO
{
    public class TreeObject : Base
    {
        public string Permissions { get; set; }
        public string ObjectType { get; set; }
        public string ObjectFileName { get; set; }
        public BlobObject BlobObject { get; set; }

        public override string GetFileContent()
        {
            var str = $"{Permissions} {ObjectFileName}\0{ToHexString(BlobObject.ByteArray)}";

            return $"tree {str.Length}\0{str}";
        }

        private static string ToHexString(byte[] bytes)
        {
            var sb = new StringBuilder(bytes.Length*2);

            foreach (var byte2 in bytes)
            {
                sb.AppendFormat("{0:x2}", byte2);
            }

            return sb.ToString();
        }
    }
}
