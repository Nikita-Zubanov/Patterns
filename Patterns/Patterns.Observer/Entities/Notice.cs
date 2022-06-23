using System;

namespace Patterns.Observer.Entities
{
    /// <summary>
    /// Извещение о наличии посылки для адресата.
    /// </summary>
    public class Notice
    {
        /// <summary>
        /// Имя адресата, идентифицирующее его.
        /// </summary>
        private string _addresseeName;

        /// <summary>
        /// Сообщение для адресата.
        /// </summary>
        private string _message;

        /// <summary>
        /// Имя адресата, идентифицирующее его.
        /// Валидация: вх. значение не null и не пустое.
        /// </summary>
        public string AddresseeName
        {
            get => _addresseeName;
            private set => _addresseeName = string.IsNullOrWhiteSpace(value)
                 ? throw new ArgumentNullException(
                        message: "Имя адресата не должно быть пустым или null.",
                        paramName: nameof(AddresseeName))
                 : value;
        }

        /// <summary>
        /// Сообщение для адресата.
        /// Валидация: вх. значение не null и не пустое.
        /// </summary>
        public string Message
        {
            get => _message;
            private set => _message = string.IsNullOrWhiteSpace(value)
                    ? throw new ArgumentNullException(
                        message: "Сообщение извещения не должно быть пустым или null.",
                        paramName: nameof(Message))
                    : value;
        }

        /// <summary>
        /// Инициализирует имя адресата и сообщение для него.
        /// </summary>
        /// <param name="addresseeName"> Имя адресата. </param>
        /// <param name="message"> Сообщение для адресата. </param>
        public Notice(string addresseeName, string message)
        {
            AddresseeName = addresseeName;
            Message = message;
        }
    }
}
