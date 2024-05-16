using Microsoft.AspNetCore.Mvc;
using Unicv.Streaming.Api.Data.Context;
using Unicv.Streaming.Api.Data.Entities;
using Unicv.Streaming.Api.Models.Requests;

namespace Unicv.Streaming.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly DataContext _db;

    public CategoryController(IConfiguration configuration)
    {
        _db = new DataContext(configuration);
    }

    #region GetById
    /// <summary>
    /// Retornar uma categoria de acordo com o Id
    /// </summary>
    /// <param name="id">Id da categoria</param>
    /// <returns></returns>
    /// <response code="200">Retorna a categoria desejada</response>
    /// <response code="404">Categoria não encontrada</response>
    [HttpGet]
    [Route("{id}")]
    public IActionResult GetById(int id)
    {
        var category = _db.Category.FirstOrDefault(x => x.Id == id);

        if (category == null)
            return NotFound();

        return Ok(category);
    }
    #endregion

    #region Get
    /// <summary>
    /// Retorna todas as categorias
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Retorna a lista de categorias</response>
    [HttpGet]
    public IActionResult Get()
    {
        var categories = _db.Category.ToList();
        return Ok(categories);
    }
    #endregion

    #region Post
    /// <summary>
    /// Criar uma categoria
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Categoria criada com sucesso</response>
    /// <response code="400">Já existe uma categoria com esse nome</response>
    /// <response code="422">Dados inválidos</response>
    [HttpPost]
    public IActionResult Post(CategoryRequest model)
    {
        // validar se existe uma categoria criada com o mesmo nome
        var entity = _db.Category.FirstOrDefault(x => x.Name == model.Name);
        if (entity != null)
            return BadRequest("Já existe uma categoria com este nome.");

        var category = new Category();
        category.Name = model.Name;
        category.CreatedAt = DateTime.UtcNow;

        _db.Add(category);
        _db.SaveChanges();

        return Ok(model);

    }
    #endregion

    #region Put
    /// <summary>
    /// Editar uma categoria
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Categoria criada com sucesso</response>
    /// <response code="400">Já existe uma categoria com esse nome</response>
    /// <response code="422">Dados inválidos</response>
    /// <response code="404">Categoria não encontrada</response>
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] CategoryRequest model)
    {
        var category = _db.Category.FirstOrDefault(x => x.Id == id);

        if (category == null)
            return NotFound();

        var entity = _db.Category.FirstOrDefault(x => x.Name == model.Name && x.Id != id);
        if (entity != null)
            return BadRequest("Já existe uma categoria com este nome.");

        category.Name = model.Name;

        _db.Update(category);
        _db.SaveChanges();

        return Ok(model);
    }
    #endregion

    #region Delete
    /// <summary>
    /// Excluir uma categoria
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Categoria excluída com sucesso</response>
    /// <response code="400">Esta categoria está relacionada com outras tabelas</response>
    /// <response code="404">Categoria não encontrada</response>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var category = _db.Category.FirstOrDefault(x => x.Id == id);

        if (category == null)
            return NotFound();

        _db.Remove(category);
        _db.SaveChanges();

        return Ok();
    }
    #endregion
}
