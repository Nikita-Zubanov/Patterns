using System;

namespace Patterns.Adapter.AnotherLibraryClasses
{
	/// <summary>
	/// Квадрат.
	/// </summary>
	public class Square
	{
		/// <summary>
		/// Размер стороны.
		/// </summary>
		public int Size { get; private set; }

		/// <summary>
		/// Инициализирует поля объекта.
		/// </summary>
		/// <param name="size"> Размер стороны. </param>
		public Square(int size)
		{
			if (size <= 0)
			{
				throw new Exception("Сторона квадрата не может быть равна или меньше 0.");
			}

			Size = size;
		}

		/// <summary>
		/// Отрисовывает квадрат.
		/// </summary>
		/// <param name="printCallback"> Метод обратного вызова для отрисовки квадрата. </param>
		/// <param name="borderFiller"> Наполнитель для границ квадрата. </param>
		/// <param name="filler"> Наполнитель для квадрата. </param>
		public void Print(Action<string> printCallback, string borderFiller, string filler)
		{
			if (printCallback == null)
			{
				throw new ArgumentNullException(nameof(printCallback));
			}

			if (string.IsNullOrEmpty(borderFiller))
			{
				throw new ArgumentNullException(nameof(borderFiller));
			}

			if (string.IsNullOrEmpty(filler))
			{
				throw new ArgumentNullException(nameof(filler));
			}

			for (var i = 0; i < Size; i++)
			{
				for (var j = 0; j < Size; j++)
				{
					if (i == 0 || j == 0 || i == Size - 1 || j == Size - 1)
					{
						printCallback(borderFiller);
					}
					else
					{
						printCallback(filler);
					}

					if (j == Size - 1)
					{
						printCallback("\n");
					}
				}
			}
		}
	}
}