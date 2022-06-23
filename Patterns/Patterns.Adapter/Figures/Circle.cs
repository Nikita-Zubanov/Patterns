using System;

namespace Patterns.Adapter.Figures
{
    /// <summary>
    /// Кольцо.
    /// </summary>
    public class Circle : IPrint
    {
        /// <summary>
        /// Толщина отрисовываемого кольца.
        /// </summary>
        private const double Thickness = 0.3;

        /// <summary>
        /// Радиус.
        /// </summary>
        private int _radius;

        /// <summary>
        /// Радиус.
        /// Валидация: радиус должен быть больше 0.
        /// </summary>
        public int Radius
        {
            get => _radius;
            set => _radius = value > 0
                ? value
                : throw new ArgumentOutOfRangeException(
                    message: "Радиус кольца не может быть меньше или равен 0.",
                    paramName: nameof(Radius));
        }

        /// <summary>
        /// Отрисовывает кольцо в консоли.
        /// </summary>
        public void PrintToConsole()
        {
            var innerRadius = Radius - Thickness;
            var outerRadius = Radius + Thickness;

            for (var y = Radius; y >= -Radius; --y)
            {
                for (double x = -Radius; x < outerRadius; x += 0.5)
                {
                    var value = Math.Pow(x, 2) + Math.Pow(y, 2);

                    var fillingSymbol = value >= Math.Pow(innerRadius, 2) &&
                                        value <= Math.Pow(outerRadius, 2)
                        ? "*"
                        : " ";

                    Console.Write(fillingSymbol);
                }
                Console.WriteLine();
            }
        }
    }
}