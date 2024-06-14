namespace Unicv.Streaming.Api.Models.Response;

public class RatingResponse
{
    public int WorkId { get; set; }
    public decimal Average { get; set; }
    public int QtyRatings { get; set; }
    public string Title { get; set; }
}
