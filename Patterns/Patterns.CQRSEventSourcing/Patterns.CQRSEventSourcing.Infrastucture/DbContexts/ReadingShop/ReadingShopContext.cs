using Microsoft.EntityFrameworkCore;
using Patterns.CQRSEventSourcing.DomainModel.Aggregates;
using Patterns.CQRSEventSourcing.DomainModel.Aggregates.Product;
using Patterns.CQRSEventSourcing.DomainModel.Events;
using Patterns.CQRSEventSourcing.DomainModel.Events.Product;

namespace Patterns.CQRSEventSourcing.Infrastucture.DbContexts.ReadingShop
{
    /// <summary>
    /// Контекст магазина для чтения данных из БД.
    /// </summary>
    public class ReadingShopContext : DbContext, IReadProductContext, ISynchronizeProductContext
    {
        /// <summary>
        /// Список продуктов.
        /// </summary>
        private readonly List<Product> _products = new List<Product>();

        /// <summary>
        /// Список категорий продуктов.
        /// </summary>
        private readonly List<Category> _categories = new List<Category>();

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        /// <param name="options">Настройки подключения к БД.</param>
        public ReadingShopContext(DbContextOptions options) : base(options)
        { }

        /// <summary>
        /// Возвращает список продуктов.
        /// </summary>
        /// <returns>Список продуктов.</returns>
        public List<Product> GetProducts()
        {
            return _products.ToList();
        }

        /// <summary>
        /// Возвращает список категорий продуктов.
        /// </summary>
        /// <returns>Список категорий продуктов.</returns>
        public List<Category> GetCategories()
        {
            return _categories.ToList();
        }

        /// <summary>
        /// Поиск продукта по Id.
        /// </summary>
        /// <param name="id">Id продукта.</param>
        /// <returns>Возвращает null или найденный продукт.</returns>
        public Product GetProduct(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            return _products.FirstOrDefault(product => product.Id == id);
        }

        /// <summary>
        /// Поиск категории продукта по Id.
        /// </summary>
        /// <param name="id">Id категории продукта.</param>
        /// <returns>Возвращает null или найденную категорию продукта.</returns>
        public Category GetCategory(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            return _categories.FirstOrDefault(category => category.Id == id);
        }

        /// <summary>
        /// Сохранить изменения.
        /// </summary>
        /// <param name="cancellationToken">Токен для отмены транзакции.</param>
        /// <returns>Результат сохранения.</returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Применяет новые события создания продуктов к текущему наполнению в БД.
        /// </summary>
        /// <param name="events">Список событий создания продуктов.</param>
        public void ApplyCreateProductEvents(List<CreateProductEvent> events)
        {
            if (events == null || events.Count == 0)
            {
                return;
            }

            ApplyEvents(_products, events);
        }

        /// <summary>
        /// Применяет новые события создания категорий продуктов к текущему наполнению в БД.
        /// </summary>
        /// <param name="events">Список событий создания категорий родуктов.</param>
        public void ApplyCreateCategoryEvents(List<CreateCategoryEvent> events)
        {
            if (events == null || events.Count == 0)
            {
                return;
            }

            ApplyEvents(_categories, events);
        }

        /// <summary>
        /// Применяет новые события к текущему наполнению в БД.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <typeparam name="TEvent">Тип события.</typeparam>
        /// <param name="entities">Список сущностей.</param>
        /// <param name="events">Список событий.</param>
        private void ApplyEvents<TEntity, TEvent>(List<TEntity> entities, List<TEvent> events)
            where TEvent : Event
            where TEntity : Entity<TEvent>
        {
            var changeEntityEvents = events
                .Where(eventObject => entities.Any(entity => eventObject.EntityId == entity.Id))
                .ToList();

            entities
                .Join(
                    changeEntityEvents,
                    entity => entity.Id,
                    @event => @event.EntityId,
                    (entity, @event) =>
                    {
                        entity.On(@event);
                        return entity;
                    })
                .ToList();

            var createEntityEvents = events
                .Where(@event => entities.All(entity => @event.EntityId != entity.Id))
                .ToList();

            var newEntities = createEntityEvents
                .Select(createEntityEvent =>
                {
                    var entity = (TEntity)Activator.CreateInstance(typeof(TEntity));
                    entity.On(createEntityEvent);

                    return entity;
                });

            entities.AddRange(newEntities);
        }
    }
}