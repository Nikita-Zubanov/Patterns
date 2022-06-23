using System;
using Patterns.Visitor.Entities.Auto;

namespace Patterns.Visitor.Visitors
{
    /// <summary>
    /// Класс улучшающий динамические хар-ки авто.
    /// </summary>
    public class XzibitVisitor : IAutoVisitor
    {
        /// <summary>
        /// В зависимости от объекта авто улучшает его динамические хар-ки.
        /// </summary>
        /// <param name="auto"> Базовый класс автомобиля. </param>
        public void Visit(Auto auto)
        {
            switch (auto)
            {
                case null:
                    throw new ArgumentNullException(nameof(auto));

                case Car car:
                    PumpCar(car);
                    break;

                case Truck truck:
                    PumpTruck(truck);
                    break;

                default:
                    throw new ArgumentException(
                        "Такой колымаге даже Xzibit не поможет.",
                        nameof(auto));
            }
        }

        /// <summary>
        /// Улучшить двигатель легкового автомобиля.
        /// </summary>
        /// <param name="car"> Легковой автомобиль. </param>
        private void PumpCar(Car car)
        {
            car.Engine.VolumeInLiters *= 1.25;
            car.Engine.HasTurbo = true;
        }

        /// <summary>
        /// Улучшить двигатель и снять прицеп у грузового автомобиля.
        /// </summary>
        /// <param name="truck"> Грузовой автомобиль. </param>
        private void PumpTruck(Truck truck)
        {
            truck.Engine.VolumeInLiters *= 1.5;
            truck.Engine.HasTurbo = true;
            truck.Trailer = null;
        }
    }
}