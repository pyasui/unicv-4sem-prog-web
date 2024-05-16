using Microsoft.AspNetCore.Mvc;
using Unicv.Streaming.Api.Data.Context;
using Unicv.Streaming.Api.Data.Entities;
using Unicv.Streaming.Api.Models.Requests;

namespace Unicv.Streaming.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorksController : ControllerBase
{
    private readonly DataContext _db;

    public WorksController(IConfiguration configuration)
    {
        _db = new DataContext(configuration);
    }

    #region GetById
    /// <summary>
    /// Retornar uma obra de acordo com o Id
    /// </summary>
    /// <param name="id">Id da obra</param>
    /// <returns></returns>
    /// <response code="200">Retorna a obra desejada</response>
    /// <response code="404">Obra não encontrada</response>
    [HttpGet]
    [Route("{id}")]
    public IActionResult GetById(int id)
    {
        var work = _db.Work.FirstOrDefault(x => x.Id == id);

        if (work == null)
            return NotFound();

        return Ok(work);
    }
    #endregion

    #region Get
    /// <summary>
    /// Retorna todas as obras cadastradas
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Retorna a lista de obras</response>
    [HttpGet]
    public IActionResult Get()
    {
        var works = _db.Work.Where(x => x.Active).ToList();
        return Ok(works);
    }
    #endregion

    #region Post
    /// <summary>
    /// Criar uma Obra
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Obra criada com sucesso</response>
    /// <response code="400">Já existe uma obra com esse nome</response>
    /// <response code="400">O gênero informado é inválido</response>
    /// <response code="400">A categoria informada é inválida</response>
    /// <response code="400">O diretor informado é inválido</response>
    /// <response code="422">Dados inválidos</response>
    [HttpPost]
    public IActionResult Post(WorkRequest model)
    {
        // o nome não pode ser duplicado na plataforma
        var entity = _db.Work.FirstOrDefault(x => x.Title == model.Title);
        if (entity != null)
            return BadRequest("Já existe uma obra com este título cadastrado.");

        var gender = _db.Gender.FirstOrDefault(x => x.Id == model.GenderId);
        if (gender == null)
            return BadRequest("O gênero informado é inválido");

        var category = _db.Category.FirstOrDefault(x => x.Id == model.CategoryId);
        if (category == null)
            return BadRequest("A categoria informada é inválida");

        var director = _db.Director.FirstOrDefault(x => x.Id == model.DirectorId);
        if (director == null)
            return BadRequest("O diretor informado é inválido");

        if (model.Actors != null)
        {
            foreach (var actorId in model.Actors)
            {
                var existsActor = _db.Actor.Any(x => x.Id == actorId);
                if (!existsActor)
                    return BadRequest($"O ator informado é inválido (Id: {actorId})");
            }
        }

        var work = new Work();
        work.Title = model.Title;
        work.Synopsis = model.Synopsis;
        work.Active = model.Active;

        work.GenderId = model.GenderId;
        work.CategoryId = model.CategoryId;
        work.DirectorId = model.DirectorId;
        work.CreatedAt = DateTime.UtcNow;

        // adicionar os ators
        foreach (var actorId in model.Actors)
        {
            var cast = new Cast();
            cast.ActorId = actorId;
            cast.CreatedAt = DateTime.UtcNow;

            work.Cast.Add(cast);
        }

        _db.Add(work);
        _db.SaveChanges();

        return Ok();
    }
    #endregion

    #region Put
    /// <summary>
    /// Editar uma Obra
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Obra criada com sucesso</response>
    /// <response code="400">Já existe uma obra com esse nome</response>
    /// <response code="400">O gênero informado é inválido</response>
    /// <response code="400">A categoria informada é inválida</response>
    /// <response code="400">O diretor informado é inválido</response>
    /// <response code="404">A obra não existe</response>
    /// <response code="422">Dados inválidos</response>
    [HttpPut]
    public IActionResult Put(int id, WorkRequest model)
    {
        var exists = _db.Work.Any(x => x.Id == id);
        if (!exists)
            return NotFound();

        // o nome não pode ser duplicado na plataforma
        var entity = _db.Work.FirstOrDefault(x => x.Title == model.Title);
        if (entity != null)
            return BadRequest("Já existe uma obra com este título cadastrado.");

        var gender = _db.Gender.FirstOrDefault(x => x.Id == model.GenderId);
        if (gender == null)
            return BadRequest("O gênero informado é inválido");

        var category = _db.Category.FirstOrDefault(x => x.Id == model.CategoryId);
        if (category == null)
            return BadRequest("A categoria informada é inválida");

        var director = _db.Director.FirstOrDefault(x => x.Id == model.DirectorId);
        if (director == null)
            return BadRequest("O diretor informado é inválido");

        entity.Title = model.Title;
        entity.Synopsis = model.Synopsis;
        entity.Active = model.Active;

        entity.GenderId = model.GenderId;
        entity.CategoryId = model.CategoryId;
        entity.DirectorId = model.DirectorId;
        entity.CreatedAt = DateTime.UtcNow;

        _db.Update(entity);
        _db.SaveChanges();

        return Ok();
    }
    #endregion
}
