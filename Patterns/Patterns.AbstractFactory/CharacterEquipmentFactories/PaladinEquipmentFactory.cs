using Patterns.AbstractFactory.Armors;
using Patterns.AbstractFactory.Weapons;

namespace Patterns.AbstractFactory.CharacterEquipmentFactories
{
    /// <summary>
    /// Абстрактная фабрика экипировки класса 'Паладин'.
    /// </summary>
    public class PaladinEquipmentFactory : ICharacterEquipmentFactory
    {
        /// <summary>
        /// Создает кирасу.
        /// </summary>
        /// <param name="resistanceMultiplier"> Множитель сопротивления урону. </param>
        /// <returns> Тип брони 'Кираса'. </returns>
        public Armor CreateArmor(double resistanceMultiplier)
        {
            return new Cuirass(resistanceMultiplier);
        }

        /// <summary>
        /// Создает молот.
        /// </summary>
        /// <param name="baseDamage"> Базовый урон. </param>
        /// <returns> Тип оружия 'Молот'. </returns>
        public Weapon CreateWeapon(double baseDamage)
        {
            return new Hammer(baseDamage);
        }
    }
}
