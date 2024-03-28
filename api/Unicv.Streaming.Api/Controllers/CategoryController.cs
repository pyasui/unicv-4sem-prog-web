using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Unicv.Streaming.Api.Models.Requests;

namespace Unicv.Streaming.Api.Controllers
{
    [ApiController]
    [Route("category")]
    public class CategoryController : ControllerBase
    {
        #region Get
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
            return Ok();
        }
        #endregion

        /// <summary>
        /// Retorna todas as categorias
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Retorna a lista de categorias</response>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

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
             return Ok(model);
        }

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
            return Ok();
        }

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
            return Ok();
        }

    }
}
