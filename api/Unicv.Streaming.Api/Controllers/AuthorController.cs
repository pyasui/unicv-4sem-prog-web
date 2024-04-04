using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Unicv.Streaming.Api.Models.Requests;

namespace Unicv.Streaming.Api.Controllers
{
    [ApiController]
    [Route("author")]
    public class AuthorController : ControllerBase
    {
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
            return Ok();
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
            return Ok();
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
        public IActionResult Post(AuthorRequest model)
        {
            return Ok(model);
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
        public IActionResult Put(int id, [FromBody] AuthorRequest model)
        {
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
            return Ok();
        }
        #endregion
    }
}
