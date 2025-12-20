using BarberSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Application.DTOs.Response
{
    public class SchedulingResponse
    {
        public string UserName { get; set; }
        public string CustomerName { get; set; }
        public DateTime StartDateTime { get; set; }
        public List<SchedulingServiceResponse> Services { get; set; }
        public double TotalValue { get; set; }
    }
}
