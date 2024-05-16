using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Unicv.Streaming.Api.Data.Context;
using Unicv.Streaming.Api.Data.Entities;
using Unicv.Streaming.Api.Models.Requests;

namespace Unicv.Streaming.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DirectorController : ControllerBase
{
    private readonly DataContext _db;

    public DirectorController(IConfiguration configuration)
    {
        _db = new DataContext(configuration);
    }

    #region GetById
    /// <summary>
    /// Retornar um diretor de acordo com o Id
    /// </summary>
    /// <param name="id">Id do diretor</param>
    /// <returns></returns>
    /// <response code="200">Retorna o diretor desejado</response>
    /// <response code="404">Diretor não encontrado</response>
    [HttpGet]
    [Route("{id}")]
    public IActionResult GetById(int id)
    {
        var director = _db.Director.FirstOrDefault(x => x.Id == id);

        if (director == null)
            return NotFound();

        return Ok(director);
    }
    #endregion

    #region Get
    /// <summary>
    /// Retorna todas os diretores
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Retorna a lista de diretores</response>
    [HttpGet]
    public IActionResult Get()
    {
        var directors = _db.Director.ToList();
        return Ok(directors);
    }
    #endregion

    #region Post
    /// <summary>
    /// Criar um diretor
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Diretor criado com sucesso</response>
    /// <response code="400">Já existe um diretor com esse nome</response>
    /// <response code="422">Dados inválidos</response>
    [HttpPost]
    public IActionResult Post(DirectorRequest model)
    {
        // onome não pode ser duplicado na plataforma
        var entity = _db.Director.FirstOrDefault(x => x.Name == model.Name);
        if (entity != null)
            return BadRequest("Já existe um diretor com este nome cadastrado.");

        var director = new Director();
        director.Name = model.Name;
        director.Profile = model.Profile;
        director.BirthDate = model.BirthDate;
        director.CreatedAt = DateTime.UtcNow;

        _db.Add(director);
        _db.SaveChanges();

        return Ok();
    }
    #endregion

    #region Put
    /// <summary>
    /// Editar um diretor
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Diretor criado com sucesso</response>
    /// <response code="400">Já existe um diretor com esse nome</response>
    /// <response code="422">Dados inválidos</response>
    /// <response code="404">Diretor não encontrado</response>
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] DirectorRequest model)
    {
        var director = _db.Director.FirstOrDefault(x => x.Id == id);

        if (director == null)
            return NotFound();

        // o nome não pode ser duplicado na plataforma
        var entity = _db.Director.FirstOrDefault(x => x.Name == director.Name && x.Id != id);
        if (entity != null)
            return BadRequest("Já existe um diretor com este nome cadastrado.");

        director.Name = model.Name;
        director.Profile = model.Profile;
        director.BirthDate = model.BirthDate;

        _db.Update(director);
        _db.SaveChanges();

        return Ok();
    }
    #endregion

    #region Delete
    /// <summary>
    /// Excluir um diretor
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Diretor excluída com sucesso</response>
    /// <response code="400">Este diretor está relacionado com outras tabelas</response>
    /// <response code="404">Diretor não encontrada</response>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var director = _db.Director.FirstOrDefault(x => x.Id == id);

        if (director == null)
            return NotFound();

        _db.Remove(director);
        _db.SaveChanges();

        return Ok();
    }
    #endregion
}
