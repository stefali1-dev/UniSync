namespace API.Entities;

public class Server
{
    public int ServerId { get; set; }
    public string ServerName { get; set; }
    public string Description { get; set; }
    // Navigation properties
    public virtual ICollection<Channel> Channels { get; set; }
    public virtual ICollection<Message> Messages { get; set; }
}