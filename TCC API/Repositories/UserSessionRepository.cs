using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TCC_API.Interfaces.Repositories;
using TCC_API.Models.Entities;
using TCC_API.Models.Entities.User;

namespace TCC_API.Repositories
{
    public class UserSessionRepository : IUserSessionRepository
    {
        private readonly TC_EstaVagaContext _context;

        public UserSessionRepository(TC_EstaVagaContext context)
        {
            _context = context;
        }

		public UserSession GetById(Guid userSessionId)
		{
			return _context.UserSession.Include(e => e.User).Where(e => e.Id == userSessionId).FirstOrDefault();
		}

		public UserSession GetByUserId(Guid userId)
		{
			return _context.UserSession.Where(s => s.UserId == userId).FirstOrDefault();
		}

		public string Add(UserSession session)
        {
            if (_context.UserSession.Any(e => e.UserId == session.UserId))
                return "Sessão já cadastrada";
            _context.UserSession.Add(session);
            return "Cadastro concluído com sucesso";
        }

        public void Update(UserSession session)
        {
            _context.UserSession.Update(session);
        }

        public ValidationResult Remove(Guid userSessionId)
        {
            var validationResult = new ValidationResult(string.Empty);
            var sessionDB = _context.UserSession.Include(e => e.User).Where(e => e.Id == userSessionId).FirstOrDefault();

            if (sessionDB != null)
            {
                _context.UserSession.Remove(sessionDB);
            }
            else
            {
                validationResult.ErrorMessage = "Sessão não existe";
            }

            return validationResult;
        }

        public ValidationResult RemoveAllByUserId(Guid userId)
        {
            var validationResult = new ValidationResult(string.Empty);
            var sessionsDB = _context.UserSession.Where(s => s.UserId == userId).ToList();

            if (sessionsDB.Any())
            {
                foreach (var sessionDB in sessionsDB)
                {
                    _context.UserSession.Remove(sessionDB);
                }
            }
            else
            {
                validationResult.ErrorMessage = "Sessão não existe";
            }

            return validationResult;
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
