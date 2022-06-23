using Patterns.CQRSEventSourcing.DomainModel.Events.Product;
using System.Runtime.Serialization;

namespace Patterns.CQRSEventSourcing.DomainModel.Aggregates.Product
{
    /// <summary>
    /// Продукт.
    /// </summary>
    [DataContract]
    public class Product : Entity<CreateProductEvent>
    {
        /// <summary>
        /// Заголовок.
        /// </summary>
        [DataMember]
        public string? Title { get; set; }

        /// <summary>
        /// Цена.
        /// </summary>
        [DataMember]
        public int? Price { get; set; }

        /// <summary>
        /// Категория.
        /// </summary>
        [DataMember]
        public Category? Category { get; set; }
        
        /// <summary>
        /// Конструктор по умолчанию для создания сущности и последующему применению к ней
        /// метода <see cref="On(CreateCategoryEvent)"/>.
        /// </summary>
        public Product()
        { }

        /// <summary>
        /// Применяет событие к текущей сущности.
        /// </summary>
        /// <param name="event">Событие.</param>
        public override void On(CreateProductEvent @event)
        {
            base.On(@event);

            if (@event.Title != null)
            {
                Title = @event.Title;
            }

            if (@event.Price != null)
            {
                Price = @event.Price;
            }

            if (@event.CategoryId != null)
            {
                Category = new Category
                {
                    Id = @event.CategoryId.Value
                };
            }
        }
    }
}