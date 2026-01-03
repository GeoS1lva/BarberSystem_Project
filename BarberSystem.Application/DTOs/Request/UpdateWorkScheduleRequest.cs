using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Application.DTOs.Request
{
    public class UpdateWorkScheduleRequest
    {
        public int IdUser { get; set; }
        public TimeOnly NewStartTime { get; set; }
        public TimeOnly NewEndTime { get; set; }
    }
}
