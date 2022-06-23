using System;

namespace Patterns.AbstractFactory.Weapons
{
    /// <summary>
    /// Тип оружия 'Магический посох'.
    /// </summary>
    public class Staff : Weapon
    {
        /// <summary>
        /// Магический урон.
        /// </summary>
        private double _magicalDamage;

        /// <summary>
        /// Магический урон.
        /// Валидация: магический урон больше 0 включительно.
        /// </summary>
        public double MagicalDamage
        {
            get => _magicalDamage;
            private set => _magicalDamage = value >= 0.0
                    ? value
                    : throw new ArgumentOutOfRangeException(
                        message: "Магический урон должен быть больше или равен 0.",
                        paramName: nameof(MagicalDamage));
        }

        /// <summary>
        /// Переопределяет конструктор базового класса.
        /// </summary>
        /// <param name="baseDamage"> Базовый урон. </param>
        public Staff(double baseDamage) 
            : base(baseDamage) { }

        /// <summary>
        /// Переопределяет конструктор базового класса и 
        /// инициализирует поле магического урона.
        /// </summary>
        /// <param name="baseDamage"> Базовый урон. </param>
        /// <param name="magicalDamage"> Магический урон. </param>
        public Staff(double baseDamage, double magicalDamage) 
            : base(baseDamage)
        {
            MagicalDamage = magicalDamage;
        }

        /// <summary>
        /// Подсчитывает урон от оружия с учетом магического урона.
        /// </summary>
        /// <returns> Урон от оружия. </returns>
        public override double CalculateDamage()
        {
            return BaseDamage + MagicalDamage;
        }

        /// <summary>
        /// Переопределяет базовый метод преобразования в строку.
        /// </summary>
        /// <returns> Название объекта и значения полей. </returns>
        public override string ToString()
        {
            return "\n\tтип оружия: магический посох; " +
                base.ToString() +
                $"\n\tмагический урон: {MagicalDamage} ед.";
        }
    }
}