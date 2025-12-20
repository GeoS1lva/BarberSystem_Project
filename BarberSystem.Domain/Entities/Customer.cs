using BarberSystem.Domain.Common;
using BarberSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Domain.Entities
{
    public sealed class Customer : EntityUserBase
    {
        public DateOnly RegistrationDate { get; private set; }

        public int IdentitySystemId { get; set; }
        public IdentitySystem IdentitySystem { get; set; }

        public ICollection<Scheduling> Schedulings { get; private set; }

        public Customer(string name, string surname, Cpf cpf, string contact, IdentitySystem identitySystem)
        {
            Name = name;
            Surname = surname;
            CPF = cpf;
            Contact = contact;
            IdentitySystem = identitySystem;
            RegistrationDate = DateOnly.FromDateTime(DateTime.Now);
        }

        public static ResultPattern<Customer> Create(string name, string surname, Cpf cpf, string contact, IdentitySystem identitySystem)
        {
            if (string.IsNullOrEmpty(name))
                return ResultPattern<Customer>.Failure("Nome não pode ser nulo!");

            if (name.Length < 2)
                return ResultPattern<Customer>.Failure("Nome precisa ter ao menos 3 caracteres");

            if (string.IsNullOrEmpty(surname))
                return ResultPattern<Customer>.Failure("Sobrenome não pode ser nulo!");

            if (surname.Length < 2)
                return ResultPattern<Customer>.Failure("Sobrenome precisa ter ao menos 3 caracteres");

            if (string.IsNullOrEmpty(contact))
                return ResultPattern<Customer>.Failure("Contato não pode ser nulo!");

            if (contact.Length < 11)
                return ResultPattern<Customer>.Failure("Contato inválido!");

            if (identitySystem == null)
                return ResultPattern<Customer>.Failure("Identidade de Login inválida!");

            return ResultPattern<Customer>.Success(new Customer(name, surname, cpf, contact, identitySystem));
        }

        protected Customer() { }
    }
}
