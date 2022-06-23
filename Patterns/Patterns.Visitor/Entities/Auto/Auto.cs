using System;
using Patterns.Visitor.Entities.AutoParts;
using Patterns.Visitor.Visitors;

namespace Patterns.Visitor.Entities.Auto
{
    /// <summary>
    /// Базовый класс автомобиля.
    /// </summary>
    public abstract class Auto
    {
        /// <summary>
        /// Двигатель.
        /// </summary>
        public Engine Engine { get; }

        /// <summary>
        /// Масса авто.
        /// </summary>
        public double WeightInKg { get; }

        /// <summary>
        /// Инициализирует поля объекта.
        /// </summary>
        /// <param name="weight"> Масса авто. </param>
        /// <param name="engine"> Двигатель. </param>
        protected Auto(double weight, Engine engine)
        {
            WeightInKg = weight == 0
                ? throw new ArgumentException("Вес автомобиля не может быть равен нулю.")
                : weight;

            Engine = engine ?? throw new ArgumentNullException(nameof(engine));
        }

        /// <summary>
        /// Принимает посетителя, передавая свое внутреннее состояние.
        /// </summary>
        /// <param name="visitor"> Посетитель. </param>
        public void Accept(IAutoVisitor visitor)
        {
            if (visitor == null)
            {
                throw new ArgumentNullException(nameof(visitor));
            }

            visitor.Visit(this);
        }
    }
}