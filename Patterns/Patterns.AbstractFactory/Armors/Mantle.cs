using System;

namespace Patterns.AbstractFactory.Armors
{
    /// <summary>
    /// Тип брони 'Мантия'.
    /// </summary>
    public class Mantle : Armor
    {
        /// <summary>
        /// Шанс вылечиться от получаемого урона.
        /// </summary>
        private double _convertDamageToHpChance;

        /// <summary>
        /// Шанс вылечиться от получаемого урона.
        /// Валидация: шанс вылечиться больше 0 включительно и меньше 1 включительно.
        /// </summary>
        public double ConvertDamageToHpChance
        {
            get => _convertDamageToHpChance;
            private set => _convertDamageToHpChance = (value >= 0.0) && (value <= 1.0)
                    ? value
                    : throw new ArgumentOutOfRangeException(
                        message: "Шанс вылечиться от получаемого урона должен быть " +
                        "в диапазоне от 0 включительно до 1 включительно.",
                        paramName: nameof(ConvertDamageToHpChance));
        }

        /// <summary>
        /// Переопределяет конструктор базового класса и
        /// инициализирует поле шанса вылечиться от получаемого урона.
        /// </summary>
        /// <param name="resistanceMultiplier"> Множитель сопротивления урону. </param>
        /// <param name="convertDamageToHpChance"> Шанс вылечиться от получаемого урона. </param>
        public Mantle(double resistanceMultiplier, double convertDamageToHpChance) 
            : base(resistanceMultiplier) 
        {
            ConvertDamageToHpChance = convertDamageToHpChance;
        }

        /// <summary>
        /// Подсчитывает проходящий урон с учетом брони. 
        /// Также позволяет вылечиться от получаемого урона, если шанс велик.
        /// </summary>
        /// <param name="damage"> Получаемый урон. </param>
        /// <returns> 
        /// Прошедший урон с учетом брони или шанса вылечиться 
        /// (в таком случае, урон отрицательный, что должно вылечить). 
        /// </returns>
        public override double ReduceDamage(double damage)
        {
            if (damage < 0.0)
            {
                throw new ArgumentOutOfRangeException(
                    message: "Урон оружия должен быть больше 0.",
                    paramName: nameof(damage));
            }

            var randProbability = new Random().NextDouble();
            var receivedDamaged = damage - (damage * ResistanceMultiplier);

            return randProbability > ConvertDamageToHpChance
                ? receivedDamaged
                : -damage;
        }

        /// <summary>
        /// Переопределяет базовый метод преобразования в строку.
        /// </summary>
        /// <returns> Название объекта и значения полей. </returns>
        public override string ToString()
        {
            return "\n\tтип брони: мантия; " +
                $"{base.ToString()} " +
                $"\n\tшанс вылечиться от получаемого урона: {ConvertDamageToHpChance * 100}%.";
        }
    }
}