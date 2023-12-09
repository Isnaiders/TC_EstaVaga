using TCC_API.Models.DTOs.Base;
using TCC_API.Models.Enums.Parking;

namespace TCC_API.Models.DTOs.Parking
{
	public class VacancyBasicDTO : BaseDTO
	{
        public VacancyBasicDTO()
        {
            Id = Guid.NewGuid();
        }

        public Guid ParkingId { get; set; }
		public string Name { get; set; }
		public bool Busy { get; set; }
		public VacancyType Type { get; set; }
	}
}
