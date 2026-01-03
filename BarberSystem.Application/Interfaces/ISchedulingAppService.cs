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
    public interface ISchedulingAppService
    {
        public Task<ResultPattern<SchedulingResponse>> CreateAsync(SchedulingRequest schedulingRequest);
        public Task<ResultPattern> CancelAsync(int idScheduling);
        public Task<ResultPattern<List<SchedulingResponse>>> GetAllByCustomer(int customerId);
        public Task<ResultPattern<List<SchedulingResponse>>> GetAllByUser(int userId);
    }
}
