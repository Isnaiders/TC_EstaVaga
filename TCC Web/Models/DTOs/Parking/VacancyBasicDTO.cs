using TCC_Web.Models.DTOs.Base;
using TCC_Web.Models.Enums.Parking;

namespace TCC_Web.Models.DTOs.Parking
{
    public class VacancyBasicDTO : BaseDTO
    {
        public Guid ParkingId { get; set; }
        public string Name { get; set; }
        public bool Busy { get; set; }
        public VacancyType Type { get; set; }
    }
}
