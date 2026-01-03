using BarberSystem.Application.Interfaces.Security;
using Microsoft.AspNetCore.Http;

namespace BarberSystem.Infrastructure.Services.Security
{
    public class AuthCookieService(IHttpContextAccessor httpContextAccessor) : IAuthCookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddMinutes(480)
            };

            _httpContextAccessor.HttpContext?.Response.Cookies.Append("jwt", token, cookieOptions);
        }

        public void RemoveTokenCookie()
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Delete("jwt");
        }
    }
}
