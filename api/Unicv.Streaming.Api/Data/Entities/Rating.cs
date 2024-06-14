using System.ComponentModel.DataAnnotations.Schema;

namespace Unicv.Streaming.Api.Data.Entities;

[Table("rating")]
public class Rating
{
    public int Id { get; set; }
    public int WorkId { get; set; }
    public int ProfileId { get; set; }
    public int UserRating { get; set; }
    public DateTime CreatedAt { get; set; }

    public Profile Profile { get; set; }
    public Work Work { get; set; }
}
