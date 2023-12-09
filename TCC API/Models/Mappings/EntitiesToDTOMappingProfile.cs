using AutoMapper;
using TCC_API.Models.DTOs.Parking;
using TCC_API.Models.DTOs.User;
using TCC_API.Models.Entities.Parking;
using TCC_API.Models.Entities.User;

namespace TCC_API.Models.Mappings
{
	public class EntitiesToDTOMappingProfile : Profile
	{
		public EntitiesToDTOMappingProfile()
		{
			CreateMap<User, UserDetailedDTO>().ReverseMap();
			CreateMap<Parking, ParkingDetailedDTO>().ReverseMap();
			CreateMap<Parking, ParkingBasicDTO>().ReverseMap();
			CreateMap<Vacancy, VacancyBasicDTO>().ReverseMap();
		}
	}
}
