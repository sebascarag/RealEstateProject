
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Domain.Entities
{
    public class Property : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(255)]
        public string Address { get; set; }
        public decimal Price { get; set; }
        [Required]
        [StringLength(100)]
        public string CodeInternal { get; set; }
        public int Year { get; set; }
        public int OwnerId { get; set; }
        public Owner Owner { get; set; }
        public List<PropertyImage> PropertyImages { get; set; }
        public List<PropertyTrace> PropertyTraces { get; set; }
    }
}
