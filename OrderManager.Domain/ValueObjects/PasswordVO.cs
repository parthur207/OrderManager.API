using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OrderManager.Domain.ValueObjects
{
    public sealed class PasswordVO
    {
        public string Value { get; }

        public PasswordVO(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Erro. A senha não pode ser vazia.");

            if (password.Length < 6)
                throw new ArgumentException("Erro. A senha deve ter pelo menos 6 caracteres.");

            if (!Regex.IsMatch(password, @"^(?=.*[A-Za-z])(?=.*\d).+$"))
                throw new ArgumentException("Erro. A senha deve conter pelo menos uma letra e um número.");


            Value = password;
        }

        public override string ToString() => Value;

        public override bool Equals(object? obj)
        {
            if (obj is PasswordVO other)
                return Value == other.Value;
            return false;
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}
