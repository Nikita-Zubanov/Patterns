using MediatR;
using Microsoft.AspNetCore.Mvc;
using Patterns.CQRSEventSourcing.DomainModel.Events.Product;
using Patterns.CQRSEventSourcing.WritingShop.DataContracts.Product;

namespace Patterns.CQRSEventSourcing.WritingShop.Controllers
{
    /// <summary>
    /// Контроллер для записи событий агрегата "Продукт".
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        /// <summary>
        /// Предоставляет посредника для публикации событий.
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// Инициализирует поля контроллера.
        /// </summary>
        /// <param name="mediator">Предоставляет посредника для публикации событий.</param>
        public ProductController(IMediator mediator)
        {
            if (mediator == null)
            {
                throw new ArgumentNullException(nameof(mediator));
            }

            _mediator = mediator;
        }

        /// <summary>
        /// Создаёт продукт.
        /// </summary>
        /// <param name="product">Продукт.</param>
        [HttpPost("CreateProduct")]
        public async void CreateProduct(Product product)
        {
            var createProductEvent = new CreateProductEvent
            {
                EntityId = product.Id,
                Title = product.Title,
                Price = product.Price,
                CategoryId = product.CategoryId
            };

            await _mediator.Publish(createProductEvent);
        }

        /// <summary>
        /// Создаёт категорию продукта.
        /// </summary>
        /// <param name="category">Категория продукта.</param>
        [HttpPost("CreateCategory")]
        public async void CreateCategory(Category category)
        {
            var createCategoryEvent = new CreateCategoryEvent
            {
                EntityId = category.Id,
                Title = category.Title
            };

            await _mediator.Publish(createCategoryEvent);
        }
    }
}