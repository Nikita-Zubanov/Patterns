namespace Patterns.CQRSEventSourcing.WritingShop.Helpers
{
    /// <summary>
    /// Предоставляет набор конфигурационных свойств сервиса.
    /// </summary>
    public interface IAppSettings
    {
        /// <summary>
        /// Адрес сервиса синхронизации продуктов в БД для чтения.
        /// </summary>
        string SynchronizingProductsServiceUrl { get; }

        /// <summary>
        /// Адрес сервиса синхронизации категорий продуктов в БД для чтения.
        /// </summary>
        string SynchronizingCategoriesServiceUrl { get; }
    }
}
