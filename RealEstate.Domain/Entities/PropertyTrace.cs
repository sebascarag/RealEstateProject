using System.ComponentModel.DataAnnotations;

namespace RealEstate.Domain.Entities
{
    public class PropertyTrace : BaseEntity
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateSale { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public decimal Value { get; set; } // pend precision
        public decimal Tax { get; set; } // pend precision
        public Property Property { get; set; }
    }
}
