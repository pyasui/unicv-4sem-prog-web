using System.ComponentModel.DataAnnotations.Schema;

namespace Unicv.Streaming.Api.Data.Entities;

[Table("director")]
public class Director
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Profile { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? BirthDate { get; set; }
}
