using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Pagination;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PaginationList<Product>> GetAllComplete(Parameters parameters)
        {
            var products = await _context.Set<Product>()
                .Include(c => c.Supplier)
                .AsNoTracking().ToListAsync();

            return PaginationList<Product>.ToPaginationList(products.AsQueryable(), parameters.PageNumber, parameters.PageSize);
        }

        public async Task<Product> GetByIdComplete(int id)
        {
            return await _context.Set<Product>()
                .Include(c => c.Supplier)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
