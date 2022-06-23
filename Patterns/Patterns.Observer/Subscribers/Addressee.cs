using System;

namespace Patterns.Observer.Subscribers
{
    /// <summary>
    /// Адресат (получатель посылок).
    /// </summary>
    public class Addressee
    {
        /// <summary>
        /// Имя, идентифицирующее адресата.
        /// </summary>
        private string _name;

        /// <summary>
        /// Проверяет, чтобы присваиваемое значение не было пустым.
        /// </summary>
        public string Name
        {
            get => _name;
            private set => _name = string.IsNullOrWhiteSpace(value)
                    ? throw new ArgumentNullException(
                        message: "Имя адресата не должно быть пустым или null.",
                        paramName: nameof(Name))
                    : value;
        }

        /// <summary>
        /// Инициализирует имя адресата.
        /// </summary>
        /// <param name="name"> Имя адресата. </param>
        public Addressee(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Выводит в консоль переданное сообщение.
        /// </summary>
        /// <param name="message"> Ожидается сообщение непустое сообщение. </param>
        public void ReceiveNotice(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(
                    message: "Сообщение извещения не должно быть пустым или null.",
                    paramName: nameof(message));
            }

            Console.WriteLine($"{Name} вскрыл уведомление и прочитал: '{message}'.");
        }
    }
}
