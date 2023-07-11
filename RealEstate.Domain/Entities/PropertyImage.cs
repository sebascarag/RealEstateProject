using System.ComponentModel.DataAnnotations;

namespace RealEstate.Domain.Entities
{
    public class PropertyImage : BaseEntity
    {
        public Property Property { get; set; }
        [Required]
        [StringLength(255)]
        public string File { get; set; }
        public bool Enable { get; set; }
    }
}
