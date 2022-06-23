using System;
using System.Text;
using System.Threading;

namespace Patterns.Singleton
{
    /// <summary>
    /// Демонстрирует паттерн 'Одиночка'.
    /// Таймер для учета рабочего времени.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Вызывает методы создания (или получения) и вывода таймера рабочего дня, 
        /// после чего отчищает память для объекта-одиночки и повторяет методы.
        /// UnambiguousExpirationSecondPerMilliseconds необходима, т.к. итерация 
        /// таймера может не успеть сработать перед вызовом метода, что выведет 
        /// неактуальный результат.
        /// </summary>
        static void Main()
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            // Однозначное истечение секунды в миллисекундах.
            const int UnambiguousExpirationSecondPerMilliseconds = 1035;
            var p = new Program();

            p.CreateAndShowWorkingTimer();
            Thread.Sleep(UnambiguousExpirationSecondPerMilliseconds);

            p.CreateAndShowWorkingTimer();
            Thread.Sleep(UnambiguousExpirationSecondPerMilliseconds);

            p.CreateAndDisposeWorkingTimer();

            p.CreateAndShowWorkingTimer();
            Thread.Sleep(UnambiguousExpirationSecondPerMilliseconds);

            p.CreateAndShowWorkingTimer();
            Thread.Sleep(UnambiguousExpirationSecondPerMilliseconds);

            Console.ReadLine();
        }

        /// <summary>
        /// Создает (или получает) таймер рабочего дня и выводит его текущее значение.
        /// </summary>
        private void CreateAndShowWorkingTimer()
        {
            Console.WriteLine("\nСоздаем (или получаем) таймер рабочего дня.");

            var workingTimer = WorkingTimer.GetInstance();
            workingTimer.ShowTimer();
        }

        /// <summary>
        /// Создает (или получает) таймер рабочего дня и заканчивает рабочий день.
        /// </summary>
        private void CreateAndDisposeWorkingTimer()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nЗаканчиваем рабочий день.");
            Console.ForegroundColor = ConsoleColor.White;

            var workingTimer = WorkingTimer.GetInstance();
            workingTimer.FinishWorking();
        }
    }
}
