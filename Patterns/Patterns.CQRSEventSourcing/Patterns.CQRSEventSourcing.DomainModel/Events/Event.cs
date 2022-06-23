using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Patterns.CQRSEventSourcing.DomainModel.Events
{
    /// <summary>
    /// Базовый класс обытия.
    /// </summary>
    public abstract class Event
    {
        /// <summary>
        /// Id сущности.
        /// </summary>
        [Column("EntityId")]
        public Guid EntityId { get; set; }

        /// <summary>
        /// Дата и время события.
        /// </summary>
        [Column("Date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Версия события.
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Сериализованная сущность в формате JSON.
        /// </summary>
        [Column("Data")]
        private string _data { get => JsonConvert.SerializeObject(this); }

        /// <summary>
        /// Соединяет входящее событие и текущее.
        /// </summary>
        /// <param name="event">Входящее событие.</param>
        public virtual void Merge(Event @event)
        {
            if (@event == null)
            {
                return;
            }

            if (EntityId != @event.EntityId)
            {
                return;
            }
        }
    }
}