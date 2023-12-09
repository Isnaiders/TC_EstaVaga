using System.ComponentModel.DataAnnotations;
using TCC_API.Models.Entities.User;

namespace TCC_API.Interfaces.Repositories
{
    public interface IUserSessionRepository
    {
        UserSession GetById(Guid userSessionId);
        UserSession GetByUserId(Guid userId);
        string Add(UserSession session);
        void Update(UserSession session);
        ValidationResult Remove(Guid userSessionId);
        ValidationResult RemoveAllByUserId(Guid userId);
        bool SaveAll();
    }
}
