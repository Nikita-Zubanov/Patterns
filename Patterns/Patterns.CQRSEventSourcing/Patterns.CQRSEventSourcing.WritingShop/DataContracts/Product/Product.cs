using System.Runtime.Serialization;

namespace Patterns.CQRSEventSourcing.WritingShop.DataContracts.Product
{
    /// <summary>
    /// Продукт.
    /// </summary>
    [DataContract]
    public class Product : Entity
    {
        /// <summary>
        /// Id категории продукта.
        /// </summary>
        [DataMember]
        public Guid? CategoryId { get; set; }

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
    }
}