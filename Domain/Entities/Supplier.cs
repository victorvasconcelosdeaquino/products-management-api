using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Supplier
    {
        public Supplier()
        {
            Products = new Collection<Product>();
        }

        [Key]
        public int Id { get; set; }
        [Required, StringLength(maximumLength: 100, MinimumLength = 2)]
        public string Name { get; set; }
        [Required, StringLength(maximumLength: 14)]
        public string CorporateTaxId { get; set; }

        [JsonIgnore]
        public ICollection<Product>? Products { get; set; }
    }
}
