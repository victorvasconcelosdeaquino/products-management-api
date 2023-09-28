using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.DTO
{
    public  class ProductDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "A descrição do produto é obrigatória", AllowEmptyStrings = false), StringLength(maximumLength: 80, MinimumLength = 4) ]
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ManufactureDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int SupplierId { get; set; }
    }
}
