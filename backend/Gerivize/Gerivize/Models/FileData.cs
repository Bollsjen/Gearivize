using Newtonsoft.Json;

namespace Gerivize.Models
{
    public class FileData
    {
        public string FileName { get; set; }
        public long Size { get; set; }
        public DateTime LastModified { get; set; }
    }
}
