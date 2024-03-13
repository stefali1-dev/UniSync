using UniSync.Domain.Common;

namespace UniSync.Domain.Entities.Communication
{
    internal class ImageMessage : Message
    {
        public string ImageUrl { get; private set; }
        public string AltText { get; private set; }

    }
}
