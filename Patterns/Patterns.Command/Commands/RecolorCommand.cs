using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace Patterns.Command.Commands
{
    /// <summary>
    /// Команда перекраски элемента управления. 
    /// </summary>
    public class RecolorCommand : ICommand
    {
        /// <summary>
        /// Элемент управления.
        /// </summary>
        private readonly Control _itemControl;

        /// <summary>
        /// Новый цвет.
        /// </summary>
        private readonly SolidColorBrush _color;

        /// <summary>
        /// Инициализирует поля.
        /// </summary>
        /// <param name="itemControl"> Элемент управления. </param>
        /// <param name="color"> Цвет. </param>
        public RecolorCommand(Control itemControl, SolidColorBrush color)
        {
            _itemControl = itemControl ?? throw new ArgumentNullException(nameof(itemControl));
            _color = color ?? throw new ArgumentNullException(nameof(color));
        }

        /// <summary>
        /// Перекрасить элемент управления.
        /// </summary>
        public void Execute()
        {
            _itemControl.Background = _color;
        }
    }
}