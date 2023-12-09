using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using TCC_API.Interfaces.Repositories;
using TCC_API.Models.Entities;
using TCC_API.Models.Entities.Parking;

namespace TCC_API.Repositories
{
	public class ParkingRepository : IParkingRepository
	{
		private readonly TC_EstaVagaContext _context;

		public ParkingRepository(TC_EstaVagaContext context)
		{
			_context = context;
		}

		public IEnumerable<Parking> GetAll()
		{
			return _context.Parking.ToList();
		}

		public Parking GetById(Guid parkingId)
		{
			return _context.Parking.Include(p => p.Vacancies).Where(e => e.Id == parkingId).FirstOrDefault();
		}

		public Vacancy GetVacancyById(Guid vacancyId)
		{
			return _context.Vacancy.Where(e => e.Id == vacancyId).FirstOrDefault();
		}

		public void Add(Parking parking)
		{
			_context.Parking.Add(parking);
		}

		public void Update(Parking parking)
		{
			_context.Parking.Update(parking);
		}

		public void VacancyUpdate(Vacancy vacancy)
		{
			_context.Vacancy.Update(vacancy);
		}

		public ValidationResult Remove(Guid parkingId)
		{
			var validationResult = new ValidationResult(string.Empty);
			var parkingDB = _context.Parking.Find(parkingId);

			if (parkingDB != null)
			{
				_context.Parking.Remove(parkingDB);
			}
			else
			{
				validationResult.ErrorMessage = "Estacionamento não existe";
			}

			return validationResult;
		}

		public bool SaveAll()
		{
			return _context.SaveChanges() > 0;
		}

		public bool Exists(Guid parkingId)
		{
			return _context.Parking.Find(parkingId) != null;
		}
	}
}
