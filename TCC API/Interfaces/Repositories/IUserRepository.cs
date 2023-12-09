using System.ComponentModel.DataAnnotations;
using TCC_API.Models.Entities.User;

namespace TCC_API.Interfaces.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetById(Guid userId);
        Guid? GetIdByUserIdentifier(string userIdentifier);
        Guid? GetUserIdByLogin(string userIdentifier, string password);
		void Add(User user);
        void Update(User user);
        ValidationResult Remove(Guid userId);
        bool SaveAll();
        Task<bool> SaveAllAsync();
		bool Exists(Guid Id);
        bool EmailExists(string email);
    }
}
