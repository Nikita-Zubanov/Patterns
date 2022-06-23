using System;

namespace Patterns.AbstractFactory.Armors
{
    /// <summary>
    /// Элемент снаряжения 'Броня'.
    /// </summary>
    public abstract class Armor
    {
        /// <summary>
        /// Множитель сопротивления урону.
        /// </summary>
        private double _resistanceMultiplier;

        /// <summary>
        /// Множитель сопротивления урону.
        /// Валидация: множитель больше 0 включительно и меньше 1 включительно.
        /// </summary>
        public double ResistanceMultiplier
        {
            get => _resistanceMultiplier;
            private set => _resistanceMultiplier = (value >= 0.0) && (value <= 1.0)
                    ? value
                    : throw new ArgumentOutOfRangeException(
                        message: "Множитель сопротивления урону должен быть " +
                        "больше 0 включительно и меньше 1 включительно.",
                        paramName: nameof(ResistanceMultiplier));
        }

        /// <summary>
        /// Инициализирует поле сопротивлению урона.
        /// </summary>
        /// <param name="resistanceMultiplier"> Множитель сопротивления урону. </param>
        public Armor(double resistanceMultiplier)
        {
            ResistanceMultiplier = resistanceMultiplier;
        }

        /// <summary>
        /// Заставляет подклассы реализовать логику подсчета проходящего урона с учетом брони.
        /// </summary>
        /// <param name="damage"> Получаемый урон. </param>
        /// <returns> Прошедший урон с учетом брони. </returns>
        public abstract double ReduceDamage(double damage);

        /// <summary>
        /// Переопределяет базовый метод преобразования в строку.
        /// </summary>
        /// <returns> Названия и значения полей. </returns>
        public override string ToString()
        {
            return $"\n\tсопротивление урону: {ResistanceMultiplier * 100}%; ";
        }
    }
}