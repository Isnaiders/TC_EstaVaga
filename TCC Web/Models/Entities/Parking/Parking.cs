using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TCC_Web.Models.Entities.Base;
using TCC_Web.Models.Enums.Parking;

namespace TCC_Web.Models.Entities.Parking
{
    public class Parking : BaseEntity<Guid>
    {
        public Parking() : base()
        {
            ParkingOpeningHour = new HashSet<ParkingOpeningHour>();
            ParkingReservation = new HashSet<ParkingReservation>();
            ParkingReservationHistory = new HashSet<ParkingReservationHistory>();
            Vacancy = new HashSet<Vacancy>();
            Id = Guid.NewGuid();
            CountryId = Guid.NewGuid();
            StateId = Guid.NewGuid();
            CityId = Guid.NewGuid();
            NeighborhoodId = Guid.NewGuid();
            Latitude = 10;
            Longitude = 10;
        }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        [StringLength(255)]
        public string PostalCode { get; set; }
        public Guid CountryId { get; set; }
        [Required]
        [StringLength(255)]
        public string CountryName { get; set; }
        public Guid StateId { get; set; }
        [Required]
        [StringLength(255)]
        public string StateName { get; set; }
        public Guid CityId { get; set; }
        [Required]
        [StringLength(255)]
        public string CityName { get; set; }
        public Guid NeighborhoodId { get; set; }
        [Required]
        [StringLength(255)]
        public string NeighborhoodName { get; set; }
        [Required]
        [StringLength(255)]
        public string Address { get; set; }
        public int AddressNumber { get; set; }
        [Required]
        [StringLength(255)]
        public string AddressComplement { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal Latitude { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal Longitude { get; set; }
        public ParkingLocationType LocationType { get; set; }


        [InverseProperty("Parking")]
        public virtual ICollection<ParkingOpeningHour> ParkingOpeningHour { get; set; }
        [InverseProperty("Parking")]
        public virtual ICollection<ParkingReservation> ParkingReservation { get; set; }
        [InverseProperty("Parking")]
        public virtual ICollection<ParkingReservationHistory> ParkingReservationHistory { get; set; }
        [InverseProperty("Parking")]
        public virtual ICollection<Vacancy> Vacancy { get; set; }
    }
}