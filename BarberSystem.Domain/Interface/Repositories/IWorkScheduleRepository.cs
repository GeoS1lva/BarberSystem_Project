using BarberSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Domain.Interface.Repositories
{
    public interface IWorkScheduleRepository
    {
        public void AddWorlSchedule(WorkSchedule workSchedule);
    }
}
