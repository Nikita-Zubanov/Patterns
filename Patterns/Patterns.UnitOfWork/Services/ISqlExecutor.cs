namespace Patterns.UnitOfWork.Services
{
    /// <summary>
    /// Предоставляет методы для выполнения SQL-запросов.
    /// </summary>
    public interface ISqlExecutor
    {
        /// <summary>
        /// Выполняет SQL-запрос.
        /// </summary>
        /// <param name="query">Текст запроса.</param>
        void Execute(string query);
    }
}