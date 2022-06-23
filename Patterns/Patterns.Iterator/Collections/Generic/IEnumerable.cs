namespace Patterns.Iterator.Collections.Generic
{
    /// <summary>
    /// Предоставляет перечислитель, который поддерживает простой перебор элементов в указанной коллекции.
    /// </summary>
    /// <typeparam name="T"> Тип объектов для перечисления. </typeparam>
    public interface IEnumerable<out T> : IEnumerable
    {
        /// <summary>
        /// Возвращает перечислитель, выполняющий перебор элементов в коллекции.
        /// </summary>
        /// <returns>
        /// Перечислитель, который можно использовать для итерации по коллекции.
        /// </returns>
        IEnumerator<T> GetEnumerator();
    }
}