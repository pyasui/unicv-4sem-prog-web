using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Unicv.Streaming.Api.Data.Context;
using Unicv.Streaming.Api.Data.Entities;
using Unicv.Streaming.Api.Models.Requests;

namespace Unicv.Streaming.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenderController : ControllerBase
{
    private readonly DataContext _db;

    public GenderController(IConfiguration configuration)
    {
        _db = new DataContext(configuration);
    }

    #region GetById
    /// <summary>
    /// Retornar um gênero de acordo com o Id
    /// </summary>
    /// <param name="id">Id do gênero</param>
    /// <returns></returns>
    /// <response code="200">Retorna o gênero desejado</response>
    /// <response code="404">Gênero não encontrado</response>
    [HttpGet]
    [Route("{id}")]
    public IActionResult GetById(int id)
    {
        var gender = _db.Gender.FirstOrDefault(x => x.Id == id);

        if (gender == null)
            return NotFound();

        return Ok(gender);
    }
    #endregion

    #region Get
    /// <summary>
    /// Retorna todas os gêneros
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Retorna a lista de gêneros</response>
    [HttpGet]
    public IActionResult Get()
    {
        var genders = _db.Gender.ToList();
        return Ok(genders);
    }
    #endregion

    #region Post
    /// <summary>
    /// Criar um gênero
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Gênero criado com sucesso</response>
    /// <response code="400">Já existe um gênero com esse nome</response>
    /// <response code="422">Dados inválidos</response>
    [HttpPost]
    public IActionResult Post(GenderRequest model)
    {
        // validar se existe um gênero criado com o mesmo nome
        var entity = _db.Gender.FirstOrDefault(x => x.Name == model.Name);
        if (entity != null)
            return BadRequest("Já existe um gênero com este nome.");

        var gender = new Gender();
        gender.Name = model.Name;
        gender.CreatedAt = DateTime.UtcNow;

        _db.Add(gender);
        _db.SaveChanges();

        return Ok(model);
    }
    #endregion

    #region Put
    /// <summary>
    /// Editar um gênero
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Gênero criado com sucesso</response>
    /// <response code="400">Já existe um gênero com esse nome</response>
    /// <response code="422">Dados inválidos</response>
    /// <response code="404">Gênero não encontrado</response>
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] GenderRequest model)
    {
        var gender = _db.Gender.FirstOrDefault(x => x.Id == id);

        if (gender == null)
            return NotFound();

        var entity = _db.Gender.FirstOrDefault(x => x.Name == model.Name && x.Id != id);
        if (entity != null)
            return BadRequest("Já existe um gênero com este nome.");

        gender.Name = model.Name;

        _db.Update(gender);
        _db.SaveChanges();

        return Ok(model);
    }
    #endregion

    #region Delete
    /// <summary>
    /// Excluir um gênero
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Gênero excluído com sucesso</response>
    /// <response code="400">Este gênero está relacionada com outras tabelas</response>
    /// <response code="404">Gênero não encontrado</response>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var gender = _db.Gender.FirstOrDefault(x => x.Id == id);

        if (gender == null)
            return NotFound();

        _db.Remove(gender);
        _db.SaveChanges();

        return Ok();
    }
    #endregion
}
