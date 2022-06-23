using System;

namespace Patterns.Strategy
{
	/// <summary>
	/// Вспомогательный класс для работы с консолью.
	/// </summary>
	public static class ConsoleHelper
	{
		/// <summary>
		/// Выводит информационное сообщение в консоль.
		/// </summary>
		/// <param name="message">Текст сообщения.</param>
		public static void WriteInfoMessage(string message)
		{
			if (string.IsNullOrWhiteSpace(message))
			{
				throw new ArgumentNullException(nameof(message));
			}

			Console.BackgroundColor = ConsoleColor.Green;
			Console.ForegroundColor = ConsoleColor.Black;

			Console.WriteLine(message);

			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;
		}

		/// <summary>
		/// Выводит сообщение об ошибке в консоль.
		/// </summary>
		/// <param name="message">Текст сообщения.</param>
		public static void WriteErrorMessage(string message)
		{
			if (string.IsNullOrWhiteSpace(message))
			{
				throw new ArgumentNullException(nameof(message));
			}

			Console.BackgroundColor = ConsoleColor.DarkRed;

			Console.WriteLine(message);

			Console.BackgroundColor = ConsoleColor.Black;
		}

		/// <summary>
		/// Выводит сообщение в консоль.
		/// </summary>
		/// <param name="message">Текст сообщения.</param>
		/// <param name="color">Цвет текста сообщения.</param>
		public static void WriteMessage(string message, ConsoleColor color = ConsoleColor.White)
		{
			if (string.IsNullOrWhiteSpace(message))
			{
				throw new ArgumentNullException(nameof(message));
			}

			Console.ForegroundColor = color;

			Console.WriteLine(message);

			Console.ForegroundColor = ConsoleColor.White;
		}

		/// <summary>
		/// Выводит сообщение в консоль по центру.
		/// </summary>
		/// <param name="message">Текст сообщения.</param>
		/// <param name="color">Цвет текста сообщения.</param>
		public static void WriteMessageInCenter(string message, ConsoleColor color = ConsoleColor.White)
		{
			if (string.IsNullOrWhiteSpace(message))
			{
				throw new ArgumentNullException(nameof(message));
			}

			var consoleCursorLeft = Console.CursorLeft;
			var consoleCursorTop = Console.CursorTop;

			Console.SetCursorPosition(Console.WindowWidth / 2, 0);

			WriteMessage(message, color);

			Console.SetCursorPosition(consoleCursorLeft, consoleCursorTop);
		}
	}
}