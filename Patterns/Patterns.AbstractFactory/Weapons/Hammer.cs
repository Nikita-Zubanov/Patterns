namespace Patterns.AbstractFactory.Weapons
{
    /// <summary>
    /// Тип оружия 'Молот'.
    /// </summary>
    public class Hammer : Weapon
    {
        /// <summary>
        /// Переопределяет конструктор базового класса.
        /// </summary>
        /// <param name="baseDamage"> Базовый урон. </param>
        public Hammer(double baseDamage) 
            : base(baseDamage) { }

        /// <summary>
        /// Возвращает урон от оружия.
        /// </summary>
        /// <returns> Урон от оружия. </returns>
        public override double CalculateDamage()
        {
            return BaseDamage;
        }

        /// <summary>
        /// Переопределяет базовый метод преобразования в строку.
        /// </summary>
        /// <returns> Название объекта и значения полей. </returns>
        public override string ToString()
        {
            return $"\n\tтип оружия: молот; {base.ToString()}";
        }
    }
}