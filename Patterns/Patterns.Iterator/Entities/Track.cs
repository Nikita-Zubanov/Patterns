namespace Patterns.Iterator.Entities
{
    /// <summary>
    /// Трек.
    /// </summary>
    public class Track
    {
        /// <summary>
        /// Исполнитель.
        /// </summary>
        public string Artist { get; set; }

        /// <summary>
        /// Название.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Кол-во прослушиваний.
        /// </summary>
        public int NumberOfListens { get; set; }
        
        /// <summary>
        /// Увеличивает количество прослушиваний на одно.
        /// </summary>
        public void Play()
        {
            NumberOfListens++;
        }

        /// <summary>
        /// Возвращает флаг, указывающий на равенство объектов.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is Track track)
            {
                return track.Title == Title &&
                       track.NumberOfListens == NumberOfListens &&
                       track.Artist == Artist;
            }

            return false;
        }

        /// <summary>
        /// Представляет объект в виде строки.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Artist} – {Title} ({NumberOfListens/1000}K прослушиваний)";
        }
    }
}