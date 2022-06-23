using System;
using Patterns.Mediator.Mediator;

namespace Patterns.Mediator.Components
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Посредник чатов.
        /// </summary>
        public IChatsMediator ChatsMediator { get; }

        /// <summary>
        /// Логин пользователя.
        /// </summary>
        public string Login { get; }

        /// <summary>
        /// Инициализирует поля объекта.
        /// </summary>
        /// <param name="chatsMediator"> Посредник чатов. </param>
        /// <param name="login"> Логин пользователя. </param>
        public User(IChatsMediator chatsMediator, string login)
        {
            ChatsMediator = chatsMediator ?? throw new ArgumentNullException(nameof(chatsMediator));

            VerifyLogin(login);
            Login = login;
        }

        /// <summary>
        /// Отправиль сообщение в чат.
        /// </summary>
        /// <param name="message"> Текст сообщения. </param>
        public void SendMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            ChatsMediator.SendMessage(this, message);
        }

        /// <summary>
        /// Проверяет логин пользователя на полноту и уникальность.
        /// </summary>
        /// <param name="login"> Название чата. </param>
        private void VerifyLogin(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                throw new ArgumentNullException(nameof(login));
            }

            if (ChatsMediator.IsUniqueLogin(login))
            {
                throw new ArgumentException($"Логин \"{login}\" уже занят.");
            }
        }
    }
}