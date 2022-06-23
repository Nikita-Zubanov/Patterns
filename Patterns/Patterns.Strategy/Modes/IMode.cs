namespace Patterns.Strategy.Modes
{
	/// <summary>
	/// Режим работы смартфона.
	/// </summary>
	public interface IMode
	{
		/// <summary>
		/// Запускает игру.
		/// </summary>
		void StartPlayingGame();

		/// <summary>
		/// Запускает музыку.
		/// </summary>
		void StartListeningMusic();

		/// <summary>
		/// Останавливает игру.
		/// </summary>
		void StopPlayingGame();

		/// <summary>
		/// Останавливает музыку.
		/// </summary>
		void StopListeningMusic();
	}
}