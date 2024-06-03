

using System.Security.Claims;
using System.Text;
using TesteLarTech.Core.Key;
using TesteLarTech.Core.Notifications;
using TesteLarTech.Domain.Service.Interfaces;

namespace TesteLarTech.Domain.Service
{
    public class TokenService : ITokenService
    {

        private readonly INotificationService _notificationService;

        public TokenService(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        public object GerarToken(Guid id, string tipo)
        {
            if (string.IsNullOrEmpty(tipo))
            {
                _notificationService.Notify("Tipo usuário inválido", "Usuário inválido!");
                return new { };
            }
            else if (id.Equals(Guid.Empty))
            {
                _notificationService.Notify("Erro ao obter usuário", "Usuário não encontrado!");

                return new { };

            };

            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(Secret.SecretKey);
            var tokenDescriptor = new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                    new Claim(ClaimTypes.Role, tipo)
                }),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials =
                new Microsoft.IdentityModel.Tokens.SigningCredentials(new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key),
                Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var strToken = tokenHandler.WriteToken(token);
            return strToken;
        }
    }
}