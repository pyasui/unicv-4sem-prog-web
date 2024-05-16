using Microsoft.AspNetCore.Mvc;
using Unicv.Streaming.Api.Data.Context;
using Unicv.Streaming.Api.Data.Entities;
using Unicv.Streaming.Api.Models.Requests;

namespace Unicv.Streaming.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfileController : ControllerBase
{
    private readonly DataContext _db;

    public ProfileController(IConfiguration configuration)
    {
        _db = new DataContext(configuration);
    }

    #region GetById
    /// <summary>
    /// Retornar um perfil de acordo com o Id
    /// </summary>
    /// <param name="id">Id do perfil</param>
    /// <returns></returns>
    /// <response code="200">Retorna o perfil desejado</response>
    /// <response code="404">Perfil não encontrado</response>
    [HttpGet]
    [Route("{id}")]
    public IActionResult GetById(int id)
    {
        var profile = _db.Profile.FirstOrDefault(x => x.Id == id);

        if (profile == null)
            return NotFound();

        return Ok(profile);
    }
    #endregion

    #region Get
    /// <summary>
    /// Retorna todas os perfis por usuário
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Retorna a lista de perfis</response>
    [HttpGet]
    public IActionResult Get(int accountId)
    {
        var profiles = _db.Profile.Where(x => x.AccountId == accountId).ToList();
        return Ok(profiles);
    }
    #endregion

    #region Post
    /// <summary>
    /// Criar um Perfil
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Perfil criado com sucesso</response>
    /// <response code="400">Já existe um perfil com esse nome</response>
    /// <response code="400">A conta relacionada não existe</response>
    /// <response code="422">Dados inválidos</response>
    [HttpPost]
    public IActionResult Post(ProfileRequest model)
    {
        // o nome não pode ser duplicado na plataforma
        var entity = _db.Profile.FirstOrDefault(x => x.Name == model.Name && x.AccountId == model.AccountId);
        if (entity != null)
            return BadRequest("Já existe um perfil com este nome cadastrado.");

        var account = _db.Account.FirstOrDefault(x => x.Id == model.AccountId);
        if (account == null)
            return BadRequest("A conta relacionada não existe");

        var profile = new Profile();
        profile.Name = model.Name;
        profile.IsChildrenProfile = model.IsChildrenProfile;
        profile.AccountId = account.Id;
        profile.CreatedAt = DateTime.UtcNow;

        _db.Add(profile);
        _db.SaveChanges();

        return Ok();
    }
    #endregion

    #region Put
    /// <summary>
    /// Editar um perfil
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Perfil criado com sucesso</response>
    /// <response code="400">Já existe um perfil com esse nome</response>
    /// <response code="400">A conta relacionada não existe</response>
    /// <response code="422">Dados inválidos</response>
    /// <response code="404">Perfil não encontrado</response>
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] ProfileRequest model)
    {
        var profile = _db.Profile.FirstOrDefault(x => x.Id == id);

        if (profile == null)
            return NotFound();

        // o nome não pode ser duplicado na plataforma
        var entity = _db.Profile.FirstOrDefault(x => x.Name == model.Name && x.AccountId == model.AccountId && x.Id != id);
        if (entity != null)
            return BadRequest("Já existe um perfil com este nome cadastrado.");

        var account = _db.Account.FirstOrDefault(x => x.Id == model.AccountId);
        if (account == null)
            return BadRequest("A conta relacionada não existe");

        profile.Name = model.Name;
        profile.IsChildrenProfile = model.IsChildrenProfile;
        profile.AccountId = account.Id;

        _db.Update(profile);
        _db.SaveChanges();

        return Ok();
    }
    #endregion

    #region Delete
    /// <summary>
    /// Excluir um perfil
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Perfil excluída com sucesso</response>
    /// <response code="400">Este perfil está relacionado com outras tabelas</response>
    /// <response code="404">Perfil não encontrada</response>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var profile = _db.Profile.FirstOrDefault(x => x.Id == id);

        if (profile == null)
            return NotFound();

        _db.Remove(profile);
        _db.SaveChanges();

        return Ok();
    }
    #endregion
}
