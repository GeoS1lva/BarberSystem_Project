using BarberSystem.Application.DTOs.Request;
using BarberSystem.Application.DTOs.Response;
using BarberSystem.Application.Interfaces;
using BarberSystem.Application.Interfaces.Queries;
using BarberSystem.Domain.Common;
using BarberSystem.Domain.Entities;
using BarberSystem.Domain.Interface.Repositories;
using BarberSystem.Domain.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Application.Services
{
    public class SchedulingAppService(IUnitOfWork unitOfWork, ISchedulingS schedulingS, ISchedulingQueries schedulingQueries) : ISchedulingAppService
    {
        public async Task<ResultPattern<SchedulingResponse>> CreateAsync(SchedulingRequest schedulingRequest)
        {
            if (!await unitOfWork.UserRepository.ValidateUser(schedulingRequest.UserId))
                return ResultPattern<SchedulingResponse>.Failure("Usuário não encontrado!");

            var user = await unitOfWork.UserRepository.GetById(schedulingRequest.UserId);

            if (!await unitOfWork.CustomerRepository.ValidateCustomer(schedulingRequest.CustomerId))
                return ResultPattern<SchedulingResponse>.Failure("Cliente não encontrado!");

            var customer = await unitOfWork.CustomerRepository.GetById(schedulingRequest.CustomerId);

            var services = await unitOfWork.ServiceProvidedRepository.GetByIds(schedulingRequest.ServicesProvidedIds);

            if (services == null)
                return ResultPattern<SchedulingResponse>.Failure("Serviço/Serviços não encontrados!");

            var result = await schedulingS.AddSchedule(schedulingRequest.UserId, schedulingRequest.CustomerId, schedulingRequest.AttemptStartDateTime, services);

            if (result.Error)
                return ResultPattern<SchedulingResponse>.Failure(result.ErrorMessage);

            unitOfWork.SchedulingRepository.AddScheduling(result.Value);

            await unitOfWork.SaveChangesAsync();

            return ResultPattern<SchedulingResponse>.Success(new SchedulingResponse
            {
                UserName = user.Name,
                CustomerName = customer.Name,
                StartDateTime = result.Value.StartDateTime,
                Services = services.Select(s => new SchedulingServiceResponse
                {
                    Name = s.Name,
                    Value = s.Value
                }).ToList(),
                TotalValue = result.Value.TotalValue
            });
        }

        public async Task<ResultPattern> CancelAsync(int idScheduling)
        {
            var scheduling = await unitOfWork.SchedulingRepository.GetById(idScheduling);

            if (scheduling == null)
                return ResultPattern.Failure("Agendamento não encontrado!");

            scheduling.CancelScheduling();

            await unitOfWork.SaveChangesAsync();

            return ResultPattern.Success();
        }

        public async Task<ResultPattern<List<SchedulingResponse>>> GetAllByCustomer(int customerId)
        {
            var result = await schedulingQueries.GetAllBySchedulingCustomer(customerId);

            if (result.Count == 0)
                return ResultPattern<List<SchedulingResponse>>.Failure("Nenhum agendamento encontrado!");

            return ResultPattern<List<SchedulingResponse>>.Success(result);
        }

        public async Task<ResultPattern<List<SchedulingResponse>>> GetAllByUser(int userId)
        {
            var result = await schedulingQueries.GetAllBySchedulingUser(userId);

            if (result.Count == 0)
                return ResultPattern<List<SchedulingResponse>>.Failure("Nenhum agendamento encontrado!");

            return ResultPattern<List<SchedulingResponse>>.Success(result);
        }
    }
}
