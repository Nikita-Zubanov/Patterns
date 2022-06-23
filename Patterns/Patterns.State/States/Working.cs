using System;

namespace Patterns.State.States
{
	/// <summary>
	/// Состояние "В работе".
	/// </summary>
	public class Working : ITaskState
	{
		/// <summary>
		/// Задача.
		/// </summary>
		public Task Task { get; }

		/// <summary>
		/// Инициализирует поля объекта.
		/// </summary>
		/// <param name="task">Задача.</param>
		public Working(Task task)
		{
			if (task == null)
			{
				throw new ArgumentNullException(nameof(task));
			}

			Task = task;
		}

		/// <summary>
		/// Выполнить доработки, описанные в задаче.
		/// </summary>
		public void DoWork()
		{
			var isSuccess = Task.Developer.DoWork();

			if (isSuccess)
			{
				Task.State = new Testing(Task);
			}
			else
			{
				throw new Exception(
					$"Стоит подумать над тем, чтобы пересчитать ЗП разработчику \"{Task.Developer}\" (не в его пользу).");
			}
		}
	}
}