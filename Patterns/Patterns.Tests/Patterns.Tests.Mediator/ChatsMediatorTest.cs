using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patterns.Mediator.Components;
using Patterns.Mediator.Mediator;
using System.Linq;

namespace Patterns.Tests.Mediator
{
    /// <summary>
    /// Тестирование посредника чатов.
    /// </summary>
    [TestClass]
    public class ChatsMediatorTest
    {
        /// <summary>
        /// Проверяет, что пользователи пишут именно в тот чат, куда были добавлены.
        /// </summary>
        [TestMethod]
        public void UsersWriteToTheirChat()
        {
            var chatMediator = new ChatsMediator();
            // Создание пользователей.
            var alex = new User(chatMediator, "Alex");
            var artem = new User(chatMediator, "228_Tema_228");
            var vasia = new User(chatMediator, "pro[100]Vasia");
            // Создание чатов.
            var sadChat = new Chat(chatMediator, "SadChat");
            var trashChat = new Chat(chatMediator, "BaH/\\NTb|");
            // Регистрация чатов.
            chatMediator.RegisterChat(sadChat);
            chatMediator.RegisterChat(trashChat);
            // Добавление пользователей в чаты.
            chatMediator.AddUserToChat(alex, sadChat.Title);
            chatMediator.AddUserToChat(artem, trashChat.Title);
            chatMediator.AddUserToChat(vasia, trashChat.Title);
            var alexMessage = "Всем привет...";
            var artemMessage = "Вася ты тут???";
            var vasiaMessage = "Леху потеряли((";

            alex.SendMessage(alexMessage);
            artem.SendMessage(artemMessage);
            vasia.SendMessage(vasiaMessage);

            CollectionAssert.Contains(
                sadChat.UserMessages.FirstOrDefault(um => um.Key == alex.Login).Value.ToArray(),
                alexMessage);
            CollectionAssert.Contains(
                trashChat.UserMessages.FirstOrDefault(um => um.Key == artem.Login).Value.ToArray(),
                artemMessage);
            CollectionAssert.Contains(
                trashChat.UserMessages.FirstOrDefault(um => um.Key == vasia.Login).Value.ToArray(),
                vasiaMessage);
        }
    }
}