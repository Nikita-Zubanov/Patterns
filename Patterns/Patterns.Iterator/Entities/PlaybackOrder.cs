namespace Patterns.Iterator.Entities
{
    /// <summary>
    /// Порядок воспроизведения.
    /// </summary>
    public enum PlaybackOrder
    {
        /// <summary>
        /// По кругу.
        /// </summary>
        Loop = 0,

        /// <summary>
        /// В случайном порядке.
        /// </summary>
        Shuffle = 1,

        /// <summary>
        /// Популярные треки в начале.
        /// </summary>
        PopularFirst = 2
    }
}