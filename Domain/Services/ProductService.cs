using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Pagination;
using Project.Core.Interfaces.IServices;
using System.Threading.Tasks;

namespace Project.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(
            IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginationList<Product>> GetProducts(Parameters paremeters)
        {
            return await _repository.GetAllComplete(paremeters);
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _repository.GetByIdComplete(id);
        }

        public async Task<Product> Create(Product model)
        {
            try
            {
                ValidateDates(model);

                return await _repository.Create(model);
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        public async Task Update(Product model)
        {
            try
            {
                ValidateDates(model);

                var product = await _repository.GetById(model.Id);

                //Manual mapping            
                product.Description = model.Description;
                product.IsActive = model.IsActive;
                product.ManufactureDate = model.ManufactureDate.Value;
                product.ExpiryDate = model.ExpiryDate.Value;

                await _repository.Update(product);
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        public async Task Delete(int id)
        {
            var entity = await _repository.GetById(id);
            entity.IsActive = false;
            await _repository.Update(entity);
        }

        private static void ValidateDates(Product model)
        {
            if (model.ManufactureDate > model.ExpiryDate)
                throw new System.Exception("A data de fabricação não pode ser maior que a data de expiração");
        }
    }
}
