using Patterns.Command.Commands;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Patterns.Command
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
    {
        /// <summary>
        /// Последний активный элемент управления.
        /// </summary>
        private Control _lastControlItemUsed;

        /// <summary>
        /// Инициализирует UI.
        /// </summary>
        public MainWindow()
        {
            //InitializeComponent();
        }

        #region Display synonym

        /// <summary>
        /// Выводит синонимы к слову в список.
        /// </summary>
        /// <param name="sender"> Элемент управления кнопки. </param>
        /// <param name="e"> Информация о состоянии кнопки. </param>
        private void DisplaySynonymsButton_Click(object sender, RoutedEventArgs e)
        {
            _lastControlItemUsed = (Control)sender;

            var word = TextBox.Text;
            if (!string.IsNullOrWhiteSpace(word))
            {
                var command = new FindSynonymCommand(word, SynonymsListBox.Items);
                ExecuteCommand(command);
            }
        }

        /// <summary>
        /// Выводит синонимы к слову в список.
        /// </summary>
        /// <param name="sender"> Элемент управления пункта меню. </param>
        /// <param name="e"> Информация о состоянии пункта меню. </param>
        private void DisplaySynonymsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            _lastControlItemUsed = (Control)sender;

            var word = TextBox.Text;
            if (!string.IsNullOrWhiteSpace(word))
            {
                var command = new FindSynonymCommand(TextBox.Text, SynonymsListBox.Items);
                ExecuteCommand(command);
            }
        }

        #endregion

        #region Cleanup synonym

        /// <summary>
        /// Очищает список синонимов.
        /// </summary>
        /// <param name="sender"> Элемент управления кнопки. </param>
        /// <param name="e"> Информация о состоянии кнопки. </param>
        private void ClearSynonymsButton_Click(object sender, RoutedEventArgs e)
        {
            _lastControlItemUsed = (Control)sender;

            var command = new CleanupCommand(SynonymsListBox.Items);
            ExecuteCommand(command);
        }

        /// <summary>
        /// Очищает список синонимов.
        /// </summary>
        /// <param name="sender"> Элемент управления пункта меню. </param>
        /// <param name="e"> Информация о состоянии пункта меню. </param>
        private void ClearSynonymsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            _lastControlItemUsed = (Control)sender;

            var command = new CleanupCommand(SynonymsListBox.Items);
            ExecuteCommand(command);
        }

        #endregion

        #region Recolor

        /// <summary>
        /// Перекрашивает в случайный цвет последний активный элемент.
        /// </summary>
        /// <param name="sender"> Элемент управления кнопки. </param>
        /// <param name="e"> Информация о состоянии кнопки. </param>
        private void RandomRecolorButton_Click(object sender, RoutedEventArgs e)
        {
            _lastControlItemUsed = _lastControlItemUsed ?? (Control)sender;

            var command = new RecolorCommand(_lastControlItemUsed, GetRandomColorBrush());
            ExecuteCommand(command);
        }

        /// <summary>
        /// Перекрашивает в случайный цвет последний активный элемент.
        /// </summary>
        /// <param name="sender"> Элемент управления пункта меню. </param>
        /// <param name="e"> Информация о состоянии пункта меню. </param>
        private void RandomRecolorMenuItem_Click(object sender, RoutedEventArgs e)
        {
            _lastControlItemUsed = _lastControlItemUsed ?? (Control)sender;

            var command = new RecolorCommand(_lastControlItemUsed, GetRandomColorBrush());
            ExecuteCommand(command);
        }

        #endregion

        /// <summary>
        /// Вызывает метод выполнения команды.
        /// </summary>
        /// <param name="command"> Команда. </param>
        private void ExecuteCommand(ICommand command)
        {
            command.Execute();
        }

        /// <summary>
        /// Возвращает случайный цвет из набора "Brushes".
        /// </summary>
        /// <returns> Цвет. </returns>
        private SolidColorBrush GetRandomColorBrush()
        {
            var props = typeof(Brushes).GetProperties();
            var randomProp = new Random().Next(0, props.Length);

            return (SolidColorBrush)props[randomProp].GetValue(null);
        }
    }
}
