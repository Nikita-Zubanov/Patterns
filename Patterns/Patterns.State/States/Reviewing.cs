using System;

namespace Patterns.State.States
{
	/// <summary>
	/// Состояние "В ревью".
	/// </summary>
	public class Reviewing : ITaskState
	{
		/// <summary>
		/// Задача.
		/// </summary>
		public Task Task { get; }

		/// <summary>
		/// Инициализирует поля объекта.
		/// </summary>
		/// <param name="task">Задача.</param>
		public Reviewing(Task task)
		{
			if (task == null)
			{
				throw new ArgumentNullException(nameof(task));
			}

			Task = task;
		}

		/// <summary>
		/// Проверить доработки, внесённые в код.
		/// </summary>
		public void DoWork()
		{
			var isSuccess = Task.Reviewer.DoWork();

			if (isSuccess)
			{
				Task.State = new Done(Task);
			}
			else
			{
				Task.State = new Working(Task);
			}
		}
	}
}