namespace TurboRentingv2.Api.Interfaces
{
    public interface IAggregateRoot<TKey>
    {
        TKey? Id { get; set; }
    }
}
