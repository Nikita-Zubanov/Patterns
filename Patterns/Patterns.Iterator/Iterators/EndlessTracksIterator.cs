using System;
using Patterns.Iterator.Entities;
using Patterns.Iterator.Collections;
using Patterns.Iterator.Collections.Generic;

namespace Patterns.Iterator.Iterators
{
    /// <summary>
    /// Итератор, перебирающий треки в случайном порядке.
    /// </summary>
    public class EndlessTracksIterator : IEnumerator<Track>
    {
        /// <summary>
        /// Текущий трек.
        /// </summary>
        public Track Current => _tracks[_position];

        /// <summary>
        /// Объект, возвращающий текущий трек.
        /// </summary>
        object IEnumerator.Current => Current;

        /// <summary>
        /// Массив треков.
        /// </summary>
        private readonly Track[] _tracks;

        /// <summary>
        /// Текущая позиция в массиве.
        /// </summary>
        private int _position = -1;

        /// <summary>
        /// Инициализирует поля.
        /// </summary>
        /// <param name="tracks"> Массив треков. </param>
        public EndlessTracksIterator(Track[] tracks)
        {
            _tracks = tracks ?? throw new ArgumentNullException(nameof(tracks));
        }

        /// <summary>
        /// Возвращает флаг, указывающий является ли текущий элемент концом массива.
        /// </summary>
        /// <returns> True, если текущий трек не конец массива. </returns>
        public bool MoveNext()
        {
            if (_tracks.Length == 0)
            {
                return false;
            }

            if (_position + 1 == _tracks.Length)
            {
                _position = -1;
            }

            _position++;

            return true;
        }

        /// <summary>
        /// Сбрасывает текущую позицию в массиве.
        /// </summary>
        public void Reset()
        {
            _position = -1;
        }
    }
}