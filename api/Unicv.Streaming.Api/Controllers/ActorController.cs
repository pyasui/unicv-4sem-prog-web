using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Unicv.Streaming.Api.Data.Context;
using Unicv.Streaming.Api.Data.Entities;
using Unicv.Streaming.Api.Models.Requests;

namespace Unicv.Streaming.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActorController : ControllerBase
{
    private readonly DataContext _db;

    public ActorController(IConfiguration configuration)
    {
        _db = new DataContext(configuration);
    }

    #region GetById
    /// <summary>
    /// Retornar um autor de acordo com o Id
    /// </summary>
    /// <param name="id">Id do autor</param>
    /// <returns></returns>
    /// <response code="200">Retorna o autor desejado</response>
    /// <response code="404">autor não encontrado</response>
    [HttpGet]
    [Route("{id}")]
    public IActionResult GetById(int id)
    {
        var author = _db.Actor.FirstOrDefault(x => x.Id == id);

        if (author == null)
            return NotFound();

        return Ok(author);
    }
    #endregion

    #region Get
    /// <summary>
    /// Retorna todas os autors
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Retorna a lista de autors</response>
    [HttpGet]
    public IActionResult Get()
    {
        var authors = _db.Actor.ToList();
        return Ok(authors);

    }
    #endregion

    #region Post
    /// <summary>
    /// Criar um autor
    /// </summary>
    /// <returns></returns>
    /// <response code="200">autor criado com sucesso</response>
    /// <response code="400">Já existe um autor com esse nome</response>
    /// <response code="422">Dados inválidos</response>
    [HttpPost]
    public IActionResult Post(ActorRequest model)
    {
        // o nome não pode ser duplicado na plataforma
        var entity = _db.Actor.FirstOrDefault(x => x.Name == model.Name);
        if (entity != null)
            return BadRequest("Já existe um ator com este e-mail cadastrado.");

        var actor = new Actor();
        actor.Name = model.Name;
        actor.Profile = model.Profile;
        actor.CreatedAt = DateTime.UtcNow;

        _db.Add(actor);
        _db.SaveChanges();

        return Ok();
    }
    #endregion

    #region Put
    /// <summary>
    /// Editar um autor
    /// </summary>
    /// <returns></returns>
    /// <response code="200">autor criado com sucesso</response>
    /// <response code="400">Já existe um autor com esse nome</response>
    /// <response code="422">Dados inválidos</response>
    /// <response code="404">autor não encontrado</response>
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] ActorRequest model)
    {
        var author = _db.Actor.FirstOrDefault(x => x.Id == id);

        if (author == null)
            return NotFound();

        // o nome não pode ser duplicado na plataforma
        var entity = _db.Actor.FirstOrDefault(x => x.Name == author.Name && x.Id != id);
        if (entity != null)
            return BadRequest("Já existe um ator com este nome cadastrado.");

        author.Name = model.Name;
        author.Profile = model.Profile;

        _db.Update(author);
        _db.SaveChanges();

        return Ok();
    }
    #endregion

    #region Delete
    /// <summary>
    /// Excluir um autor
    /// </summary>
    /// <returns></returns>
    /// <response code="200">autor excluído com sucesso</response>
    /// <response code="400">Este autor está relacionada com outras tabelas</response>
    /// <response code="404">autor não encontrado</response>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var actor = _db.Actor.FirstOrDefault(x => x.Id == id);

        if (actor == null)
            return NotFound();

        _db.Remove(actor);
        _db.SaveChanges();

        return Ok();
    }
    #endregion
}
