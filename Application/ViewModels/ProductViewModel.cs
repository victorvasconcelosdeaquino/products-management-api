using System;

namespace Domain.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ManufactureDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string CorporateTaxId { get; set; }
    }
}
