using System.ComponentModel.DataAnnotations;

namespace TCC_Web.Models.DTOs.Login
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Digite o Usuario")]
        public string UserIdentifier { get; set; }

        [Required(ErrorMessage = "Digite a Senha")]
        public string Password { get; set; }
    }
}
