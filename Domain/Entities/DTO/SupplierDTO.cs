using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.DTO
{
    public class SupplierDTO
    {
        public int Id { get; set; }
        [Required, StringLength(maximumLength: 100, MinimumLength = 2)]
        public string Name { get; set; }
        [Required, StringLength(maximumLength: 14)]
        public string CorporateTaxId { get; set; }
    }
}
