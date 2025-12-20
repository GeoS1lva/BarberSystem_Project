using BarberSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Domain.Interface.Repositories
{
    public interface IServiceProvidedRepository
    {
        public void AddServiceProvided(ServiceProvided serviceProvided);
        public Task<List<ServiceProvided>?> GetByIds(List<int> ids);
        public Task<bool> GetByName(string name);
    }
}
