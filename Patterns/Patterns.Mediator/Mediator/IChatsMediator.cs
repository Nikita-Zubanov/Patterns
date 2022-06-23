using Patterns.Mediator.Components;

namespace Patterns.Mediator.Mediator
{
    /// <summary>
    /// Посредник чатов.
    /// </summary>
    public interface IChatsMediator
    {
        /// <summary>
        /// Зарегистрировать чат для общения.
        /// </summary>
        /// <param name="chat"> Чат. </param>
        void RegisterChat(Chat chat);

        /// <summary>
        /// Добавить пользователя в чат.
        /// </summary>
        /// <param name="user"> Пользователь. </param>
        /// <param name="chatTitle"> Название чата. </param>
        void AddUserToChat(User user, string chatTitle);

        /// <summary>
        /// Отправить сообщение в чат.
        /// </summary>
        /// <param name="sender"> Пользователь, отправивший сообщение. </param>
        /// <param name="message"> Текст сообщения. </param>
        void SendMessage(User sender, string message);

        /// <summary>
        /// Проверить логин на уникальность во всех чатах.
        /// </summary>
        /// <param name="login"> Логин пользователя. </param>
        /// <returns> Признак, указывающий уникален ли логин. </returns>
        bool IsUniqueLogin(string login);

        /// <summary>
        /// Проверить уникальность названия чата.
        /// </summary>
        /// <param name="title"> Название чата. </param>
        /// <returns> Признак, указывающий уникально ли название чата. </returns>
        bool IsUniqueChatTile(string title);
    }
}