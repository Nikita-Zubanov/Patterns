using System;

namespace Patterns.AbstractFactory.Weapons
{
    /// <summary>
    /// Тип оружия 'Кинжал'.
    /// </summary>
    public class Dagger : Weapon
    {
        /// <summary>
        /// Шанс нанести критический урон.
        /// </summary>
        private double _critChance;

        /// <summary>
        /// Множитель критического урона.
        /// </summary>
        private double _critDamageMultiplier = 1.0;

        /// <summary>
        /// Шанс нанести критический урон.
        /// Валидация: шанс крит. урона больше 0 включительно и меньше 1 включительно.
        /// </summary>
        public double CritChance
        {
            get => _critChance;
            private set => _critChance = (value >= 0.0) && (value <= 1.0)
                    ? value
                    : throw new ArgumentOutOfRangeException(
                        message: "Шанс критического урона должен быть в диапазоне " +
                        "от 0 включительно до 1 включительно.",
                        paramName: nameof(CritChance));
        }

        /// <summary>
        /// Множитель критического урона.
        /// Валидация: множитель крит. урона больше 1 включительно.
        /// </summary>
        public double CritDamageMultiplier
        {
            get => _critDamageMultiplier;
            private set => _critDamageMultiplier = value >= 1.0
                    ? value
                    : throw new ArgumentOutOfRangeException(
                        message: "Множитель критического урона должен быть больше или хотя бы равен 1.",
                        paramName: nameof(CritDamageMultiplier));
        }

        /// <summary>
        /// Переопределяет конструктор базового класса и 
        /// инициализирует поля шанса и множителя крит. урона.
        /// </summary>
        /// <param name="baseDamage"> Базовый урон. </param>
        /// <param name="critChance"> Шанс крит. урона.</param>
        /// <param name="critDamageMultiplier"> Множитель крит. урона. </param>
        public Dagger(double baseDamage, double critChance, double critDamageMultiplier) 
            : base(baseDamage)
        {
            CritChance = critChance;
            CritDamageMultiplier = critDamageMultiplier;
        }

        /// <summary>
        /// Подсчитывает урон от оружия с учетом шанса и множителя крит. урона.
        /// </summary>
        /// <returns> Урон от оружия. </returns>
        public override double CalculateDamage()
        {
            var randProbability = new Random().NextDouble();

            return randProbability > CritChance
                ? BaseDamage
                : BaseDamage * CritDamageMultiplier;
        }

        /// <summary>
        /// Переопределяет базовый метод преобразования в строку.
        /// </summary>
        /// <returns> Название объекта и значения полей. </returns>
        public override string ToString()
        {
            return "\n\tтип оружия: кинжал; " +
                base.ToString() +
                $"\n\tшанс крит. урона: {CritChance * 100}%; " +
                $"\n\tмнож. крит. урона: {CritDamageMultiplier * 100}%.";
        }
    }
}