using System.ComponentModel.DataAnnotations;
using TCC_API.Interfaces.Repositories;
using TCC_API.Models.Entities;
using TCC_API.Models.Entities.User;

namespace TCC_API.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly TC_EstaVagaContext _context;

		public UserRepository(TC_EstaVagaContext context)
		{
			_context = context;
		}

		public IEnumerable<User> GetAll()
		{
			return _context.User.ToList();
		}

		public User GetById(Guid userId)
		{
			return _context.User.Where(e => e.Id == userId).FirstOrDefault();
		}

		public Guid? GetIdByUserIdentifier(string userIdentifier)
		{
			var result = _context.User.Where(e => e.Email == userIdentifier)?.FirstOrDefault();
            return result != null ? result.Id : null;
        }

		public Guid? GetUserIdByLogin(string userIdentifier, string password)
		{
			var result = _context.User.Where(e => e.Email == userIdentifier && e.Password == password)?.FirstOrDefault();
			return result != null ? result.Id : null;
		}

		public UserSession GetSessionByUserId(Guid userId)
		{
			return _context.UserSession.Where(s => s.UserId == userId).FirstOrDefault();
		}

		public void Add(User user)
		{
			_context.User.Add(user);
		}

		public void Update(User user)
		{
			_context.User.Update(user);
		}

		public ValidationResult Remove(Guid userId)
		{
			var validationResult = new ValidationResult(string.Empty);
			var userDB = _context.User.Find(userId);

			if (userDB != null)
			{
				_context.User.Remove(userDB);
			}
			else
			{
				validationResult.ErrorMessage = "Usuário não existe";
			}

			return validationResult;
		}

		public bool SaveAll()
		{
			return _context.SaveChanges() > 0;
		}

		public async Task<bool> SaveAllAsync()
		{
			return await _context.SaveChangesAsync() > 0;
		}

		public bool Exists(Guid userId)
		{
			return _context.User.Find(userId) != null;
		}

		public bool EmailExists(string email)
		{
			return _context.User.Any(u => u.Email == email);
		}
	}
}
