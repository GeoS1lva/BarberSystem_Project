using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Application.DTOs.Response
{
    public class ReturnCustomerResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Contact { get; set; }

        public DateOnly RegistrationDate { get; set; }
    }
}
