using System;

namespace Patterns.State.Employees
{
	/// <summary>
	/// Сотрудник.
	/// </summary>
	public abstract class Employee
	{
		/// <summary>
		/// Имя.
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		/// Фамилия.
		/// </summary>
		public string LastName { get; set; }

		/// <summary>
		/// Время выполнения работы в миллисекундах.
		/// </summary>
		public int WorkTimeInMilliseconds { get; set; }

		/// <summary>
		/// Шанс успешного выполнения работы в процентах.
		/// </summary>
		public int SuccessWorkingChanceInPercent { get; set; }

		/// <summary>
		/// Инициализирует поля объекта.
		/// </summary>
		/// <param name="firstName">Имя.</param>
		/// <param name="lastName">Фамилия.</param>
		/// <param name="workTimeInMilliseconds">Время выполнения работы в миллисекундах.</param>
		/// <param name="successWorkingChanceInPercent">Шанс успешного выполнения работы в процентах.</param>
		protected Employee(
			string firstName,
			string lastName,
			int workTimeInMilliseconds,
			int successWorkingChanceInPercent)
		{
			if (string.IsNullOrWhiteSpace(firstName))
			{
				throw new ArgumentNullException(nameof(firstName));
			}

			if (string.IsNullOrWhiteSpace(lastName))
			{
				throw new ArgumentNullException(nameof(lastName));
			}

			FirstName = firstName;
			LastName = lastName;
			WorkTimeInMilliseconds = workTimeInMilliseconds;
			SuccessWorkingChanceInPercent = successWorkingChanceInPercent;
		}

		/// <summary>
		/// Выполнить работу и вернуть результат.
		/// </summary>
		/// <returns>True, если задача выполнена.</returns>
		public abstract bool DoWork();

		/// <summary>
		/// Строковое представление сотрудника.
		/// </summary>
		/// <returns>Фамилия и имя.</returns>
		public override string ToString()
		{
			return $"{LastName} {FirstName}";
		}
	}
}