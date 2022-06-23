namespace Patterns.UnitOfWork
{
    /// <summary>
    /// Состояние сущности.
    /// </summary>
    public enum EntityState
    {
        /// <summary>
        /// Без изменений.
        /// </summary>
        Unchanged = 0,

        /// <summary>
        /// Изменена.
        /// </summary>
        Changed = 1,

        /// <summary>
        /// Добавлена.
        /// </summary>
        Added = 3,

        /// <summary>
        /// Удалена.
        /// </summary>
        Deleted = 4
    }
}