using System;

namespace Patterns.Builder.Attributes
{
    /// <summary>
    /// Атрибут, хранящий заголовок свойства.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class TitleAttribute : Attribute
    {
        /// <summary>
        /// Значение заголовка.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Инициализирует поля объекта.
        /// </summary>
        /// <param name="title">Значение заголовка.</param>
        public TitleAttribute(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentNullException(nameof(title));
            }

            Value = title;
        }
    }
}