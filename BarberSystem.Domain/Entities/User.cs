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
            if (string.IsNullOrEmpty(name))
                return ResultPattern<User>.Failure("Nome não pode ser nulo!");

            if (name.Length < 2)
                return ResultPattern<User>.Failure("Nome precisa ter ao menos 3 caracteres");

            if (string.IsNullOrEmpty(surname))
                return ResultPattern<User>.Failure("Sobrenome não pode ser nulo!");

            if (surname.Length < 2)
                return ResultPattern<User>.Failure("Sobrenome precisa ter ao menos 3 caracteres");

            if(contact.Length < 11)
                return ResultPattern<User>.Failure("Contato inválido!");

            return ResultPattern<User>.Success(new User(name, surname, cpf, contact, identitySystem));
        }

        protected User() { }
    }
}
