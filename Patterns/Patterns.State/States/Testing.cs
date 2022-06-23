using System;

namespace Patterns.State.States
{
	/// <summary>
	/// Состояние "Тестирование".
	/// </summary>
	public class Testing : ITaskState
	{
		/// <summary>
		/// Задача.
		/// </summary>
		public Task Task { get; }

		/// <summary>
		/// Инициализирует поля объекта.
		/// </summary>
		/// <param name="task">Задача.</param>
		public Testing(Task task)
		{
			if (task == null)
			{
				throw new ArgumentNullException(nameof(task));
			}

			Task = task;
		}

		/// <summary>
		/// Протестировать доработки.
		/// </summary>
		public void DoWork()
		{
			var isSuccess = Task.Analyst.DoWork();

			if (isSuccess)
			{
				Task.State = new Reviewing(Task);
			}
			else
			{
				Task.State = new Working(Task);
			}
		}
	}
}