using Patterns.Visitor.Entities.AutoParts;

namespace Patterns.Visitor.Entities.Auto
{
    /// <summary>
    /// Легковой автомобиль.
    /// </summary>
    public class Car : Auto
    {
        /// <summary>
        /// Инициализирует поля объекта.
        /// </summary>
        /// <param name="weight"> Масса авто. </param>
        /// <param name="engine"> Двигатель. </param>
        public Car(double weight, Engine engine)
            : base(weight, engine)
        {
        }
    }
}