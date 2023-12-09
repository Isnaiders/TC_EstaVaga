using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RazorClassLibrary.Models.Entities.Base;
using RazorClassLibrary.Models.Entities.Parking;

namespace RazorClassLibrary.Models.Entities.User
{
    public class Car : BaseEntity<Guid>
    {
        public Car()
        {
            ParkingReservation = new HashSet<ParkingReservation>();
            ParkingReservationHistory = new HashSet<ParkingReservationHistory>();
        }

        public Guid UserId { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        [StringLength(255)]
        public string Plate { get; set; }
        [Required]
        [StringLength(255)]
        public string Model { get; set; }

        [InverseProperty("Car")]
        public virtual ICollection<ParkingReservation> ParkingReservation { get; set; }
        [InverseProperty("Car")]
        public virtual ICollection<ParkingReservationHistory> ParkingReservationHistory { get; set; }
    }
}