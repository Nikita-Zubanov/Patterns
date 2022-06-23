using System.Runtime.Serialization;

namespace Patterns.CQRSEventSourcing.WritingShop.DataContracts.Product
{
    /// <summary>
    /// Категория продукта.
    /// </summary>
    [DataContract]
    public class Category : Entity
    {
        /// <summary>
        /// Заголовок.
        /// </summary>
        [DataMember]
        public string? Title { get; set; }
    }
}