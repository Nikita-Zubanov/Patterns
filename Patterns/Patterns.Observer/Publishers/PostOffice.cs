using Patterns.Observer.Entities;
using Patterns.Observer.Subscribers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Patterns.Observer.Publishers
{
    /// <summary>
    /// Почтовое отделение.
    /// </summary>
    public class PostOffice
    {
        /// <summary>
        /// Адресаты.
        /// </summary>
        private readonly List<Addressee> _addressees;

        /// <summary>
        /// Посылки в отделении.
        /// </summary>
        private List<Parcel> _parcels;

        /// <summary>
        /// Инициализирует списки адресатов и посылок.
        /// </summary>
        public PostOffice()
        {
            _addressees = new List<Addressee>();
            _parcels = new List<Parcel>();
        }

        /// <summary>
        /// Подписывает нового адресата на извещения и уведомляет об этом.
        /// </summary>
        /// <param name="addressee"> Новый адресат. </param>
        /// <param name="reportCallback"> Метод для уведомления. </param>
        public void Subscribe(Addressee addressee, Action<string> reportCallback)
        {
            if (addressee == null)
            {
                throw new ArgumentNullException(
                    message: "Подписываемый адресат не должен быть null.",
                    paramName: nameof(addressee));
            }

            _addressees.Add(addressee);

            reportCallback?.Invoke($"{addressee.Name} прикрепился к почтовому отделению.");
        }

        /// <summary>
        /// Отписывает нового адресата от извещений и уведомляет об этом.
        /// </summary>
        /// <param name="addressee"> Отписываемый адресат. </param>
        /// <param name="reportCallback"> Метод для уведомления. </param>
        public void Unsubscribe(Addressee addressee, Action<string> reportCallback)
        {
            if (addressee == null)
            {
                throw new ArgumentNullException(
                    message: "Отписываемый адресат не должен быть null.",
                    paramName: nameof(addressee));
            }

            _addressees.Remove(addressee);

            reportCallback?.Invoke($"{addressee.Name} открепился от почтового отделения.");
        }

        /// <summary>
        /// Получение отделением новых посылок.
        /// </summary>
        /// <param name="parcels"> Пришедшие в отделение посылки. </param>
        public void AcceptParcels(IEnumerable<Parcel> parcels)
        {
            if (parcels == null)
            {
                throw new ArgumentNullException(
                    message: "Список посылок не должен быть null.",
                    paramName: nameof(parcels));
            }

            _parcels = parcels.ToList();

            var notices = _parcels
                .Where(p => _addressees.Any(a => a.Name == p.AddresseeName))
                .Select(p => new Notice(
                    p.AddresseeName,
                    $"{p.AddresseeName}, вам пришла посылка. Просьба забрать в течении 14 рабочих дней."));

            SendNotices(notices);
        }

        /// <summary>
        /// Отправляет извещения соответствующим по имени адресатам.
        /// </summary>
        /// <param name="notices"></param>
        private void SendNotices(IEnumerable<Notice> notices)
        {
            foreach(var notice in notices)
            {
                var addressee = _addressees.Find(a => a.Name == notice.AddresseeName);
                addressee.ReceiveNotice(notice.Message);
            }
        }
    }
}
