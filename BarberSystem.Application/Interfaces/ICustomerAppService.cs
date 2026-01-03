using BarberSystem.Application.DTOs.Request;
using BarberSystem.Application.DTOs.Response;
using BarberSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Application.Interfaces
{
    public interface ICustomerAppService
    {
        public Task<ResultPattern<CreateCustomerResponse>> CreateAsync(CreateCustomerRequest customer);
        public Task<ResultPattern<UpdateCustomerResponse>> UpdateAsync(UpdateCustomerRequest updateCustomer);
        public Task<ResultPattern<List<ReturnCustomerResponse>>> GetAllCustomersAsync();
    }
}
