namespace API.Entities;

public class Channel
{
    public int ChannelId { get; set; }
    public string ChannelName { get; set; }
    public int ServerId { get; set; }
    public virtual Server Server { get; set; }
    // Navigation properties
    public virtual ICollection<Message> Messages { get; set; }
}