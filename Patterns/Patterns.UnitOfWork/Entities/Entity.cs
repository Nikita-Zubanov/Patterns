using Patterns.UnitOfWork.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patterns.UnitOfWork.Entities
{
    /// <summary>
    /// Сущность.
    /// Сопоставляется с сущностью из БД.
    /// </summary>
    public abstract class Entity : ICloneable
    {
        /// <summary>
        /// Уникальный идентификатор записи.
        /// </summary>
        [Column("Id")]
        public Guid Id { get; internal set; }

        /// <summary>
        /// Состояние сущности.
        /// </summary>
        internal EntityState State = EntityState.Unchanged;

        /// <summary>
        /// Инициализирует поля объекта.
        /// </summary>
        public Entity()
        {
            Id = Guid.NewGuid();
            State = EntityState.Added;
        }

        /// <summary>
        /// Возвращает дубликат объекта.
        /// </summary>
        /// <returns>Дубликат объекта.</returns>
        public object Clone()
        {
            var newEntity = Activator.CreateInstance(this.GetType());
            var properties = this.GetType()
                .GetProperties();

            foreach (var property in properties)
            {
                newEntity.GetType()
                    .GetProperty(property.Name)
                    .SetValue(newEntity, property.GetValue(this));
            }

            return newEntity;
        }

        /// <summary>
        /// Возвращает текст SQL-запроса для внесения изменений в БД.
        /// </summary>
        /// <returns>Текст SQL-запроса.</returns>
        internal string GetSqlQuery()
        {
            if (State == EntityState.Unchanged)
            {
                return null;
            }

            switch (State)
            {
                case EntityState.Unchanged:
                    return null;

                case EntityState.Added:
                    return GetInsertQuery();

                case EntityState.Changed:
                    return GetUpdateQuery();

                case EntityState.Deleted:
                    return GetDeleteQuery();

                default:
                    return null;
            }
        }

        /// <summary>
        /// Возвращает текст SQL-запроса для добавления сущности в БД.
        /// </summary>
        /// <returns>Текст SQL-запроса.</returns>
        private string GetInsertQuery()
        {
            var columns = GetColumns();

            return string.Format(
                "INSERT INTO {0}({1}) VALUES ({2});",
                GetTableName(),
                string.Join(", ", columns.Keys),
                string.Join(", ", columns.Values));
        }

        /// <summary>
        /// Возвращает текст SQL-запроса для изменения сущности в БД.
        /// </summary>
        /// <returns>Текст SQL-запроса.</returns>
        private string GetUpdateQuery()
        {
            var columns = GetColumns();

            return string.Format(
                "UPDATE {0} SET {1}; WHERE {2} = '{3}';",
                GetTableName(),
                string.Join(", ", columns.Select(c => $"{c.Key} = {c.Value}")),
                nameof(Id),
                Id);
        }

        /// <summary>
        /// Возвращает текст SQL-запроса для удаления сущности из БД.
        /// </summary>
        /// <returns>Текст SQL-запроса.</returns>
        private string GetDeleteQuery()
        {
            return string.Format(
                "DELETE {0} WHERE {1} = '{2}';",
                GetTableName(),
                nameof(Id),
                Id);
        }

        /// <summary>
        /// Возвращает словарь из названий колонок и их значений.
        /// </summary>
        /// <returns>Словарь из названий колонок и их значений.</returns>
        private Dictionary<string, object> GetColumns()
        {
            return this.GetType()
                .GetProperties()
                .Where(p => p.CustomAttributes.Any(a => a.AttributeType == typeof(ColumnAttribute)))
                .ToDictionary(pi => pi.Name, pi => pi.GetValue(this));
        }

        /// <summary>
        /// Возвращает название сущности.
        /// </summary>
        /// <returns>Название сущности.</returns>
        private string GetTableName()
        {
            var attribute =  this.GetType()
                .GetCustomAttributes(false)
                .FirstOrDefault(a => a.GetType() == typeof(TableAttribute));
            var tableAttribute = attribute == null
                ? null
                : (TableAttribute)attribute;

            if (tableAttribute == null)
            {
                return null;
            }

            return tableAttribute.Name;
        }
    }
}