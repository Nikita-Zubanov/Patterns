using Patterns.Builder.Attributes;
using Patterns.Builder.Helpers;

namespace Patterns.Builder.Buttons.Xaml
{
    /// <summary>
    /// Xaml-кнопка.
    /// </summary>
    public class XamlButton
    {
        /// <summary>
        /// Название.
        /// </summary>
        [Title("Name")]
        public string Name { get; set; }

        /// <summary>
        /// Заголовок.
        /// </summary>
        [Title("Content")]
        public string Content { get; set; }

        /// <summary>
        /// Название метода-обработчика.
        /// </summary>
        [Title("Click")]
        public string ClickHandlerName { get; set; }

        /// <summary>
        /// Доступность кнопки.
        /// </summary>
        [Title("IsEnabled")]
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Высота кнопки.
        /// </summary>
        [Title("Height")]
        public string Height { get; set; }

        /// <summary>
        /// Ширина кнопки.
        /// </summary>
        [Title("Width")]
        public string Width { get; set; }

        /// <summary>
        /// Возвращает строковое представление кнопки.
        /// </summary>
        /// <returns>Строковое представление кнопки.</returns>
        public override string ToString()
        {
            var nameTitle = TitleAttributeHelper.GetValue<XamlButton>(nameof(Name));
            var contentTitle = TitleAttributeHelper.GetValue<XamlButton>(nameof(Content));
            var clickHandlerNameTitle = TitleAttributeHelper.GetValue<XamlButton>(nameof(ClickHandlerName));
            var isEnabledTitle = TitleAttributeHelper.GetValue<XamlButton>(nameof(IsEnabled));
            var heightTitle = TitleAttributeHelper.GetValue<XamlButton>(nameof(Height));
            var widthTitle = TitleAttributeHelper.GetValue<XamlButton>(nameof(Width));

            return "<Button " +
                $"{nameTitle}=\"{Name}\" " +
                $"{contentTitle}=\"{Content}\" " +
                $"{clickHandlerNameTitle}=\"{ClickHandlerName}\" " +
                $"{heightTitle}=\"{Height}\" " +
                $"{widthTitle}=\"{Width}\" " +
                $"{isEnabledTitle}=\"{IsEnabled}\" " +
                "></Button>";
        }
    }
}