namespace Unicv.Streaming.Api.Data.Entities;

public class Rating
{
    public int Id { get; set; }
    public int WorkId { get; set; }
    public int ProfileId { get; set; }
    public int UserRating { get; set; }
    public DateTime CreatedAt { get; set; }
}
