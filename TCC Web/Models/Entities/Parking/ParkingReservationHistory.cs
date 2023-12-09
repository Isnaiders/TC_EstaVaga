using System.ComponentModel.DataAnnotations.Schema;
using TCC_Web.Models.Entities.Base;
using TCC_Web.Models.Entities.User;

namespace TCC_Web.Models.Entities.Parking
{
    public class ParkingReservationHistory : BaseEntity<Guid>
    {
        public Guid ParkingId { get; set; }
        public Guid VacancyId { get; set; }
        public Guid CarId { get; set; }
        public int VacancyType { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CommittedStartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CommittedEndDate { get; set; }

        [ForeignKey("CarId")]
        [InverseProperty("ParkingReservationHistory")]
        public virtual Car Car { get; set; }
        [ForeignKey("ParkingId")]
        [InverseProperty("ParkingReservationHistory")]
        public virtual Parking Parking { get; set; }
        [ForeignKey("VacancyId")]
        [InverseProperty("ParkingReservationHistory")]
        public virtual Vacancy Vacancy { get; set; }
    }
}