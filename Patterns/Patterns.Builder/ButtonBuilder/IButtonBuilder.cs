namespace Patterns.Builder.ButtonBuilder
{
    /// <summary>
    /// Предоставляет методы для построения кнопки.
    /// </summary>
    public interface IButtonBuilder
    {
        /// <summary>
        /// Устанавливает название кнопки.
        /// </summary>
        /// <param name="name">Название кнопки.</param>
        void SetName(string name);

        /// <summary>
        /// Устанавливает заголовок кнопки.
        /// </summary>
        /// <param name="name">Заголовок кнопки.</param>
        void SetTitle(string title);

        /// <summary>
        /// Устанавливает название метода-обработчика нажатия на кнопку.
        /// </summary>
        /// <param name="name">Название метода-обработчика.</param>
        void SetClickHandlerName(string clickHandlerName);

        /// <summary>
        /// Устанавливает доступность кнопки.
        /// </summary>
        /// <param name="name">Доступность кнопки.</param>
        void SetEnabled(bool isEnabled);

        /// <summary>
        /// Устанавливает значение аттрибута стиля кнопки.
        /// </summary>
        /// <param name="attribute">Название аттрибута.</param>
        /// <param name="value">Значение аттрибута.</param>
        void SetStyle(string attribute, string value);

        /// <summary>
        /// Возвращает строковое представление кнопки.
        /// </summary>
        /// <returns>Строковое представление кнопки.</returns>
        string GetButtonString();
    }
}