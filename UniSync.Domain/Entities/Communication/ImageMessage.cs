using UniSync.Domain.Common;

namespace UniSync.Domain.Entities.Communication
{
    internal class ImageMessage : Message
    {
        public string ImageUrl { get; private set; }
        public string AltText { get; private set; }

        private ImageMessage(Guid messageId, DateTime messageDate, Guid senderId, Guid? receiverId, Guid? channelId, string imageUrl, string altText)
            : base(messageId, messageDate, senderId, receiverId, channelId)
        {
            ImageUrl = imageUrl;
            AltText = altText;
        }

        public static Result<ImageMessage> Create(Guid messageId, DateTime messageDate, Guid senderId, Guid? receiverId, Guid? channelId, string imageUrl, string altText)
        {
            if (messageId == Guid.Empty)
            {
                return Result<ImageMessage>.Failure("MessageId cannot be empty.");
            }

            if (senderId == Guid.Empty)
            {
                return Result<ImageMessage>.Failure("SenderId cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                return Result<ImageMessage>.Failure("ImageUrl cannot be empty or whitespace.");
            }

            // AltText could be empty or null

            var imageMessage = new ImageMessage(messageId, messageDate, senderId, receiverId, channelId, imageUrl, altText);
            return Result<ImageMessage>.Success(imageMessage);
        }
    }
}
