using System;
using System.Threading;

namespace Patterns.State.Employees
{
	/// <summary>
	/// Разработчик.
	/// </summary>
	public class Developer : Employee
	{
		#region Constants

		/// <summary>
		/// Время выполнения работы в миллисекундах по умолчанию.
		/// </summary>
		private const int WorkTimeInMillisecondsByDefault = 1000;

		/// <summary>
		/// Шанс невыполнения работы в процентах по умолчанию.
		/// </summary>
		private const int SuccessWorkingChanceInPercentByDefault = 97;

		#endregion

		/// <summary>
		/// Инициализирует поля объекта.
		/// </summary>
		/// <param name="firstName">Имя.</param>
		/// <param name="lastName">Фамилия.</param>
		public Developer(string firstName, string lastName)
			: base(firstName, lastName, WorkTimeInMillisecondsByDefault, SuccessWorkingChanceInPercentByDefault)
		{ }

		/// <summary>
		/// Инициализирует поля объекта.
		/// </summary>
		/// <param name="firstName">Имя.</param>
		/// <param name="lastName">Фамилия.</param>
		/// <param name="workTimeInMilliseconds">Время выполнения работы в миллисекундах.</param>
		/// <param name="successWorkingChanceInPercent">Шанс успешного выполнения работы в процентах.</param>
		public Developer(
			string firstName,
			string lastName,
			int workTimeInMilliseconds = default,
			int successWorkingChanceInPercent = default)
			: base(firstName, lastName, workTimeInMilliseconds, successWorkingChanceInPercent)
		{ }

		/// <summary>
		/// Разработать или доработать что-то.
		/// </summary>
		/// <returns>True, если задача выполнена.</returns>
		public override bool DoWork()
		{
			// Имитация работы (как в жизни).
			Thread.Sleep(WorkTimeInMilliseconds);

			var randomNumber = new Random().Next(0, 100);
			// Есть шанс, что задача очень сложная и разработчик её не выполнит.
			return randomNumber < SuccessWorkingChanceInPercent;
		}
	}
}