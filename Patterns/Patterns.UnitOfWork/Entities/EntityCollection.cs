using Patterns.UnitOfWork.Extensions;
using Patterns.UnitOfWork.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Patterns.UnitOfWork.Entities
{
    /// <summary>
    /// Коллекция сущностей.
    /// </summary>
    /// <typeparam name="TEntity">Сущность.</typeparam>
    public class EntityCollection<TEntity> : IEntityTransactionManager<TEntity>
        where TEntity : Entity
    {
        /// <summary>
        /// Коллекция сущностей.
        /// </summary>
        private List<TEntity> _entities = new List<TEntity>();

        /// <summary>
        /// Коллекция сущностей, хранящаяся в БД.
        /// </summary>
        private List<TEntity> _savedEntities = new List<TEntity>();

        /// <summary>
        /// Признак, указывающий, что текущая коллекция была изменена.
        /// </summary>
        private bool _isChanged = false;

        /// <summary>
        /// Вовзарщает коллекцию сущностей.
        /// </summary>
        /// <returns>Коллекция сущностей.</returns>
        public List<TEntity> GetAll()
        {
            return _entities
                .Where(e => e.State != EntityState.Deleted)
                .ToList();
        }

        /// <summary>
        /// Возвращает признак, указывающий на наличие сущности с переданным Id.
        /// </summary>
        /// <param name="id">Id сущности.</param>
        /// <returns>True, если сущность есть в коллекции.</returns>
        public bool HasEntity(Guid id)
        {
            if (id == Guid.Empty)
            {
                return false;
            }

            return GetAll().Any(e => e.Id == id);
        }

        /// <summary>
        /// Добавляет коллекцию сущностей к текущей.
        /// </summary>
        /// <param name="entities">Коллекция сущностей.</param>
        public void AddRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            foreach (var entity in entities)
            {
                Add(entity);
            }
        }

        /// <summary>
        /// Добавляет сущность к текущей коллекции.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        public void Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _entities.Add(entity);
            _isChanged = true;
        }

        /// <summary>
        /// Изменяет сущность по переданному Id.
        /// </summary>
        /// <param name="id">Id сущности.</param>
        /// <param name="entity">Изменённая сущность.</param>
        public void Change(Guid id, TEntity entity)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var findedEntity = GetAll().FirstOrDefault(e => e.Equals(entity));

            if (findedEntity == null)
            {
                return;
            }

            findedEntity = entity;
            findedEntity.State = EntityState.Changed;

            _isChanged = true;
        }

        /// <summary>
        /// Удаляет сущность по переданному Id.
        /// </summary>
        /// <param name="id">Id сущности.</param>
        public void Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var findedEntity = GetAll().FirstOrDefault(e => e.Id == id);

            if (findedEntity == null)
            {
                return;
            }

            findedEntity.State = EntityState.Deleted;

            _isChanged = true;
        }

        /// <summary>
        /// Сохранить изменения в БД.
        /// </summary>
        /// <param name="sqlExecutor">Исполнитель SQL-запросов.</param>
        /// <returns>True, если изменения были сохранены в БД.</returns>
        public bool SaveChanges(ISqlExecutor sqlExecutor)
        {
            if (!_isChanged)
            {
                return true;
            }

            try
            {
                var sqlQuery = string.Join("\n", _entities.Select(e => e.GetSqlQuery()));
                sqlExecutor.Execute(sqlQuery);

                _entities.ForEach(e => e.State = EntityState.Unchanged);

                _savedEntities = _entities.Clone();
                _isChanged = false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Отменяет все внесённые в коллекцию изменения.
        /// </summary>
        public void RollbackChanges()
        {
            if (!_isChanged)
            {
                return;
            }

            _entities = _savedEntities.Clone();
            _isChanged = false;
        }
    }
}