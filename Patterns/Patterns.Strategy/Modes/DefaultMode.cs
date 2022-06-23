using System;
using System.Media;
using System.Threading;
using System.Threading.Tasks;

namespace Patterns.Strategy.Modes
{
	/// <summary>
	/// Обычный режим смартфона.
	/// </summary>
	public class DefaultMode : IMode
	{
		/// <summary>
		/// Плеер для воспроизведения музыки.
		/// </summary>
		private SoundPlayer _player;

		/// <summary>
		/// Смартфон.
		/// </summary>
		public Smartphone Smartphone { get; set; }

		/// <summary>
		/// Инициализирует поля объекта.
		/// </summary>
		/// <param name="smartphone">Смартфон.</param>
		public DefaultMode(Smartphone smartphone)
		{
			if (smartphone == null)
			{
				throw new ArgumentNullException(nameof(smartphone));
			}

			smartphone.ProcessorPowerLevel = ProcessorPowerLevel.Default;
			Smartphone = smartphone;
		}

		/// <summary>
		/// Запускает игру.
		/// </summary>
		public void StartPlayingGame()
		{
			if (!Smartphone.IsPlayingGame())
			{
				Smartphone.GameCancellationTokenSource = new CancellationTokenSource();

				Smartphone.GameTask = Task.Factory.StartNew(
					() =>
					{
						var rnd = new Random();
						var figures = new[] { "Король", "Ферзь", "Ладья", "Слон", "Конь", "Пешка" };
						var horizontalChars = new[] { "A", "B", "C", "D", "E", "F", "G", "H" };

						while (!Smartphone.GameCancellationTokenSource.IsCancellationRequested)
						{
							var figure = figures[rnd.Next(0, figures.Length)];
							var horizontalChar = horizontalChars[rnd.Next(0, horizontalChars.Length)];
							var verticalNumber = rnd.Next(0, horizontalChars.Length);

							Console.WriteLine($"{figure} – {horizontalChar}:{verticalNumber}");

							Thread.Sleep(Smartphone.GetInterval());
						}
					},
					Smartphone.GameCancellationTokenSource.Token);
			}
		}

		/// <summary>
		/// Запускает музыку.
		/// </summary>
		public void StartListeningMusic()
		{
			if (!Smartphone.IsPlayingMusic())
			{
				Smartphone.MusicCancellationTokenSource = new CancellationTokenSource();

				Smartphone.MusicTask = Task.Factory.StartNew(
					() =>
					{
						_player = new SoundPlayer(@"../../Files/Татьяна Буланова - Ясный Мой Свет.wav");
						_player.Play();
					},
					Smartphone.MusicCancellationTokenSource.Token);
			}
		}

		/// <summary>
		/// Останавливает игру.
		/// </summary>
		public void StopPlayingGame()
		{
			if (!Smartphone.IsPlayingGame())
			{
				return;
			}

			Smartphone.GameCancellationTokenSource.Cancel();
			Smartphone.GameTask.Wait();
			Smartphone.GameTask.Dispose();

			Smartphone.GameTask = null;
		}

		/// <summary>
		/// Останавливает музыку.
		/// </summary>
		public void StopListeningMusic()
		{
			if (!Smartphone.IsPlayingMusic())
			{
				return;
			}

			_player.Stop();
			_player.Dispose();

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
			return "Обычный режим";
		}
	}
}