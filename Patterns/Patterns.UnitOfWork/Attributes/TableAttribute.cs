using System;

namespace Patterns.UnitOfWork.Attributes
{
    /// <summary>
    /// Атрибут таблица.
    /// Хранит информацию для сопоставления объекта и сущности в БД.
    /// </summary>
    public class TableAttribute : Attribute
    {
        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Инициализирует поля объекта.
        /// </summary>
        /// <param name="name">Название таблицы.</param>
        public TableAttribute(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
        }
    }
}