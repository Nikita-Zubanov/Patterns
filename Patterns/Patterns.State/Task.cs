using System;
using Patterns.State.Employees;
using Patterns.State.States;

namespace Patterns.State
{
	/// <summary>
	/// Задача.
	/// </summary>
	public class Task
	{
		/// <summary>
		/// Текущая стадия.
		/// </summary>
		public ITaskState State { get; set; }

		/// <summary>
		/// Разработчик.
		/// </summary>
		public Developer Developer { get; }

		/// <summary>
		/// Другой разработчик, проверяющий доработки разработчика.
		/// </summary>
		public Developer Reviewer { get; }

		/// <summary>
		/// Аналитик.
		/// </summary>
		public Analyst Analyst { get; }

		/// <summary>
		/// Инициализирует поля объекта.
		/// </summary>
		/// <param name="developer">Разработчик.</param>
		/// <param name="reviewer">Другой разработчик, проверяющий доработки разработчика.</param>
		/// <param name="analyst">Аналитик.</param>
		public Task(Developer developer, Developer reviewer, Analyst analyst)
		{
			if (developer == null)
			{
				throw new ArgumentNullException(nameof(developer));
			}

			if (reviewer == null)
			{
				throw new ArgumentNullException(nameof(reviewer));
			}

			if (analyst == null)
			{
				throw new ArgumentNullException(nameof(analyst));
			}

			Developer = developer;
			Reviewer = reviewer;
			Analyst = analyst;
			State = new Working(this);
		}

		/// <summary>
		/// Выполнить задачу.
		/// </summary>
		public void DoWork()
		{
			State.DoWork();
		}
	}
}