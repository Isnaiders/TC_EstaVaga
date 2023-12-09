using System.ComponentModel.DataAnnotations;
using TCC_API.Models.Entities.Parking;

namespace TCC_API.Interfaces.Repositories
{
    public interface IParkingRepository
    {
        IEnumerable<Parking> GetAll();
        Parking GetById(Guid parkingId);
        Vacancy GetVacancyById(Guid vacancyId);
        void Add(Parking parking);
        void Update(Parking parking);
        void VacancyUpdate(Vacancy vacancy);
        ValidationResult Remove(Guid parkingId);

		bool SaveAll();
        bool Exists(Guid parkingId);
    }
}
