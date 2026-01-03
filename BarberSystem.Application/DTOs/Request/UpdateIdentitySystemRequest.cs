using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Application.DTOs.Request
{
    public class UpdateIdentitySystemRequest
    {
        public int Id { get; set; }
        public string NewEmail { get; set; }
    }
}
