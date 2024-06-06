
using Unicv.Streaming.Api.Data.Entities;
using Unicv.Streaming.Api.Models.Response.DefaultItems;

namespace Unicv.Streaming.Api.Models.Response;

public class HomeResponse
{
    public int CategoryCount { get; set; }
    public int GenderCount { get; set; }
    public int ActorsCount { get; set; }
    public int DirectorsCount { get; set; }
    public int WorksCount { get; internal set; }
    public int NewWorksCount { get; internal set; }

    public List<Item> MostCategories { get; internal set; }
    public List<Item> MostGenders { get; internal set; }
    public object MostDirectors { get; internal set; }
    public List<Work> LastWorksFull { get; internal set; }
    public List<WorkResponse> LastWorks { get; internal set; }
}
