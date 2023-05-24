namespace Gerivize.Models
{
    public class DirectoryData
    {
        public string DirectoryName { get; set; }
        public List<FileData> Files { get; set; }
        public List<DirectoryData> Directories { get; set; }
        public long Size { get; set; }
        public DateTime LastModified { get; set; }
    }
}
