using Patterns.CQRSEventSourcing.DomainModel.Events.Product;
using System.Runtime.Serialization;

namespace Patterns.CQRSEventSourcing.DomainModel.Aggregates.Product
{
    /// <summary>
    /// Категория продукта.
    /// </summary>
    [DataContract]
    public class Category : Entity<CreateCategoryEvent>
    {
        /// <summary>
        /// Заголовок.
        /// </summary>
        [DataMember]
        public string? Title { get; set; }

        /// <summary>
        /// Конструктор по умолчанию для создания сущности и последующему применению к ней
        /// метода <see cref="On(CreateCategoryEvent)"/>.
        /// </summary>
        public Category()
        { }

        /// <summary>
        /// Применяет событие к текущей сущности.
        /// </summary>
        /// <param name="event">Событие.</param>
        public override void On(CreateCategoryEvent @event)
        {
            base.On(@event);

            if (@event.Title != null)
            {
                Title = @event.Title;
            }
        }
    }
}