using System.ComponentModel.DataAnnotations;

namespace RazorClassLibrary.Models.DTOs.Login
{
    public class LoginDTO
    {
		[Required(ErrorMessage = "Nome é obrigatório")]
        public string UserIdentifier { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        public string Password { get; set; }

    }
}
