using Domain.Entities;
using Domain.Pagination;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<PaginationList<Product>> GetAllProducts(Parameters parameters);
        Task<Product> GetProductById(int id);
    }
}
