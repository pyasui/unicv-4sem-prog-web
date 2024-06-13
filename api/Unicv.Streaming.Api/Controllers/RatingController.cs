using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    #region GetData
    ///// <summary>
    ///// Retorna os dados para carregar a tela de avaliações
    ///// </summary>
    ///// <returns></returns>
    ///// <response code="200">Dados da tela</response>
    [HttpGet]
    [Route("get-data")]
    public IActionResult GetData()
    {
        var profiles = _db.Profile
            .Include(x => x.Account)
            .ToList();

        var works = _db.Work
            .Include(x => x.Gender)
            .Include(x => x.Category)
            .ToList();

        var ddlProfile = profiles.Select(x => new
        {
            Id = x.Id,
            Title = string.Format("{0} - {1} (Perfil infantil: {2})", x.Account.Name, x.Name, x.IsChildrenProfile ? "Sim" : "Não")
        })
        .OrderBy(x => x.Title)
        .ToList();

        var ddlWork = works.Select(x => new
        {
            x.Id,
            Title = string.Format("{0} - Cat: {1} - Gen: {2}", x.Title, x.Category.Name, x.Gender.Name)
        })
        .OrderBy(x => x.Title)
        .ToList();

        return Ok(new
        {
            Profiles = ddlProfile,
            Works = ddlWork
        });
    }
    #endregion

    #region CreateRating
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

        var ratings = _db.Rating
            .Include(x => x.Profile)
            .ThenInclude(x => x.Account)
            .Where(x => x.WorkId == workId)
            .ToList();

        var listProfiles = ratings.Select(x => new
        {
            Id = x.Id,
            Profile = string.Format("{0} - {1} (Perfil infantil: {2})", x.Profile.Account.Name, x.Profile.Name, x.Profile.IsChildrenProfile ? "Sim" : "Não"),
            Rating = x.UserRating
        })
        .ToList();

        var rating = (decimal)0;

        if (ratings.Count > 0)
        {
            var sum = (decimal)ratings.Sum(x => x.UserRating);
            rating = sum / ratings.Count;
        }

        return Ok(new
        {
            Rating = rating,
            Profiles = listProfiles
        });
    }
    #endregion

    #region Delete
    /// <summary>
    /// Excluir uma avaliação
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Avaliação excluída com sucesso</response>
    /// <response code="404">Avaliação não encontrada</response>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var rating = _db.Rating.FirstOrDefault(x => x.Id == id);

        if (rating == null)
            return NotFound();

        _db.Remove(rating);
        _db.SaveChanges();

        return Ok();
    }
    #endregion
}
