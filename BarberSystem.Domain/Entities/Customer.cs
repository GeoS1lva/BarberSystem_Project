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
            if (string.IsNullOrEmpty(name) || name.Length < 2)
                return ResultPattern<Customer>.Failure("Nome Inválido");

            if (string.IsNullOrEmpty(surname) || surname.Length < 2)
                return ResultPattern<Customer>.Failure("Sobrenome Inválido");

            if (string.IsNullOrEmpty(contact) || contact.Length < 11)
                return ResultPattern<Customer>.Failure("Contato inválido!");

            return ResultPattern<Customer>.Success(new Customer(name, surname, cpf, contact, identitySystem));
        }

        public ResultPattern<Customer> Update(string name, string surname, string contact)
        {
            if (string.IsNullOrEmpty(name) || name.Length < 2)
                return ResultPattern<Customer>.Failure("Nome Inválido");

            if (string.IsNullOrEmpty(surname) || surname.Length < 2)
                return ResultPattern<Customer>.Failure("Sobrenome Inválido");

            if (string.IsNullOrEmpty(contact) || contact.Length < 11)
                return ResultPattern<Customer>.Failure("Contato inválido!");

            Name = name;
            Surname = surname;
            Contact = contact;

            return ResultPattern<Customer>.Success(this);
        }

        protected Customer() { }
    }
}
