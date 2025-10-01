using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OrderManager.Domain.ValueObjects
{
    public sealed class UserEmailVO
    {
        public string Value { get; }

        public UserEmailVO(string email) 
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Erro. O email não pode ser vazio.");

            }

            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new ArgumentException("Erro. Email ínválido");
            }
            Value = email;
        }
        public override string ToString()
        {
            return Value;
        }
    }
}
