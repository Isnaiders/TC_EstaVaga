using RazorClassLibrary.Models.DTOs.Base;
using RazorClassLibrary.Models.Enums.Parking;

namespace RazorClassLibrary.Models.DTOs.Parking
{
	public class VacancyBasicDTO : BaseDTO
	{
		public Guid ParkingId { get; set; }
		public string Name { get; set; }
		public bool Busy { get; set; }
		public VacancyType Type { get; set; }
	}
}
