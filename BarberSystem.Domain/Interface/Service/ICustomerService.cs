using BarberSystem.Domain.Common;
using BarberSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Domain.Interface.Service
{
    public interface ICustomerService
    {
        public Task<ResultPattern<Customer>> CreateCustomer(string name, string surname, string cpf, string contact, IdentitySystem identitySystem);
    }
}
