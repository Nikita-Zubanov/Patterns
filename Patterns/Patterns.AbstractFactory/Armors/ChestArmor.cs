using System;

namespace Patterns.AbstractFactory.Armors
{
    /// <summary>
    /// Тип брони 'Нагрудник с уворотами'.
    /// </summary>
    public class ChestArmor : Armor
    {
        /// <summary>
        /// Шанс уворота от атаки.
        /// </summary>
        private double _dodgeChance;

        /// <summary>
        /// Шанс уворота от атаки.
        /// Валидация: шанс уворота больше 0 включительно и меньше 1 включительно.
        /// </summary>
        public double DodgeChance
        {
            get => _dodgeChance;
            private set => _dodgeChance = (value >= 0.0) && (value <= 1.0)
                    ? value
                    : throw new ArgumentOutOfRangeException(
                        message: "Шанс увернуться должен быть в диапазоне от 0 до 1 включительно.",
                        paramName: nameof(DodgeChance));
        }

        /// <summary>
        /// Переопределяет конструктор базового класса и инициализирует поле шанса уворота.
        /// </summary>
        /// <param name="resistanceMultiplier"> Множитель сопротивления урону. </param>
        /// <param name="dodgeChance"> Шанс уворота. </param>
        public ChestArmor(double resistanceMultiplier, double dodgeChance) 
            : base(resistanceMultiplier)
        {
            DodgeChance = dodgeChance;
        }

        /// <summary>
        /// Подсчитывает проходящий урон с учетом брони. 
        /// А также позволяет вовсе не получить урон, если шанс уворота велик.
        /// </summary>
        /// <param name="damage"> Получаемый урон. </param>
        /// <returns> Прошедший урон с учетом брони или уворота. </returns>
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
            const double DamageAtDodge = 0;

            return randProbability > DodgeChance
                ? receivedDamaged
                : DamageAtDodge;
        }

        /// <summary>
        /// Переопределяет базовый метод преобразования в строку.
        /// </summary>
        /// <returns> Название объекта и значения полей. </returns>
        public override string ToString()
        {
            return $"\n\tтип брони: нагрудник; {base.ToString()} \n\tшанс уклонения: {DodgeChance * 100}%.";
        }
    }
}