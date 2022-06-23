using System;

namespace Patterns.UnitOfWork.Attributes
{
    /// <summary>
    /// Атрибут колонка.
    /// Хранит информацию для сопоставления полей объекта и колонок сущности в БД.
    /// </summary>
    public class ColumnAttribute : Attribute
    {
        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Инициализирует поля объекта.
        /// </summary>
        /// <param name="name">Название колонки.</param>
        public ColumnAttribute(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
        }
    }
}