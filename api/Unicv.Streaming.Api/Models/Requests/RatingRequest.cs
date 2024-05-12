namespace Unicv.Streaming.Api.Models.Requests;

public class RatingRequest
{
    public int WorkId { get; set; }
    public int ProfileId { get; set; }
    public int UserRating { get; set; }
}
