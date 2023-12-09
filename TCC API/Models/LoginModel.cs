using System.ComponentModel.DataAnnotations;

namespace TCC_API.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Digite o email")]
        public string UserIdentifier { get; set; }

        [Required(ErrorMessage = "Digite a senha")]
        public string Password { get; set; }
    }
}
