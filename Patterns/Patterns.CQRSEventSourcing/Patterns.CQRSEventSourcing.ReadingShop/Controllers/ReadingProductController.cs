using Microsoft.AspNetCore.Mvc;
using Patterns.CQRSEventSourcing.DomainModel.Aggregates.Product;
using Patterns.CQRSEventSourcing.Infrastucture.DbContexts.ReadingShop;

namespace Patterns.CQRSEventSourcing.ReadingShop.Controllers
{
    /// <summary>
    /// ���������� ��� ������ �������� "�������".
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ReadingProductController : ControllerBase
    {
        /// <summary>
        /// ������������� ������ ��� ������ �������� "�������" �� ��.
        /// </summary>
        private readonly IReadProductContext _productContext;

        /// <summary>
        /// �������������� ���� �����������.
        /// </summary>
        /// <param name="dbContext">������������� ������ ��� ������ �������� "�������" �� ��.</param>
        public ReadingProductController(IReadProductContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            _productContext = dbContext;
        }

        /// <summary>
        /// ���������� ��� ��������.
        /// </summary>
        /// <returns>������ ���������.</returns>
        [HttpGet("GetAllProducts")]
        public List<Product> GetAllProducts()
        {
            return _productContext.GetProducts();
        }

        /// <summary>
        /// ���������� ������� �� Id.
        /// </summary>
        /// <param name="id">Id ��������.</param>
        /// <returns>Null ��� ��������� �������.</returns>
        [HttpGet("GetProduct/{id}")]
        public Product GetProduct(Guid id)
        {
            return _productContext.GetProduct(id);
        }
    }
}