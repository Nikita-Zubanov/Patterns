using Microsoft.AspNetCore.Mvc;
using Patterns.CQRSEventSourcing.DomainModel.Aggregates.Product;
using Patterns.CQRSEventSourcing.Infrastucture.DbContexts.ReadingShop;

namespace Patterns.CQRSEventSourcing.ReadingShop.Controllers
{
    /// <summary>
    /// Контроллер для чтения агрегата "Продукт".
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ReadingProductController : ControllerBase
    {
        /// <summary>
        /// Предоставляет методы для чтения агрегата "Продукт" из БД.
        /// </summary>
        private readonly IReadProductContext _productContext;

        /// <summary>
        /// Инициализирует поля контроллера.
        /// </summary>
        /// <param name="dbContext">Предоставляет методы для чтения агрегата "Продукт" из БД.</param>
        public ReadingProductController(IReadProductContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            _productContext = dbContext;
        }

        /// <summary>
        /// Возвращает все продукты.
        /// </summary>
        /// <returns>Список продуктов.</returns>
        [HttpGet("GetAllProducts")]
        public List<Product> GetAllProducts()
        {
            return _productContext.GetProducts();
        }

        /// <summary>
        /// Возвращает продукт по Id.
        /// </summary>
        /// <param name="id">Id продукта.</param>
        /// <returns>Null или найденный продукт.</returns>
        [HttpGet("GetProduct/{id}")]
        public Product GetProduct(Guid id)
        {
            return _productContext.GetProduct(id);
        }
    }
}