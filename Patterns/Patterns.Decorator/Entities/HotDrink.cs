using System;

namespace Patterns.Decorator.Entities
{
    /// <summary>
    /// Горячий напиток.
    /// </summary>
    public abstract class HotDrink
    {
        /// <summary>
        /// Стоимость горячего напитка.
        /// </summary>
        private double _cost;

        /// <summary>
        /// Название горячего напитка.
        /// </summary>
        private string _title;

        /// <summary>
        /// Стоимость горячего напитка.
        /// Валидация: стоимость напитка > 0.
        /// </summary>
        public double Cost
        {
            get => _cost;
            set => _cost = value > 0
                ? value
                : throw new ArgumentNullException(
                    message: "Стоимость напитка должна быть больше 0.",
                    paramName: nameof(Cost));
        }

        /// <summary>
        /// Название горячего напитка.
        /// Валидация: название должно быть не пустое и не null.
        /// </summary>
        public string Title
        {
            get => _title;
            private set => _title = string.IsNullOrWhiteSpace(value)
                ? throw new ArgumentNullException(
                    message: "Название горячего напитка пусто или равно null.",
                    paramName: nameof(Title))
                : value;
        }

        /// <summary>
        /// Позволяет производным классам определять собственный конструктор.
        /// </summary>
        protected HotDrink() { }

        /// <summary>
        /// Инициализирует поля стоимости и названия напитка.
        /// </summary>
        /// <param name="cost"> Стоимость напитка. </param>
        /// <param name="title"> Название напитка. </param>
        protected HotDrink(double cost, string title)
        {
            Cost = cost;
            Title = title;
        }

        /// <summary>
        /// Возвращает описание напитка.
        /// </summary>
        /// <returns>
        /// Тип, название напитка и [возможное] дополнительное описание.
        /// </returns>
        public abstract string GetDescription();

        /// <summary>
        /// Возвращает суммарную стоимость напитка, позволяя её переопределить.
        /// </summary>
        /// <returns> Суммарная стоимость напитка. </returns>
        public virtual double GetSummaryCost()
        {
            return Cost;
        }
    }
}
