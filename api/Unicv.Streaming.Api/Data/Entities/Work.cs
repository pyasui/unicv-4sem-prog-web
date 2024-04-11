namespace Unicv.Streaming.Api.Data.Entities;

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
}
