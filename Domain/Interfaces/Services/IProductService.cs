using Domain.Entities;
using Domain.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Core.Interfaces.IServices
{
    public interface IProductService
    {
        Task<PaginationList<Product>> GetProducts(Parameters paremeters);
        Task<Product> GetProduct(int id);
        Task<Product> Create(Product model);
        Task Update(Product model);
        Task Delete(int id);
    }
}
