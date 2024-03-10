using UniSync.Domain.Common;

namespace UniSync.Domain.Entities.Communication
{
    internal class FileMessage : Message
    {
        public string FileUrl { get; private set; }
        public string FileName { get; private set; }
        public long FileSize { get; set; } // File size in bytes

        private FileMessage(Guid messageId, DateTime messageDate, Guid senderId, Guid? receiverId, Guid? channelId, string fileUrl, string fileName, long fileSize)
            : base(messageId, messageDate, senderId, receiverId, channelId)
        {
            FileUrl = fileUrl;
            FileName = fileName;
            FileSize = fileSize;
        }

        public static Result<FileMessage> Create(Guid messageId, DateTime messageDate, Guid senderId, Guid? receiverId, Guid? channelId, string fileUrl, string fileName, long fileSize)
        {
            if (messageId == Guid.Empty)
            {
                return Result<FileMessage>.Failure("MessageId cannot be empty.");
            }

            if (senderId == Guid.Empty)
            {
                return Result<FileMessage>.Failure("SenderId cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(fileUrl))
            {
                return Result<FileMessage>.Failure("FileUrl cannot be empty or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                return Result<FileMessage>.Failure("FileName cannot be empty or whitespace.");
            }

            var fileMessage = new FileMessage(messageId, messageDate, senderId, receiverId, channelId, fileUrl, fileName, fileSize);
            return Result<FileMessage>.Success(fileMessage);
        }   
    }
}
