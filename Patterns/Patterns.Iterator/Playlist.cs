using System;
using System.ComponentModel;
using Patterns.Iterator.Collections;
using Patterns.Iterator.Collections.Generic;
using Patterns.Iterator.Entities;
using Patterns.Iterator.Iterators;

namespace Patterns.Iterator
{
    /// <summary>
    /// Список треков.
    /// </summary>
    public class Playlist : IEnumerable<Track>
    {
        /// <summary>
        /// Массив треков.
        /// </summary>
        private readonly Track[] _tracks;

        /// <summary>
        /// Порядок воспроизведения.
        /// </summary>
        private readonly PlaybackOrder _playbackOrder;

        /// <summary>
        /// Инициализирует поля.
        /// </summary>
        /// <param name="tracks"> Массив треков. </param>
        /// <param name="playbackOrder"> Порядок воспроизведения. </param>
        public Playlist(Track[] tracks, PlaybackOrder playbackOrder = PlaybackOrder.Loop)
        {
            _tracks = tracks ?? throw new ArgumentNullException(nameof(tracks));
            _playbackOrder = playbackOrder;
        }

        /// <summary>
        /// Возвращает обобщенный перечислитель в зависимости от порядка воспроизведения.
        /// </summary>
        /// <returns> Перечислитель треков. </returns>
        public IEnumerator<Track> GetEnumerator()
        {
            switch (_playbackOrder)
            {
                case PlaybackOrder.Loop:
                    return new EndlessTracksIterator(_tracks);

                case PlaybackOrder.Shuffle:
                    return new RandomTracksIterator(_tracks);

                case PlaybackOrder.PopularFirst:
                    return new MostPopularTracksIterator(_tracks);

                default:
                    throw new InvalidEnumArgumentException(
                        nameof(_playbackOrder),
                        (int)_playbackOrder,
                        typeof(PlaybackOrder));
            }
        }

        /// <summary>
        /// Возвращает пользовательскую реализацию перечислителя.
        /// </summary>
        /// <returns> Перечислитель. </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}