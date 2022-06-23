using Microsoft.AspNetCore.Mvc;
using Patterns.CQRSEventSourcing.DomainModel.Events.Product;
using Patterns.CQRSEventSourcing.Infrastucture.DbContexts.ReadingShop;

namespace Patterns.CQRSEventSourcing.ReadingShop.Controllers
{
    /// <summary>
    /// Контроллер для синхронизации агрегата "Продукт".
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SynchronizingProductController : ControllerBase
    {
        /// <summary>
        /// Предоставляет методы для синхронизации агрегата "Продукт" в БД.
        /// </summary>
        private readonly ISynchronizeProductContext _synchronizeProductContext;

        /// <summary>
        /// Инициализирует поля контроллера.
        /// </summary>
        /// <param name="dbContext">Предоставляет методы для синхронизации агрегата "Продукт" в БД.</param>
        public SynchronizingProductController(ISynchronizeProductContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            _synchronizeProductContext = dbContext;
        }

        /// <summary>
        /// Применяет новые события создания продуктов к текущему наполнению.
        /// </summary>
        /// <param name="events">Список событий создания продуктов.</param>
        [HttpPost("ApplyCreateProductEvents")]
        public void ApplyCreateProductEvents(List<CreateProductEvent> createProductEvents)
        {
            _synchronizeProductContext.ApplyCreateProductEvents(createProductEvents);
            _synchronizeProductContext.SaveChangesAsync();
        }

        /// <summary>
        /// Применяет новые события создания категорий продуктов к текущему наполнению.
        /// </summary>
        /// <param name="events">Список событий создания категорий родуктов.</param>
        [HttpPost("ApplyCreateCategoryEvents")]
        public void ApplyCreateCategoryEvents(List<CreateCategoryEvent> createCategoryEvents)
        {
            _synchronizeProductContext.ApplyCreateCategoryEvents(createCategoryEvents);
            _synchronizeProductContext.SaveChangesAsync();
        }
    }
}