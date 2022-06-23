namespace Patterns.Iterator.Collections
{
    /// <summary>
    /// Предоставляет перечислитель, который поддерживает простой перебор элементов неуниверсальной коллекции.
    /// </summary>
    public interface IEnumerable
    {
        /// <summary>
        /// Возвращает перечислитель, который осуществляет итерацию по коллекции.
        /// </summary>
        /// <returns> Объект <see cref="IEnumerator"/>, который используется для прохода по коллекции. </returns>
        IEnumerator GetEnumerator();
    }
}