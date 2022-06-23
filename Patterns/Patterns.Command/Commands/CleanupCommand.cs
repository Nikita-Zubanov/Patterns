using System;
using System.Windows.Controls;

namespace Patterns.Command.Commands
{
    /// <summary>
    /// Команда очистки содержимого.
    /// </summary>
    public class CleanupCommand : ICommand
    {
        /// <summary>
        /// Список содержимого.
        /// </summary>
        private readonly ItemCollection _items;

        /// <summary>
        /// Инициализирует поля.
        /// </summary>
        /// <param name="items"> Список содержимого. </param>
        public CleanupCommand(ItemCollection items)
        {
            _items = items ?? throw new ArgumentNullException(nameof(items));
        }

        /// <summary>
        /// Очистить содержимое.
        /// </summary>
        public void Execute()
        {
            _items.Clear();
        }
    }
}