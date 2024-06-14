using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Unicv.Streaming.Api.Data.Context;
using Unicv.Streaming.Api.Data.Entities;
using Unicv.Streaming.Api.Infra.Extensions;
using Unicv.Streaming.Api.Models.Requests;
using Unicv.Streaming.Api.Models.Response;

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

    #region Get
    /// <summary>
    /// Retorna todas as avaliações
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Retorna a lista de gêneros</response>
    [HttpGet]
    public IActionResult Get()
    {
        var works = _db.Work
            .Include(x => x.Rating)
            .Where(x => x.Rating.Count > 0)
            .OrderBy(x => x.Title)
            .ToList();

        var ratings = works.Select(x => new RatingResponse
        {
            WorkId = x.Id,
            Title = x.Title,
            QtyRatings = x.Rating.Where(y => y.WorkId == x.Id).Count(),
            Average = x.GetAverage(x.Rating)
        })
        .ToList();

        return Ok(ratings);
    }
    #endregion

    #region GetRatingByWork
    /// <summary>
    /// Retorna avaliação de uma obra específica
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Dados da avaliação por obra</response>
    /// <response code="404">Obra não encontrada</response>
    [HttpGet("{workId}")]
    public IActionResult GetRatingByWork(int workId)
    {
        var exists = _db.Work.Any(x => x.Id == workId);

        if (!exists)
            return NotFound();

        var work = _db.Work
            .Include(x => x.Category)
            .Include(x => x.Director)
            .Include(x => x.Gender)
            .FirstOrDefault(x => x.Id == workId);

        var ratings = _db.Rating
            .Include(x => x.Profile)
            .ThenInclude(x => x.Account)
            .Where(x => x.WorkId == workId)
            .ToList();

        var listProfiles = ratings.Select(x => new
        {
            RatingId = x.Id,
            Profile = string.Format("{0} - {1})", x.Profile.Account.Name, x.Profile.Name),
            Rating = x.UserRating
        })
        .ToList();

        return Ok(new
        {
            Rating = work.GetAverage(ratings),
            Category = work.Category.Name,
            Director = work.Director.Name,
            Gender = work.Gender.Name,
            work.Title,
            Profiles = listProfiles
        });
    }
    #endregion

    #region ClearRating
    /// <summary>
    /// Limpar a avaliações de uma obra
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Avaliações excluídas com sucesso</response>
    [HttpDelete("{workId}/clear")]
    public IActionResult Delete(int workId)
    {
        var ratings = _db.Rating.Where(x => x.WorkId == workId);

        if (ratings.Any())
        {
            foreach (var rating in ratings)
                _db.Remove(rating);

            _db.SaveChanges();
        }
        return Ok();
    }
    #endregion

    #region Post
    /// <summary>
    /// Criar uma Avaliação
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Avalaição criada com sucesso</response>
    /// <response code="404">A obra relacionada não existe</response>
    /// <response code="404">O perfil relacionado não existe</response>
    /// <response code="400">A avaliação deve ser entre 1 e 5</response>
    [HttpPost]
    public IActionResult Post(RatingRequest request)
    {
        var existsWork = _db.Work.Any(x => x.Id == request.WorkId);
        if (!existsWork)
            return NotFound("Obra não encontrada");

        var existsProfile = _db.Profile.Any(x => x.Id == request.ProfileId);
        if (!existsProfile)
            return NotFound("Perfil não encontrado");

        var isValidRating = request.UserRating >= 1 && request.UserRating <= 5;
        if (!isValidRating)
            return BadRequest("A avaliação deve ser entre 1 e 5");

        var rating = _db.Rating.FirstOrDefault(x => x.WorkId == request.WorkId && x.ProfileId == request.ProfileId);
        if (rating == null)
        {
            rating = new Rating();
            rating.CreatedAt = DateTime.UtcNow;
            rating.WorkId = request.WorkId;
            rating.ProfileId = request.ProfileId;
        }

        rating.UserRating = request.UserRating;

        if (rating.Id == 0)
            _db.Add(rating);
        else
            _db.Update(rating);
        _db.SaveChanges();

        return Ok();
    }
    #endregion

    #region Delete
    /// <summary>
    /// Excluir uma avaliação
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Avaliação excluída com sucesso</response>
    /// <response code="404">Avaliação não encontrada</response>
    [HttpDelete("{ratingId}/delete-recommendation")]
    public IActionResult DeleteRecommendaton(int ratingId)
    {
        var rating = _db.Rating.FirstOrDefault(x => x.Id == ratingId);

        if (rating == null)
            return NotFound();

        _db.Remove(rating);
        _db.SaveChanges();

        return Ok();
    }
    #endregion
}
