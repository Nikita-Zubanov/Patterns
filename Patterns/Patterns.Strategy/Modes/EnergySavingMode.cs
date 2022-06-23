using System;
using System.Threading;
using System.Threading.Tasks;

namespace Patterns.Strategy.Modes
{
	/// <summary>
	/// Сберегающий режим смартфона.
	/// </summary>
	public class EnergySavingMode : IMode
	{
		/// <summary>
		/// Строковое обозначение камня для игры.
		/// </summary>
		private const string Rock = "камень";

		/// <summary>
		/// Строковое обозначение ножниц для игры.
		/// </summary>
		private const string Scissors = "ножницы";

		/// <summary>
		/// Строковое обозначение бумаги для игры.
		/// </summary>
		private const string Paper = "бумага";

		/// <summary>
		/// Смартфон.
		/// </summary>
		public Smartphone Smartphone { get; set; }

		/// <summary>
		/// Инициализирует поля объекта.
		/// </summary>
		/// <param name="smartphone">Смартфон.</param>
		public EnergySavingMode(Smartphone smartphone)
		{
			if (smartphone == null)
			{
				throw new ArgumentNullException(nameof(smartphone));
			}

			smartphone.ProcessorPowerLevel = ProcessorPowerLevel.Economic;
			Smartphone = smartphone;
		}

		/// <summary>
		/// Запускает экономную для батареи смартфона игру.
		/// </summary>
		public void StartPlayingGame()
		{
			var rockPaperScissors = new[] { Rock, Scissors, Paper };

			Console.WriteLine("Играем в камень, ножницы, бумага. Загадай и введи, что выбрал.");

			var rndNumber = new Random().Next(0, 3);

			var computerResult = rockPaperScissors[rndNumber];
			var humanResult = Console.ReadLine()?.ToLower();

			try
			{
				var compareResult = IsHumanWinInRockPaperScissors(humanResult, computerResult);

				if (compareResult == 1)
				{
					Console.WriteLine("Победил. Сейчас победную включу!");

					Smartphone.StartListeningMusic();
				}
				else if (compareResult == 0)
				{
					Console.WriteLine("Ничья, человек...");
				}
				else
				{
					Console.WriteLine("Ты проиграл. Зацени трек");
				}
			}
			catch(Exception ex)
			{
				ConsoleHelper.WriteErrorMessage(ex.Message);
			}
		}

		/// <summary>
		/// Запускает экономную для батареи смартфона музыку.
		/// </summary>
		public void StartListeningMusic()
		{
			if (!Smartphone.IsPlayingMusic())
			{
				Smartphone.MusicCancellationTokenSource = new CancellationTokenSource();

				Smartphone.MusicTask = Task.Factory.StartNew(
					() =>
					{
						while (!Smartphone.MusicCancellationTokenSource.IsCancellationRequested)
						{
							Console.Beep();

							var waitingTime = new Random().Next(1, 8) * Smartphone.GetInterval() / 5;
							Thread.Sleep(waitingTime);
						}
					},
					Smartphone.MusicCancellationTokenSource.Token);
			}
		}

		/// <summary>
		/// Останавливает игру.
		/// </summary>
		public void StopPlayingGame()
		{ }

		/// <summary>
		/// Останавливает музыку.
		/// </summary>
		public void StopListeningMusic()
		{
			if (!Smartphone.IsPlayingMusic())
			{
				return;
			}

			Smartphone.MusicCancellationTokenSource.Cancel();
			Smartphone.MusicTask.Wait();
			Smartphone.MusicTask.Dispose();

			Smartphone.MusicTask = null;
		}

		/// <summary>
		/// Возвращает строковое представление объекта.
		/// </summary>
		/// <returns>Локализованное название класса.</returns>
		public override string ToString()
		{
			return "Сберегающий режим";
		}

		/// <summary>
		/// Возвращает результат игры "Камень, ножницы, бумага".
		/// </summary>
		/// <param name="humanResult">Загаданное человеком.</param>
		/// <param name="computerResult">Случайно выбранное компьютером.</param>
		/// <returns>-1 – человек проиграл; 0 – ничья; 1 – человек выйграл.</returns>
		private int IsHumanWinInRockPaperScissors(string humanResult, string computerResult)
		{
			if (humanResult == computerResult)
			{
				return 0;
			}

			if (humanResult == Rock)
			{
				if (computerResult == Scissors)
				{
					return 1;
				}

				return -1;
			}

			if (humanResult == Scissors)
			{
				if (computerResult == Paper)
				{
					return 1;
				}

				return -1;
			}

			if (humanResult == Paper)
			{
				if (computerResult == Rock)
				{
					return 1;
				}

				return -1;
			}

			throw new Exception($"Введено некорректное значение: \"{humanResult}\".");
		}
	}
}