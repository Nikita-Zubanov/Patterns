using System;
using System.Linq;

namespace Patterns.Adapter.Figures
{
    /// <summary>
    /// Равносторонний треугольник.
    /// </summary>
    public class EquilateralTriangle : IPrint
    {
        /// <summary>
        /// Размер стороны.
        /// </summary>
        private int _size;

        /// <summary>
        /// Размер стороны.
        /// Валидация: сторона должна быть больше 0.
        /// </summary>
        public int Size
        {
            get => _size;
            set => _size = value > 0
                ? value
                : throw new ArgumentOutOfRangeException(
                    message: "Сторона треугольника не может быть меньше или равна 0.",
                    paramName: nameof(Size));
        }

        /// <summary>
        /// Отрисовывает равносторонний треугольник в консоли.
        /// </summary>
        public void PrintToConsole()
        {
            for (var i = 0; i < Size; i++)
            {
                var leftSpaces = string.Concat(Enumerable.Repeat(" ", Size - i));
                Console.Write(leftSpaces);
                Console.Write("*");

                if (i != 0)
                {
                    var middleSpaces = string.Concat(Enumerable.Repeat(" ", i * 2 - 1));
                    Console.Write(middleSpaces);
                    Console.Write("*");
                }

                Console.WriteLine();
            }

            for (var i = 0; i <= Size; i++)
            {
                Console.Write("* ");
            }

            Console.WriteLine();
        }
    }
}