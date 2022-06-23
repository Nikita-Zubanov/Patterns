using System;

namespace Patterns.AbstractFactory.Weapons
{
    /// <summary>
    /// Элемент снаряжения 'Оружие'.
    /// </summary>
    public abstract class Weapon
    {
        /// <summary>
        /// Базовый урон.
        /// </summary>
        private double _baseDamage;

        /// <summary>
        /// Базовый урон.
        /// Валидация: базовый урон больше 0.
        /// </summary>
        public double BaseDamage
        {
            get => _baseDamage;
            private set => _baseDamage = value > 0.0
                    ? value
                    : throw new ArgumentOutOfRangeException(
                        message: "Урон оружия должен быть больше 0.",
                        paramName: nameof(BaseDamage));
        }

        /// <summary>
        /// Инициализирует поле базового урона.
        /// </summary>
        /// <param name="baseDamage"> Базовый урон. </param>
        public Weapon(double baseDamage)
        {
            BaseDamage = baseDamage;
        }

        /// <summary>
        /// Заставляет подклассы реализовать логику подсчета урона от оружия.
        /// </summary>
        /// <returns> Урон от оружия. </returns>
        public abstract double CalculateDamage();

        /// <summary>
        /// Переопределяет базовый метод преобразования в строку.
        /// </summary>
        /// <returns> Названия и значения полей. </returns>
        public override string ToString()
        {
            return $"\n\tбазовый урон: {BaseDamage} ед.; ";
        }
    }
}