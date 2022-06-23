using System;
using Patterns.Visitor.Entities.AutoParts;

namespace Patterns.Visitor.Entities.Auto
{
    /// <summary>
    /// Грузовой автомобиль.
    /// </summary>
    public class Truck : Auto
    {
        /// <summary>
        /// Прицеп.
        /// </summary>
        public Trailer Trailer { get; set; }

        /// <summary>
        /// Инициализирует поля объекта.
        /// </summary>
        /// <param name="weight"> Масса авто. </param>
        /// <param name="engine"> Двигатель. </param>
        /// <param name="trailer"> Прицеп. </param>
        public Truck(double weight, Engine engine, Trailer trailer)
            : base(weight, engine)
        {
            Trailer = trailer ?? throw new ArgumentNullException(nameof(trailer));
        }
    }
}