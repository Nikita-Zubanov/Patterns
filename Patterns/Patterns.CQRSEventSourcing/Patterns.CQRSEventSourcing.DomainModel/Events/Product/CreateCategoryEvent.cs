using MediatR;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Patterns.CQRSEventSourcing.DomainModel.Events.Product
{
    /// <summary>
    /// Событие создания категории продукта.
    /// </summary>
    [Table("CreateCategoryEvent")]
    public class CreateCategoryEvent : Event, INotification
    {
        /// <summary>
        /// Заголовок категории продукта.
        /// </summary>
        [Column("Title")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? Title { get; set; }

        /// <summary>
        /// Соединяет входящее событие и текущее.
        /// </summary>
        /// <param name="event">Входящее событие.</param>
        public override void Merge(Event @event)
        {
            base.Merge(@event);

            if (@event is not CreateCategoryEvent)
            {
                return;
            }

            var createCategoryEvent = (CreateCategoryEvent)@event;

            if (createCategoryEvent.Title != null)
            {
                Title = createCategoryEvent.Title;
            }

            Version = createCategoryEvent.Version;
        }
    }
}