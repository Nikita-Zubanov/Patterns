using Patterns.CQRSEventSourcing.DomainModel.Events;
using Patterns.CQRSEventSourcing.DomainModel.Events.Product;

namespace Patterns.CQRSEventSourcing.Infrastucture.DbContexts.WritingShop
{
    /// <summary>
    /// Хранилище событий, ожидающих отправки в процессе синхронизации.
    /// </summary>
    public class SendEventStore
    {
        /// <summary>
        /// Блокировщик потоков для событий изменения категорий продуктов.
        /// </summary>
        private static readonly object _createCategoryEventLock = new object();

        /// <summary>
        /// Блокировщик потоков для событий изменения продуктов.
        /// </summary>
        private static readonly object _createProductEventLock = new object();

        /// <summary>
        /// Список событий создания категорий продуктов, ожидающих отправки.
        /// </summary>
        private static readonly List<CreateCategoryEvent> SendingCreateCategoryEvents = new List<CreateCategoryEvent>();

        /// <summary>
        /// Список событий создания продуктов, ожидающих отправки.
        /// </summary>
        private static readonly List<CreateProductEvent> SendingCreateProductEvents = new List<CreateProductEvent>();

        /// <summary>
        /// Возвращает соединённые события создания категорий продуктов.
        /// </summary>
        /// <returns>Список событий создания категорий продуктов.</returns>
        public List<CreateCategoryEvent> GetMergedSendingCreateCategoryEvents()
        {
            lock (_createCategoryEventLock)
            {
                if (SendingCreateCategoryEvents.Count > 0)
                {
                    return GetMergedEvents(SendingCreateCategoryEvents);
                }

                return SendingCreateCategoryEvents;
            }
        }

        /// <summary>
        /// Возвращает соединённые события создания продуктов.
        /// </summary>
        /// <returns>Список событий создания продуктов.</returns>
        public List<CreateProductEvent> GetMergedSendingCreateProductEvents()
        {
            lock (_createProductEventLock)
            {
                if (SendingCreateProductEvents.Count > 0)
                {
                    return GetMergedEvents(SendingCreateProductEvents);
                }

                return SendingCreateProductEvents;
            }
        }

        /// <summary>
        /// Очищает список событий создания категорий продукта, ожидающих отправки.
        /// </summary>
        public void ClearSendingCreateCategoryEvents()
        {
            lock (_createCategoryEventLock)
            {
                SendingCreateCategoryEvents.Clear();
            }
        }

        /// <summary>
        /// Очищает список событий создания продукта, ожидающих отправки.
        /// </summary>
        public void ClearSendingCreateProductEvents()
        {
            lock (_createProductEventLock)
            {
                SendingCreateProductEvents.Clear();
            }
        }

        /// <summary>
        /// Добавить событие создания категории продукта в список событий, ожидающих отправки.
        /// </summary>
        /// <param name="event">Событие создания категории продукта.</param>
        public void AddSendingCreateCategoryEvent(CreateCategoryEvent @event)
        {
            lock (_createCategoryEventLock)
            {
                @event.Date = DateTime.UtcNow;
                @event.Version = SendingCreateCategoryEvents.Count;

                SendingCreateCategoryEvents.Add(@event);
            }
        }

        /// <summary>
        /// Добавить событие создания продукта в список событий, ожидающих отправки.
        /// </summary>
        /// <param name="event">Событие создания продукта.</param>
        public void AddSendingCreateProductEvent(CreateProductEvent @event)
        {
            lock (_createProductEventLock)
            {
                @event.Date = DateTime.UtcNow;
                @event.Version = SendingCreateProductEvents.Count;

                SendingCreateProductEvents.Add(@event);
            }
        }

        /// <summary>
        /// Соединяет события, каждое из которых применяется к нужной сущности.
        /// </summary>
        /// <typeparam name="TEvent">Тип события.</typeparam>
        /// <param name="events">Список событий.</param>
        /// <returns>Список событий для конкретных сущностей.</returns>
        private List<TEvent> GetMergedEvents<TEvent>(IEnumerable<TEvent> events)
            where TEvent : Event
        {
            var groupedByIdEvents = events.GroupBy(@event => @event.EntityId);
            var mergedEvents = new List<TEvent>();

            foreach (var aggregateEvents in groupedByIdEvents)
            {
                var aggregateEvent = aggregateEvents
                    .OrderBy(@event => @event.Version)
                    .ToList()
                    .Aggregate((mainEvent, @event) =>
                    {
                        mainEvent.Merge(@event);
                        return mainEvent;
                    });

                mergedEvents.Add(aggregateEvent);
            }

            return mergedEvents;
        }
    }
}