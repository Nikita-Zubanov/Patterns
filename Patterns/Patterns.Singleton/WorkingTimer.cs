using System;
using System.Threading;

namespace Patterns.Singleton
{
    /// <summary>
    /// Таймер рабочего дня.
    /// </summary>
    public class WorkingTimer
    {
        /// <summary>
        /// Кол-во миллисекунд в секунде.
        /// </summary>
        private const int MillisecondsPerSecond = 1000;

        /// <summary>
        /// Объект-одиночка текущего класса.
        /// </summary>
        private static WorkingTimer WorkingTimerInstance;

        /// <summary>
        /// Объект, блокирующий доступ к получению объекта-одиночки.
        /// </summary>
        private static readonly object InstanceLock = new object();

        /// <summary>
        /// Таймер, управляющий интервальным методом добавления 
        /// секунд к счетчику времени текущего объекта.
        /// </summary>
        private readonly Timer _timer;

        /// <summary>
        /// Счетчик времени с начала создания объекта.
        /// </summary>
        private DateTime _time;

        /// <summary>
        /// Инициализирует таймер и счетчик времени с начала создания объекта.
        /// </summary>
        private WorkingTimer()
        {
            _time = new DateTime();

            _timer = new Timer(AddSecondCallback, this, MillisecondsPerSecond, MillisecondsPerSecond);
        }

        /// <summary>
        /// Возвращает объект-одиночку, если он создан, иначе инициализирует его.
        /// </summary>
        /// <returns> Объект-одиночка. </returns>
        public static WorkingTimer GetInstance()
        {
            if (WorkingTimerInstance == null)
            {
                lock (InstanceLock)
                {
                    if (WorkingTimerInstance == null)
                    {
                        WorkingTimerInstance = new WorkingTimer();
                    }
                }
            }

            return WorkingTimerInstance;
        }

        /// <summary>
        /// Финализирует объект-одиночку.
        /// </summary>
        public void FinishWorking()
        {
            WorkingTimerInstance = null;

            _timer.Dispose();
        }

        /// <summary>
        /// Выводит часы, минуты и секунды с начала создания объекта.
        /// </summary>
        public void ShowTimer()
        {
            Console.WriteLine($"Прошло {_time:HH} часа(-ов), {_time:mm} минут(-ы) и {_time:ss} секунд(-ы) с начала рабочего дня...");
        }

        /// <summary>
        /// Добавляет секунду к счетчику времени с начала рабочего дня объекта (WorkingTimer).
        /// </summary>
        /// <param name="sender"> Ожидается экземпляр класса WorkingTimer. </param>
        private void AddSecondCallback(object sender)
        {
            if (sender is WorkingTimer)
            {
                var workingTimer = (WorkingTimer)sender;
                workingTimer._time = workingTimer._time.AddSeconds(1.0);
            }
        }
    }
}
