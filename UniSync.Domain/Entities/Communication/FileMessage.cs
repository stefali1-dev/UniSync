using UniSync.Domain.Common;

namespace UniSync.Domain.Entities.Communication
{
    internal class FileMessage : Message
    {
        public string FileUrl { get; private set; }
        public string FileName { get; private set; }
        public long FileSize { get; set; } // File size in bytes

    }
}
