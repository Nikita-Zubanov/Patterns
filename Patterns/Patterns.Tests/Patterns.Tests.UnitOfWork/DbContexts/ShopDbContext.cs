using Patterns.UnitOfWork.Entities;
using Patterns.UnitOfWork.Services;

namespace Patterns.UnitOfWork.DbContext
{
    /// <summary>
    /// Контекст интернет-магазина, предоставляющий коллекции сущностей и тразакционные методы для их сохранения в БД.
    /// </summary>
    public class ShopDbContext : UnitOfWork
    {
        /// <summary>
        /// Инициализирует поля объекта.
        /// </summary>
        /// <param name="sqlExecutor">Исполнитель SQL-запросов.</param>
        public ShopDbContext(ISqlExecutor sqlExecutor) : base(sqlExecutor)
        { }

        /// <summary>
        /// Товары.
        /// </summary>
        public EntityCollection<Product> Products { get; set; } = new EntityCollection<Product>();

        /// <summary>
        /// Пользователи.
        /// </summary>
        public EntityCollection<User> Users { get; set; } = new EntityCollection<User>();
    }
}