using Patterns.AbstractFactory.CharacterEquipmentFactories;
using System;
using System.Linq;
using System.Text;

namespace Patterns.AbstractFactory
{
    /// <summary>
    /// Демонстрирует паттерн 'Абстрактная фабрика'.
    /// Экипирует персонажа определенного класс оружием и броней.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Метод зациклен для лучшей отзывчивости.
        /// Спрашивает у пользователя, какой класс персонажа тот хочет экипировать, 
        /// после чего создает комплект указанного класса.
        /// </summary>
        static void Main()
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            var p = new Program();
            var assassinFactory = new AssassinEquipmentFactory();
            var mageFactory = new MageEquipmentFactory();
            var paladinFactory = new PaladinEquipmentFactory();

            while (true)
            {
                try
                {
                    var characterClass = p.OfferChooseCharacterClass();
                    switch (characterClass)
                    {
                        case CharacterClass.Assassin:
                            p.CreateAndShowCharacterEquipment(assassinFactory);
                            break;

                        case CharacterClass.Mage:
                            p.CreateAndShowCharacterEquipment(mageFactory);
                            break;

                        case CharacterClass.Paladin:
                            p.CreateAndShowCharacterEquipment(paladinFactory);
                            break;

                        default:
                            throw new Exception(
                                $"Для класса \"{characterClass}\" не предусмотрено снаряжение.");
                    }
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        /// <summary>
        /// Предлагает пользователю выбрать класс (ввести его название в консоль), после чего 
        /// парсит ответ в enum и возвращает, если парсинг успешен, иначе генерирует ошибку.
        /// </summary>
        /// <returns> Класс персонажа. </returns>
        private CharacterClass OfferChooseCharacterClass()
        {
            var classes = Enum.GetValues(typeof(CharacterClass)).Cast<CharacterClass>();
            Console.WriteLine($"На кого сделать снаряжение? " +
                $"\n\t{string.Join("\n\t", classes)} " +
                $"\nВведите полностью класс персонажа.");

            var answer = Console.ReadLine();

            if (!Enum.TryParse(answer, out CharacterClass characterClass))
            {
                throw new Exception($"Значение {answer} не определяется как класс персонажа.");
            }

            return characterClass;
        }

        /// <summary>
        /// Создает броню и оружие с помощью абстрактной фабрики.
        /// </summary>
        /// <param name="factory"> Абстрактная фабрика по экипировке персонажа. </param>
        private void CreateAndShowCharacterEquipment(ICharacterEquipmentFactory factory)
        {
            var resistanceMultiplier = GetResultOnQuestion<double>(
                "Введите множитель сопротивления урону(0 - минимальный, 1 - неуязвимость):");
            var armor = factory.CreateArmor(resistanceMultiplier);

            var baseDamage = GetResultOnQuestion<double>("Введите базовый урон:");
            var weapon = factory.CreateWeapon(baseDamage);

            Console.WriteLine($"{armor} \n\tПри уроне 100 ед. носитель брони получает " +
                              $"{armor.ReduceDamage(100)} ед. (с учетом доп. эффектов)");
            Console.WriteLine($"{weapon} \n\tУрон от атаки составляет " +
                              $"{weapon.CalculateDamage()} (с учетом доп. эффектов)\n");
        }

        /// <summary>
        /// Задает в консоли вопрос, ответ на который интерпретирует в соответствующий тип и возвращает.
        /// </summary>
        /// <param name="question"> 
        /// Вопрос для пользователя, чтобы он понимал, что от него ожидает программа.
        /// </param>
        /// <returns> Экземпляр типа данных T. </returns>
        private T GetResultOnQuestion<T>(string question)
        {
            if (string.IsNullOrEmpty(question))
            {
                throw new ArgumentNullException(
                    paramName: nameof(question),
                    message: "Вопрос пустой!");
            }

            Console.WriteLine(question);
            var answer = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(answer))
            {
                throw new ArgumentNullException(
                    paramName: nameof(answer),
                    message: "Вы ничего не ответили!");
            }

            if (typeof(T) == typeof(float) ||
                typeof(T) == typeof(double) ||
                typeof(T) == typeof(decimal))
            {
                answer = answer.Replace(".", ",");
            }

            try
            {
                var resultObj = Convert.ChangeType(answer, typeof(T));

                return (T)resultObj;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(
                    message: $"Не удалось интерпретировать введенную строку как {typeof(T)}",
                    innerException: e);
            }
        }
    }
}