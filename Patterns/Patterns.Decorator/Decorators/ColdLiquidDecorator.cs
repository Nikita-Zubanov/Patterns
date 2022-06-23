using Patterns.Decorator.Entities;

namespace Patterns.Decorator.Decorators
{
    /// <summary>
    /// Холодная жидкая добавка.
    /// </summary>
    public class ColdLiquidDecorator : AdditiveDecorator
    {
        /// <summary>
        /// Инициализирует поля: напиток, стоимость и название холодной добавки.
        /// </summary>
        /// <param name="hotDrink"> Горячий напиток. </param>
        /// <param name="coldLiquidCost"> Стоимость холодной добавки. </param>
        /// <param name="coldLiquidTitle"> Название холодной добавки. </param>
        public ColdLiquidDecorator(HotDrink hotDrink, double coldLiquidCost, string coldLiquidTitle) 
            : base(hotDrink, coldLiquidCost, coldLiquidTitle)
        {
        }

        /// <summary>
        /// Возвращает описание напитка.
        /// </summary>
        /// <returns> Название напитка и холодной добавки. </returns>
        public override string GetDescription()
        {
            return $"{HotDrink.GetDescription()} с холодной добавкой \"{Title}\"";
        }
    }
}
