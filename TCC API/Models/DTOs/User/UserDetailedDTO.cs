using System.ComponentModel.DataAnnotations;
using TCC_API.Models.DTOs.Base;
using TCC_API.Models.Enums.User;

namespace TCC_API.Models.DTOs.User
{
    public class UserDetailedDTO : BaseDTO
    {
        public string Name { get; set; }

        public UserType Type { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }
        
        public string Password { get; set; }
        
    }
}
