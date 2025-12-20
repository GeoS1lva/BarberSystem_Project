using BarberSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Application.DTOs.Request
{
    public class CreateServiceProvidedRequest
    {
        public string Name { get; set; }
        public TimeOnly ServiceTime { get; set; }
        public Category Category { get; set; }
        public double Value { get; set; }
    }
}
