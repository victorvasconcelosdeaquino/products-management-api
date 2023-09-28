using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface ISupplierService
    {
        Task<IEnumerable<Supplier>> GetSuppliers();
        Task<Supplier> GetSupplier(int id);
        Task<Supplier> Create(Supplier model);
        Task Update(Supplier model);
        Task Delete(int id);
    }
}
