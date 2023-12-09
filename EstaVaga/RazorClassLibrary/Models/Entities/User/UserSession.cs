using System.ComponentModel.DataAnnotations.Schema;
using RazorClassLibrary.Models.Entities.Base;

namespace RazorClassLibrary.Models.Entities.User
{
    public class UserSession : BaseEntity<Guid>
    {
        [ForeignKey("UserId")]
        [InverseProperty("UserSession")]
        public Guid UserId { get; set; }

        public User User { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}
