using Patterns.Observer.Entities;
using Patterns.Observer.Publishers;
using Patterns.Observer.Subscribers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Patterns.Observer
{
    /// <summary>
    /// Демонстрирует паттерн 'Наблюдатель'.
    /// Почтовое отделение принимает посылки и извещает об этом адресатов.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Создает адресатов и подписывает их на почтовое отделение; 
        /// создает посылки и передает их почте. Далее отписывает 
        /// адресата и опять передает почте посылки (для наглядности).
        /// </summary>
        static void Main()
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            var oleg = new Addressee("Oleg");
            var vasia = new Addressee("Vasia");

            var postOffice = new PostOffice();
            var p = new Program();

            postOffice.Subscribe(oleg, p.PrintToConsole);
            postOffice.Subscribe(vasia, p.PrintToConsole);

            var parcels = new List<Parcel>
            {
                new Parcel("Oleg", new Exception()),
                new Parcel("Vasia", "[данные удалены]"),
            };

            Console.WriteLine("\n[почта высылает уведомления]");
            postOffice.AcceptParcels(parcels);

            Console.WriteLine();
            postOffice.Unsubscribe(vasia, p.PrintToConsole);

            Console.WriteLine("\n[почта опять высылает уведомления]");
            postOffice.AcceptParcels(parcels);

            Console.ReadLine();
        }

        /// <summary>
        /// Выводит в консоль цветное сообщение.
        /// </summary>
        /// <param name="message"> Текст сообщения. </param>
        public void PrintToConsole(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(
                    message: "Сообщение не должно быть пустым или null.",
                    paramName: nameof(message));
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
