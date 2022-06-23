using Patterns.Builder.Buttons.Xaml;
using System;

namespace Patterns.Builder.ButtonBuilder
{
    /// <summary>
    /// Строитель для xaml-кнопки.
    /// </summary>
    public class XamlButtonBuilder : IButtonBuilder
    {
        /// <summary>
        /// Xam-кнопка.
        /// </summary>
        private XamlButton _button = new XamlButton();

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

            _button.Content = title;
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
            _button.IsEnabled = isEnabled;
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

            switch (attribute.ToLower())
            {
                case var width when width == nameof(_button.Width).ToLower():
                    _button.Width = value;
                    break;

                case var height when height == nameof(_button.Height).ToLower():
                    _button.Height = value;
                    break;

                default:
                    throw new ArgumentException(
                        $"Атрибута стиля с названием \"{attribute}\" " +
                        $"не найдено в объекте \"{nameof(XamlButtonBuilder)}\".");
            }
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