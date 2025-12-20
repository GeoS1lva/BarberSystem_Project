using BarberSystem.Domain.Common;
using BarberSystem.Domain.Entities;
using BarberSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Domain.Interface.Service
{
    public interface IServiceProvidedService
    {
        public Task<ResultPattern<ServiceProvided>> CreateServiceProvided(string name, TimeOnly serviceTime, Category category, double value);
    }
}
