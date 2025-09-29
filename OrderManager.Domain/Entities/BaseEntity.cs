using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Domain.Entities
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }
        public int Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public bool IsActive { get; private set; }

        public bool SetToInactive()
        {
            if (IsActive is true)
            {
                IsActive = false;
                return true;
            }
            return false;
        }

        public bool SetToActive()
        {
            if (IsActive is false)
            {
                IsActive = true;
                return true;
            }
            return false;
        }

    }
}
