using Patterns.UnitOfWork.Attributes;

namespace Patterns.UnitOfWork.Entities
{
    /// <summary>
    /// Сущность товара.
    /// </summary>
    [Table("Product")]
    public class Product : Entity
    {
        /// <summary>
        /// Название.
        /// </summary>
        [Column("Name")]
        public string Name { get; set; }
    }
}