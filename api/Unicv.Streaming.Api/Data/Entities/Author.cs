namespace Unicv.Streaming.Api.Data.Entities;

public class Author
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Profile { get; set; }
    
    public DateTime CreatedAt { get; set; }

    public DateTime? BirthDate { get; set; }
}
