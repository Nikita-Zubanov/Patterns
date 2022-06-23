using Patterns.UnitOfWork.Entities;
using Patterns.UnitOfWork.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Patterns.UnitOfWork.DbContext
{
    /// <summary>
    /// Предоставляет базовый клас для транзакционного взаимодействия с коллекциями сущностей.
    /// Производные классы должны лишь унаследоваться от текущего, установить <see cref="_sqlExecutor"/> и
    /// добавить в качестве поля(-ей) или свойства(-в) экземляр(-ы) класса <see cref="EntityCollection{TEntity}"/>.
    /// </summary>
    public abstract class UnitOfWork
    {
        /// <summary>
        /// Исполнитель SQL-запросов.
        /// </summary>
        private ISqlExecutor _sqlExecutor;

        /// <summary>
        /// Инициализирует поля объекта.
        /// </summary>
        /// <param name="sqlExecutor">Исполнитель SQL-запросов.</param>
        public UnitOfWork(ISqlExecutor sqlExecutor)
        {
            if(sqlExecutor == null)
            {
                throw new ArgumentNullException(nameof(sqlExecutor));
            }

            _sqlExecutor = sqlExecutor;
        }

        /// <summary>
        /// Зафиксировать изменения в БД.
        /// </summary>
        public void Commit()
        {
            foreach(var entityTransactionManager in GetIEntityTransactionManagers())
            {
                entityTransactionManager.SaveChanges(_sqlExecutor);
            }
        }

        /// <summary>
        /// Отменить все изменения.
        /// </summary>
        public void Rollback()
        {
            foreach (var entityTransactionManager in GetIEntityTransactionManagers())
            {
                entityTransactionManager.RollbackChanges();
            }
        }

        /// <summary>
        /// Возвращает перечислитель для транзиктивного управления коллекциями сущностей.
        /// </summary>
        /// <returns>Перечислитель для транзиктивного управления коллекциями сущностей.</returns>
        private IEnumerable<IEntityTransactionManager<Entity>> GetIEntityTransactionManagers()
        {
            return this.GetType()
                .GetProperties()
                .Where(p =>
                    p.PropertyType.GetGenericTypeDefinition()?.Name ==
                    typeof(EntityCollection<Entity>).GetGenericTypeDefinition().Name)
                .Where(p =>
                    p.PropertyType.GetGenericArguments().First().BaseType == typeof(Entity))
                .Select(p => (IEntityTransactionManager<Entity>)p.GetValue(this));
        }
    }
}