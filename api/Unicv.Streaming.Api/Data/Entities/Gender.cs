using System.ComponentModel.DataAnnotations.Schema;

namespace Unicv.Streaming.Api.Data.Entities;

[Table("gender")]
public class Gender
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
}
