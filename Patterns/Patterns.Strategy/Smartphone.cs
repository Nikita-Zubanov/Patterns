using Patterns.Strategy.Modes;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Patterns.Strategy
{
	/// <summary>
	/// Смартфон.
	/// </summary>
	public class Smartphone : IDisposable
	{
		#region Constants

		/// <summary>
		/// Полный уровень заряда батареи.
		/// </summary>
		public const int FullBattery = 100;

		/// <summary>
		/// Высокий уровень заряда батареи.
		/// </summary>
		public const int HightBattery = 85;

		/// <summary>
		/// Средний уровень заряда батареи.
		/// </summary>
		public const int MiddleBattery = 60;

		/// <summary>
		/// Низкий уровень заряда батареи.
		/// </summary>
		public const int LowBattery = 35;

		/// <summary>
		/// Уровень полной разрядки батареи.
		/// </summary>
		public const int EmptyBattery = default;

		/// <summary>
		/// Исходный интервал разрядки батареи.
		/// На него также влияет мощность процессора смартфона.
		/// </summary>
		private const int OriginalInterval = 1000;

		#endregion

		#region Fields

		/// <summary>
		/// Синхронизирует потоки для работы с объектами, связанными с игровой частью смартфона.
		/// </summary>
		private object _gameLocker = new object();

		/// <summary>
		/// Синхронизирует потоки для работы с объектами, связанными с музыкальной частью смартфона.
		/// </summary>
		private object _musicLocker = new object();

		/// <summary>
		/// Блокировщик потоков для освобождения ресурсов.
		/// </summary>
		private object _disposingLock = new object();

		/// <summary>
		/// Таймер, позволяющий: симмулировать разрядку батареи, контролировать используемый режим.
		/// </summary>
		private Timer _batteryTimer;

		/// <summary>
		/// Уровень заряда батареи.
		/// </summary>
		private double _batteryLevel;

		/// <summary>
		/// Признак, указывающий, что ресурсы объекта были освобождены.
		/// </summary>
		private bool _isDisposed;

		#endregion

		#region Properties

		/// <summary>
		/// Выпполняемая параллельно задача, симмулирующая игру в смартфон.
		/// </summary>
		public Task GameTask { get; set; }

		/// <summary>
		/// Выпполняемая параллельно задача для воспроизведения музыки.
		/// </summary>
		public Task MusicTask { get; set; }

		/// <summary>
		/// Токен для возможности преждевременного завешения задачи для игры.
		/// </summary>
		public CancellationTokenSource GameCancellationTokenSource { get; set; }

		/// <summary>
		/// Токен для возможности преждевременного завешения задачи для воспроизведения музыки.
		/// </summary>
		public CancellationTokenSource MusicCancellationTokenSource { get; set; }

		/// <summary>
		/// Уровень мощности работы процессора.
		/// </summary>
		public ProcessorPowerLevel ProcessorPowerLevel { get; set; }

		/// <summary>
		/// Уровень заряда батареи.
		/// </summary>
		public double BatteryLevel {
			get
			{
				return _batteryLevel;
			}
			private set
			{
				if (value < 0 || value > 100)
				{
					throw new Exception("Уровень заряда батареи не может быть меньше 0 или больше 100.");
				}

				_batteryLevel = value;
			}
		}

		/// <summary>
		/// Режим работы.
		/// </summary>
		public IMode Mode { get; private set; }

		#endregion

		/// <summary>
		/// Инициализирует поля объекта.
		/// </summary>
		public Smartphone()
		{
			BatteryLevel = FullBattery;
			Mode = new DefaultMode(this);

			_batteryTimer = new Timer(OnBatteryTick, null, 0, GetInterval());
		}

		#region Methods: Public

		/// <summary>
		/// Освобождает все занимаемые объектом ресурсы.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Запускает игру.
		/// </summary>
		public void StartPlayingGame()
		{
			Mode.StartPlayingGame();
		}

		/// <summary>
		/// Запускает музыку.
		/// </summary>
		public void StartListeningMusic()
		{
			Mode.StartListeningMusic();
		}

		/// <summary>
		/// Останавливает игру.
		/// </summary>
		public void StopPlayingGame()
		{
			lock (_gameLocker)
			{
				Mode.StopPlayingGame();
			}
		}

		/// <summary>
		/// Останавливает музыку.
		/// </summary>
		public void StopListeningMusic()
		{
			lock (_musicLocker)
			{
				Mode.StopListeningMusic();
			}
		}

		/// <summary>
		/// возвращает период изменения уровня заряда батареи.
		/// </summary>
		/// <returns></returns>
		public int GetInterval()
		{
			if (ProcessorPowerLevel == ProcessorPowerLevel.None)
			{
				TurnOff();
			}

			return OriginalInterval / (int)ProcessorPowerLevel;
		}

		/// <summary>
		/// Возвращает признак, указывающий, что в текущий момент запущена игра.
		/// </summary>
		/// <returns>True, если игра запущена.</returns>
		public bool IsPlayingGame()
		{
			return GameTask != null;
		}

		/// <summary>
		/// Возвращает признак, указывающий, что в текущий момент играет музыка.
		/// </summary>
		/// <returns>True, если музыка играет.</returns>
		public bool IsPlayingMusic()
		{
			return MusicTask != null;
		}

		/// <summary>
		/// Сменяет режим работы смартфона.
		/// </summary>
		/// <param name="mode"></param>
		public void ChangeMode(IMode mode)
		{
			if (mode == null)
			{
				throw new ArgumentNullException(nameof(mode));
			}

			Mode = mode;

			ConsoleHelper.WriteMessage($"***Включен \"{mode}\"***", ConsoleColor.Blue);
		}

		#endregion

		#region Methods: Protected

		/// <summary>
		/// Освобождает все занимаемые объектом ресурсы.
		/// </summary>
		/// <param name="disposing">Признак, указывающий, что нужно освободить управляемые ресурсы.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (_isDisposed)
			{
				return;
			}

			lock(_disposingLock)
			{
				if (_isDisposed)
				{
					return;
				}

				if (disposing)
				{
					if (_batteryTimer != null)
					{
						_batteryTimer.Dispose();
						_batteryTimer = null;
					}

					if (GameTask != null)
					{
						GameCancellationTokenSource.Cancel();
						GameTask.Wait();
						GameTask.Dispose();
						GameTask = null;
					}

					if (MusicTask != null)
					{
						MusicCancellationTokenSource.Cancel();
						MusicTask.Wait();
						MusicTask.Dispose();
						MusicTask = null;
					}

					if (GameCancellationTokenSource != null)
					{
						GameCancellationTokenSource.Dispose();
						GameCancellationTokenSource = null;
					}

					if (MusicCancellationTokenSource != null)
					{
						MusicCancellationTokenSource.Dispose();
						MusicCancellationTokenSource = null;
					}

					Mode = null;
					_gameLocker = null;
					_musicLocker = null;
					_disposingLock = null;
				}

				_isDisposed = true;
			}
		}

		#endregion

		#region Methods: Private

		/// <summary>
		/// Обрабатывает работу смартфона.
		/// </summary>
		/// <param name="state">Состояние смартфона.</param>
		private void OnBatteryTick(object state)
		{
			ManageMode();
			ManageBattery();
		}

		/// <summary>
		/// Управляет уровнем заряда батареи в соответствии с состоянием.
		/// </summary>
		/// <param name="obj">Состояние объекта.</param>
		private void ManageBattery()
		{
			var multiplyByGame = IsPlayingGame()
				? 2
				: 0.01;
			var multiplyByMusic = IsPlayingMusic()
				? 0.5
				: 0.01;
			var nextBatteryLevel = BatteryLevel - 1000 / GetInterval() * (multiplyByGame + multiplyByMusic);
			if (nextBatteryLevel <= 0)
			{
				TurnOff();
				return;
			}

			BatteryLevel = nextBatteryLevel;

			var color = ConsoleColor.White;
			if (BatteryLevel >= HightBattery)
			{
				color = ConsoleColor.Green;
			}
			else if (BatteryLevel >= MiddleBattery && BatteryLevel < HightBattery)
			{
				color = ConsoleColor.Yellow;
			}
			else if (BatteryLevel >= LowBattery && BatteryLevel < MiddleBattery)
			{
				color = ConsoleColor.DarkYellow;
			}
			else if (BatteryLevel < LowBattery)
			{
				color = ConsoleColor.Red;
			}

			ConsoleHelper.WriteMessageInCenter($"Заряд батареи: {Math.Round(BatteryLevel, 2)}%", color);
		}

		/// <summary>
		/// Управляет режимом работы телефона.
		/// </summary>
		private void ManageMode()
		{
			if (BatteryLevel <= LowBattery && !(Mode is EnergySavingMode))
			{
				StopPlayingGame();
				StopListeningMusic();

				ChangeMode(new EnergySavingMode(this));
			}
		}

		/// <summary>
		/// Выключает телефон.
		/// </summary>
		private void TurnOff()
		{
			ConsoleHelper.WriteMessage("***Телефон выключен***", ConsoleColor.DarkRed);
			ConsoleHelper.WriteMessageInCenter("Батарея разряжена", ConsoleColor.DarkRed);

			Dispose();

			ProcessorPowerLevel = ProcessorPowerLevel.None;
			BatteryLevel = EmptyBattery;
		}

		#endregion
	}
}