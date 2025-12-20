using BarberSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Domain.Common
{
    public abstract class EntityUserBase
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }
        public string Surname { get; protected set; }
        public Cpf CPF { get; protected set; }
        public string Contact { get; protected set; }
    }
}
