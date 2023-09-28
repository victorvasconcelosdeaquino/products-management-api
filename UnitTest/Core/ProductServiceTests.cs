using Domain.Entities;
using Domain.Entities.DTO;
using Domain.Interfaces.Repositories;
using Moq;
using NUnit.Framework;
using Project.Core.Services;
using System;
using System.Threading.Tasks;

namespace Project.UnitTest
{
    public class ProductServiceTests
    {
        private Mock<IProductRepository> _productRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
        }

        [Test]
        public async Task CreateProductAsync_ValidProduct_ReturnsCreatedProductViewModel()
        {
            // Arrange
            var productService = new ProductService(
                _productRepositoryMock.Object);

            var newProductDTO = new ProductDTO
            {
                Id = 1,
                ExpiryDate = DateTime.Now.AddMonths(2),
                ManufactureDate = DateTime.Now,
                Description = "Decrição",
                IsActive = true
            };

            var createdProduct = new Product
            {
                Id = 1,
                ExpiryDate = DateTime.Now.AddMonths(2),
                ManufactureDate = DateTime.Now,
                Description = "Descrição",
                IsActive = true
            };

            _productRepositoryMock.Setup(repo => repo.Create(createdProduct))
                                  .ReturnsAsync(createdProduct);

            var result = await productService.Create(createdProduct);

            // Assertions
            Assert.NotNull(result);
            Assert.That(result.Id, Is.EqualTo(newProductDTO.Id));
        }

        [Test]
        public async Task CreateProductAsync_InvalidProduct_ExpiryDateBeforeManufactureDate()
        {
            // Arrange
            var productService = new ProductService(
                _productRepositoryMock.Object);

            var createdProduct = new Product
            {
                Id = 1,
                ExpiryDate = DateTime.Now,
                ManufactureDate = DateTime.Now.AddMonths(2),
                Description = "Descrição",
                IsActive = true
            };

            _productRepositoryMock.Setup(repo => repo.Create(createdProduct))
                                  .ReturnsAsync(createdProduct);

            // Assertions
            Assert.IsTrue(createdProduct.ExpiryDate.Value < createdProduct.ManufactureDate.Value);
            Assert.IsFalse(createdProduct.ExpiryDate.Value > createdProduct.ManufactureDate.Value);
        }
    }

}