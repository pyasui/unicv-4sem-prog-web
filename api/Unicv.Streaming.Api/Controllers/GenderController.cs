using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Unicv.Streaming.Api.Models.Requests;

namespace Unicv.Streaming.Api.Controllers
{
    [ApiController]
    [Route("gender")]
    public class GenderController : ControllerBase
    {
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
            return Ok();
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
            return Ok();
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
            return Ok();
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
            return Ok();
        }
        #endregion
    }
}
