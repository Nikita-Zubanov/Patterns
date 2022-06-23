using Patterns.AbstractFactory.Armors;
using Patterns.AbstractFactory.Weapons;

namespace Patterns.AbstractFactory.CharacterEquipmentFactories
{
    /// <summary>
    /// Абстрактная фабрика экипировки персонажа.
    /// </summary>
    public interface ICharacterEquipmentFactory
    {
        /// <summary>
        /// Создать броню.
        /// </summary>
        /// <param name="resistanceMultiplier"> Множитель сопротивления урону. </param>
        /// <returns> Элемент снаряжения 'Броня'. </returns>
        Armor CreateArmor(double resistanceMultiplier);

        /// <summary>
        /// Создать оружие.
        /// </summary>
        /// <param name="baseDamage"> Базовый урон. </param>
        /// <returns> Элемент снаряжения 'Оружие'. </returns>
        Weapon CreateWeapon(double baseDamage);
    }
}
