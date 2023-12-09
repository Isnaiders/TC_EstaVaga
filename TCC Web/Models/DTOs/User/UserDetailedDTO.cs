using System.ComponentModel.DataAnnotations;
using TCC_Web.Models.DTOs.Base;
using TCC_Web.Models.Enums.User;

namespace TCC_Web.Models.DTOs.User
{
	public class UserDetailedDTO : BaseDTO
	{
		public UserDetailedDTO() : base()
		{

		}
		[Required(ErrorMessage = "Digite o Mome")]
		[StringLength(255)]
		public string Name { get; set; }

		public UserType Type { get; set; }

		[Required(ErrorMessage = "Digite o Email")]
		[StringLength(255)]
		public string Email { get; set; }

        [Required(ErrorMessage = "Digite a Data de Nascimento")]
        public DateTime BirthDate { get; set; }

		[Required(ErrorMessage = "Digite o Nº de Registro")]
		[StringLength(255)]
		public string Password { get; set; }
	}
}
