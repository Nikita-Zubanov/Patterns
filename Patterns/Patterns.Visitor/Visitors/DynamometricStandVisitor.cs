using System;
using Patterns.Visitor.Entities.Auto;
using Patterns.Visitor.Entities.AutoParts;

namespace Patterns.Visitor.Visitors
{
    /// <summary>
    /// Динамометрический стенд. 
    /// </summary>
    public class DynamometricStandVisitor : IAutoVisitor
    {
        /// <summary>
        /// В зависимости от объекта авто высчитывает его динамические хар-ки.
        /// </summary>
        /// <param name="auto"> Базовый класс автомобиля. </param>
        public void Visit(Auto auto)
        {
            switch (auto)
            {
                case null:
                    throw new ArgumentNullException(nameof(auto));

                case Car car:
                    ShowCarСharacteristics(car);
                    break;

                case Truck truck:
                    ShowTruckСharacteristics(truck);
                    break;

                default:
                    throw new ArgumentException(
                        "Не определена логика вычисления динамических хар-к.",
                        nameof(auto));
            }
        }

        /// <summary>
        /// Высчитывает хар-ки легкового авто и выводит их в консоль.
        /// </summary>
        /// <param name="car"> Легковой автомобиль. </param>
        private void ShowCarСharacteristics(Car car)
        {
            var horsePower = CalculateHorsePower(car.Engine);
            var accelerationTo100KmPerHour = Math.Round(car.WeightInKg / horsePower, 2);

            Console.WriteLine(
                $"Легковой автомобиль. Мощность: {horsePower} л.с.; разгон до 100 км/ч: {accelerationTo100KmPerHour} сек.");
        }

        /// <summary>
        /// Высчитывает хар-ки грузового авто и выводит их в консоль.
        /// </summary>
        /// <param name="truck"> Грузовой автомобиль. </param>
        private void ShowTruckСharacteristics(Truck truck)
        {
            var horsePower = CalculateHorsePower(truck.Engine);
            var trailerWeight = truck.Trailer?.WeightInKg ?? 0;
            var accelerationTo100KmPerHour = Math.Round((trailerWeight + truck.WeightInKg) / horsePower, 2);

            Console.WriteLine(
                $"Грузовой автомобиль. Мощность: {horsePower} л.с.; разгон до 100 км/ч: {accelerationTo100KmPerHour} сек.");
        }

        /// <summary>
        /// Высчитывает примерное количество лошадиных сил.
        /// </summary>
        /// <param name="engine"> Двигатель. </param>
        /// <returns> Кол-во л.с. </returns>
        private int CalculateHorsePower(Engine engine)
        {
            const int RevolutionsPerMinute = 4500;
            const double TurboMultiplier = 1.5;
            const double AvgEffectivePressure = 0.8;
            const double PowerInKWattsToHPRatio = 1.3596;

            var enginePowerInKWatts = engine.VolumeInLiters * AvgEffectivePressure * RevolutionsPerMinute / 120 *
                                      (engine.HasTurbo ? TurboMultiplier : 1);
            return (int) Math.Round(enginePowerInKWatts * PowerInKWattsToHPRatio);
        }
    }
}