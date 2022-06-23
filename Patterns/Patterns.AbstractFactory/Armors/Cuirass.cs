using System;

namespace Patterns.AbstractFactory.Armors
{
    /// <summary>
    /// Тип брони 'Кираса'.
    /// </summary>
    public class Cuirass : Armor
    {
        /// <summary>
        /// Переопределяет конструктор базового класса.
        /// </summary>
        /// <param name="resistanceMultiplier"> Множитель сопротивления урону. </param>
        public Cuirass(double resistanceMultiplier) 
            : base(resistanceMultiplier) { }

        /// <summary>
        /// Подсчитывает проходящий урон с учетом брони. 
        /// </summary>
        /// <param name="damage"> Получаемый урон. </param>
        /// <returns> Прошедший урон с учетом брони. </returns>
        public override double ReduceDamage(double damage)
        {
            if (damage < 0.0)
            {
                throw new ArgumentOutOfRangeException(
                    message: "Урон оружия должен быть больше 0.",
                    paramName: nameof(damage));
            }

            return damage - (damage * ResistanceMultiplier);
        }

        /// <summary>
        /// Переопределяет базовый метод преобразования в строку.
        /// </summary>
        /// <returns> Название объекта и значения полей. </returns>
        public override string ToString()
        {
            return $"\n\tтип брони: кираса; {base.ToString()}";
        }
    }
}