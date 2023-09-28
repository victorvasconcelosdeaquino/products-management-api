using Domain.Entities;
using Domain.Pagination;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<PaginationList<Product>> GetAllComplete(Parameters parameters);
        Task<Product> GetByIdComplete(int id);
    }
}
