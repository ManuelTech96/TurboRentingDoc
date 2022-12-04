using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboRentingv2.Api.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public object? EntityId { get; private set; }
        public Type? EntityType { get; private set; }

        public EntityNotFoundException(string message) : base(message)
        {

        }


        public EntityNotFoundException(object? entityId, Type entityType) : this($"Entity of type \"{entityType.Name}\" and identifier \"{entityId ?? "NULL"}\" not found")
        {
            EntityId = entityId;
            EntityType = entityType;
        }
    }

    public class EntityNotFoundException<T> : EntityNotFoundException
    {
        public EntityNotFoundException(object? entityId) : base(entityId, typeof(T))
        {
        }
    }
}
