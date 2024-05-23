using System.ComponentModel.DataAnnotations.Schema;

namespace Unicv.Streaming.Api.Data.Entities;

[Table("work")]
public class Work
{
    // propriedades nativas
    public int Id { get; set; }
    public string Title { get; set; }
    public string Synopsis { get; set; }
    public bool Active { get; set; }
    public DateTime CreatedAt { get; set; }

    // chaves estrangeiras - relacionamentos
    public int GenderId { get; set; }
    public int CategoryId { get; set; }
    public int DirectorId { get; set; }

    public List<Cast> Cast { get; set; }
    public List<Rating> Rating { get; set; }

    public Work()
    {
        Cast = new List<Cast>();
        Rating = new List<Rating>();
    }
}
