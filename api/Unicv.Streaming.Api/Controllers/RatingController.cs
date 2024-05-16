using Microsoft.AspNetCore.Mvc;
using Unicv.Streaming.Api.Data.Context;
using Unicv.Streaming.Api.Data.Entities;
using Unicv.Streaming.Api.Models.Requests;

namespace Unicv.Streaming.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RatingController : ControllerBase
{
    private readonly DataContext _db;

    public RatingController(IConfiguration configuration)
    {
        _db = new DataContext(configuration);
    }

    #region GetRatingByWork
    /// <summary>
    /// Retorna avaliação de uma obra específica
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Avaliação média da obra</response>
    /// <response code="404">Obra não encontrada</response>
    [HttpGet]
    public IActionResult GetRatingByWork(int workId)
    {
        var work = _db.Work.FirstOrDefault(x => x.Id == workId);

        if (work == null) 
            return NotFound();

        var ratings = _db.Rating.Where(x => x.WorkId == workId).ToList();
        var rating = (decimal)0;

        if (ratings.Count>0)
        {
            var sum = ratings.Sum(x => x.UserRating);
            rating = sum / ratings.Count;
        }

        return Ok(rating);
    }
    #endregion

    #region Post
    /// <summary>
    /// Criar uma Avaliação
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Avalaição criada com sucesso</response>
    /// <response code="400">A obra relacionada não existe</response>
    /// <response code="400">O perfil relacionado não existe</response>
    /// <response code="400">A avaliação deve ser entre 1 e 5</response>
    /// <response code="422">Dados inválidos</response>
    [HttpPost]
    public IActionResult Post(RatingRequest model)
    {
        // o nome não pode ser duplicado na plataforma
        var existsWork = _db.Work.Any(x => x.Id == model.WorkId);
        if (!existsWork)
            return BadRequest("A obra relacionada não existe.");

        var existsProfile = _db.Profile.Any(x => x.Id == model.ProfileId);
        if (!existsProfile)
            return BadRequest("O perfil relacionado não existe.");

        var isValidRating = model.UserRating >= 1 && model.UserRating <= 5;
        if (!isValidRating)
            return BadRequest("A avaliação deve ser entre 1 e 5");

        // caso já exista a avalaiação, apenas atualiza
        var rating = _db.Rating.FirstOrDefault(x => x.WorkId == model.WorkId && x.ProfileId == model.ProfileId);

        if (rating == null)
        {
            rating = new Rating();
            rating.WorkId= model.WorkId;
            rating.ProfileId= model.ProfileId;
            rating.UserRating = model.UserRating;
            rating.CreatedAt = DateTime.UtcNow;

            _db.Add(rating);
        }
        else
        {
            rating.UserRating = model.UserRating;

            _db.Update(rating);
        }

        _db.SaveChanges();
        return Ok();
    }
    #endregion
}
