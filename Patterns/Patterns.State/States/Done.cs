using System;

namespace Patterns.State.States
{
	/// <summary>
	/// Состояние "Выполнено".
	/// </summary>
	public class Done : ITaskState
	{
		/// <summary>
		/// Задача.
		/// </summary>
		public Task Task { get; }

		/// <summary>
		/// Инициализирует поля объекта.
		/// </summary>
		/// <param name="task">Задача.</param>
		public Done(Task task)
		{
			if (task == null)
			{
				throw new ArgumentNullException(nameof(task));
			}

			Task = task;
		}

		/// <summary>
		/// Ничего не делать, т.к. задача выполнена.
		/// </summary>
		public void DoWork()
		{ }
	}
}