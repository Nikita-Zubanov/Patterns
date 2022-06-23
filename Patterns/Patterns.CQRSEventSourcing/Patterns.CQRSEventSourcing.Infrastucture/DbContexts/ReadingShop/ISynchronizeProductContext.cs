using Patterns.CQRSEventSourcing.DomainModel.Events.Product;

namespace Patterns.CQRSEventSourcing.Infrastucture.DbContexts.ReadingShop
{
    /// <summary>
    /// Предоставляет методы для синхронизации агрегата "Продукт" в БД.
    /// </summary>
    public interface ISynchronizeProductContext
    {
        /// <summary>
        /// Применяет новые события создания продуктов к текущему наполнению в БД.
        /// </summary>
        /// <param name="events">Список событий создания продуктов.</param>
        void ApplyCreateProductEvents(List<CreateProductEvent> events);

        /// <summary>
        /// Применяет новые события создания категорий продуктов к текущему наполнению в БД.
        /// </summary>
        /// <param name="events">Список событий создания категорий родуктов.</param>
        void ApplyCreateCategoryEvents(List<CreateCategoryEvent> events);

        /// <summary>
        /// Сохранить изменения.
        /// </summary>
        /// <param name="cancellationToken">Токен для отмены транзакции.</param>
        /// <returns>Результат сохранения.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}