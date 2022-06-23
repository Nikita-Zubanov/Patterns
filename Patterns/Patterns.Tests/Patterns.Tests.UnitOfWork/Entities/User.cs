using Patterns.UnitOfWork.Attributes;

namespace Patterns.UnitOfWork.Entities
{
    /// <summary>
    /// Сущность пользователя.
    /// </summary>
    [Table("User")]
    public class User : Entity
    {
        /// <summary>
        /// Полное имя пользователя.
        /// </summary>
        [Column("Name")]
        public string FullName { get; set; }

        /// <summary>
        /// Возраст пользователя.
        /// </summary>
        [Column("Age")]
        public int Age { get; set; }
    }
}