using System;
using Patterns.Visitor.Entities.Auto;
using Patterns.Visitor.Entities.AutoParts;
using Patterns.Visitor.Visitors;

namespace Patterns.Visitor
{
    /// <summary>
    /// Демонстрирует паттерн "Посетитель".
    /// </summary>
    class Program
    {
        /// <summary>
        /// Создает экземпляры дочерних классов авто, после чего:
        /// 1. демонстрирует их динамические хар-ки;
        /// 2. прокачивает авто;
        /// 3. повторно демонстрирует хар-ки.
        /// </summary>
        static void Main()
        {
            var program = new Program();
            var standVisitor = new DynamometricStandVisitor();
            var xzibitVisitor = new XzibitVisitor();

            var car = program.MakeCar();
            car.Accept(standVisitor);
            car.Accept(xzibitVisitor);
            car.Accept(standVisitor);

            var truck = program.MakeTruck();
            truck.Accept(standVisitor);
            truck.Accept(xzibitVisitor);
            truck.Accept(standVisitor);

            Console.ReadLine();
        }

        /// <summary>
        /// Создает и возвращает легковой автомобиль.
        /// </summary>
        /// <returns> Легковой автомобиль. </returns>
        private Car MakeCar()
        {
            var engine = new Engine {VolumeInLiters = 1.8};
           
            return new Car(1400, engine);
        }

        /// <summary>
        /// Создает и возвращает грузовой автомобиль.
        /// </summary>
        /// <returns> Грузовой автомобиль. </returns>
        private Truck MakeTruck()
        {
            var engine = new Engine {VolumeInLiters = 6.0};
            var trailer = new Trailer {TrailerType = TrailerType.Tank, WeightInKg = 2000};
            
            return new Truck(4000, engine, trailer);
        }
    }
}