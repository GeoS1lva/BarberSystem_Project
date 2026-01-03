using BarberSystem.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Application.Interfaces.Queries
{
    public interface ICustomerQueries
    {
        public Task<List<ReturnCustomerResponse>?> GetAllCustomers();
    }
}
