namespace API.Entities;

public class Message
{
    public int MessageId { get; set; }
    public int SenderId { get; set; }
    public virtual User Sender { get; set; }
    public int ReceiverId { get; set; }
    public virtual User Receiver { get; set; }
    public string MessageContent { get; set; }
    public DateTime Timestamp { get; set; }
}