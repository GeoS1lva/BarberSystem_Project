using BarberSystem.Domain.Common;
using BarberSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Domain.Entities
{
    public sealed class User : EntityUserBase
    {
        public DateOnly HiringDate { get; private set; }
        public DateOnly? DismissalDate { get; private set; }

        public int IdentitySystemId { get; private set; }
        public IdentitySystem IdentitySystem { get; private set; }

        public WorkSchedule? WorkSchedule { get; private set; }
        public ICollection<Scheduling> Schedulings { get; private set; }

        private User(string name, string surname, Cpf cpf, string contact, IdentitySystem identitySystem)
        {
            Name = name;
            Surname = surname;
            CPF = cpf;
            Contact = contact;
            IdentitySystem = identitySystem;
            HiringDate = DateOnly.FromDateTime(DateTime.Now);
            DismissalDate = null;
        }

        public static ResultPattern<User> Create(string name, string surname, Cpf cpf, string contact, IdentitySystem identitySystem)
        {
            if (string.IsNullOrEmpty(name) || name.Length < 3)
                return ResultPattern<User>.Failure("Nome Inválido");

            if (string.IsNullOrEmpty(surname) || surname.Length < 3)
                return ResultPattern<User>.Failure("Sobrenome Inválido");

            if(string.IsNullOrEmpty(contact) || contact.Length < 11)
                return ResultPattern<User>.Failure("Contato Inválido");

            return ResultPattern<User>.Success(new User(name, surname, cpf, contact, identitySystem));
        }

        public ResultPattern<User> Update(string name, string surname, string contact)
        {
            if (string.IsNullOrEmpty(name) || name.Length < 2)
                return ResultPattern<User>.Failure("Nome Inválido");

            if (string.IsNullOrEmpty(surname) || surname.Length < 2)
                return ResultPattern<User>.Failure("Sobrenome Inválido");

            if (string.IsNullOrEmpty(contact) || contact.Length < 11)
                return ResultPattern<User>.Failure("Contato inválido!");

            Name = name;
            Surname = surname;
            Contact = contact;

            return ResultPattern<User>.Success(this);
        }

        protected User() { }
    }
}
