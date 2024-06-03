using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using TesteLarTech.Api.Controller;
using TesteLarTech.Core.Notifications;
using TesteLarTech.Domain.Commands;
using TesteLarTech.Domain.Commands.Pessoa.AdicionarPessoa;
using TesteLarTech.Domain.Commands.Pessoa.AtualizarPessoa;
using TesteLarTech.Domain.Constants;
using TesteLarTech.Domain.Entities;
using TesteLarTech.Domain.Interfaces.Repositories;
using TesteLarTech.Domain.Service.Interfaces;
using TesteLarTech.Domain.ViewModels;


namespace TesteLarTech.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : BaseController<Pessoa>
    {

        #region Declarações e Construtor

        private readonly IMediator _mediator;
        private readonly INotificationService _notificationService;

        public PessoaController(IMediator mediator,
                                INotificationService notificationService,
                                IPessoaRepository pessoaRepository) : base(notificationService, pessoaRepository)
        {
            _mediator = mediator;
            _notificationService = notificationService;
        }

        #endregion  

        #region Cadastro Pessoa

        [Authorize(Roles = Roles.UsuarioDefault)]
        [HttpPost("cadastroPessoa")]
        public async Task<IActionResult> CadastroPessoa([FromBody] PessoaViewModel data)
        {
            DateTime temp;
            if (!DateTime.TryParse(data.Nascimento.ToString(), out temp))
                return BadRequest(new { Errors = "Data de nascimento informada inválida!" });

            if (data == null)
                return BadRequest(new { Errors = "Erro ao registrar nova pessoa!" });

            var command = new AdicionarPessoaCommand(data.Nome, data.CPF, data.Nascimento, data.Email, data.Password, data.Nivel, data.Contatos);

            await _mediator.Send(command);

            if (_notificationService.HasNotifications())
            {
                return BadRequest(new { Errors = _notificationService.GetNotifications() });
            }

            return Ok(new { message = "Registro realizado com sucesso!" });
        }

        #endregion

        #region Atualizar Pessoa

        [Authorize(Roles = Roles.UsuarioDefault)]
        [HttpPut("atualizarPessoa")]
        public async Task<IActionResult> AtualizarPessoa([FromBody] PessoaViewModel data)
        {
            if (data == null)
                return BadRequest(new { Errors = "Erro ao atualizar pessoa!" });

            var command = new AtualizarPessoaCommand(data.Id, data.Nome, data.CPF, data.Nascimento, data.Email, data.Password, data.Nivel, data.Contatos);

            await _mediator.Send(command);

            if (_notificationService.HasNotifications())
            {
                return BadRequest(new { Errors = _notificationService.GetNotifications() });
            }

            return Ok(new { message = "Registro atualizado com sucesso!" });
        }

        #endregion
    }
}
