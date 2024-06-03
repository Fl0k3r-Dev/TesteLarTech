using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TesteLarTech.Core.Notifications;
using TesteLarTech.Domain.Constants;
using TesteLarTech.Domain.Interfaces.Repositories.Base;
using TesteLarTech.Domain.ViewModels;

namespace TesteLarTech.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
        where T : class
    {
        #region Declarações e Construtor

        private readonly IRepositoryBase<T> _repository;
        private readonly INotificationService _notificationService;

        public BaseController(INotificationService notificationService,
                              IRepositoryBase<T> repository)
        {
            _notificationService = notificationService;
            _repository = repository;
        }

        #endregion

        /// <summary>
        /// Obtém uma lista paginada de objetos.
        /// </summary>
        /// <param name="termoBusca">Termo de busca opcional.</param>
        /// <param name="pagina">Número da página.</param>
        /// <param name="tamanhoPaginas">Tamanho da página.</param>
        /// <returns>Lista paginada de objetos.</returns>
        [Authorize(Roles = Roles.Todos)]
        [HttpGet("paginacao")]
        [SwaggerResponse(StatusCodes.Status200OK, "Retorna uma lista paginada de objetos.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Erro na requisição.", typeof(object))]
        public async Task<IActionResult> Paginacao(string? termoBusca, int pagina = 1, int tamanhoPaginas = 10)
        {
            var request = new PaginacaoViewModel<T>()
            {
                TermoBusca = termoBusca,
                Pagina = pagina,
                TamanhoPaginas = tamanhoPaginas
            };
            var result = await _repository.GetAllWithPagination(request);

            if (_notificationService.HasNotifications())
            {
                return BadRequest(new { Errors = _notificationService.GetNotifications() });
            }

            return Ok(new ResponseViewModel<PaginacaoViewModel<T>>(result));
        }

        /// <summary>
        /// Obtém um objeto específico por ID.
        /// </summary>
        /// <param name="id">ID do objeto.</param>
        /// <returns>Objeto encontrado.</returns>
        [Authorize(Roles = Roles.Todos)]
        [HttpGet("getById")]
        [SwaggerResponse(StatusCodes.Status200OK, "Retorna o objeto especificado.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Erro na requisição.", typeof(object))]
        public async Task<IActionResult> GetSpecifyGuidId([FromQuery] Guid id)
        {
            var result = await _repository.GetById(id);

            if (_notificationService.HasNotifications())
            {
                return BadRequest(new { Errors = _notificationService.GetNotifications() });
            }

            return Ok(new ResponseViewModel<T>(result));
        }

        /// <summary>
        /// Exclui um objeto específico por ID.
        /// </summary>
        /// <param name="id">ID do objeto.</param>
        /// <returns>Mensagem de sucesso.</returns>
        [Authorize(Roles = Roles.Todos)]
        [HttpDelete("delete/{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Objeto excluído com sucesso.", typeof(object))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Erro na requisição.", typeof(object))]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _repository.DeleteAsync(id);

            if (_notificationService.HasNotifications())
            {
                return BadRequest(new { Errors = _notificationService.GetNotifications() });
            }

            return Ok(new { message = "Sucesso ao excluir!" });
        }
    }
}
