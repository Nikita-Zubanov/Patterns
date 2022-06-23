using System;
using System.Collections.Generic;
using System.Linq;
using Patterns.Mediator.Components;

namespace Patterns.Mediator.Mediator
{
    /// <summary>
    /// Посредник чатов.
    /// </summary>
    public class ChatsMediator : IChatsMediator
    {
        /// <summary>
        /// Словарь, состоязий из чатов и пользователей, которые в них есть.
        /// </summary>
        private Dictionary<Chat, List<User>> _usersInChats { get; } = new Dictionary<Chat, List<User>>();

        private readonly IList<string> _log = new List<string>();

        /// <summary>
        /// Зарегистрировать чат для общения.
        /// </summary>
        /// <param name="chat"> Чат. </param>
        public void RegisterChat(Chat chat)
        {
            if (chat == null)
            {
                throw new ArgumentNullException(nameof(chat));
            }

            _usersInChats.Add(chat, new List<User>());
        }

        /// <summary>
        /// Добавить пользователя в чат.
        /// </summary>
        /// <param name="user"> Пользователь. </param>
        /// <param name="chatTitle"> Название чата. </param>
        public void AddUserToChat(User user, string chatTitle)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrWhiteSpace(chatTitle))
            {
                throw new ArgumentNullException(nameof(chatTitle));
            }

            var findedChat = _usersInChats.Keys.FirstOrDefault(chat => chat.Title == chatTitle);
            if (findedChat == null)
            {
                LogError($"Чат с названием {chatTitle} не найден.");
            }
            else
            {
                _usersInChats[findedChat].Add(user);
            }
        }

        /// <summary>
        /// Отправить сообщение в чат.
        /// </summary>
        /// <param name="sender"> Пользователь, отправивший сообщение. </param>
        /// <param name="message"> Текст сообщения. </param>
        public void SendMessage(User sender, string message)
        {
            if (sender == null)
            {
                throw new ArgumentNullException(nameof(sender));
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            var findedChat = _usersInChats
                .FirstOrDefault(usersInChats => usersInChats.Value.Contains(sender))
                .Key;
            if (findedChat == null)
            {
                LogError($"Пользователь {sender.Login} не состоит ни в одном чате.");
            }

            try
            {
                findedChat?.ReceiveMessage(sender.Login, message);
            }
            catch (Exception e)
            {
                LogError($"При добавлении сообщении в чат возникла ошибка: \"{e.Message}\". \n{e.StackTrace}");
                throw;
            }
            
        }

        /// <summary>
        /// Проверить логин на уникальность во всех чатах.
        /// </summary>
        /// <param name="login"> Логин пользователя. </param>
        /// <returns> Признак, указывающий уникален ли логин. </returns>
        public bool IsUniqueLogin(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                throw new ArgumentNullException(nameof(login));
            }

            var allLogins = _usersInChats.Values
                .SelectMany(users => users.Select(u => u.Login))
                .ToList();
            return allLogins.Contains(login);
        }

        /// <summary>
        /// Проверить уникальность названия чата.
        /// </summary>
        /// <param name="title"> Название чата. </param>
        /// <returns> Признак, указывающий уникально ли название чата. </returns>
        public bool IsUniqueChatTile(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentNullException(nameof(title));
            }

            var allChatTitles = _usersInChats.Keys
                .Select(chat => chat.Title)
                .ToList();
            return allChatTitles.Contains(title);
        }

        /// <summary>
        /// Записывает ошибку в журнал.
        /// </summary>
        /// <param name="message"> Текст ошибки. </param>
        private void LogError(string message)
        {
            _log.Add(message);
        }
    }
}