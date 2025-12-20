using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Application.DTOs.Request
{
    public class SchedulingRequest
    {
        public int UserId { get; set; }
        public int CustomerId { get; set; }
        public DateTime AttemptStartDateTime { get; set; }
        public List<int> ServicesProvidedIds { get; set; }
    }
}
