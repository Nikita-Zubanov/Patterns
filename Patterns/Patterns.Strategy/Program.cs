using System;
using System.Text;
using Patterns.Strategy.Modes;

namespace Patterns.Strategy
{
	class Program
	{
		/// <summary>
		/// Колчичество символов, которые нужно ввести в консоль, что программа посчитала это за флуд.
		/// </summary>
		private const int SymbolsToFloodCount = 15;

		/// <summary>
		/// Включает смартфон, который в зависимости от действий пользователя и выбранного режима работы
		/// включает игру или музыку.
		/// </summary>
		static void Main(string[] args)
		{
			Console.InputEncoding = Encoding.Unicode;
			Console.OutputEncoding = Encoding.UTF8;

			ConsoleHelper.WriteInfoMessage("Телефон включен.");
			ConsoleHelper.WriteInfoMessage("Чтобы начать играть в шахматы, нажмите \"Enter\".");
			ConsoleHelper.WriteInfoMessage("Чтобы послушать музыку, нажмите \"Space\".");
			ConsoleHelper.WriteInfoMessage("Чтобы сменить режим работы, нажмите \"Tab\".");

			var smartphone = new Smartphone();
			var otherKeysPressedCount = 0;

			while (true)
			{
				var keyInfo = Console.ReadKey();
				switch (keyInfo.Key)
				{
					case ConsoleKey.Enter:
						if (smartphone.IsPlayingGame())
						{
							smartphone.StopPlayingGame();
						}
						else
						{
							smartphone.StartPlayingGame();
						}
						break;

					case ConsoleKey.Spacebar:
						if (smartphone.IsPlayingMusic())
						{
							smartphone.StopListeningMusic();
						}
						else
						{
							smartphone.StartListeningMusic();
						}
						break;

					case ConsoleKey.Tab:
						if (smartphone.Mode.GetType() == typeof(EnergySavingMode))
						{
							smartphone.ChangeMode(new DefaultMode(smartphone));
						}
						else
						{
							smartphone.ChangeMode(new EnergySavingMode(smartphone));
						}
						break;

					default:
						otherKeysPressedCount++;
						break;
				}

				if (otherKeysPressedCount > SymbolsToFloodCount)
				{
					otherKeysPressedCount = 0;

					Console.WriteLine();
				}
			}
		}
	}
}