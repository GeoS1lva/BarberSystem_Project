using BarberSystem.Domain.Common;
using System.Text.RegularExpressions;

namespace BarberSystem.Domain.ValueObjects
{
    public record Email
    {
        public string Value { get; }

        private Email(string value)
        {
            Value = value;
        }

        public static ResultPattern<Email> Create(string value)
        {
            if (string.IsNullOrEmpty(value))
                return ResultPattern<Email>.Failure("Email Inválido!");

            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            if (!regex.IsMatch(value))
                return ResultPattern<Email>.Failure("Email Inválido!");

            return ResultPattern<Email>.Success(new Email(value));
        }
    }
}
