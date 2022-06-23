using System;
using System.Threading;

namespace Patterns.State.Employees
{
	/// <summary>
	/// Аналитик.
	/// </summary>
	public class Analyst : Employee
	{
		#region Constants

		/// <summary>
		/// Время выполнения работы в миллисекундах.
		/// </summary>
		private const int WorkTimeInMillisecondsByDefault = 500;

		/// <summary>
		/// Шанс успешного тестирования в процентах по умолчанию.
		/// </summary>
		private const int SuccessWorkingChanceInPercentByDefault = 80;

		#endregion
		
		/// <summary>
		/// Инициализирует поля объекта.
		/// </summary>
		/// <param name="firstName">Имя.</param>
		/// <param name="lastName">Фамилия.</param>
		public Analyst(string firstName, string lastName)
			: base(firstName, lastName, WorkTimeInMillisecondsByDefault, SuccessWorkingChanceInPercentByDefault)
		{ }

		/// <summary>
		/// Инициализирует поля объекта.
		/// </summary>
		/// <param name="firstName">Имя.</param>
		/// <param name="lastName">Фамилия.</param>
		/// <param name="workTimeInMilliseconds">Время выполнения работы в миллисекундах.</param>
		/// <param name="successWorkingChanceInPercent">Шанс успешного выполнения работы в процентах.</param>
		public Analyst(
			string firstName,
			string lastName,
			int workTimeInMilliseconds = default,
			int successWorkingChanceInPercent = default)
			: base(firstName, lastName, workTimeInMilliseconds, successWorkingChanceInPercent)
		{ }

		/// <summary>
		/// Выполнить аналитическую работу.
		/// </summary>
		/// <returns>True, если задача выполнена.</returns>
		public override bool DoWork()
		{
			// Имитация работы.
			Thread.Sleep(WorkTimeInMilliseconds);

			var randomNumber = new Random().Next(0, 100);
			return randomNumber < SuccessWorkingChanceInPercent;
		}
	}
}