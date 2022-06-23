namespace Patterns.Iterator.Collections.Generic
{
    /// <summary>
    /// Поддерживает простой перебор элементов универсальной коллекции.
    /// </summary>
    /// <typeparam name="T"> Тип объектов для перечисления. </typeparam>
    public interface IEnumerator<out T> : IEnumerator
    {
        /// <summary>
        /// Возвращает элемент коллекции, соответствующий текущей позиции перечислителя.
        /// </summary>
        T Current { get; }
    }
}