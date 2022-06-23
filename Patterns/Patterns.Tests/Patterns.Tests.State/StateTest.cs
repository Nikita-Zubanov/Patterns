using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patterns.State;
using Patterns.State.Employees;
using Patterns.State.States;

namespace Patterns.Tests.Patterns.State
{
	/// <summary>
	/// Тестирование перехода из одного состояния в другое объекта "Задача" (по аналогии с нашей методологие разработки).
	/// </summary>
	[TestClass]
	public class StateTest
	{
		/// <summary>
		/// Разработчик.
		/// Ожидается, что шанс выполнения задачи равен 100%.
		/// </summary>
		private Developer _developer;

		/// <summary>
		/// Ревьюер.
		/// Ожидается, что шанс выполнения задачи равен 100%.
		/// </summary>
		private Developer _reviewer;

		/// <summary>
		/// Аналитик.
		/// Ожидается, что шанс выполнения задачи равен 100%.
		/// </summary>
		private Analyst _analyst;

		/// <summary>
		/// Инициализирует поля объекта.
		/// </summary>
		public StateTest()
		{
			// Чтобы гарантировать, что разраб. выполнил задачу, установим шанс выполнения 100%.
			_developer = new Developer("Никита", "Зубанов", successWorkingChanceInPercent: 100);
			// Чтобы гарантировать, что ревью проёдет без замечаний, установим шанс выполнения 100%.
			_reviewer = new Developer("Василий", "Петров", successWorkingChanceInPercent: 100);
			// Чтобы гарантировать, что тестирование проёдет успешно, установим шанс выполнения 100%.
			_analyst = new Analyst("Андрей", "Андреев", successWorkingChanceInPercent: 100);
		}

		/// <summary>
		/// Проверяет, что стадия работы сменяется с "Разработка" на "Тестирование".
		/// </summary>
		[TestMethod]
		public void FromWorkingToTesting()
		{
			var task = new Task(_developer, _reviewer, _analyst);

			task.DoWork();

			Assert.AreEqual(task.State.GetType(), typeof(Testing));
		}

		/// <summary>
		/// Проверяет, что стадия работы сменяется с "Тестирование" на "Ревью кода".
		/// </summary>
		[TestMethod]
		public void FromTestingToReviewing()
		{
			var task = new Task(_developer, _reviewer, _analyst);
			task.State = new Testing(task);

			task.DoWork();

			Assert.AreEqual(task.State.GetType(), typeof(Reviewing));
		}

		/// <summary>
		/// Проверяет, что стадия работы сменяется с "Ревью кода" на "Выполнена".
		/// </summary>
		[TestMethod]
		public void FromReviewingToDone()
		{
			var task = new Task(_developer, _reviewer, _analyst);
			task.State = new Reviewing(task);

			task.DoWork();

			Assert.AreEqual(task.State.GetType(), typeof(Done));
		}

		/// <summary>
		/// Проверяет, что задача выполняется (разработка→тестирование→ревью кода→выполнено).
		/// </summary>
		[TestMethod]
		public void TaskPassed()
		{
			var task = new Task(_developer, _reviewer, _analyst);

			task.DoWork();  // Разработка	→ тестирование.
			Assert.AreEqual(task.State.GetType(), typeof(Testing));

			task.DoWork();  // Тестирование	→ ревью кода.
			Assert.AreEqual(task.State.GetType(), typeof(Reviewing));

			task.DoWork();  // Ревью кода	→ выполнено.
			Assert.AreEqual(task.State.GetType(), typeof(Done));
		}

		/// <summary>
		/// Проверяет, что со стадии "Тестирование" задача может перейти в стадию "Разработка".
		/// </summary>
		[TestMethod]
		public void TaskDidNotPassTesting()
		{
			// Чтобы гарантировать, что задача не пройдет тестирование, установим шанс выполнения 0%.
			var analyst = new Analyst("Вова", "Сидоров", successWorkingChanceInPercent: 0);
			var task = new Task(_developer, _reviewer, analyst);
			task.State = new Testing(task);

			task.DoWork();  // Тестирование	→ разработка.

			Assert.AreEqual(task.State.GetType(), typeof(Working));
		}

		/// <summary>
		/// Проверяет, что со стадии "Ревью кода" задача может перейти в стадию "Разработка".
		/// </summary>
		[TestMethod]
		public void TaskDidNotPassCodeReview()
		{
			// Чтобы гарантировать, что задача не пройдет ревью, установим шанс выполнения 0%.
			var reviewer = new Developer("Петя", "Зыков Роман Максимович", successWorkingChanceInPercent: 0);
			var task = new Task(_developer, reviewer, _analyst);
			task.State = new Reviewing(task);

			task.DoWork();  // Ревью кода	→ разработка.

			Assert.AreEqual(task.State.GetType(), typeof(Working));
		}
	}
}