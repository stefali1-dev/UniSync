namespace API.Entities;

public class Resource
{
    public int ResourceId { get; set; }
    public string ResourceType { get; set; } // e.g., Document, Video, etc.
    public string Title { get; set; }
    public string Description { get; set; }
    public string Url { get; set; }
    public int CourseId { get; set; }
    public virtual Course Course { get; set; }
}