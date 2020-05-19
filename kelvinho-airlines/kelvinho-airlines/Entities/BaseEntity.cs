using System;

namespace kelvinho_airlines.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; private set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}