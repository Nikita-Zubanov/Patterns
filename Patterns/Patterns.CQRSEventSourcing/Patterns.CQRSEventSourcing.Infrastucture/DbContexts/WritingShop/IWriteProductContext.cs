using Patterns.CQRSEventSourcing.DomainModel.Events.Product;

namespace Patterns.CQRSEventSourcing.Infrastucture.DbContexts.ReadingShop
{
    /// <summary>
    /// Предоставляет методы для записи событий агрегата "Продукт" из БД.
    /// </summary>
    public interface IWriteProductContext
    {
        /// <summary>
        /// Добавить событие создания продукта.
        /// </summary>
        /// <param name="event">Событие создания продукта.</param>
        void AddCreateProductEvent(CreateProductEvent @event);

        /// <summary>
        /// Добавить событие создания категории продукта.
        /// </summary>
        /// <param name="event">Событие создания категории продукта.</param>
        void AddCreateCategoryEvent(CreateCategoryEvent @event);

        /// <summary>
        /// Сохранить изменения.
        /// </summary>
        /// <param name="cancellationToken">Токен для отмены транзакции.</param>
        /// <returns>Результат сохранения.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}