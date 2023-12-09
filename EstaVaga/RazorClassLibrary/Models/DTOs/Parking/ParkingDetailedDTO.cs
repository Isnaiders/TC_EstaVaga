using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RazorClassLibrary.Models.DTOs.Base;
using RazorClassLibrary.Models.Enums.Base;
using RazorClassLibrary.Models.Enums.Parking;

namespace RazorClassLibrary.Models.DTOs.Parking
{
	public class ParkingDetailedDTO : BaseDTO
	{
		public ParkingDetailedDTO()
		{
			Id = Guid.NewGuid();
			CountryId = Guid.NewGuid();
			StateId = Guid.NewGuid();
			CityId = Guid.NewGuid();
			NeighborhoodId = Guid.NewGuid();
			Latitude = 10;
			Longitude = 10;
			SystemStatus = SystemStatusType.PendingApproval;
			Vacancies = new List<VacancyBasicDTO>();
		}

		[Required]
		[StringLength(255)]
		public string Name { get; set; }

		[Required]
		[StringLength(255)]
		public string PostalCode { get; set; }

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

		public ICollection<VacancyBasicDTO> Vacancies { get; set; }
	}
}
