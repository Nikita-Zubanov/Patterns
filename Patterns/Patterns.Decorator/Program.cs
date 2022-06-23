using Patterns.Decorator.Decorators;
using Patterns.Decorator.Entities;
using System;
using System.Text;

namespace Patterns.Decorator
{
    /// <summary>
    /// Демонстрирует паттерн 'Декоратор'.
    /// Предметная область — кофейня, где мы можем создать горячий напиток и добавки для него.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Создается горячий напиток (наследник класса HotDrink), 
        /// после чего создается добавка для напитка (наследник класса-обертки AdditiveDecorator). 
        /// </summary>
        static void Main()
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            var p = new Program();

            var espresso = new Coffee(200, "эспрессо");
            Console.WriteLine($"Наливаем {espresso.Title} стоимостью {espresso.Cost} руб...");
            var espressoWithMilk = new ColdLiquidDecorator(espresso, 35, "молоко");
            Console.WriteLine($"Добавляем {espressoWithMilk.Title} стоимостью {espressoWithMilk.Cost} руб...");
            var espressoWithMilkAndVanilla = new SpiceDecorator(espressoWithMilk, 20, "ваниль");
            Console.WriteLine($"Добавляем {espressoWithMilkAndVanilla.Title} стоимостью {espressoWithMilkAndVanilla.Cost} руб...");

            p.PrintHotDrink(espressoWithMilkAndVanilla);

            Console.ReadLine();
        }

        /// <summary>
        /// Выводит в консоль информацию о горячем напитке.
        /// </summary>
        /// <param name="hotDrink"> Горячий напиток. </param>
        private void PrintHotDrink(HotDrink hotDrink)
        {
            Console.WriteLine($"{hotDrink.GetDescription()}. Стоимость: {hotDrink.GetSummaryCost()} руб.");
        }
    }
}
