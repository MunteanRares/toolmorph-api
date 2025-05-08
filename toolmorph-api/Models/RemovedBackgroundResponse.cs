using System.Runtime.CompilerServices;

namespace toolmorph_api.Models
{
    public class RemovedBackgroundResponse
    {
        public byte[] FileBytes { get; set; } = Array.Empty<byte>();
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
    }
}
