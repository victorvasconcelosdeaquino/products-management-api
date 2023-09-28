using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _repository;

        public SupplierService(ISupplierRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Supplier>> GetSuppliers()
        {
            return await _repository.GetAll();
        }

        public async Task<Supplier> GetSupplier(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Supplier> Create(Supplier model)
        {
            return await _repository.Create(model);
        }

        public async Task Update(Supplier model)
        {
            var supplier = await _repository.GetById(model.Id);

            //Manual mapping            
            supplier.Name = model.Name;
            supplier.CorporateTaxId = model.CorporateTaxId;

            await _repository.Update(supplier);
        }

        public async Task Delete(int id)
        {
            var entity = await _repository.GetById(id);
            await _repository.Delete(entity);
        }
    }
}
