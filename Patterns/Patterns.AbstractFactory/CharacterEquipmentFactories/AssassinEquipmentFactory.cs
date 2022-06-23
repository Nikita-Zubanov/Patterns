using Patterns.AbstractFactory.Armors;
using Patterns.AbstractFactory.Weapons;

namespace Patterns.AbstractFactory.CharacterEquipmentFactories
{
    /// <summary>
    /// Абстрактная фабрика экипировки класса 'Наемный убийца'.
    /// </summary>
    public class AssassinEquipmentFactory : ICharacterEquipmentFactory
    {
        /// <summary>
        /// Шанс уворота от атаки по умолчанию.
        /// </summary>
        private const double DefaultDodgeChance = 0.25;

        /// <summary>
        /// Шанс крит. урона по умолчанию.
        /// </summary>
        private const double DefaultCritChance = 0.45;

        /// <summary>
        /// Множитель крит. урона по умолчанию.
        /// </summary>
        private const double DefaultCritDamageMultiplier = 1.5;

        /// <summary>
        /// Создает нагрудник.
        /// </summary>
        /// <param name="resistanceMultiplier"> Множитель сопротивления урону. </param>
        /// <returns> Тип брони 'Нагрудник'. </returns>
        public Armor CreateArmor(double resistanceMultiplier)
        {
            return new ChestArmor(resistanceMultiplier, DefaultDodgeChance);
        }

        /// <summary>
        /// Создает кинжал.
        /// </summary>
        /// <param name="baseDamage"> Базовый урон. </param>
        /// <returns> Тип оружия 'Кинжал'. </returns>
        public Weapon CreateWeapon(double baseDamage)
        {
            return new Dagger(baseDamage, DefaultCritChance, DefaultCritDamageMultiplier);
        }
    }
}
