namespace Patterns.Decorator.Entities
{
    /// <summary>
    /// Кофе.
    /// </summary>
    public class Coffee : HotDrink
    {
        /// <summary>
        /// Инициализирует поля стоимости и названия кофе.
        /// </summary>
        /// <param name="cost"> Стоимость кофе. </param>
        /// <param name="title"> Название кофе. </param>
        public Coffee(double cost, string title) : base(cost, title) { }

        /// <summary>
        /// Возвращает описание кофе.
        /// </summary>
        /// <returns> Название кофе. </returns>
        public override string GetDescription()
        {
            return $"Кофе \"{Title}\"";
        }
    }
}
