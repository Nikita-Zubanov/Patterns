using Patterns.UnitOfWork.Services;

namespace Patterns.UnitOfWork.Entities
{
    /// <summary>
    /// Предоставляет методы для транзакционного внесения изменений в объект.
    /// </summary>
    /// <typeparam name="TEntity">Сущность.</typeparam>
    internal interface IEntityTransactionManager<out TEntity>
        where TEntity : Entity
    {
        /// <summary>
        /// Сохранить изменения в БД.
        /// </summary>
        /// <param name="sqlExecutor">Исполнитель SQL-запросов.</param>
        /// <returns>True, если изменения были сохранены в БД.</returns>
        bool SaveChanges(ISqlExecutor sqlExecutor);

        /// <summary>
        /// Отменяет все изменения, внесённые в коллекцию.
        /// </summary>
        void RollbackChanges();
    }
}