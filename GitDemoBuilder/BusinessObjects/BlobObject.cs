namespace GitDemo.DTO
{
    public class BlobObject : Base
    {
        public string Content { get; set; } = "";

        public override string GetFileContent()
        {
            return $"blob {Content.Length}\0{Content}";
        }
    }
}
