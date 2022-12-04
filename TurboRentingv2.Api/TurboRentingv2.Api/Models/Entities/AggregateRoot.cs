using TurboRentingv2.Api.Interfaces;

namespace TurboRentingv2.Api.Models.Entities
{
    public abstract class AggregateRoot <TKey> : Entity<TKey>, IAggregateRoot<TKey>
    {
    }
}
