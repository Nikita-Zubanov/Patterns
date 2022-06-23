using System;
using System.Collections.Generic;
using Patterns.Adapter.AnotherLibraryClasses;
using Patterns.Adapter.Figures;

namespace Patterns.Adapter
{
    /// <summary>
    /// Демонстрирует паттерн "Адаптер".
    /// Использует иерархию классов, имеющих функциональность отрисовки в консоли.
    /// Преобразует интерфейс класса "Квадрат" из сторонней библиотеки в
    /// используемый в приложении интерфейс.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Создает список объектов, после чего поочередно передает их в метод
        /// для отрисовки в качестве параметра с единым интерфейсом.
        /// </summary>
        static void Main()
        {
            var program = new Program();

            var printingObjects = new List<IPrint>
            {
                new Circle {Radius = 5},
                new EquilateralTriangle {Size = 10},
                new SquareAdapter(new Square(10)),
            };

            foreach (var printingObj in printingObjects)
            {
                program.PrintToConsole(printingObj);
                Console.WriteLine();
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Вызывает метод отрисовки у объекта.
        /// </summary>
        /// <param name="obj"> Объект, позволяющий себя отрисовать. </param>
        private void PrintToConsole(IPrint obj)
        {
            obj.PrintToConsole();
        }
    }
}