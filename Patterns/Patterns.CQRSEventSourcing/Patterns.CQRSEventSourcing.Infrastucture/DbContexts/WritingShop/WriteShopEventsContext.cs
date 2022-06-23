using Microsoft.EntityFrameworkCore;
using Patterns.CQRSEventSourcing.DomainModel.Events.Product;
using Patterns.CQRSEventSourcing.Infrastucture.DbContexts.ReadingShop;

namespace Patterns.CQRSEventSourcing.Infrastucture.DbContexts.WritingShop
{
    /// <summary>
    /// Контекст магазина для записи событий в БД.
    /// </summary>
    public class WriteShopEventsContext : DbContext, IWriteProductContext
    {
        /// <summary>
        /// Список событий создания продукта.
        /// </summary>
        private readonly List<CreateProductEvent> _createProductEvents = new List<CreateProductEvent>();

        /// <summary>
        /// Список событий создания категории продукта.
        /// </summary>
        private readonly List<CreateCategoryEvent> _createCategoryEvents = new List<CreateCategoryEvent>();

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        /// <param name="options">Настройки подключения к БД.</param>
        public WriteShopEventsContext(DbContextOptions options) : base(options)
        { }

        /// <summary>
        /// Добавляет событие создания продукта.
        /// </summary>
        /// <param name="createProductEvent">Событие создания продукта.</param>
        public void AddCreateProductEvent(CreateProductEvent createProductEvent)
        {
            _createProductEvents.Add(createProductEvent);
        }

        /// <summary>
        /// Добавляет событие создания категории продукта.
        /// </summary>
        /// <param name="createProductEvent">Событие создания категории продукта.</param>
        public void AddCreateCategoryEvent(CreateCategoryEvent createCategoryEvent)
        {
            _createCategoryEvents.Add(createCategoryEvent);
        }

        /// <summary>
        /// Сохранить изменения.
        /// </summary>
        /// <param name="cancellationToken">Токен для отмены транзакции.</param>
        /// <returns>Результат сохранения.</returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}