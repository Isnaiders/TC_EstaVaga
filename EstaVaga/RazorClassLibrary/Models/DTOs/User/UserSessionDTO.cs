using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RazorClassLibrary.Models.DTOs.Base;
using RazorClassLibrary.Models.DTOs.User;

namespace RazorClassLibrary.Models.DTOs.User
{
	public class UserSessionDTO : BaseDTO
	{
        public UserSessionDTO() : base()
        {
			User = new UserDetailedDTO();
			UserId = User.Id;
        }

		[ForeignKey("UserId")]
		[InverseProperty("UserSession")]
		public Guid UserId { get; set; }

		public UserDetailedDTO User { get; set; }
		
		public DateTime Start { get; set; }

		public DateTime End { get; set; }
	}
}
