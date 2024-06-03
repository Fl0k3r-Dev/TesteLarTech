using MediatR;
using TesteLarTech.Core.Notifications;
using TesteLarTech.Domain.Interfaces.Repositories;
using TesteLarTech.Domain.Entities;
using TesteLarTech.Domain.ViewModels;
using TesteLarTech.Core.Hash;
using System.Security.Cryptography;
using AutoMapper;

namespace TesteLarTech.Domain.Commands.Pessoa.AdicionarPessoa
{
    public class AdicionarPessoaHandle : IRequestHandler<AdicionarPessoaCommand, Response>
    {
        private readonly IPessoaRepository _repositoryPessoa;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public AdicionarPessoaHandle(INotificationService notificationService, IPessoaRepository pessoaRepository, IMapper mapper)
        {
            _repositoryPessoa = pessoaRepository;
            _notificationService = notificationService;
            _mapper = mapper;
        }

        public async Task<Response> Handle(AdicionarPessoaCommand request, CancellationToken cancellationToken)
        {
            bool userEmailExists = await _repositoryPessoa.EmailExists(request.Email);
            if (userEmailExists)
            {
                _notificationService.Notify(nameof(request.Email), "E-mail já em uso!");
            }

            var pessoaObj = new Entities.Pessoa(
                request.Email,
                new Encrypt(SHA512.Create()).EncryptString(request.Password),
                request.Nivel,
                request.Nome,
                request.CPF,
                request.Nascimento,
                request.Ativo);

            foreach (var telefone in request.Contatos)
            {
                var contatoMapper = _mapper.Map<Telefone>(telefone);
                pessoaObj.Telefones.Add(contatoMapper);
            }

            var result = await _repositoryPessoa.Add(pessoaObj);

            if (!result)
            {
                _notificationService.Notify("Erro", "Erro ao persistir dados do usuário!");
            }

            return new Response(pessoaObj);
        }
    }
}
