using Patterns.CQRSEventSourcing.DomainModel.Events;
using System.Runtime.Serialization;

namespace Patterns.CQRSEventSourcing.DomainModel.Aggregates
{
    /// <summary>
    /// Базовый класс сущности
    /// </summary>
    /// <typeparam name="TEvent">Тип события.</typeparam>
    [DataContract]
    public abstract class Entity<TEvent>
        where TEvent : Event
    {
        /// <summary>
        /// Id сущности.
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }

        /// <summary>
        /// Применяет событие к текущей сущности.
        /// </summary>
        /// <param name="event">Событие.</param>
        public virtual void On(TEvent @event)
        {
            if (@event == null)
            {
                return;
            }

            if (Id == Guid.Empty)
            {
                Id = @event.EntityId;
            }

            if (Id != @event.EntityId)
            {
                return;
            }
        }
    }
}