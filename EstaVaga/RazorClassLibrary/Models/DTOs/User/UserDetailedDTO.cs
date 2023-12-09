using System;
using System.ComponentModel.DataAnnotations;
using RazorClassLibrary.Models.Base;
using RazorClassLibrary.Models.DTOs.Base;
using RazorClassLibrary.Models.Enums.User;

namespace RazorClassLibrary.Models.DTOs.User
{
    public class UserDetailedDTO : BaseDTO
    {
        public UserDetailedDTO() : base()
        {
            BirthDate = DateTime.Now.AddYears(-18);
        }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(255)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        [StringLength(255)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,24}$", ErrorMessage = "A senha deve ter entre 8 e 24 caracteres, incluindo pelo menos uma letra minúscula, uma letra maiúscula, um número e um caractere especial.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [StringLength(255)]
        public string Email { get; set; }

        [MinimumAge(18)]
        public DateTime BirthDate { get; set; }

        public UserType Type { get; set; }
    }
}
