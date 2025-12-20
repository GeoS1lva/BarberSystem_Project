using BarberSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Domain.ValueObjects
{
    public record Cpf
    {
        public string Value { get; }

        private Cpf(string value)
        {
            Value = value;
        }

        public static ResultPattern<Cpf> Create(string value)
        {
            value = new string(value.Where(char.IsDigit).ToArray());

            if (value.Length != 11)
                return ResultPattern<Cpf>.Failure("CPF Inválido!");

            if (value.All(c => c == value[0]))
                return ResultPattern<Cpf>.Failure("CPF Inválido!");

            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(value[i].ToString()) * (10 - i);

            int primeiroDigitoVerificador = 11 - (soma % 11);
            if (primeiroDigitoVerificador >= 10)
                primeiroDigitoVerificador = 0;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(value[i].ToString()) * (11 - i);

            int segundoDigitoVerificador = 11 - (soma % 11);
            if (segundoDigitoVerificador >= 10)
                segundoDigitoVerificador = 0;

            if(!(value[9] == primeiroDigitoVerificador.ToString()[0] && value[10] == segundoDigitoVerificador.ToString()[0]))
                return ResultPattern<Cpf>.Failure("CPF Inválido!");

            return ResultPattern<Cpf>.Success(new Cpf(value));
        }
    }
}
