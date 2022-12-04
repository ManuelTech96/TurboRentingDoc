using MediatR;

namespace TurboRentingv2.Api.Models.Entities
{
    public abstract class Entity
    {
        private IList<INotification>? events;

        public IList<INotification>? Events => events;


        /// <summary>
        /// Añade un evento de dominio a la entidad
        /// </summary>
        /// <param name="event">Evento a añadir</param>
        public void AddEvent(INotification @event)
        {
            events = events ?? new List<INotification>();

            events.Add(@event);
        }


        /// <summary>
        /// Elimina todos los eventos de dominio de la entidad
        /// </summary>
        public void ClearEvents() => events?.Clear();


        /// <summary>
        /// Elimina un evento de dominio de la entidad
        /// </summary>
        /// <param name="event">Evento a eliminar</param>
        public void RemoveEvent(INotification @event) => events?.Remove(@event);
    }
    public abstract class Entity<TKey> : Entity
    {
        public TKey? Id { get; set; }
    }
}
