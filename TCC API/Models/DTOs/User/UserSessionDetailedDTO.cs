using TCC_API.Models.DTOs.Base;

namespace TCC_API.Models.DTOs.User
{
    public class UserSessionDetailedDTO : BaseDTO
    {
        public Guid UserId { get; set; }

        public UserDetailedDTO User { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}
