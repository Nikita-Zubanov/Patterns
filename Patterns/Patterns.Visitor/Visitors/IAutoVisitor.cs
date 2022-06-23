using Patterns.Visitor.Entities.Auto;

namespace Patterns.Visitor.Visitors
{
    /// <summary>
    /// Интерфейс посетителя.
    /// </summary>
    public interface IAutoVisitor
    {
        /// <summary>
        /// Посетить объект.
        /// </summary>
        /// <param name="auto"> Базовый класс автомобиля. </param>
        void Visit(Auto auto);
    }
}