using System;

namespace Patterns.Observer.Entities
{
    /// <summary>
    /// Посылка.
    /// </summary>
    public class Parcel
    {
        /// <summary>
        /// Имя адресата, идентифицирующее его.
        /// </summary>
        private string _addresseeName;
        
        /// <summary>
        /// Содержимое посылки.
        /// </summary>
        private object _content;

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
        /// Содержимое посылки.
        /// Валидация: вх. значение не null.
        /// </summary>
        public object Сontent
        {
            get => _content;
            private set => _content = value ?? throw new ArgumentNullException(
                        message: "Содержимое посылки не должно быть null.",
                        paramName: nameof(AddresseeName));
        }

        /// <summary>
        /// Инициализирует имя адресата и содержимое посылки.
        /// </summary>
        /// <param name="addresseeName"> Имя адресата. </param>
        /// <param name="сontent"> Содержимое посылки. </param>
        public Parcel(string addresseeName, object сontent)
        {
            AddresseeName = addresseeName;
            Сontent = сontent;
        }
    }
}
