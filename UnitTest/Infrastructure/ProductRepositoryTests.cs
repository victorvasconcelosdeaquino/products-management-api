using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Project.UnitTest.Infrastructure
{
    public class ProductRepositoryTests
    {
        private Mock<ApplicationDbContext> _dbContextMock;
        private ProductRepository _productRepository;

        [SetUp]
        public void Setup()
        {
            _dbContextMock = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
            _productRepository = new ProductRepository(_dbContextMock.Object);
        }

        [Test]
        public async Task AddAsync_ValidProduct_ReturnsAddedProduct()
        {

            // Arrange
            var newProduct = new Product
            {
                Id = 1,
                ExpiryDate = DateTime.Now,
                ManufactureDate = DateTime.Now.AddMonths(2),
                Description = "Descrição",
                IsActive = true
            };

            var productDbSetMock = new Mock<DbSet<Product>>();

            _dbContextMock.Setup(db => db.Set<Product>())
                          .Returns(productDbSetMock.Object);

            productDbSetMock.Setup(dbSet => dbSet.AddAsync(newProduct, default))
                            .ReturnsAsync((EntityEntry<Product>)null);

            var result = await _productRepository.Create(newProduct);

            // Assertions
            Assert.NotNull(result);
            Assert.That(result, Is.EqualTo(newProduct));
        }
    }
}
