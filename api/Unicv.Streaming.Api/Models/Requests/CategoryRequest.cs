using System.ComponentModel.DataAnnotations;

namespace Unicv.Streaming.Api.Models.Requests;

public class CategoryRequest
{
    [Required]
    public string Name{get;set;}
}