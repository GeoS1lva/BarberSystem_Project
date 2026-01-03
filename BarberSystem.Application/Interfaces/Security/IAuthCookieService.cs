using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Application.Interfaces.Security
{
    public interface IAuthCookieService
    {
        public void SetTokenCookie(string token);
        public void RemoveTokenCookie();
    }
}
