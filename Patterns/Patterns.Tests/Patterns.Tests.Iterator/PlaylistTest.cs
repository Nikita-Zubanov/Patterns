using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patterns.Iterator;
using Patterns.Iterator.Entities;

namespace Patterns.Tests.Iterator
{
    /// <summary>
    /// Тестирование перечисляемого объекта "Список треков".
    /// </summary>
    [TestClass]
    public class PlaylistTest
    {
        /// <summary>
        /// Массив треков.
        /// </summary>
        private readonly Track[] _tracks;

        /// <summary>
        /// Инициализирует массив треков.
        /// </summary>
        public PlaylistTest()
        {
            _tracks = GetTracks();
        }

        /// <summary>
        /// Проверяет, следует ли за последним треком первый.
        /// Таймаут необходим, т.к. треки будут воспроизводится бесконечно.
        /// </summary>
        [TestMethod]
        [Timeout(3000)]
        public void FirstTrackDoesNotFollowLast()
        {
            var tracklist = new Playlist(_tracks, PlaybackOrder.Loop);
            var isFirstAfterLastTrack = false;
            var isCheckNextTrack = false;
            var firstTrack = _tracks.First();
            var lastTrack = _tracks.Last();

            foreach (var track in tracklist)
            {
                if (isCheckNextTrack)
                {
                    isFirstAfterLastTrack = track.Equals(firstTrack);
                    break;
                }

                if (track.Equals(lastTrack))
                {
                    isCheckNextTrack = true;
                }
            }

            Assert.IsTrue(isFirstAfterLastTrack, "Первый трек не следует за последним.");
        }

        /// <summary>
        /// Проверяет, воспроизводятся ли треки по популярности (сначала самые популярные).
        /// </summary>
        [TestMethod]
        public void PlayTracksByPopularity()
        {
            var tracklist = new Playlist(_tracks, PlaybackOrder.PopularFirst);
            var isMorePopularPreviousTrack = true;
            Track previousTrack = null;

            foreach (var track in tracklist)
            {
                if (previousTrack != null)
                {
                    if (previousTrack.NumberOfListens < track.NumberOfListens)
                    {
                        isMorePopularPreviousTrack = false;
                        break;
                    }
                }

                previousTrack = track;
            }

            Assert.IsTrue(isMorePopularPreviousTrack, "Треки в плейлисте отфильтрованы не по наибольшему числу прослушиваний.");
        }

        /// <summary>
        /// Возвращает 17 треков из топ-200 шазама.
        /// </summary>
        /// <returns> Массив треков. </returns>
        private Track[] GetTracks()
        {
            return new Track[]
            {
                new Track {Artist = "Issam Alnajjar", Title = "Hadal Ahbek", NumberOfListens = 526600},
                new Track {Artist = "Olivia Rodrigo", Title = "drivers license", NumberOfListens = 490900},
                new Track {Artist = "CJ", Title = "Whoopty", NumberOfListens = 2200000},
                new Track {Artist = "Ed Sheeran", Title = "Afterglow", NumberOfListens = 737600},
                new Track {Artist = "The Weeknd", Title = "Save Your Tears", NumberOfListens = 1400000},
                new Track {Artist = "Tiësto", Title = "The Business", NumberOfListens = 2300000},
                new Track {Artist = "Masked Wolf", Title = "Astronaut In The Ocean", NumberOfListens = 685000},
                new Track {Artist = "Jason Derulo", Title = "Take You Dancing", NumberOfListens = 3800000},
                new Track {Artist = "The Weeknd", Title = "Blinding Lights", NumberOfListens = 18800000},
                new Track {Artist = "Fousheé", Title = "Deep End", NumberOfListens = 4100000},
                new Track {Artist = "Black Eyed Peas & Shakira", Title = "GIRL LIKE ME", NumberOfListens = 827900},
                new Track {Artist = "Kim Dracula", Title = "Paparazzi", NumberOfListens = 427100},
            };
        }
    }
}