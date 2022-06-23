using System;
using System.Collections.Generic;
using System.Linq;

namespace Patterns.UnitOfWork.Extensions
{
    /// <summary>
    /// Расширяет методы класса <see cref="List"/>.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Создаёт дубликат коллекции.
        /// </summary>
        /// <typeparam name="T">Тип, реализующий интерфейс <see cref="ICloneable"/>.</typeparam>
        /// <param name="listToClone">Исходная коллекция.</param>
        /// <returns>Дубликат коллекции.</returns>
        public static List<T> Clone<T>(this List<T> list) where T : ICloneable
        {
            return list.Select(item => (T)item.Clone()).ToList();
        }
    }
}