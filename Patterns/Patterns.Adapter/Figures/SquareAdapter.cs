using Patterns.Adapter.AnotherLibraryClasses;
using System;

namespace Patterns.Adapter.Figures
{
    /// <summary>
    /// Адаптер класса "Квадрат".
    /// </summary>
    public class SquareAdapter : IPrint
    {
        /// <summary>
        /// Квадрат.
        /// </summary>
        private readonly Square _square;

        /// <summary>
        /// Инициализирует поля объекта.
        /// </summary>
        /// <param name="square"> Адаптируемый квадрат. </param>
        public SquareAdapter(Square square)
		{
            if (square == null)
			{
                throw new ArgumentNullException(nameof(square));
			}

            _square = square;
        }

        /// <summary>
        /// Отрисовывает квадрат в консоли.
        /// </summary>
        public void PrintToConsole()
        {
            _square.Print(Console.Write, "* ", "  ");
        }
    }
}