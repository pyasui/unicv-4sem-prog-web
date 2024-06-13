using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Unicv.Streaming.Api.Models.Requests;

public class WorkRequest
{
    [Required]
    public string Title { get; set; }

    [Required]
    public string Synopsis { get; set; }

    [Required]
    public bool Active { get; set; }

    // chaves estrangeiras - relacionamentos
    [Required]
    public int GenderId { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [Required]
    public int DirectorId { get; set; }

    //public int[] Actors { get; set; }

    public WorkRequest()
    {
        //Actors = new int[] { };
    }
}
