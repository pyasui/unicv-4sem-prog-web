using System.ComponentModel.DataAnnotations;

namespace Unicv.Streaming.Api.Models.Requests;

public class DirectorRequest
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Profile { get; set; }

    public DateTime? BirthDate { get; set; }
}
