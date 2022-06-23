using System;
using System.Collections.Generic;
using System.Linq;
using Patterns.Mediator.Mediator;

namespace Patterns.Mediator.Components
{
    /// <summary>
    /// Чат.
    /// </summary>
    public class Chat
    {
        /// <summary>
        /// Посредник чатов.
        /// </summary>
        public IChatsMediator ChatsMediator { get; }

        /// <summary>
        /// Название чата.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Словарь пользователей и их сообщений в чате.
        /// </summary>
        public IDictionary<string, IList<string>> UserMessages { get; } = new Dictionary<string, IList<string>>();

        /// <summary>
        /// Инициализирует поля объекта.
        /// </summary>
        /// <param name="chatsMediator">  Посредник чатов. </param>
        /// <param name="title"> Название чата. </param>
        public Chat(IChatsMediator chatsMediator, string title)
        {
            ChatsMediator = chatsMediator ?? throw new ArgumentNullException(nameof(chatsMediator));

            VerifyTitle(title);
            Title = title;
        }

        /// <summary>
        /// Добавляет сообщение в чат.
        /// </summary>
        /// <param name="userLogin"> Логин пользователя. </param>
        /// <param name="message"> Текст сообщения. </param>
        public void ReceiveMessage(string userLogin, string message)
        {
            if (string.IsNullOrWhiteSpace(userLogin))
            {
                throw new ArgumentNullException(nameof(userLogin));
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            var messages = UserMessages.FirstOrDefault(um => um.Key == userLogin);

            if (string.IsNullOrWhiteSpace(messages.Key))
            {
                UserMessages.Add(userLogin, new List<string>{ message });

                return;
            }

            messages.Value.Add(message);
        }

        /// <summary>
        /// Проверяет название чата на полноту и уникальность.
        /// </summary>
        /// <param name="title"> Название чата. </param>
        private void VerifyTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentNullException(nameof(title));
            }

            if (ChatsMediator.IsUniqueChatTile(title))
            {
                throw new ArgumentException($"Чат с названием \"{title}\" уже существует.");
            }
        }
    }
}