using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Domain.ValueObjects
{
    public sealed class OrderNumberVO
    {
        public int Value { get; }

        public OrderNumberVO(int orderNumber)
        {
            if (orderNumber < 1000)
                throw new ArgumentException("Erro. O número do pedido deve ter pelo menos 4 dígitos.");

            if (orderNumber <= 0)
                throw new ArgumentException("Erro. O número do pedido deve ser positivo.");

            Value = orderNumber;
        }

        public override string ToString() => Value.ToString();

        public override bool Equals(object? obj)
        {
            if (obj is OrderNumberVO other)
                return Value == other.Value;
            return false;
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}
