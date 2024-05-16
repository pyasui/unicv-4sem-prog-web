using Microsoft.AspNetCore.Mvc;
using Unicv.Streaming.Api.Data.Context;
using Unicv.Streaming.Api.Data.Entities;
using Unicv.Streaming.Api.Models.Requests;

namespace Unicv.Streaming.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly DataContext _db;

    public AccountController(IConfiguration configuration)
    {
        _db = new DataContext(configuration);
    }

    #region GetById
    /// <summary>
    /// Retornar uma conta de acordo com o Id
    /// </summary>
    /// <param name="id">Id da conta</param>
    /// <returns></returns>
    /// <response code="200">Retorna a conta desejada</response>
    /// <response code="404">Conta não encontrada</response>
    [HttpGet]
    [Route("{id}")]
    public IActionResult GetById(int id)
    {
        var account = _db.Account.FirstOrDefault(x => x.Id == id);

        if (account == null)
            return NotFound();

        return Ok(account);
    }
    #endregion

    #region Get
    /// <summary>
    /// Retorna todas as contas
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Retorna a lista de contas</response>
    [HttpGet]
    public IActionResult Get()
    {
        var accounts = _db.Account.ToList();
        return Ok(accounts);
    }
    #endregion

    #region Post
    /// <summary>
    /// Criar uma conta
    /// </summary>
    /// <returns></returns>
    /// <response code="200">conta criada com sucesso</response>
    /// <response code="400">Já existe uma conta com esse e-mail</response>
    /// <response code="400">O usuário deve ter mais de 18 anos para cadastrar</response>
    /// <response code="422">Dados inválidos</response>
    [HttpPost]
    public IActionResult Post(AccountRequest model)
    {
        // validar os dados da plataforma
        // o e-mail não pode ser duplicado na plataforma
        var user = _db.Account.FirstOrDefault(x => x.Email == model.Email);
        if (user != null)
            return BadRequest("Já existe uma conta com este e-mail cadastrado.");

        // o usuário deve ter mais de 18 anos
        var minDate = DateTime.Today.AddYears(-18);
        if (model.BirthDate > minDate)
            return BadRequest("É preciso ter mais de 18 anos para criar uma conta");

        var account = new Account();
        account.Name = model.Name;
        account.Email = model.Email;
        account.Password = model.Password;
        account.BirthDate = model.BirthDate;
        account.CreatedAt = DateTime.UtcNow;

        _db.Add(account);
        _db.SaveChanges();

        return Ok();
    }
    #endregion

    #region Put
    /// <summary>
    /// Editar uma conta
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Conta criada com sucesso</response>
    /// <response code="400">Já existe uma conta com esse nome</response>
    /// <response code="422">Dados inválidos</response>
    /// <response code="404">Conta não encontrada</response>
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] AccountRequest model)
    {
        var account = _db.Account.FirstOrDefault(x => x.Id == id);

        if (account == null)
            return NotFound();

        // o e-mail não pode ser duplicado na plataforma
        var user = _db.Account.FirstOrDefault(x => x.Email == account.Email && x.Id != id);
        if (user != null)
            return BadRequest("Já existe uma conta com este e-mail cadastrado.");

        // o usuário deve ter mais de 18 anos
        var minDate = DateTime.Today.AddYears(-18);
        if (model.BirthDate > minDate)
            return BadRequest("É preciso ter mais de 18 anos para criar uma conta");

        account.Name = model.Name;
        account.Email = model.Email;
        account.BirthDate = model.BirthDate;

        _db.Update(account);
        _db.SaveChanges();

        return Ok();
    }
    #endregion

    #region Delete
    /// <summary>
    /// Excluir uma conta
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Conta excluída com sucesso</response>
    /// <response code="400">Esta conta está relacionada com outras tabelas</response>
    /// <response code="404">conta não encontrada</response>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var account = _db.Account.FirstOrDefault(x => x.Id == id);

        if (account == null)
            return NotFound();

        var hasProfiles = _db.Profile.Any(x => x.AccountId == id);
        if (hasProfiles)
            return BadRequest("Esta conta não pode ser excluída pois possui perfis ativos. Exclua os perfis antes de excluir a conta");

        _db.Remove(account);
        _db.SaveChanges();

        return Ok();
    }
    #endregion
}
