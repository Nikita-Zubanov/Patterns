using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using Microsoft.Office.Interop.Word;

namespace Patterns.Command.Commands
{
    /// <summary>
    /// Команда поиска синонима. 
    /// </summary>
    public class FindSynonymCommand : ICommand
    {
        /// <summary>
        /// Строка, которая выводится в список, если синономов не найдено.
        /// </summary>
        private const string NoSynonymsFound = "Синонимов не найдено.";

        /// <summary>
        /// Слово, по которому ищется синоним.
        /// </summary>
        private readonly string _word;

        /// <summary>
        /// Список содержимого, куда записываются синонимы.
        /// </summary>
        private readonly ItemCollection _items;

        /// <summary>
        /// Инициализирует поля.
        /// </summary>
        /// <param name="word"> Слово, по которому ищется синоним. </param>
        /// <param name="items"> Список содержимого, куда записываются синонимы. </param>
        public FindSynonymCommand(string word, ItemCollection items)
        {
            _word = string.IsNullOrWhiteSpace(word)
                ? throw new ArgumentNullException(nameof(word))
                : word;

            _items = items ?? throw new ArgumentNullException(nameof(items));
        }

        /// <summary>
        /// Найти и записать синонимы.
        /// </summary>
        public void Execute()
        {
            try
            {
                var synonyms = GetSynonyms(_word);

                if (synonyms.Count == 0)
                {
                    _items.Add(NoSynonymsFound);
                    return;
                }

                foreach (var synonym in GetSynonyms(_word))
                {
                    _items.Add(synonym);
                }
            }
            catch (Exception e)
            {
                _items.Add($"При поиске синонимов возникла ошибка: \"{e}\".");
            }
        }

        /// <summary>
        /// Возвращает список синонимов к слову.
        /// </summary>
        /// <param name="term"> Слово, по которому ищется синоним. </param>
        /// <returns> Список синонимов. </returns>
        private List<string> GetSynonyms(string term)
        {
            var wordApp = new Application();
            var languageId = (object) WdLanguageID.wdRussian;
            var synonymInfo = wordApp.get_SynonymInfo(term, ref languageId);

            var synonyms = new List<string>();
            foreach (var meaning in (Array) (object) synonymInfo.MeaningList)
            {
                synonyms.Add(meaning.ToString());
            }

            wordApp.Quit();
            Marshal.ReleaseComObject(wordApp);

            return synonyms.Distinct().ToList();
        }
    }
}