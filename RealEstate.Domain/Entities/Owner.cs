using System.ComponentModel.DataAnnotations;

namespace RealEstate.Domain.Entities
{
    public class Owner : BaseEntity
    {
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [Required]
        [StringLength(255)]
        public string Address { get; set; }
        [StringLength(255)]
        public string Photo { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        public List<Property> Properties { get; set; }
    }
}
