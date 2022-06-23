using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patterns.Prototype.Array;

namespace Patterns.Tests.Patterns.Tests.Prototype
{
    /// <summary>
    /// Класс для тестирования <see cref="RandomArray"/>.
    /// </summary>
    [TestClass]
    public class RandomArrayTest
    {
        /// <summary>
        /// Проверяет, что можно получить копию массива.
        /// </summary>
        [TestMethod]
        public void SuccessfullyArrayClonned()
        {
            var randomArray = new RandomArray(length: 5, min: 10, max: 100);  
            var firstValue = randomArray[0];

            var clonnabledRandomArray = randomArray.Clone();
            clonnabledRandomArray[0] = -firstValue;

            Assert.AreNotEqual(randomArray[0], clonnabledRandomArray[0]);
        }
    }
}