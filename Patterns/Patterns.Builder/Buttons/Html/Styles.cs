using System;
using System.Collections.Generic;

namespace Patterns.Builder.Buttons.Html
{
    /// <summary>
    /// Стили html-кнопки.
    /// </summary>
    public class Styles
    {
        /// <summary>
        /// Словарь, хранящий атрибуты стилей и их значения. 
        /// </summary>
        public Dictionary<string, string> AttributeValues { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Добавить атрибут.
        /// </summary>
        /// <param name="attribute">Название атрибута.</param>
        /// <param name="value">Значение атрибута.</param>
        public void AddAttribute(string attribute, string value)
        {
            if (string.IsNullOrWhiteSpace(attribute))
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            AttributeValues.Add(attribute, value);
        }

        /// <summary>
        /// Возвращает строковое представление стилей.
        /// </summary>
        /// <returns>Строковое представление стилей.</returns>
        public override string ToString()
        {
            var styleString = "";

            foreach(var attribute in AttributeValues)
            {
                styleString += $"{attribute.Key}: {attribute.Value}; ";
            }

            return styleString;
        }
    }
}