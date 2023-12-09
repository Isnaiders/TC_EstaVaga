using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TCC_API.Models.Entities.Base;
using TCC_API.Models.Enums.User;

namespace TCC_API.Models.Entities.User
{
    [Serializable]
    public partial class User : BaseEntity<Guid>
    {
        [StringLength(255)]
        public string Name { get; set; }
        public UserType Type { get; set; }
        [Required]
        [StringLength(255)]
        public string Email { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime BirthDate { get; set; }

        [StringLength(255)]
        public string Password { get; set; }
    }
}