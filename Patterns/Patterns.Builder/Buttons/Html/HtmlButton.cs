using Patterns.Builder.Attributes;
using Patterns.Builder.Helpers;

namespace Patterns.Builder.Buttons.Html
{
    /// <summary>
    /// Html-кнопка.
    /// </summary>
    public class HtmlButton
    {
        /// <summary>
        /// Название.
        /// </summary>
        [Title("name")]
        public string Name { get; set; }

        /// <summary>
        /// Заголовок.
        /// </summary>
        [Title("value")]
        public string Title { get; set; }

        /// <summary>
        /// Название метода-обработчика.
        /// </summary>
        [Title("onClick")]
        public string ClickHandlerName { get; set; }

        /// <summary>
        /// Доступность кнопки.
        /// </summary>
        [Title("disabled")]
        public bool IsDisabled { get; set; }

        /// <summary>
        /// Стили кнопки
        /// </summary>
        [Title("style")]
        public Styles Styles { get; set; } = new Styles();

        /// <summary>
        /// Возвращает строковое представление кнопки.
        /// </summary>
        /// <returns>Строковое представление кнопки.</returns>
        public override string ToString()
        {
            var nameTitle = TitleAttributeHelper.GetValue<HtmlButton>(nameof(Name));
            var valueTitle = TitleAttributeHelper.GetValue<HtmlButton>(nameof(Title));
            var clickHandlerNameTitle = TitleAttributeHelper.GetValue<HtmlButton>(nameof(ClickHandlerName));
            var isDisabledTitle = TitleAttributeHelper.GetValue<HtmlButton>(nameof(IsDisabled));
            var styleTitle = TitleAttributeHelper.GetValue<HtmlButton>(nameof(Styles));

            return "<input " +
                "type=\"button\" " +
                $"{nameTitle}=\"{Name}\" " +
                $"{valueTitle}=\"{Title}\" " +
                $"{clickHandlerNameTitle}=\"{ClickHandlerName}\" " +
                $"{styleTitle}=\"{Styles}\" " +
                (IsDisabled ? isDisabledTitle + " " : string.Empty) +
                ">";
        }
    }
}