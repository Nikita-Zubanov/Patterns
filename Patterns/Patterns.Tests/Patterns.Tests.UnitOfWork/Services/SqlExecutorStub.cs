using Patterns.UnitOfWork.Services;

namespace Patterns.Tests.Patterns.Tests.UnitOfWork.Services
{
    /// <summary>
    /// Заглушка исполнителя SQL-запросов.
    /// </summary>
    internal class SqlExecutorStub : ISqlExecutor
    {
        /// <summary>
        /// Имитирует выполнение SQL-запроса.
        /// </summary>
        /// <param name="query"></param>
        public void Execute(string query)
        { }
    }
}