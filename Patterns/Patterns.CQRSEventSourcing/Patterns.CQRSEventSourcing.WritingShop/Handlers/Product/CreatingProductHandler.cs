using MediatR;
using Patterns.CQRSEventSourcing.DomainModel.Events.Product;
using Patterns.CQRSEventSourcing.Infrastucture.DbContexts.ReadingShop;
using Patterns.CQRSEventSourcing.Infrastucture.DbContexts.WritingShop;

namespace Patterns.CQRSEventSourcing.WritingShop.Handlers.Product
{
    /// <summary>
    /// Обрабатывает события агрегата "Продукт".
    /// </summary>
    public class CreatingProductHandler : INotificationHandler<CreateCategoryEvent>, INotificationHandler<CreateProductEvent>
    {
        /// <summary>
        /// Блокировщик потоков для сохранения событий изменения категорий продуктов.
        /// </summary>
        private static readonly object _addingCreateCategoryEventLock = new object();

        /// <summary>
        /// Блокировщик потоков для сохранения событий изменения продуктов.
        /// </summary>
        private static readonly object _addingCreateProductEventLock = new object();

        /// <summary>
        /// Предоставляет методы для записи событий агрегата "Продукт" из БД.
        /// </summary>
        private readonly IWriteProductContext _writeProductContext;

        /// <summary>
        /// Хранилище событий, ожидающих отправки в процессе синхронизации.
        /// </summary>
        private readonly SendEventStore _sendEventStore;

        /// <summary>
        /// Инициализирует поля обработчика событий агрегата "Продукт".
        /// </summary>
        /// <param name="dbContext">
        /// Предоставляет методы для записи событий агрегата "Продукт" из БД.
        /// </param>
        /// <param name="sendEventStore">
        /// Хранилище событий, ожидающих отправки в процессе синхронизации.
        /// </param>
        public CreatingProductHandler(IWriteProductContext dbContext, SendEventStore sendEventStore)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (sendEventStore == null)
            {
                throw new ArgumentNullException(nameof(sendEventStore));
            }

            _writeProductContext = dbContext;
            _sendEventStore = sendEventStore;
        }

        /// <summary>
        /// Обрабатывает событие создания категории продукта.
        /// </summary>
        /// <param name="event">Событие создания категории продукта.</param>
        /// <param name="cancellationToken">Токен для отмены обработки.</param>
        /// <returns>Результат обработки события.</returns>
        public async Task Handle(CreateCategoryEvent @event, CancellationToken cancellationToken)
        {
            lock (_addingCreateCategoryEventLock)
            {
                _sendEventStore.AddSendingCreateCategoryEvent(@event);
                _writeProductContext.AddCreateCategoryEvent(@event);
            }
        }

        /// <summary>
        /// Обрабатывает событие создания продукта.
        /// </summary>
        /// <param name="event">Событие создания продукта.</param>
        /// <param name="cancellationToken">Токен для отмены обработки.</param>
        /// <returns>Результат обработки события.</returns>
        public async Task Handle(CreateProductEvent @event, CancellationToken cancellationToken)
        {
            lock (_addingCreateProductEventLock)
            {
                _sendEventStore.AddSendingCreateProductEvent(@event);
                _writeProductContext.AddCreateProductEvent(@event);
            }
        }
    }
}