using Patterns.Decorator.Entities;
using System;

namespace Patterns.Decorator.Decorators
{
    /// <summary>
    /// Добавка к горячему напитку. Обертка над базовым классом.
    /// </summary>
    public abstract class AdditiveDecorator : HotDrink
    {
        /// <summary>
        /// Горячий напиток.
        /// </summary>
        private HotDrink _hotDrink;

        /// <summary>
        /// Горячий напиток.
        /// Валидация: вх. значение не null.
        /// </summary>
        public HotDrink HotDrink
        {
            get => _hotDrink;
            set => _hotDrink = value ?? throw new ArgumentNullException(
                message: "Напиток не должен быть null.",
                paramName: nameof(HotDrink));
        }

        /// <summary>
        /// Инициализирует поля: напиток, стоимость и название добавки.
        /// </summary>
        /// <param name="hotDrink"> Горячий напиток. </param>
        /// <param name="additiveCost"> Стоимость добавки. </param>
        /// <param name="additiveTitle"> Название добавки. </param>
        protected AdditiveDecorator(HotDrink hotDrink, double additiveCost, string additiveTitle)
            : base(additiveCost, additiveTitle)
        {
            HotDrink = hotDrink;
        }

        /// <summary>
        /// Возвращает суммарную стоимость напитка.
        /// </summary>
        /// <returns> Стоимость напитка (вложенного объекта) и добавки. </returns>
        public override double GetSummaryCost()
        {
            return HotDrink.GetSummaryCost() + Cost;
        }
    }
}
