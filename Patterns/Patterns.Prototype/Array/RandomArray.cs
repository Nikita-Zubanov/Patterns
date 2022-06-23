using System;

namespace Patterns.Prototype.Array
{
    /// <summary>
    /// Массив, генерирующий случайные значения.
    /// </summary>
    public class RandomArray : IArray<int>, IClonableArray<int>
    {
        /// <summary>
        /// Массив случайных значений.
        /// </summary>
        private int[] _array;

        /// <summary>
        /// Минимальное значение диапозона.
        /// </summary>
        private int _min = 0;

        /// <summary>
        /// Максимальное значение диапозона.
        /// </summary>
        private int _max = 100;

        /// <summary>
        /// Объект, генерирующий псевдослучайные числа.
        /// </summary>
        private Random _random = new Random();

        /// <summary>
        /// Длина массива.
        /// </summary>
        public int Count => _array.Length;

        /// <summary>
        /// Индексатор массива.
        /// </summary>
        /// <param name="index">Индекс массива.</param>
        /// <returns>Значение массива.</returns>
        public int this[int index]
        {
            get
            {
                if (index < 0 || index >= _array.Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                if (_array[index] == default)
                {
                    _array[index] = _random.Next(_min, _max);
                }

                return _array[index];
            }
            set
            {
                _array[index] = value;
            }
        }

        /// <summary>
        /// Инициализирует массив случайных значений.
        /// </summary>
        /// <param name="array">Массив значений.</param>
        public RandomArray(int[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            _array = array;
        }

        /// <summary>
        /// Инициализирует минимальное и максимальное значения массива.
        /// </summary>
        /// <param name="length">Длина массива.</param>
        /// <param name="min">Минимальное значение массива.</param>
        /// <param name="max">Максимальное значение массива.</param>
        public RandomArray(int length, int min, int max)
            : this(length)
        {
            _min = min;
            _max = max;
        }

        /// <summary>
        /// Инициализирует массив случайных значений.
        /// </summary>
        /// <param name="length">Длина массива.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public RandomArray(int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("Длина массива не может быть меньше нуля.");
            }

            _array = new int[length];
        }

        /// <summary>
        /// Возвращает копию массива.
        /// </summary>
        /// <returns>Новый массив.</returns>
        public IArray<int> Clone()
        {
            var clonebledArray = new int[_array.Length];
            for (int i = 0; i < _array.Length; i++)
            {
                clonebledArray[i] = this[i];
            }

            return new RandomArray(clonebledArray);
        }
    }
}