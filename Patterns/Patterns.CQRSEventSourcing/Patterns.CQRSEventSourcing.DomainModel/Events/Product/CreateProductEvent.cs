using MediatR;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Patterns.CQRSEventSourcing.DomainModel.Events.Product
{
    /// <summary>
    /// Событие создания продукта.
    /// </summary>
    [Table("CreateProductEvent")]
    public class CreateProductEvent : Event, INotification
    {
        /// <summary>
        /// Id категории продукта.
        /// </summary>
        [Column("CategoryId")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Guid? CategoryId { get; set; }

        /// <summary>
        /// Заголовок продукта.
        /// </summary>
        [Column("Title")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? Title { get; set; }

        /// <summary>
        /// Цена продукта.
        /// </summary>
        [Column("Price")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Price { get; set; }

        /// <summary>
        /// Соединяет входящее событие и текущее.
        /// </summary>
        /// <param name="event">Входящее событие.</param>
        public override void Merge(Event @event)
        {
            base.Merge(@event);

            if (@event is not CreateProductEvent)
            {
                return;
            }

            var createProductEvent = (CreateProductEvent)@event;

            if (createProductEvent.CategoryId != null)
            {
                CategoryId = createProductEvent.CategoryId;
            }

            if (createProductEvent.Title != null)
            {
                Title = createProductEvent.Title;
            }

            if (createProductEvent.Price != null)
            {
                Price = createProductEvent.Price;
            }

            Version = createProductEvent.Version;
        }
    }
}