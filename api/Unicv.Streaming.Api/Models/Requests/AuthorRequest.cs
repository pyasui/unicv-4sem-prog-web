using System.ComponentModel.DataAnnotations;

namespace Unicv.Streaming.Api.Models.Requests;

public class AuthorRequest
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Profile { get; set; }

    public DateTime? BirthDate { get; set; }
}
