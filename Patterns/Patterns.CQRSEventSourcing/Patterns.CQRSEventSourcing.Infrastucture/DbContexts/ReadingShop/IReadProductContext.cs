using Patterns.CQRSEventSourcing.DomainModel.Aggregates.Product;

namespace Patterns.CQRSEventSourcing.Infrastucture.DbContexts.ReadingShop
{
    /// <summary>
    /// Предоставляет методы для чтения агрегата "Продукт" из БД.
    /// </summary>
    public interface IReadProductContext
    {
        /// <summary>
        /// Возвращает список продуктов.
        /// </summary>
        /// <returns>Список продуктов.</returns>
        List<Product> GetProducts();

        /// <summary>
        /// Возвращает список категорий продуктов.
        /// </summary>
        /// <returns>Список категорий продуктов.</returns>
        List<Category> GetCategories();

        /// <summary>
        /// Поиск продукта по Id.
        /// </summary>
        /// <param name="id">Id продукта.</param>
        /// <returns>Null или найденный продукт.</returns>
        Product GetProduct(Guid id);

        /// <summary>
        /// Поиск категории продукта по Id.
        /// </summary>
        /// <param name="id">Id категории продукта.</param>
        /// <returns>Null или найденную категорию продукта.</returns>
        Category GetCategory(Guid id);
    }
}