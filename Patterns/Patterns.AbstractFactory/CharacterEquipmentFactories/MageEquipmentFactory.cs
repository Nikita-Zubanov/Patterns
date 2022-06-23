using Patterns.AbstractFactory.Armors;
using Patterns.AbstractFactory.Weapons;

namespace Patterns.AbstractFactory.CharacterEquipmentFactories
{
    /// <summary>
    /// Абстрактная фабрика экипировки класса 'Маг'.
    /// </summary>
    public class MageEquipmentFactory : ICharacterEquipmentFactory
    {
        /// <summary>
        /// Шанс вылечиться от получаемого урона по умолчанию.
        /// </summary>
        private const double DefaultConvertDamageToHpChance = 0.3;

        /// <summary>
        /// Магический урон от атаки по умолчанию.
        /// </summary>
        private const double DefaultMagicalDamage = 50;

        /// <summary>
        /// Создает мантию.
        /// </summary>
        /// <param name="resistanceMultiplier"> Множитель сопротивления урону. </param>
        /// <returns> Тип брони 'Мантия'. </returns>
        public Armor CreateArmor(double resistanceMultiplier)
        {
            return new Mantle(resistanceMultiplier, DefaultConvertDamageToHpChance);
        }

        /// <summary>
        /// Создает магический посох.
        /// </summary>
        /// <param name="baseDamage"> Базовый урон. </param>
        /// <returns> Тип оружия 'Магический посох'. </returns>
        public Weapon CreateWeapon(double baseDamage)
        {
            return new Staff(baseDamage, DefaultMagicalDamage);
        }
    }
}
