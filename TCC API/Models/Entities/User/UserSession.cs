using System.ComponentModel.DataAnnotations.Schema;
using TCC_API.Models.Entities.Base;

namespace TCC_API.Models.Entities.User
{
    public class UserSession : BaseEntity<Guid>
    {
        public UserSession(Guid userId, DateTime start, DateTime end) : base()
        {        
            UserId = userId;
            Start = start;
            End = end;
        }

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}
