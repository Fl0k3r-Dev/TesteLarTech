using MediatR;
using TesteLarTech.Core.Notifications;
using TesteLarTech.Domain.Interfaces.Repositories;
using TesteLarTech.Domain.Entities;
using TesteLarTech.Domain.ViewModels;
using TesteLarTech.Core.Hash;
using System.Security.Cryptography;
using AutoMapper;
using TesteLarTech.Domain.Commands.Pessoa.AtualizarPessoa;

namespace TesteLarTech.Domain.Commands.Pessoa
{
    public class AtualizarPessoaHandle : IRequestHandler<AtualizarPessoaCommand, Response>
    {
        private readonly IPessoaRepository _repositoryPessoa;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public AtualizarPessoaHandle(INotificationService notificationService, IPessoaRepository pessoaRepository, IMapper mapper)
        {
            _repositoryPessoa = pessoaRepository;
            _notificationService = notificationService;
            _mapper = mapper;
        }

        public async Task<Response> Handle(AtualizarPessoaCommand request, CancellationToken cancellationToken)
        {
            bool userEmailExists = await _repositoryPessoa.EmailExists(request.Email);
            if (!userEmailExists)
            {
                _notificationService.Notify(nameof(request.Email), "E-mail não existe!");
                return new Response(null);
            }

            var pessoaObj = await _repositoryPessoa.GetById(request.Id);
            if (pessoaObj == null)
            {
                _notificationService.Notify("Erro", "Usuário não encontrado!");
                return new Response(null);
            }

            if (!string.IsNullOrEmpty(request.Email))
            {
                pessoaObj.SetEmail(request.Email);
            }

            if (!string.IsNullOrEmpty(request.Password))
            {
                pessoaObj.SetPassword(request.Password);
            }

            if (!string.IsNullOrEmpty(request.Nome))
            {
                pessoaObj.SetNome(request.Nome);
            }

            var result = await _repositoryPessoa.UpdateAsync(pessoaObj);

            if (!result)
            {
                _notificationService.Notify("Erro", "Erro ao persistir dados do usuário!");
                return new Response(null);
            }

            return new Response(pessoaObj);
        }
    }
}
