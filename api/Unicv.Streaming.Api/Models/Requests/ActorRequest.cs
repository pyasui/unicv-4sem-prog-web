using System.ComponentModel.DataAnnotations;

namespace Unicv.Streaming.Api.Models.Requests;

public class ActorRequest
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Profile { get; set; }
}
