namespace Patterns.State.States
{
	/// <summary>
	/// Предоставляет методы взаимодействия со стадией задачи.
	/// </summary>
	public interface ITaskState
	{
		/// <summary>
		/// Выполнить задачу.
		/// </summary>
		void DoWork();
	}
}