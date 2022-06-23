using Patterns.CQRSEventSourcing.Infrastucture.DbContexts.WritingShop;
using Patterns.CQRSEventSourcing.WritingShop.Helpers;
using Quartz;

namespace Patterns.CQRSEventSourcing.WritingShop.QuartzJobs
{
    /// <summary>
    /// Рабочий процесс синхронизации данных в БД для чтения.
    /// </summary>
    public class SynchronizeEntitiesInReadingDbJob : IJob
    {
        /// <summary>
        /// Блокировщик потоков для отправки событий изменения категорий продуктов.
        /// </summary>
        private static readonly object _synchronizingSendCategoriesLock = new object();

        /// <summary>
        /// Блокировщик потоков для отправки событий изменения продуктов.
        /// </summary>
        private static readonly object _synchronizingSendProductsLock = new object();

        /// <summary>
        /// Предоставляет набор конфигурационных свойств сервиса.
        /// </summary>
        private readonly IAppSettings _appSettings;

        /// <summary>
        /// Хранилище событий, ожидающих отправки в процессе синхронизации.
        /// </summary>
        private readonly SendEventStore _sendEventStore;

        /// <summary>
        /// Инициализирует поля объекта.
        /// </summary>
        /// <param name="appSettings">Предоставляет набор конфигурационных свойств сервиса.</param>
        /// <param name="sendEventStore">
        /// Хранилище событий, ожидающих отправки в процессе синхронизации.
        /// </param>
        public SynchronizeEntitiesInReadingDbJob(
            IAppSettings appSettings,
            SendEventStore sendEventStore)
        {
            if (appSettings == null)
            {
                throw new ArgumentNullException(nameof(appSettings));
            }

            if (sendEventStore == null)
            {
                throw new ArgumentNullException(nameof(sendEventStore));
            }

            _appSettings = appSettings;
            _sendEventStore = sendEventStore;
        }

        /// <summary>
        /// Выполнить процесс синхронизации данных.
        /// </summary>
        /// <param name="context">Контекст, содержащий информацию о среде.</param>
        /// <returns>Результат выполнения процесса.</returns>
        public Task Execute(IJobExecutionContext context)
        {
            SendCreateProductEvents();
            SendCreateCategoryEvents();

            return Task.CompletedTask;
        }

        /// <summary>
        /// Отправляет события создания/обновления продуктов для синхронизации данных
        /// в БД для чтения.
        /// </summary>
        private void SendCreateProductEvents()
        {
            lock (_synchronizingSendProductsLock)
            {
                var createProductEvents = _sendEventStore.GetMergedSendingCreateProductEvents();

                if (createProductEvents.Count == 0)
                {
                    return;
                }

                WebInteractionHelper.SendRequest(
                    createProductEvents,
                    new Uri(_appSettings.SynchronizingProductsServiceUrl));

                _sendEventStore.ClearSendingCreateProductEvents();
            }
        }

        /// <summary>
        /// Отправляет события создания/обновления категорий продуктов для синхронизации данных
        /// в БД для чтения.
        /// </summary>
        private void SendCreateCategoryEvents()
        {
            lock (_synchronizingSendCategoriesLock)
            {
                var createCategoryEvents = _sendEventStore.GetMergedSendingCreateCategoryEvents();

                if (createCategoryEvents.Count == 0)
                {
                    return;
                }

                WebInteractionHelper.SendRequest(
                    createCategoryEvents,
                    new Uri(_appSettings.SynchronizingCategoriesServiceUrl));

                _sendEventStore.ClearSendingCreateCategoryEvents();
            }
        }
    }
}