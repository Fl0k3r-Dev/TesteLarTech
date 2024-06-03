using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using System.Security.Claims;
using TesteLarTech.Domain.Service.Interfaces;
using TesteLarTech.Domain.ViewModels;
using AuthenticationProperties = Microsoft.AspNetCore.Authentication.AuthenticationProperties;

namespace TesteLarTech.Domain.Service
{
    public class LoginService : ILoginService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private const string EmailClaimType = ClaimTypes.Email;
        private const string RoleClaimType = ClaimTypes.Role;
        private const string ActiveClaimType = "ativo";

        public LoginService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task Login(PessoaViewModel data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var claims = new List<Claim>
            {
                new Claim(EmailClaimType, data.Email ?? string.Empty),
                new Claim(RoleClaimType, data.Nivel ?? string.Empty),
                new Claim(ActiveClaimType, data.Ativo.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(4),
                IssuedUtc = DateTimeOffset.UtcNow
            };

            var context = _httpContextAccessor.HttpContext;
            if (context != null)
            {
                await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProperties);
            }
        }

        public async Task Logoff()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context != null)
            {
                await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
        }
    }
}
