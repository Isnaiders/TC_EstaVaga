using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TCC_API.Interfaces.Repositories;
using TCC_API.Models.DTOs.Parking;
using TCC_API.Models.Entities.Parking;

namespace TCC_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParkingController : Controller
    {
        public readonly IMapper _mapper;
        public readonly IParkingRepository _parkingRepository;

        public ParkingController(IMapper mapper, IParkingRepository parkingRepository)
        {
            _mapper = mapper;
            _parkingRepository = parkingRepository;
        }

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<ParkingBasicDTO>> GetAllParking()
        {
            var parkingDBs = _parkingRepository.GetAll();
            var parkingDTOs = _mapper.Map<IEnumerable<ParkingBasicDTO>>(parkingDBs);
            return Ok(parkingDTOs);
        }

        [HttpGet("GetById/{parkingId}")]
        public ActionResult GetParkingById(Guid parkingId)
        {
            var parkingDB = _parkingRepository.GetById(parkingId);

            if (parkingDB == null)
            {
                return NotFound("Estacionamento não encontrado.");
            }

            var parkingDTO = _mapper.Map<ParkingDetailedDTO>(parkingDB);
            return Ok(parkingDTO);
        }

        [HttpPost("Add")]
        public ActionResult AddParking(ParkingBasicDTO parkingDTO)
            {
            var parkingDB = _mapper.Map<Parking>(parkingDTO);
            _parkingRepository.Add(parkingDB);

            if (_parkingRepository.SaveAll())
            {
                return Ok("Estacionamento cadastrado com sucesso!");
            }

            return BadRequest("Ocorreu um erro ao salvar o estacionamento.");
        }

        [HttpPut("Update")]
        public ActionResult UpdateParking(ParkingBasicDTO parkingDTO)
        {
            var parkingDB = _parkingRepository.GetById(parkingDTO.Id);

            if (parkingDB == null)
            {
                return BadRequest("Estacionamento não encontrado.");
            }

            parkingDB.Name = parkingDTO.Name;
            parkingDB.PostalCode = parkingDTO.PostalCode;
            //parkingDB.CountryId = parkingDTO.CountryId;
            parkingDB.CountryName = parkingDTO.CountryName;
            //parkingDB.StateId = parkingDTO.StateId;
            parkingDB.StateName = parkingDTO.StateName;
            //parkingDB.CityId = parkingDTO.CityId;
            parkingDB.CityName = parkingDTO.CityName;
            //parkingDB.NeighborhoodId = parkingDTO.NeighborhoodId;
            parkingDB.NeighborhoodName = parkingDTO.NeighborhoodName;
            parkingDB.Address = parkingDTO.Address;
            parkingDB.AddressNumber = parkingDTO.AddressNumber;
            parkingDB.AddressComplement = parkingDTO.AddressComplement;
            parkingDB.Latitude = parkingDTO.Latitude;
            parkingDB.Longitude = parkingDTO.Longitude;
            parkingDB.LocationType = parkingDTO.LocationType;
            parkingDB.WhenUpdated = DateTime.UtcNow;
            parkingDB.SystemStatus = parkingDTO.SystemStatus;

            _parkingRepository.Update(parkingDB);

            if (_parkingRepository.SaveAll())
            {
                return Ok("Estacionamento alterado com sucesso!");
            }

            return BadRequest("Ocorreu um erro ao alterar o estacionamento.");
        }

        [HttpDelete("Remove/{parkingId}")]
        public ActionResult RemoveParking(Guid parkingId)
        {
            var validationResult = _parkingRepository.Remove(parkingId);

            if (!string.IsNullOrEmpty(validationResult.ErrorMessage))
            {
                return NotFound(validationResult.ErrorMessage);
            }

            if (_parkingRepository.SaveAll())
            {
                return Ok("Estacionamento foi removido com sucesso!");
            }

            return BadRequest("Ocorreu um erro ao remover o estacionamento.");
        }

		[HttpPost("SetVacancyStatus/{vacancyIdAsString}/{isBusy}")]
		public ActionResult SetVacancyStatus(string? vacancyIdAsString = null, bool isBusy = false)
		{
            if (!Guid.TryParse(vacancyIdAsString, out Guid vacancyId))
                return BadRequest("Vaga não existe.");

			var vacancyDB = _parkingRepository.GetVacancyById(vacancyId);

			if (vacancyDB == null)
			{
				return BadRequest("Vaga não encontrada.");
			}

			vacancyDB.Busy = isBusy;

			_parkingRepository.VacancyUpdate(vacancyDB);

			if (_parkingRepository.SaveAll())
			{
				return Ok("Vaga alterada com sucesso!");
			}

			return BadRequest("Ocorreu um erro ao alterar a vaga.");
		}
	}
}
