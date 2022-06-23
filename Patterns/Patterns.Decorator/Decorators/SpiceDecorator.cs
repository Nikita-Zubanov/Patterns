using Patterns.Decorator.Entities;

namespace Patterns.Decorator.Decorators
{
    /// <summary>
    /// Пряность.
    /// </summary>
    public class SpiceDecorator : AdditiveDecorator
    {
        /// <summary>
        /// Инициализирует поля: напиток, стоимость и название пряности.
        /// </summary>
        /// <param name="hotDrink"> Горячий напиток. </param>
        /// <param name="spiceCost"> Стоимость пряности. </param>
        /// <param name="spiceTitle"> Название пряности. </param>
        public SpiceDecorator(HotDrink hotDrink, double spiceCost, string spiceTitle)
            : base(hotDrink, spiceCost, spiceTitle)
        {
        }

        /// <summary>
        /// Возвращает описание напитка.
        /// </summary>
        /// <returns> Название напитка и пряности. </returns>
        public override string GetDescription()
        {
            return $"{HotDrink.GetDescription()} с пряностью \"{Title}\"";
        }
    }
}
