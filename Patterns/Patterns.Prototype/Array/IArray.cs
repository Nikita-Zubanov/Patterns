namespace Patterns.Prototype.Array
{
    /// <summary>
    /// Предоставляет необходимый для массива члены класса.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public interface IArray<TItem>
    {
        /// <summary>
        /// Длина массива.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Индексатор массива.
        /// </summary>
        /// <param name="index">Индекс массива.</param>
        /// <returns>Значение массива.</returns>
        TItem this[int index] { get; set; }
    }
}