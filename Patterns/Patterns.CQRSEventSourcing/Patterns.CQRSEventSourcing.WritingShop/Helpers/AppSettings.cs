namespace Patterns.CQRSEventSourcing.WritingShop.Helpers
{
    /// <summary>
    /// Класс для работы с конфигурационными настройками сервиса.
    /// </summary>
    public class AppSettings : IAppSettings
    {
        /// <summary>
        /// Название секции конфигурации "Uri-адреса служб".
        /// </summary>
        private const string ServicesAddressesSectionName = "ServicesAddresses";

        /// <summary>
        /// Название параметра "Адрес сервиса синхронизации продуктов в БД для чтения".
        /// </summary>
        private const string SynchronizingProductsServiceUrlParameterName = "SynchronizingProductsServiceUrl";

        /// <summary>
        /// Название параметра "Адрес сервиса синхронизации категорий продуктов в БД для чтения".
        /// </summary>
        private const string SynchronizingCategoriesServiceUrlParameterName = "SynchronizingCategoriesServiceUrl";

        /// <summary>
        /// Адрес сервиса синхронизации продуктов в БД для чтения.
        /// </summary>
        public string SynchronizingProductsServiceUrl { get; }

        /// <summary>
        /// Адрес сервиса синхронизации категорий продуктов в БД для чтения.
        /// </summary>
        public string SynchronizingCategoriesServiceUrl { get; }

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="configuration">
        /// Представляет набор свойств конфигурации приложения в вид пар "ключ — значение".
        /// </param>
        public AppSettings(IConfiguration configuration)
        {
            var synchronizingProductsServiceUrl = configuration
                .GetSection(ServicesAddressesSectionName)
                [SynchronizingProductsServiceUrlParameterName];
            var synchronizingCategoriesServiceUrl = configuration
                .GetSection(ServicesAddressesSectionName)
                [SynchronizingCategoriesServiceUrlParameterName];

            CheckAppSettingsIsEmpty(
                synchronizingProductsServiceUrl,
                "Не удалось получить \"Адрес сервиса синхронизации продуктов в БД для чтения.");
            CheckAppSettingsIsEmpty(
                synchronizingCategoriesServiceUrl,
                "Не удалось получить \"Адрес сервиса синхронизации категорий продуктов в БД для чтения.");

            SynchronizingProductsServiceUrl = synchronizingProductsServiceUrl;
            SynchronizingCategoriesServiceUrl = synchronizingCategoriesServiceUrl;
        }

        /// <summary>
        /// Валидирует значение конфигурационного свойства.
        /// Если оно пустое, то выводит переданную ошибку.
        /// </summary>
        /// <param name="settingName">Название конфигурационного свойства.</param>
        /// <param name="exceptionText">Текст генерируемой ошибки.</param>
        private static void CheckAppSettingsIsEmpty(string settingName, string exceptionText)
        {
            if (string.IsNullOrWhiteSpace(settingName))
            {
                throw new Exception(exceptionText);
            }
        }
    }
}