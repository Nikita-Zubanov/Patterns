namespace Patterns.Strategy
{
	/// <summary>
	/// Уровень мощности работы процессора смартфона.
	/// </summary>
	public enum ProcessorPowerLevel
	{
		/// <summary>
		/// Не работает.
		/// </summary>
		None = 0,

		/// <summary>
		/// Экономичный.
		/// </summary>
		Economic = 1,

		/// <summary>
		/// Обычный.
		/// </summary>
		Default = 3
	}
}