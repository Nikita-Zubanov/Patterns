namespace Patterns.Decorator.Entities
{
    /// <summary>
    /// Чай.
    /// </summary>
    public class Tea : HotDrink
    {
        /// <summary>
        /// Инициализирует поля стоимости и названия чая.
        /// </summary>
        /// <param name="cost"> Стоимость чая. </param>
        /// <param name="title"> Название чая. </param>
        public Tea(double cost, string title) : base(cost, title) { }

        /// <summary>
        /// Возвращает описание чая.
        /// </summary>
        /// <returns> Название чая. </returns>
        public override string GetDescription()
        {
            return $"Чай \"{Title}\"";
        }
    }
}
