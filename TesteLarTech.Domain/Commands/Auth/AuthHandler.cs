using AutoMapper;
using MediatR;
using TesteLarTech.Core.Notifications;
using TesteLarTech.Domain.Commands;
using TesteLarTech.Domain.Repositories;
using TesteLarTech.Domain.ViewModels;

namespace TesteLarTech.Domain.Commands.Handlers
{
    public class AuthHandler : IRequestHandler<AuthCommand, PessoaViewModel>
    {
        #region Declarações e Construtor

        private readonly IAuthRepository _authRepository;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;
        public AuthHandler(IAuthRepository authRepository, INotificationService notificationService, IMapper mapper)
        {
            _authRepository = authRepository;
            _notificationService = notificationService;
            _mapper = mapper;
        }

        #endregion

        #region LoginAuthentication

        public async Task<PessoaViewModel> Handle(AuthCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                _notificationService.Notify("Erro ao realizar Login", "Dados insuficientes ou inválidos!");

            var loginDataResult = await _authRepository.Login(request.Email, request.Senha);

            var mapperResult = _mapper.Map<PessoaViewModel>(loginDataResult);
            
            return mapperResult;
        }

        #endregion
    }
}
