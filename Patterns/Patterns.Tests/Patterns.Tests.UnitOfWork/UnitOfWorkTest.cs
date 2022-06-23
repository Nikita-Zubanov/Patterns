using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Patterns.Tests.Patterns.Tests.UnitOfWork.Services;
using Patterns.UnitOfWork.DbContext;
using Patterns.UnitOfWork.Entities;
using Patterns.UnitOfWork.Services;
using System.Collections.Generic;

namespace Patterns.Tests.Patterns.Tests.UnitOfWork
{
    /// <summary>
    /// Теструет класс <see cref="UnitOfWork"/>.
    /// </summary>
    [TestClass]
    public class UnitOfWorkTest
    {
        /// <summary>
        /// Контекст интернет-магазина.
        /// </summary>
        private static ShopDbContext DbContext;

        /// <summary>
        /// Подгатавливает систему и инициализирует тестовые данные.
        /// </summary>
        /// <param name="context">Храненит информацию для unit-тестов.</param>
        [ClassInitialize]
        public static void SetupTestingData(TestContext context)
        {
            DbContext = new ShopDbContext(new SqlExecutorStub());

            DbContext.Users.AddRange(GetUsers());
            DbContext.Products.AddRange(GetProducts());
            DbContext.Commit();
        }

        /// <summary>
        /// Проверяет, что в коллекции сущностей существует добавленный пользователь.
        /// </summary>
        [TestMethod]
        public void HasAddedUser()
        {
            var newUser = new User { FullName = "Павлов Геннадий Даниилович", Age = 42 };

            DbContext.Users.Add(newUser);

            Assert.IsTrue(DbContext.Users.HasEntity(newUser.Id));
        }

        /// <summary>
        /// Проверяет, что внесённые изменения в коллекцию сущностей успешно откатились.
        /// </summary>
        [TestMethod]
        public void SuccessfullyRollbackAddedUser()
        {
            var newUser = new User { FullName = "Колесников Гаянэ Богуславович", Age = 15 };

            DbContext.Users.Add(newUser);
            DbContext.Rollback();

            Assert.IsFalse(DbContext.Users.HasEntity(newUser.Id));
        }

        /// <summary>
        /// Проверяет, что внесённые изменения в коллекцию сущностей успешно сохранились.
        /// </summary>
        [TestMethod]
        public void SuccessfullyCommitAddedUser()
        {
            var newUser = new User { FullName = "Колесников Гаянэ Богуславович", Age = 15 };

            DbContext.Users.Add(newUser);
            DbContext.Commit();

            Assert.IsTrue(DbContext.Users.HasEntity(newUser.Id));
        }

        /// <summary>
        /// Проверяет, что внесённые изменения формируются в запрос и выполняются.
        /// </summary>
        [TestMethod]
        public void CalledExecutingSqlQuery()
        {
            var sqlExecutorMock = new Mock<ISqlExecutor>();
            sqlExecutorMock.Setup(x => x.Execute(It.IsAny<string>()));
            var dbContext = new ShopDbContext(sqlExecutorMock.Object);
            var newUser = new User { FullName = "Колесников Гаянэ Богуславович", Age = 15 };

            dbContext.Users.Add(newUser);
            dbContext.Commit();

            sqlExecutorMock.Verify(x => x.Execute(It.IsAny<string>()));
        }

        /// <summary>
        /// Отменяет все изменения, внесённые в коллекции после каждого тест-кейса.
        /// </summary>
        [TestCleanup]
        public void RollbackTestMethodChanges()
        {
            DbContext.Rollback();
        }

        /// <summary>
        /// Возвращает коллекцию тестовых пользователей.
        /// </summary>
        /// <returns>Коллекцию пользователей.</returns>
        private static IEnumerable<User> GetUsers()
        {
            return new List<User>
            {
                new User { FullName = "Устинов Ираклий Эльдарович", Age = 68 },
                new User { FullName = "Сафонов Иосиф Викторович", Age = 11 },
                new User { FullName = "Степанов Вилен Лаврентьевич", Age = 21 },
                new User { FullName = "Ершов Витольд Пётрович", Age = 40 }
            };
        }

        /// <summary>
        /// Возвращает коллекцию тестовых продуктов.
        /// </summary>
        /// <returns>Коллекцию продуктов.</returns>
        private static IEnumerable<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product { Name = "Велосипед" },
                new Product { Name = "Пирог" }
            };
        }
    }
}