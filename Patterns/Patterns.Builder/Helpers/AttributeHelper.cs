using Patterns.Builder.Attributes;
using System.Reflection;

namespace Patterns.Builder.Helpers
{
    /// <summary>
    /// Предоставляет методы для работы с классом <see cref="TitleAttribute"/>.
    /// </summary>
    internal static class TitleAttributeHelper
    {
        /// <summary>
        /// Возвращает значение атрибута для свойства объекта.
        /// </summary>
        /// <typeparam name="TObject">Тип объекта.</typeparam>
        /// <param name="propertyName">Название свойства.</param>
        /// <returns>Заголовок свойства.</returns>
        internal static string GetValue<TObject>(string propertyName)
        {
            var objectType = typeof(TObject);

            PropertyInfo findedProperty = null;
            foreach (var property in objectType.GetProperties())
            {
                if (property.Name == propertyName)
                {
                    findedProperty = property;
                    break;
                }
            }

            if (findedProperty == null)
            {
                return null;
            }

            var titleAttribute = findedProperty.GetCustomAttribute<TitleAttribute>();
            return titleAttribute?.Value;
        }
    }
}