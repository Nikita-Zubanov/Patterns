namespace Patterns.Iterator.Collections
{
    /// <summary>
    /// Поддерживает простой перебор по неуниверсальной коллекции.
    /// </summary>
    public interface IEnumerator
    {
        /// <summary>
        /// Возвращает элемент коллекции, соответствующий текущей позиции перечислителя.
        /// </summary>
        object Current { get; }

        /// <summary>
        /// Перемещает перечислитель к следующему элементу коллекции.
        /// </summary>
        /// <returns></returns>
        bool MoveNext();

        /// <summary>
        /// Устанавливает перечислитель в его начальное положение, т. е. перед первым элементом коллекции.
        /// </summary>
        void Reset();
    }
}