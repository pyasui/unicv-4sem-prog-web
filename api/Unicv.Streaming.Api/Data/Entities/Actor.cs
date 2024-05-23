using System.ComponentModel.DataAnnotations.Schema;

namespace Unicv.Streaming.Api.Data.Entities;

[Table("actor")]
public class Actor
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Profile { get; set; }
    
    public DateTime CreatedAt { get; set; }
}
