using Patterns.Builder.ButtonBuilder;
using System;
using System.Collections.Generic;

namespace Patterns.Builder
{
    internal class Program
    {
        /// <summary>
        /// Формирует две кнопки: html и xaml, - с одинаковыми параметрами и выводит их строковое
        /// представление в консоль.
        /// </summary>
        static void Main()
        {
            var buttonName = "messageShow";
            var buttonTitle = "Показать сообщение";
            var buttonСlickHandlerName = "alert";
            var isEnabledButton = false;
            var buttonStyles = new Dictionary<string, string>
            {
                { "width", "500" },
                { "height", "200" }
            };

            var htmlButton = BuildButton(
                new HtmlButtonBuilder(),
                buttonName,
                buttonTitle,
                buttonСlickHandlerName,
                isEnabledButton,
                buttonStyles);
            Console.WriteLine(htmlButton);

            var xamlButton = BuildButton(
                new XamlButtonBuilder(),
                buttonName,
                buttonTitle,
                buttonСlickHandlerName,
                isEnabledButton,
                buttonStyles);
            Console.WriteLine(xamlButton);

            Console.ReadKey();
        }

        /// <summary>
        /// Формирует кнопку и возвращает её строковое представление.
        /// </summary>
        /// <param name="buttonBuilder">Строитель кнопок.</param>
        /// <param name="name">Название кнопки.</param>
        /// <param name="title">Заголовок кнопки.</param>
        /// <param name="clickHandlerName">Название метода-обработчика кнопки.</param>
        /// <param name="isEnabled">Доступность кнопки.</param>
        /// <param name="styles">Стили кнопки.</param>
        /// <returns>Строковое представление кнопки.</returns>
        private static string BuildButton(
            IButtonBuilder buttonBuilder,
            string name,
            string title,
            string clickHandlerName,
            bool isEnabled,
            Dictionary<string, string> styles)
        {
            buttonBuilder.SetName(name);
            buttonBuilder.SetTitle(title);
            buttonBuilder.SetClickHandlerName(clickHandlerName);
            buttonBuilder.SetEnabled(isEnabled);
            foreach (var style in styles)
            {
                buttonBuilder.SetStyle(style.Key, style.Value);
            }

            return buttonBuilder.GetButtonString();
        }
    }
}