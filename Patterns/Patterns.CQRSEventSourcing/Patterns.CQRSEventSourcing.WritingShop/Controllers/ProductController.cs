using MediatR;
using Microsoft.AspNetCore.Mvc;
using Patterns.CQRSEventSourcing.DomainModel.Events.Product;
using Patterns.CQRSEventSourcing.WritingShop.DataContracts.Product;

namespace Patterns.CQRSEventSourcing.WritingShop.Controllers
{
    /// <summary>
    /// ���������� ��� ������ ������� �������� "�������".
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        /// <summary>
        /// ������������� ���������� ��� ���������� �������.
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// �������������� ���� �����������.
        /// </summary>
        /// <param name="mediator">������������� ���������� ��� ���������� �������.</param>
        public ProductController(IMediator mediator)
        {
            if (mediator == null)
            {
                throw new ArgumentNullException(nameof(mediator));
            }

            _mediator = mediator;
        }

        /// <summary>
        /// ������ �������.
        /// </summary>
        /// <param name="product">�������.</param>
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
        /// ������ ��������� ��������.
        /// </summary>
        /// <param name="category">��������� ��������.</param>
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