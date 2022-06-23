namespace Patterns.Prototype.Array
{
    /// <summary>
    /// Предоставляет метод для клонирования массива.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public interface IClonableArray<TItem>
    {
        /// <summary>
        /// Возвращает копию массива.
        /// </summary>
        /// <returns>Новый массив.</returns>
        IArray<TItem> Clone();
    }
}