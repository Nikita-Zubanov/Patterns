using Patterns.Builder.Buttons.Html;
using System;

namespace Patterns.Builder.ButtonBuilder
{
    /// <summary>
    /// Строитель для htnl-кнопки.
    /// </summary>
    public class HtmlButtonBuilder : IButtonBuilder
    {
        /// <summary>
        /// Html-кнопка.
        /// </summary>
        private HtmlButton _button = new HtmlButton();

        /// <summary>
        /// Устанавливает название кнопки.
        /// </summary>
        /// <param name="name">Название кнопки.</param>
        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(nameof(name)))
            {
                throw new ArgumentNullException(nameof(name));
            }

            _button.Name = name;
        }

        /// <summary>
        /// Устанавливает заголовок кнопки.
        /// </summary>
        /// <param name="name">Заголовок кнопки.</param>
        public void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(nameof(title)))
            {
                throw new ArgumentNullException(nameof(title));
            }

            _button.Title = title;
        }

        /// <summary>
        /// Устанавливает название метода-обработчика нажатия на кнопку.
        /// </summary>
        /// <param name="name">Название метода-обработчика.</param>
        public void SetClickHandlerName(string clickHandlerName)
        {
            if (string.IsNullOrWhiteSpace(nameof(clickHandlerName)))
            {
                throw new ArgumentNullException(nameof(clickHandlerName));
            }

            _button.ClickHandlerName = clickHandlerName;
        }

        /// <summary>
        /// Устанавливает доступность кнопки.
        /// </summary>
        /// <param name="name">Доступность кнопки.</param>
        public void SetEnabled(bool isEnabled)
        {
            _button.IsDisabled = !isEnabled;
        }

        /// <summary>
        /// Устанавливает значение аттрибута стиля кнопки.
        /// </summary>
        /// <param name="attribute">Название аттрибута.</param>
        /// <param name="value">Значение аттрибута.</param> 
        public void SetStyle(string attribute, string value)
        {
            if (string.IsNullOrWhiteSpace(nameof(attribute)))
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            if (string.IsNullOrWhiteSpace(nameof(value)))
            {
                throw new ArgumentNullException(nameof(value));
            }

            _button.Styles.AddAttribute(attribute, value);
        }

        /// <summary>
        /// Возвращает строковое представление кнопки.
        /// </summary>
        /// <returns>Строковое представление кнопки.</returns>
        public string GetButtonString()
        {
            return _button.ToString();
        }
    }
}