using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Cryptography;
using TesteLarTech.Core.Hash;
using TesteLarTech.Core.Notifications;
using TesteLarTech.Domain.Commands;
using TesteLarTech.Domain.Constants;
using TesteLarTech.Domain.Service.Interfaces;
using TesteLarTech.Domain.ViewModels;


namespace TesteLarTech.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        #region Declarações e Construtor

        private readonly IMediator _mediator;
        private readonly ITokenService _tokenService;
        private readonly ILoginService _loginService;
        private readonly INotificationService _notificationService;

        public AuthController(IMediator mediator,
                              ILoginService loginService,
                              ITokenService tokenService,
                              INotificationService notificationService)
        {
            _mediator = mediator;
            _tokenService = tokenService;
            _loginService = loginService;
            _notificationService = notificationService;
        }

        #endregion

        #region Login

        /// <summary>
        /// Realiza o login de um usuário.
        /// </summary>
        /// <param name="request">Dados do usuário para login.</param>
        /// <returns>Token JWT e dados do usuário.</returns>
        [AllowAnonymous]
        [HttpPost("login")]
        [SwaggerOperation(Summary = "Realiza o login de um usuário.", Description = "Endpoint para login de usuários.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Login realizado com sucesso.", typeof(object))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Login não autorizado.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Erro na requisição.", typeof(object))]
        public async Task<IActionResult> Login([FromBody] LoginViewModel request)
        {
            if (request == null)
            {
                return Unauthorized();
            }

            var command = new AuthCommand(request.Email, request.Password);
            var result = await _mediator.Send(command);

            if (result == null)
            {
                return Unauthorized();
            }

            if (_notificationService.HasNotifications())
            {
                return BadRequest(new { Errors = _notificationService.GetNotifications() });
            }

            var tokenString = _tokenService.GerarToken(result.Id, result.Nivel);
            await _loginService.Login(result);

            return Ok(new
            {
                token = tokenString,
                usuario = result
            });
        }

        #endregion

        #region Logout
        /// <summary>
        /// Realiza o logout do usuário.
        /// </summary>
        /// <returns>Mensagem de sucesso.</returns>
        [Authorize(Roles = Roles.Todos)]
        [HttpPost("logout")]
        [SwaggerOperation(Summary = "Realiza o logout do usuário.", Description = "Endpoint para logout de usuários.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Logout realizado com sucesso.", typeof(object))]
        public async Task<IActionResult> Logout()
        {
            await _loginService.Logoff();
            return Ok(new { message = "Logout realizado com sucesso!" });
        }

        #endregion
    }
}
