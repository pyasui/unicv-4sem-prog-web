using System.ComponentModel.DataAnnotations;

namespace Unicv.Streaming.Api.Models.Requests;

public class GenderRequest
{
    [Required]
    public string Name{get;set;}
}