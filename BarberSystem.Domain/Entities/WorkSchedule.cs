using BarberSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Domain.Entities
{
    public sealed class WorkSchedule : EntityBase
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        private WorkSchedule(User user, TimeOnly startTime, TimeOnly endTime)
        {
            User = user;
            StartTime = startTime;
            EndTime = endTime;
        }

        public static ResultPattern<WorkSchedule> Create(User user, TimeOnly startTime, TimeOnly endTime)
        {
            TimeOnly barberOpeningHours = new TimeOnly(8, 00);
            TimeOnly barberClosingHours = new TimeOnly(18, 00);

            if (startTime < barberOpeningHours)
                return ResultPattern<WorkSchedule>.Failure("Colaborador inicia antes do horário de expediente!");

            if (endTime > barberClosingHours)
                return ResultPattern<WorkSchedule>.Failure("Colaborador sai após o fim do expediente!");

            return ResultPattern<WorkSchedule>.Success(new WorkSchedule(user, startTime, endTime));
        }

        public ResultPattern Update (TimeOnly newStartTime, TimeOnly newEndTime)
        {
            TimeOnly barberOpeningHours = new TimeOnly(8, 00);
            TimeOnly barberClosingHours = new TimeOnly(18, 00);

            if (newStartTime < barberOpeningHours)
                return ResultPattern.Failure("Colaborador inicia antes do horário de expediente!");

            if (newEndTime < barberClosingHours)
                return ResultPattern.Failure("Colaborador sai após o fim do expediente!");

            StartTime = newStartTime;
            EndTime = newEndTime;

            return ResultPattern.Success();
        }

        protected WorkSchedule() { }
    }
}
