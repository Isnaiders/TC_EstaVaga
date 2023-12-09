using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TCC_Web.Models.Entities.Base;

namespace TCC_Web.Models.Entities.Parking
{
    public class Vacancy : BaseEntity<Guid>
    {
        public Guid ParkingId { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public bool Busy { get; set; }
        public int Type { get; set; }

        [ForeignKey("ParkingId")]
        [InverseProperty("Vacancy")]
        public virtual Parking Parking { get; set; }
    }
}