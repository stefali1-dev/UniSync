using UniSync.Domain.Common; // Assuming a similar structure for Result<T>
using System;

namespace UniSync.Domain.Entities.Communication
{
    internal class TextMessage : Message
    {
        public string Text { get; private set; }

        private TextMessage(Guid messageId, DateTime messageDate, Guid senderId, Guid? receiverId, Guid? channelId, string text)
            : base(messageId, messageDate, senderId, receiverId, channelId)
        {
            Text = text;
        }

        public static Result<TextMessage> Create(Guid messageId, DateTime messageDate, Guid senderId, Guid? receiverId, Guid? channelId, string text)
        {
            if (messageId == Guid.Empty)
            {
                return Result<TextMessage>.Failure("MessageId cannot be empty.");
            }

            if (senderId == Guid.Empty)
            {
                return Result<TextMessage>.Failure("SenderId cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                return Result<TextMessage>.Failure("Text cannot be empty or whitespace.");
            }

            var textMessage = new TextMessage(messageId, messageDate, senderId, receiverId, channelId, text);
            return Result<TextMessage>.Success(textMessage);
        }
    }
}
