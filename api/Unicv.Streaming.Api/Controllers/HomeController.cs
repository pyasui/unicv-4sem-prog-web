using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Unicv.Streaming.Api.Data.Context;
using Unicv.Streaming.Api.Models.Response;
using Unicv.Streaming.Api.Models.Response.DefaultItems;

namespace Unicv.Streaming.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HomeController : ControllerBase
{
    private readonly DataContext _db;

    public HomeController(IConfiguration configuration)
    {
        _db = new DataContext(configuration);
    }

    #region Get
    /// <summary>
    /// Retorna os dados para criação dos gráficos da home
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Dados da home</response>
    [HttpGet]
    public IActionResult Get()
    {
        var home = new HomeResponse();
        var dtLastWeek = DateTime.Today.AddDays(-7);

        // indicadores de cadastros
        home.CategoryCount = _db.Category.Count();
        home.GenderCount = _db.Gender.Count();
        home.ActorsCount = _db.Actor.Count();
        home.DirectorsCount = _db.Director.Count();

        home.WorksCount = _db.Work.Count();
        home.NewWorksCount = _db.Work.Where(x => x.CreatedAt > dtLastWeek).Count();

        // categorias com mais obras
        home.MostCategories = _db.Work.Include(x => x.Category)
                          .GroupBy(x => x.Category)
                          .Select(x => new Item { Key = x.Key.Name, Value = x.Count() })
                          .OrderByDescending(x => x.Value)
                          .Take(3)
                          .ToList();

        home.MostGenders = _db.Work
                          .Include(x => x.Gender)
                          .GroupBy(x => x.Gender)
                          .Select(x => new Item { Key = x.Key.Name, Value = x.Count() })
                          .OrderByDescending(x => x.Value)
                          .Take(3)
                          .ToList();

        home.MostDirectors = _db.Work
                          .Include(x => x.Director)
                          .GroupBy(x => x.Director)
                          .Select(x => new Item { Key = x.Key.Name, Value = x.Count() })
                          .OrderByDescending(x => x.Value)
                          .Take(3)
                          .ToList();

        var works = _db.Work
            .Include(x => x.Category)
            .Include(x => x.Gender)
            .Include(x => x.Director)
            .OrderByDescending(x => x.CreatedAt).Take(10).ToList();
        home.LastWorksFull = works;

        home.LastWorks = works.Select(x => new WorkResponse
        {
            Work = x.Title,
            Director = x.Director.Name,
            Category = x.Category.Name,
            Gender = x.Gender.Name
        }).ToList();

        return Ok(home);
    }
    #endregion

}
